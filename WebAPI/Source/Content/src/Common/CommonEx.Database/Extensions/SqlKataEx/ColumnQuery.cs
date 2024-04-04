namespace CommonEx.Database.Extensions.SqlKataEx
{
    /// <summary>
    /// Sqlkata Column Query
    /// </summary>
    public class ColumnQuery
    {
        /// <summary>
        /// Schema
        /// </summary>
        public string SchemaName { get; set; } = string.Empty;

        /// <summary>
        /// Table
        /// </summary>
        public string TableName { get; set; } = string.Empty;

        /// <summary>
        /// Column Name
        /// </summary>
        public string ColumnName { get; set; } = string.Empty;

        /// <summary>
        /// Alias Name
        /// </summary>
        public string AliasName { get; set; } = string.Empty;


        public ColumnQuery()
        {

        }
        public ColumnQuery(string columnName, string aliasName = null)
        {
            ColumnName = columnName;
            AliasName = aliasName;
        }

        /// <summary>
        /// Column Name
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static ColumnQuery Column(string columnName)
        {
            return new ColumnQuery(columnName);
        }
        /// <summary>
        /// Table Name
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static ColumnQuery Table(string tableName, string schema = null)
        {
            if (!string.IsNullOrWhiteSpace(schema))
            {
                return new ColumnQuery()
                {
                    TableName = tableName,
                    SchemaName = schema
                };
            }
            else
            {
                return new ColumnQuery() { TableName = tableName };
            }
        }

        /// <summary>
        /// Table Name
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="schema"></param>
        /// <returns></returns>
        public ColumnQuery Of(string tableName, string schemaName = null)
        {
            TableName = tableName;
            SchemaName = schemaName;
            return this;
        }
        /// <summary>
        /// Column Name
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public ColumnQuery Col(string columnName)
        {
            ColumnName = columnName;
            return this;
        }
        /// <summary>
        /// As Alias Name
        /// </summary>
        /// <param name="aliasName"></param>
        /// <returns></returns>
        public ColumnQuery As(string aliasName)
        {
            AliasName = aliasName;
            return this;
        }


        /// <summary>
        /// To Raw
        /// </summary>
        /// <returns></returns>
        public string ToRaw()
        {
            string raw = ColumnName.Trim();

            if (!string.IsNullOrWhiteSpace(TableName))
            {
                if (!string.IsNullOrWhiteSpace(SchemaName))
                {
                    raw = $"{SchemaName.Trim()}.{TableName.Trim()}.{raw}";
                }
                else
                {
                    raw = $"{TableName.Trim()}.{raw}";
                }
            }

            if (!string.IsNullOrWhiteSpace(AliasName))
            {
                raw = $"{raw} AS {AliasName}";
            }

            return raw;
        }
    }
}
