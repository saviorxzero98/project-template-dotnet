namespace CommonEx.Caching
{
    public interface ICacheManager<TEntity>
    {
        /// <summary>
        /// 取得 Cache 值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheMissCallback"></param>
        /// <returns></returns>
        Task<TEntity?> GetOrDefaultAsync(string key, Func<string, Task<TEntity?>>? cacheMissCallback);

        /// <summary>
        /// 取得 Cache 值，Cache Miss 時設定 Cache 值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheMissCallback"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Task<TEntity> GetOrSetAsync(string key,
                                    Func<string, Task<TEntity>>? cacheMissCallback = null,
                                    CacheOptions options = null);

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
