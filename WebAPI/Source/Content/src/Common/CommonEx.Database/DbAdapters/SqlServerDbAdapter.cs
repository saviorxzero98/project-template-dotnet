using CommonEx.Utilities.GuidGenerators;
using Microsoft.Data.SqlClient;
using SqlKata.Compilers;
using System.Data;

namespace CommonEx.Database.DbAdapters
{
    public class SqlServerDbAdapter : IDbAdapter
    {
        protected string _connectionString;
        /// <summary>
        /// Connection String
        /// </summary>
        public string ConnectionString { get => _connectionString; }

        /// <summary>
        /// Database Adapter Type
        /// </summary>
        public string AdapterType { get => DatabaseTypes.SqlServer.ToString().ToLower(); }


        public SqlServerDbAdapter(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// 建立 Microsoft SQL Server Database Connection
        /// </summary>
        /// <returns></returns>
        public IDbConnection CreateDbConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        /// <summary>
        /// 建立 Microsoft SQL Server SQL Compiler
        /// </summary>
        /// <returns></returns>
        public Compiler GetSqlCompiler()
        {
            return new SqlServerCompiler();
        }

        /// <summary>
        /// 取得 GUID Generator
        /// </summary>
        /// <returns></returns>
        public IGuidGenerator GetGuidGenerator()
        {
            return GuidV8Generator.Instance;
        }
    }
}
