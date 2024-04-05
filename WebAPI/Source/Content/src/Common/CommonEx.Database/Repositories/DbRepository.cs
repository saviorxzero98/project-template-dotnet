using CommonEx.Database.DbAdapters;
using CommonEx.Database.Entities;
using CommonEx.Database.Exceptions;
using CommonEx.Database.Extensions;
using CommonEx.Database.Extensions.SqlKataEx;
using CommonEx.Database.UnitOfWorks;
using CommonEx.Utilities.GuidGenerators;
using Dapper;
using SqlKata;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Data;

namespace CommonEx.Database.Repositories
{
    public class DbRepository<TEntity> : IDbRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Database Connection
        /// </summary>
        private readonly IDbConnection _connection;
       
        /// <summary>
        /// Database Connection
        /// </summary>
        public IDbConnection Connection { get => _connection; }

        /// <summary>
        /// 取得 SQL Compiler
        /// </summary>
        /// <returns></returns>
        public Compiler SqlCompiler { get => _dbAdapter.GetSqlCompiler(); }

        /// <summary>
        /// 取得 GUID Generator
        /// </summary>
        /// <returns></returns>
        public IGuidGenerator GuidGenerator { get => _dbAdapter.GetGuidGenerator(); }

        /// <summary>
        /// Database Adapter
        /// </summary>
        private readonly IDbAdapter _dbAdapter;

        public DbRepository(IDbAdapter dbAdapter)
        {
            _dbAdapter = dbAdapter;
            _connection = _dbAdapter.CreateDbConnection();
        }
        public DbRepository(IDbAdapter dbAdapter, IDbConnection connection)
        {
            _dbAdapter = dbAdapter;
            _connection = connection;
        }
        public DbRepository(IDbAdapter dbAdapter, IUnitOfWork unitOfWork)
        {
            _dbAdapter = dbAdapter;
            _connection = unitOfWork.Connection;
        }

        /// <summary>
        /// Get TableName
        /// </summary>
        /// <returns></returns>
        public string GetTableName()
        {
            dynamic tableattr = typeof(TEntity).GetCustomAttributes(false)
                                               .SingleOrDefault(attr => attr.GetType().Name == nameof(TableAttribute));
            var name = string.Empty;

            if (tableattr != null)
            {
                name = tableattr.Name;
            }

            return name;
        }

        #region Select

        /// <summary>
        /// Get First
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public TEntity Get(SqlQueryable queryable)
        {
            if (Connection != null)
            {
                // 取得 SQL Query
                Query sqlQuery = GetSelectSqlQuery(queryable);

                // 執行 SQL
                var result = GetQueryExecution(sqlQuery).FirstOrDefault<TEntity>();
                return result;
            }
            return default(TEntity);
        }
        /// <summary>
        /// Get First
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public async Task<TEntity> GetAsync(SqlQueryable queryable)
        {
            if (Connection != null)
            {
                // 取得 SQL Query
                Query sqlQuery = GetSelectSqlQuery(queryable);

                // 執行 SQL
                var result = await GetQueryExecution(sqlQuery).FirstOrDefaultAsync<TEntity>();
                return result;
            }
            return default(TEntity);
        }

        /// <summary>
        /// Get List
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetList(SqlQueryable queryable = null)
        {
            if (Connection != null)
            {
                // 取得 SQL Query
                Query sqlQuery = GetSelectSqlQuery(queryable);

                // 執行 SQL
                var results = GetQueryExecution(sqlQuery).Get<TEntity>();
                return results;
            }

            return new List<TEntity>();
        }
        /// <summary>
        /// Get List
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetListAsync(SqlQueryable queryable = null)
        {
            if (Connection != null)
            {
                // 取得 SQL Query
                Query sqlQuery = GetSelectSqlQuery(queryable);

                // 執行 SQL
                var results = await GetQueryExecution(sqlQuery).GetAsync<TEntity>();
                return results;
            }

            return new List<TEntity>();
        }

        /// <summary>
        /// Get Count
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public int GetCount(SqlQueryable queryable)
        {
            if (Connection != null)
            {
                if (_dbAdapter == null)
                {
                    throw new NullDbAdapterException();
                }

                // 取得 SQL Query
                Query sqlQuery = GetSelectCountSqlQuery(queryable);

                // 執行 SQL
                var result = GetQueryExecution(sqlQuery).FirstOrDefault<RecordCountEntity>();

                if (result != null)
                {
                    return result.Count;
                }
            }
            return 0;
        }
        /// <summary>
        /// Get Count
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public async Task<int> GetCountAsync(SqlQueryable queryable)
        {
            if (Connection != null)
            {
                if (_dbAdapter == null)
                {
                    throw new NullDbAdapterException();
                }

                // 取得 SQL Query
                Query sqlQuery = GetSelectCountSqlQuery(queryable);

                // 執行 SQL
                var result = await GetQueryExecution(sqlQuery).FirstOrDefaultAsync<RecordCountEntity>();

                if (result != null)
                {
                    return result.Count;
                }
            }
            return 0;
        }


        /// <summary>
        /// Create Select Sql Statement
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        protected Query GetSelectSqlQuery(SqlQueryable queryable)
        {
            // 建立 SQL Statement
            Query query = new Query(GetTableName());

            if (queryable == null)
            {
                return query.SelectAllColumns();
            }

            // Select Column
            if (queryable.Columns != null &&
                queryable.Columns.Any())
            {
                query = query.Select(queryable.Columns.ToArray());
            }
            else
            {
                query = query.SelectAllColumns();
            }
            return GetSelectSqlQuery(query, queryable);
        }

        /// <summary>
        ///  Create Select Sql Statement
        /// </summary>
        /// <param name="query"></param>
        /// <param name="queryable"></param>
        /// <returns></returns>
        protected Query GetSelectSqlQuery(Query query, SqlQueryable queryable)
        {
            if (queryable == null)
            {
                return query;
            }

            // Limit
            if (queryable.Limit > 0)
            {
                query = query.Limit(queryable.Limit);
            }

            // Offset
            if (queryable.Offset >= 0)
            {
                query = query.Offset(queryable.Offset);
            }

            // Where
            if (queryable.WhereCondictions != null && queryable.WhereCondictions.Any())
            {
                query = query.WhereColumns(queryable.WhereCondictions);
            }

            // Order By
            if (queryable.OrderByCondictions != null &&
                queryable.OrderByCondictions.Any())
            {
                query = query.OrderByColumns(queryable.OrderByCondictions);
            }
            return query;
        }

        /// <summary>
        /// Create Select Count Sql Statement
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        protected Query GetSelectCountSqlQuery(SqlQueryable queryable)
        {
            // 建立 SQL Statement
            Query query = new Query(GetTableName());

            // Where
            if (queryable.WhereCondictions != null && queryable.WhereCondictions.Any())
            {
                query = query.WhereColumns(queryable.WhereCondictions);
            }

            // Count
            query.AsCount();
            return query;
        }

        #endregion


        #region Insert / Update / Delete

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insert(TEntity entity)
        {
            if (Connection != null && entity != null)
            {
                // 執行 SQL
                var factory = CreateQueryFactory();
                int result = factory.Query(GetTableName())
                                    .Insert(entity);
                return result;
            }
            return 0;
        }
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> InsertAsync(TEntity entity)
        {
            if (Connection != null && entity != null)
            {
                // 執行 SQL
                var factory = CreateQueryFactory();
                int result = await factory.Query(GetTableName())
                                          .InsertAsync(entity);
                return result;
            }
            return 0;
        }


        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public int Update(TEntity entity, List<WhereCondiction> where)
        {
            if (Connection != null && entity != null)
            {
                if (where.Any())
                {
                    // 取得 SQL Query
                    Query sqlQuery = GetUpdateSqlQuery(where);

                    // 執行 SQL
                    var factory = CreateQueryFactory();
                    int result = factory.FromQuery(sqlQuery)
                                        .Update(entity);
                    return result;
                }
                else
                {
                    int result = Connection.Update(entity);
                    return result;
                }
            }
            return 0;
        }
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(TEntity entity, List<WhereCondiction> where)
        {
            if (Connection != null && entity != null)
            {
                if (where.Any())
                {
                    // 取得 SQL Query
                    Query sqlQuery = GetUpdateSqlQuery(where);

                    // 執行 SQL
                    var factory = CreateQueryFactory();
                    int result = await factory.FromQuery(sqlQuery)
                                              .UpdateAsync(entity);
                    return result;
                }
                else
                {
                    int result = await Connection.UpdateAsync(entity);
                    return result;
                }
            }
            return 0;
        }


        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public int Delete(List<WhereCondiction> where)
        {
            if (Connection != null)
            {
                // 取得 SQL Query
                Query sqlQuery = GetDeleteSqlQuery(where);

                // 執行 SQL
                var factory = CreateQueryFactory();
                int result = factory.FromQuery(sqlQuery)
                                    .Delete();
                return result;
            }
            return 0;
        }
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(List<WhereCondiction> where)
        {
            if (Connection != null)
            {
                // 取得 SQL Query
                Query sqlQuery = GetDeleteSqlQuery(where);

                // 執行 SQL
                var factory = CreateQueryFactory();
                int result = await factory.FromQuery(sqlQuery)
                                          .DeleteAsync();
                return result;
            }
            return 0;
        }


        /// <summary>
        /// Get Update SQL Query
        /// </summary>
        /// <param name="whereConditions"></param>
        /// <returns></returns>
        protected Query GetUpdateSqlQuery(List<WhereCondiction> whereConditions)
        {
            // 建立 SQL Query
            Query query = new Query(GetTableName());

            // Where
            if (whereConditions != null && whereConditions.Any())
            {
                query = query.WhereColumns(whereConditions);
            }
            return query;
        }

        /// <summary>
        /// Get Delete SQL Quert
        /// </summary>
        /// <param name="whereConditions"></param>
        /// <returns></returns>
        protected Query GetDeleteSqlQuery(List<WhereCondiction> whereConditions = null)
        {
            // 建立 SQL Statement
            Query query = new Query(GetTableName());

            // Where
            if (whereConditions != null && whereConditions.Any())
            {
                query = query.WhereColumns(whereConditions);
            }

            // Update
            query.AsDelete();

            return query;
        }

        #endregion


        #region Private Method

        /// <summary>
        /// Get Sqlkata Execution
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public Query GetQueryExecution(Query query)
        {
            if (_dbAdapter == null)
            {
                throw new NullDbAdapterException();
            }

            var factory = new QueryFactory(Connection, _dbAdapter.GetSqlCompiler());
            return factory.FromQuery(query);
        }

        /// <summary>
        /// 建立 SQL Query Factory
        /// </summary>
        /// <returns></returns>
        public QueryFactory CreateQueryFactory()
        {
            if (_dbAdapter == null)
            {
                throw new NullDbAdapterException();
            }

            var factory = new QueryFactory(Connection, _dbAdapter.GetSqlCompiler());
            return factory;
        }

        #endregion
    }
}
