﻿using Microsoft.Data.Sqlite;
using SqlKata.Compilers;
using System.Data;

namespace CommonEx.Database.DbAdapters
{
    public class SqliteDbAdapter : IDbAdapter
    {
        protected string _connectionString;
        /// <summary>
        /// Connection String
        /// </summary>
        public string ConnectionString { get => _connectionString; }

        /// <summary>
        /// Database Adapter Type
        /// </summary>
        public string AdapterType { get => DatabaseTypes.Sqlite.ToString().ToLower(); }


        public SqliteDbAdapter(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// 建立 SQLite Database Connection
        /// </summary>
        /// <returns></returns>
        public IDbConnection CreateDbConnection()
        {
            return new SqliteConnection(ConnectionString);
        }

        /// <summary>
        /// 建立 SQLite SQL Compiler
        /// </summary>
        /// <returns></returns>
        public Compiler GetSqlCompiler()
        {
            return new SqliteCompiler();
        }
    }
}
