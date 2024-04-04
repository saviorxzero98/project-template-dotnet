namespace CommonEx.Database.DbAdapters
{
    public static class DbAdapterFactory
    {
        /// <summary>
        /// 建立 Database Adapter
        /// </summary>
        /// <param name="type"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IDbAdapter Create(DatabaseTypes type, string connectionString)
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
        public static IDbAdapter Create(string typeName, string connectionString)
        {
            DatabaseTypes type = DatabaseTypeParser.Parse(typeName);
            return Create(type, connectionString);
        }
    }
}
