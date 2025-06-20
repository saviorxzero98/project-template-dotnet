using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace CommonEx.Caching
{
    public class DistributedCacheCacheManager<TEntity> : ICacheManager<TEntity>
    {
        private readonly IDistributedCache _cache;
        private readonly CacheOptions _options;
        protected JsonSerializerSettings _serializerSettings { get; set; }


        public DistributedCacheCacheManager(IDistributedCache cache)
        {
            _cache = cache;
            _serializerSettings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.None,
            };
        }
        public DistributedCacheCacheManager(IDistributedCache cache, CacheOptions option)
        {
            _cache = cache;
            _options = option;
            _serializerSettings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.None,
            };
        }

        /// <summary>
        /// 設定 JSON 序列化設定
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public DistributedCacheCacheManager<TEntity> SetSerializerSettings(JsonSerializerSettings settings)
        {
            if (settings != null)
            {
                _serializerSettings = settings;
            }
            return this;
        }


        /// <summary>
        /// 取得 Cache 值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheMissCallback"></param>
        /// <returns></returns>
        public async Task<TEntity?> GetOrDefaultAsync(string key, Func<string, Task<TEntity?>>? cacheMissCallback)
        {
            try
            {
                var stringValue = await _cache.GetStringAsync(key);

                if (!string.IsNullOrWhiteSpace(stringValue))
                {
                    var value = JsonConvert.DeserializeObject<TEntity>(stringValue);
                    return value;
                }
            }
            catch
            {

            }

            if (cacheMissCallback == null)
            {
                return default(TEntity);
            }

            var defaultValue = await cacheMissCallback.Invoke(key);
            return defaultValue;
        }

        /// <summary>
        /// 取得 Cache 值，Cache Miss 時設定 Cache 值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheMissCallback"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<TEntity> GetOrSetAsync(string key, Func<string, Task<TEntity>>? cacheMissCallback = null, CacheOptions options = null)
        {
            try
            {
                var stringValue = await _cache.GetStringAsync(key);

                if (!string.IsNullOrWhiteSpace(stringValue))
                {
                    var value = JsonConvert.DeserializeObject<TEntity>(stringValue);
                    if (value != null)
                    {
                        return value;
                    }
                }
            }
            catch
            {

            }

            if (cacheMissCallback == null)
            {
                return default(TEntity);
            }

            var newValue = await cacheMissCallback.Invoke(key);
            return await SetAsync(key, newValue, options);
        }

        /// <summary>
        /// 設定 Cache 值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<TEntity> SetAsync(string key, TEntity value, CacheOptions options = null)
        {
            var stringValue = JsonConvert.SerializeObject(value, _serializerSettings);
            
            var cacheOptions = ToEntryOptions(options ?? _options);
            if (cacheOptions != null)
            {
                await _cache.SetStringAsync(key, stringValue, cacheOptions);
            }
            else
            {
                await _cache.SetStringAsync(key, stringValue);
            }
            return value;
        }

        /// <summary>
        /// 移除 Cache 值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task RemoveAsync(string key)
        {
            return _cache.RemoveAsync(key);
        }

        /// <summary>
        /// Entry Options 轉換
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        protected DistributedCacheEntryOptions? ToEntryOptions(CacheOptions options)
        {
            if (options == null)
            {
                return default(DistributedCacheEntryOptions);
            }
            
            var entryOptions = new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = options.AbsoluteExpiration,
                AbsoluteExpirationRelativeToNow = options.AbsoluteExpirationRelativeToNow,
                SlidingExpiration = options.SlidingExpiration
            };
            return entryOptions;
        }
    }
}
