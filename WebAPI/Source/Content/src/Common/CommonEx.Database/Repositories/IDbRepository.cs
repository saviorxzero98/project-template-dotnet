using CommonEx.Database.Extensions.SqlKataEx;

namespace CommonEx.Database.Repositories
{
    public interface IDbRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Get Table Name
        /// </summary>
        /// <returns></returns>
        string GetTableName();


        /// <summary>
        /// Get First
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        TEntity Get(SqlQueryable queryable);
        /// <summary>
        /// Get First
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(SqlQueryable queryable);


        /// <summary>
        /// Get List
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        IEnumerable<TEntity> GetList(SqlQueryable queryable = null);
        /// <summary>
        /// Get List
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetListAsync(SqlQueryable queryable = null);


        /// <summary>
        /// Get Count
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        int GetCount(SqlQueryable queryable);
        /// <summary>
        /// Get Count
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        Task<int> GetCountAsync(SqlQueryable queryable);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insert(TEntity entity);
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> InsertAsync(TEntity entity);


        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        int Update(TEntity entity, List<WhereCondiction> where);
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(TEntity entity, List<WhereCondiction> where);


        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        int Delete(List<WhereCondiction> where);
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(List<WhereCondiction> where);
    }
}
