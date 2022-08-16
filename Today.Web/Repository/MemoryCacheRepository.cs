using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text.Json;

namespace Today.Web.Repository
{
    public class MemoryCacheRepository : IMemoryCacheRepository
    {
        private readonly IDistributedCache _iDistributeCache;
        public MemoryCacheRepository(IDistributedCache iDistributeCache)
        {
            _iDistributeCache = iDistributeCache;
        }
        public void Set<T>(string key, T value) where T : class
        {
            _iDistributeCache.Set(key, ObjectToByteArray(value), new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(3)
            });
        }

        public T Get<T>(string key) where T : class
        {
            return ByteArrayToObject<T>(_iDistributeCache.Get(key));
        }

        public void Remove(string key)
        {
            _iDistributeCache.Remove(key);
        }

        private byte[] ObjectToByteArray(object obj)
        {
            return JsonSerializer.SerializeToUtf8Bytes(obj);
        }

        private T ByteArrayToObject<T>(byte[] bytes) where T : class
        {
            return bytes is null ? null : JsonSerializer.Deserialize<T>(bytes);
        }
    }
}
