using Microsoft.Extensions.Caching.Memory;

namespace CommonEx.Caching
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
        /// <returns></returns>
        public Task<TEntity?> GetOrDefaultAsync(string key, Func<string, Task<TEntity?>>? cacheMissCallback)
        {
            if (_cache.TryGetValue(key, out TEntity? value))
            {
                return Task.FromResult(value);
            }

            if (cacheMissCallback == null)
            {
                return Task.FromResult(default(TEntity));
            }

            return cacheMissCallback.Invoke(key);
        }

        /// <summary>
        /// 取得 Cache 值，Cache Miss 時設定 Cache 值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheMissCallback"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<TEntity> GetOrSetAsync(string key,
                                                 Func<string, Task<TEntity>>? cacheMissCallback = null,
                                                 CacheOptions options = null)
        {
            if (_cache.TryGetValue(key, out TEntity? cacheValue))
            {
                return cacheValue;
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
        public Task<TEntity> SetAsync(string key, TEntity value, CacheOptions options = null)
        {
            TEntity cacheValue;
            var cacheOptions = ToEntryOptions(options ?? _options);
            if (cacheOptions != null)
            {
                cacheValue = _cache.Set(key, value, cacheOptions);
            }
            else
            {
                cacheValue = _cache.Set(key, value);
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