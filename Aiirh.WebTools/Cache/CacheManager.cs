using Aiirh.Basic.Utilities;
using Aiirh.DateAndTime;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aiirh.WebTools.Cache
{
    public class CacheManagerSimple<TResult> : CacheManager<byte, TResult>
    {
        public CacheManagerSimple(string name) : base(name)
        {
        }

        public TResult ExecuteCached(Func<TResult> callToBeCached)
        {
            byte key = 0;
            TResult result;
            lock (this)
            {
                if (!TryGetValue(key, out result))
                {
                    result = callToBeCached();
                    Add(key, result);
                }
            }

            return result;
        }
    }

    /// <summary>
    ///   For holding cache with last write time
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public class CacheManager<TKey, TResult> : ICacheInfo
    {
        private static int createCounter;
        private readonly Dictionary<TKey, CacheValueEntry> _internalCache = new Dictionary<TKey, CacheValueEntry>();

        public TimeSpan CacheDuration = new TimeSpan(1, 0, 0);
        public int MaxKeysInCache = 600;
        private static readonly object CacheCreateLock = new object();

        public CacheManager(string name)
        {
            Name = name;
            lock (CacheCreateLock)
            {
                CreateNr = ++createCounter;
                var key = name + CreateNr;
                CacheRegistry.Registry.Add(key ?? Guid.NewGuid().ToString(), this);
            }
        }

        public string Name { get; }

        public int CreateNr { get; }

        public int TotalCallCount => CacheMissCallCount + CacheHitCallCount;

        public int CacheMissCallCount { get; private set; }

        public int CacheHitCallCount { get; private set; }

        public int CacheHitRatio => TotalCallCount > 0 ? CacheHitCallCount * 100 / TotalCallCount : 0;

        public int CacheRemovalExecutions { get; private set; }

        public IEnumerable<TKey> Keys => _internalCache.Keys;

        public int Count => _internalCache.Count;

        public TimeSpan CacheTime => CacheDuration;

        public bool TryGetValue(TKey key, out TResult value)
        {
            if (_internalCache.TryGetValue(key, out var ret))
            {
                value = ret.Value;
                if (IsEntryValid(ret))
                {
                    CacheHitCallCount++;
                    return true;
                }
            }
            else
            {
                value = default(TResult);
            }

            CacheMissCallCount++;
            return false;
        }

        private bool IsEntryValid(CacheValueEntry ret)
        {
            return SystemClock.Now - ret.LastWrite < CacheDuration;
        }

        public void Add(TKey key, TResult data)
        {
            if (_internalCache.ContainsKey(key))
            {
                // seems like you must remove key from dictionary before you can readd it
                // internalCache.Remove(key);
                var val = _internalCache[key];
                val.LastWrite = SystemClock.Now;
                val.Value = data;
            }
            else
            {
                _internalCache.Add(key, new CacheValueEntry { LastWrite = SystemClock.Now, Value = data });
            }

            if (_internalCache.Count > MaxKeysInCache)
            {
                lock (this)
                {
                    CacheRemovalExecutions++;
                    var removeKeys = MaxKeysInCache / 2; // remove half of keys when cache is full
                    foreach (var keyValue in _internalCache.OrderBy(o => o.Value.LastWrite))
                    {
                        _internalCache.Remove(keyValue.Key);
                        if (--removeKeys <= 0)
                        {
                            break;
                        }
                    }

                    // Remove also expired keys
                    var now = SystemClock.Now;
                    foreach (var keyValue in _internalCache.Where(w => now - w.Value.LastWrite >= CacheDuration).ToList())
                    {
                        _internalCache.Remove(keyValue.Key);
                    }
                }
            }
        }

        /// <summary>
        ///   Clears the cache
        /// </summary>
        public void Clear()
        {
            _internalCache.Clear();
        }

        public void ClearByKeySearch(List<string> keySearch)
        {
            if (keySearch == null || keySearch.Count == 0)
            {
                return;
            }

            var keysToRemove = Keys.Where(k => keySearch.Any(ks => k.ToString().ContainsCaseInsensitive(ks))).ToList();
            foreach (var key in keysToRemove)
            {
                ClearKey(key);
            }
        }

        public void ClearKey(TKey key)
        {
            _internalCache.Remove(key);
        }

        private class CacheValueEntry
        {
            public DateTime LastWrite;
            public TResult Value;
        }

        public bool ContainsExpiredEntries(TimeSpan timeToExpiration = default(TimeSpan))
        {
            return _internalCache.Count == 0 || _internalCache.Any(w => (SystemClock.Now - w.Value.LastWrite).Add(timeToExpiration) >= CacheDuration);
        }

        public DateTime GetItemLastWrite(TKey key)
        {
            if (_internalCache.TryGetValue(key, out var ret) && IsEntryValid(ret))
            {
                return ret.LastWrite;
            }

            return default(DateTime);
        }

        /// <summary>
        ///   To simplify cache access
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <param name="createKey"></param>
        /// <param name="callToBeCached"></param>
        /// <param name="p1"></param>
        /// <returns></returns>
        public TResult ExecuteCached<TIn>(Func<TIn, TKey> createKey, Func<TIn, TResult> callToBeCached, TIn p1)
        {
            var key = createKey(p1);
            TResult result;
            lock (this)
            {
                if (!TryGetValue(key, out result))
                {
                    result = callToBeCached(p1);
                    Add(key, result);
                }
            }

            return result;
        }

        public TResult ExecuteCached<TIn1, TIn2>(Func<TIn1, TIn2, TKey> createKey, Func<TIn1, TIn2, TResult> callToBeCached, TIn1 p1, TIn2 p2)
        {
            var key = createKey(p1, p2);
            TResult result;
            lock (this)
            {
                if (!TryGetValue(key, out result))
                {
                    result = callToBeCached(p1, p2);
                    Add(key, result);
                }
            }

            return result;
        }

        public TResult ExecuteCached<TIn1, TIn2, TIn3>(Func<TIn1, TIn2, TIn3, TKey> createKey, Func<TIn1, TIn2, TIn3, TResult> callToBeCached, TIn1 p1, TIn2 p2, TIn3 p3)
        {
            var key = createKey(p1, p2, p3);
            TResult result;
            lock (this)
            {
                if (!TryGetValue(key, out result))
                {
                    result = callToBeCached(p1, p2, p3);
                    Add(key, result);
                }
            }

            return result;
        }

        public TResult ExecuteCached<TIn1, TIn2, TIn3, TIn4>(Func<TIn1, TIn2, TIn3, TIn4, TKey> createKey, Func<TIn1, TIn2, TIn3, TIn4, TResult> callToBeCached, TIn1 p1, TIn2 p2, TIn3 p3, TIn4 p4)
        {
            var key = createKey(p1, p2, p3, p4);
            TResult result;
            lock (this)
            {
                if (!TryGetValue(key, out result))
                {
                    result = callToBeCached(p1, p2, p3, p4);
                    Add(key, result);
                }
            }

            return result;
        }

        public TResult ExecuteCached<TIn1, TIn2, TIn3, TIn4, TIn5>(Func<TIn1, TIn2, TIn3, TIn4, TIn5, TKey> createKey, Func<TIn1, TIn2, TIn3, TIn4, TIn5, TResult> callToBeCached, TIn1 p1, TIn2 p2, TIn3 p3, TIn4 p4, TIn5 p5)
        {
            var key = createKey(p1, p2, p3, p4, p5);
            TResult result;
            lock (this)
            {
                if (!TryGetValue(key, out result))
                {
                    result = callToBeCached(p1, p2, p3, p4, p5);
                    Add(key, result);
                }
            }

            return result;
        }

        public TResult ExecuteCached<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>(Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TKey> createKey, Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TResult> callToBeCached, TIn1 p1, TIn2 p2, TIn3 p3, TIn4 p4, TIn5 p5, TIn6 p6)
        {
            var key = createKey(p1, p2, p3, p4, p5, p6);
            TResult result;
            lock (this)
            {
                if (!TryGetValue(key, out result))
                {
                    result = callToBeCached(p1, p2, p3, p4, p5, p6);
                    Add(key, result);
                }
            }

            return result;
        }

        public TResult ExecuteCached<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7>(Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TKey> createKey, Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TResult> callToBeCached, TIn1 p1, TIn2 p2, TIn3 p3, TIn4 p4, TIn5 p5, TIn6 p6, TIn7 p7)
        {
            var key = createKey(p1, p2, p3, p4, p5, p6, p7);
            TResult result;
            lock (this)
            {
                if (!TryGetValue(key, out result))
                {
                    result = callToBeCached(p1, p2, p3, p4, p5, p6, p7);
                    Add(key, result);
                }
            }

            return result;
        }
    }
}