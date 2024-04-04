namespace CommonEx.Database
{
    public enum DatabaseTypes
    {
        None,
        SqlServer,
        Postgres,
        Sqlite,
        MySql
    }

    public static class DatabaseTypeParser
    {
        /// <summary>
        /// 解析資料庫類型
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static DatabaseTypes Parse(string typeName)
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
