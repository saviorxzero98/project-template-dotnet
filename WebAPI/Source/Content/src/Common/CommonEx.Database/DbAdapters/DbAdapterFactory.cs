namespace CommonEx.Database.DbAdapters
{
    public enum DatabaseTypes
    {
        None,
        SqlServer,
        Postgres,
        Sqlite,
        MySql
    }

    public static class DbAdapterFactory
    {
        /// <summary>
        /// 建立 Database Adapter
        /// </summary>
        /// <param name="type"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IDbAdapter CreateDbAdapter(DatabaseTypes type, string connectionString)
        {
            switch (type)
            {
                case DatabaseTypes.SqlServer:
                    return new SqlServerDbAdapter(connectionString);

                case DatabaseTypes.Postgres:
                    return new PostgresDbAdapter(connectionString);

                case DatabaseTypes.Sqlite:
                    return new SqliteDbAdapter(connectionString);

                case DatabaseTypes.MySql:
                    return new MySqlDbAdapter(connectionString);
            }
            return null;
        }

        /// <summary>
        /// 建立 Database Adapter
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IDbAdapter CreateDbAdapter(string typeName, string connectionString)
        {
            DatabaseTypes type = ParseDbType(typeName);
            return CreateDbAdapter(type, connectionString);
        }

        /// <summary>
        /// 解析資料庫類型
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static DatabaseTypes ParseDbType(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
            {
                return DatabaseTypes.None;
            }

            switch (typeName.ToLower())
            {
                case "mssql":
                case "sqlserver":
                    return DatabaseTypes.SqlServer;

                case "sqlite":
                case "sqlite3":
                    return DatabaseTypes.Sqlite;

                case "pgsql":
                case "postgre":
                case "postgres":
                case "postgresql":
                    return DatabaseTypes.Postgres;

                case "mysql":
                case "mariadb":
                    return DatabaseTypes.MySql;

                default:
                    return DatabaseTypes.None;
            }
        }
    }
}
