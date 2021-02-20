using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace Aiirh.Basic.Services.Cache
{
    public static class CacheSettings
    {
        public static TimeSpan DefaultExpiration => new TimeSpan(0, 0, 30);
    }

    public interface IMemoryCacheManager
    {
        /// <summary>
        /// Relative cache expiration - it will expire only in a case when no calls towards cache with the same key will be made
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheGroup"></param>
        /// <param name="cacheKey"></param>
        /// <param name="factory"></param>
        /// <param name="absoluteExpiration"></param>
        /// <returns></returns>
        T GetOrAddRelative<T>(string cacheGroup, string cacheKey, Func<T> factory, TimeSpan absoluteExpiration);

        /// <summary>
        /// Absolute expiration - it will definitely expire after expiration period
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheGroup"></param>
        /// <param name="cacheKey"></param>
        /// <param name="factory"></param>
        /// <param name="absoluteExpiration"></param>
        /// <returns></returns>
        T GetOrAddAbsolute<T>(string cacheGroup, string cacheKey, Func<T> factory, TimeSpan absoluteExpiration);

        /// <summary>
        /// Absolute expiration - it will definitely expire after expiration period. Default expiration period (30 sec) will be applied
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheGroup"></param>
        /// <param name="cacheKey"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        T GetOrAddAbsolute<T>(string cacheGroup, string cacheKey, Func<T> factory);

        /// <summary>
        /// Static dictionary which can be refreshed only with APP restart
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheGroup"></param>
        /// <param name="cacheKey"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        T GetOrAddImmortal<T>(string cacheGroup, string cacheKey, Func<T> factory);
        void Invalidate(string cacheGroup, string cacheKey);
        void InvalidateAll();
        List<string> CacheKeys();
    }

    public class MemoryCacheManager : IMemoryCacheManager
    {

        private static CancellationTokenSource _resetCacheToken = new CancellationTokenSource();
        private readonly IMemoryCache _memoryCache;
        private readonly bool _disableCache;
        private static readonly IDictionary<string, object> Lock = new ConcurrentDictionary<string, object>();
        private static volatile object globalLockObject = new object();

        public MemoryCacheManager(IMemoryCache memoryCache, IConfiguration configuration)
        {
            _disableCache = bool.Parse(configuration["Application:DisableCache"] ?? "false");
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public T GetOrAddRelative<T>(string cacheGroup, string cacheKey, Func<T> factory, TimeSpan relativeExpiration)
        {
            return GetOrAdd(CacheExpirationType.Relative, cacheGroup, cacheKey, factory, relativeExpiration);
        }

        public T GetOrAddAbsolute<T>(string cacheGroup, string cacheKey, Func<T> factory, TimeSpan absoluteExpiration)
        {
            return GetOrAdd(CacheExpirationType.Absolute, cacheGroup, cacheKey, factory, absoluteExpiration);
        }

        public T GetOrAddAbsolute<T>(string cacheGroup, string cacheKey, Func<T> factory)
        {
            return GetOrAddAbsolute(cacheGroup, cacheKey, factory, CacheSettings.DefaultExpiration);
        }

        public T GetOrAddImmortal<T>(string cacheGroup, string cacheKey, Func<T> factory)
        {
            return GetOrAdd(CacheExpirationType.Immortal, cacheGroup, cacheKey, factory, null);
        }

        public void Invalidate(string cacheGroup, string cacheKey)
        {
            var fullCacheKey = GetFullCacheKey(cacheGroup, cacheKey);
            lock (GetLockObject(cacheGroup))
            {
                _memoryCache.Remove(fullCacheKey);
            }
        }

        public void InvalidateAll()
        {
            if (_resetCacheToken != null && !_resetCacheToken.IsCancellationRequested && _resetCacheToken.Token.CanBeCanceled)
            {
                _resetCacheToken.Cancel();
                _resetCacheToken.Dispose();
            }

            _resetCacheToken = new CancellationTokenSource();
        }

        public List<string> CacheKeys()
        {
            var field = typeof(MemoryCache).GetProperty("EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance);
            var items = new List<string>();
            if (field.GetValue(_memoryCache) is ICollection collection)
            {
                foreach (var item in collection)
                {
                    var methodInfo = item.GetType().GetProperty("Key");
                    var val = methodInfo.GetValue(item);
                    items.Add(val.ToString());
                }
            }
            return items;
        }

        private T GetOrAdd<T>(CacheExpirationType cacheType, string cacheGroup, string cacheKey, Func<T> factory, TimeSpan? expiration)
        {
            if (_disableCache)
            {
                return factory();
            }

            var fullCacheKey = GetFullCacheKey(cacheGroup, cacheKey);
            lock (GetLockObject(cacheGroup))
            {
                if (_memoryCache.TryGetValue<T>(fullCacheKey, out var result))
                {
                    return result;
                }

                result = factory();
                AddCacheItem(cacheType, fullCacheKey, result, expiration);
                return result;
            }
        }

        private static string GetFullCacheKey(string cacheGroup, string cacheKey)
        {
            return $"{cacheGroup}|{cacheKey}";
        }

        private static object GetLockObject(string cacheKey)
        {
            if (Lock.TryGetValue(cacheKey, out var lockObject))
            {
                return lockObject;
            }
            lockObject = new object();

            lock (globalLockObject)
            {
                if (!Lock.ContainsKey(cacheKey))
                {
                    Lock.Add(cacheKey, lockObject);
                }
            }
            return lockObject;
        }

        private void AddCacheItem<T>(CacheExpirationType cacheType, string cacheKey, T obj, TimeSpan? expiration)
        {
            var options = new MemoryCacheEntryOptions()
                    .SetPriority(CacheItemPriority.Normal)
                    .AddExpirationToken(new CancellationChangeToken(_resetCacheToken.Token));

            if (cacheType == CacheExpirationType.Immortal || expiration == null)
            {
                SetImmortal(cacheKey, obj, options);
            }
            else if (cacheType == CacheExpirationType.Absolute)
            {
                SetAbsolute(cacheKey, obj, expiration.Value, options);
            }
            else if (cacheType == CacheExpirationType.Relative)
            {
                SetRelative(cacheKey, obj, expiration.Value, options);
            }
        }

        private void SetImmortal<T>(string key, T obj, MemoryCacheEntryOptions options)
        {
            _memoryCache.Set(key, obj, options);
        }

        private void SetRelative<T>(string key, T obj, TimeSpan expiration, MemoryCacheEntryOptions options)
        {
            ValidateExpirationTime(expiration, key);
            options.SetSlidingExpiration(expiration);
            _memoryCache.Set(key, obj, options);
        }

        private void SetAbsolute<T>(string key, T obj, TimeSpan expiration, MemoryCacheEntryOptions options)
        {
            var absoluteExpiration = DateTimeOffset.Now.Add(expiration);
            options.SetAbsoluteExpiration(absoluteExpiration);
            _memoryCache.Set(key, obj, options);
        }

        private void Set<T>(string key, T obj, MemoryCacheEntryOptions options)
        {
            _memoryCache.Set(key, obj, options);
        }

        private static void ValidateExpirationTime(TimeSpan expiration, string key)
        {
            if (expiration < TimeSpan.FromSeconds(2))
            {
                throw new ExpirationToSmallException($"Trying to set relative expiration that are less than 2 seconds. Key->{key}. Expiration->{expiration}. You will not benefit from it since mem-cache have a limitiation");
            }
        }
    }
}
