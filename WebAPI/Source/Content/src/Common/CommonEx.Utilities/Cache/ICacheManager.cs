using Microsoft.Extensions.Caching.Memory;

namespace CommonEx.Utilities.Cache
{
    public interface ICacheManager<TEntity>
    {
        /// <summary>
        /// 取得 Cache 值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheMissCallback"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(string key,
                               Func<Task<TEntity>>? cacheMissCallback = null,
                               CacheOptions options = null);

        /// <summary>
        /// 取得 Cache 值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<bool> TryGetAsync(string key, out TEntity value);

        /// <summary>
        /// 設定 Cache 值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Task<TEntity> SetAsync(string key, TEntity value, CacheOptions options = null);

        /// <summary>
        /// 移除 Cache 值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task RemoveAsync(string key);
    }
}
