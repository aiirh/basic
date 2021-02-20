using System.Collections.Generic;
using System.Linq;

namespace Aiirh.Basic.Services.Cache
{
    public class CacheRegistry
    {
        private static Dictionary<string, ICacheInfo> registry;

        public static Dictionary<string, ICacheInfo> Registry
        {
            get
            {
                if (registry == null)
                {
                    registry = new Dictionary<string, ICacheInfo>();
                }

                return registry;
            }
        }

        public static void Clear(string name)
        {
            if (Registry.TryGetValue(name, out var cache))
            {
                cache.Clear();
            }
        }

        public static List<ICacheInfo> Caches => Registry.Values.ToList();

        public static int ClearByName(string name)
        {
            var caches = Caches.Where(c => c.Name == name).ToList();
            caches.ForEach(c => c.Clear());
            return caches.Count;
        }

        public static void ClearByNameAndKey(string name, List<string> keysSearch)
        {
            var caches = Caches.Where(c => c.Name == name).ToList();
            caches.ForEach(c => c.ClearByKeySearch(keysSearch));
        }
    }
}