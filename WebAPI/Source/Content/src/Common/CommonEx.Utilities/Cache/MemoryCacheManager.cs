using Microsoft.Extensions.Caching.Memory;

namespace CommonEx.Utilities.Cache
{
    public class MemoryCacheManager<TEntity> : ICacheManager<TEntity>
    {
        private readonly IMemoryCache _cache;
        private readonly CacheOptions _options;


        public MemoryCacheManager(IMemoryCache cache)
        {
            _cache = cache;
        }
        public MemoryCacheManager(IMemoryCache cache, CacheOptions option)
        {
            _cache = cache;
            _options = option;
        }


        /// <summary>
        /// 取得 Cache 值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheMissCallback"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<TEntity> GetAsync(string key,
                                            Func<Task<TEntity>>? cacheMissCallback = null,
                                            CacheOptions options = null)
        {
            if (_cache.TryGetValue(key, out TEntity cacheValue))
            {
                return cacheValue;
            }

            if (cacheMissCallback == null)
            {
                return default(TEntity);
            }

            var newValue = await cacheMissCallback.Invoke();
            return await SetAsync(key, newValue, options);
        }

        /// <summary>
        /// 取得 Cache 值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<bool> TryGetAsync(string key, out TEntity value)
        {
            var result = _cache.TryGetValue(key, out value);
            return Task.FromResult(result);
        }

        /// <summary>
        /// 設定 Cache 值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public Task<TEntity> SetAsync(string key, TEntity value, CacheOptions options = null)
        {
            TEntity cacheValue;
            if (options != null)
            {
                cacheValue = _cache.Set(key, value, ToEntryOptions(options));
            }
            else
            {
                cacheValue = _cache.Set(key, value, ToEntryOptions(_options));
            }
            return Task.FromResult(cacheValue);
        }

        /// <summary>
        /// 移除 Cache 值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task RemoveAsync(string key)
        {
            _cache.Remove(key);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Entry Options 轉換
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        protected MemoryCacheEntryOptions? ToEntryOptions(CacheOptions options)
        {
            if (options == null)
            {
                return default(MemoryCacheEntryOptions);
            }

            var entryOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = options.AbsoluteExpiration,
                AbsoluteExpirationRelativeToNow = options.AbsoluteExpirationRelativeToNow,
                SlidingExpiration = options.SlidingExpiration,
                Size = options.Size,
                Priority = options.Priority
            };
            return entryOptions;
        }
    }
}
