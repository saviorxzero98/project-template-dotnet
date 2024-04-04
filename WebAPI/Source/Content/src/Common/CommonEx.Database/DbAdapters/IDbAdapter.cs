using SqlKata.Compilers;
using System.Data;

namespace CommonEx.Database.DbAdapters
{
    public interface IDbAdapter
    {
        /// <summary>
        /// Connection String
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// Database Adapter Type
        /// </summary>
        string AdapterType { get; }


        /// <summary>
        /// 建立 Database Conneciton
        /// </summary>
        /// <returns></returns>
        IDbConnection CreateDbConnection();

        /// <summary>
        /// 取得 SQL Compiler
        /// </summary>
        /// <returns></returns>
        Compiler GetSqlCompiler();
    }
}
