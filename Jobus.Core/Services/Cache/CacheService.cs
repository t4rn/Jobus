using Microsoft.Extensions.Caching.Memory;
using System;

namespace Jobus.Core.Services.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public T Get<T>(string key)
        {
            _cache.TryGetValue(key, out T itemFromCache);

            return itemFromCache;
        }

        public void Set<T>(string key, T obj, double secondsInCache = 60)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(secondsInCache));

            _cache.Set(key, obj, cacheEntryOptions);
        }
    }
}
