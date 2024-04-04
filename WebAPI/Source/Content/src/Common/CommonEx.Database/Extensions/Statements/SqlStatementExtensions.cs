namespace CommonEx.Database.Extensions.Statements
{
    public static class SqlStatementExtensions
    {
        /// <summary>
        /// SQL AS Alias
        /// </summary>
        /// <param name="name"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public static string AsAlias(this string name, string alias)
        {
            return $"{name.Trim()} AS {alias.Trim()}";
        }

        /// <summary>
        /// SQL As Alias
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="tableName"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public static string AsAlias(this string columnName, string tableName, string alias)
        {
            return $"{tableName.TableColumn(columnName)} AS {alias.Trim()}";
        }

        /// <summary>
        /// Combine Table and Column
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static string TableColumn(this string tableName, string columnName)
        {
            return $"{tableName.Trim()}.{columnName.Trim()}";
        }
    }
}
