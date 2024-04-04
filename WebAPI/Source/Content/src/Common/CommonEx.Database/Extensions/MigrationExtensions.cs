using CommonEx.Database.DbAdapters;
using FluentMigrator.Builders.Create.Table;
using FluentMigrator.Builders.Delete;
using FluentMigrator.Builders.IfDatabase;
using FluentMigrator.Builders.Schema;
using SqlKata;
using SqlKata.Compilers;

namespace CommonEx.Database.Extensions
{
    public static class MigrationExtensions
    {
        #region Exist


        /// <summary>
        /// 檢查 Table 是否已經存在
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static bool IsTableExist(this ISchemaExpressionRoot schema, string tableName)
        {
            return schema.Table(tableName).Exists();
        }

        /// <summary>
        /// 檢查 Table Column 是否已經存在
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static bool IsTableColumnExist(this ISchemaExpressionRoot schema, string tableName, string columnName)
        {
            if (schema.IsTableExist(tableName))
            {
                return schema.Table(tableName).Column(columnName).Exists();
            }
            return false;
        }

        /// <summary>
        /// 檢查 Table Index 是否已經存在
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="tableName"></param>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public static bool IsIndexExist(this ISchemaExpressionRoot schema, string tableName, string indexName)
        {
            if (schema.IsTableExist(tableName))
            {
                return schema.Table(tableName).Index(indexName).Exists();
            }
            return false;
        }


        #endregion


        #region Table

        /// <summary>
        /// 如果 Table 存在，則刪除 Table
        /// </summary>
        /// <param name="delete"></param>
        /// <param name="schema"></param>
        /// <param name="tableName"></param>
        public static void TableIfExist(this IDeleteExpressionRoot delete, ISchemaExpressionRoot schema, string tableName)
        {
            if (schema.IsTableExist(tableName))
            {
                delete.Table(tableName);
            }
        }

        #endregion


        #region View

        /// <summary>
        /// 建立 View
        /// </summary>
        /// <param name="ifDatabase"></param>
        /// <param name="viewName"></param>
        /// <param name="dbType"></param>
        /// <param name="selectQuery"></param>
        public static void CreateView(this IIfDatabaseExpressionRoot ifDatabase,
                                      string viewName,
                                      DatabaseTypes dbType,
                                      Query selectQuery)
        {
            // 依據資料庫建立對應的 Select SQL Statement
            (string start, string end) = (string.Empty, string.Empty);
            Compiler compiler;
            switch (dbType)
            {
                case DatabaseTypes.SqlServer:
                    compiler = new SqlServerCompiler();
                    (start, end) = SqlServerIdentifiers;
                    break;

                case DatabaseTypes.Postgres:
                    compiler = new PostgresCompiler();
                    (start, end) = PostgresIdentifiers;
                    break;

                case DatabaseTypes.Sqlite:
                    compiler = new SqliteCompiler();
                    (start, end) = SqliteIdentifiers;
                    break;

                case DatabaseTypes.MySql:
                    compiler = new MySqlCompiler();
                    (start, end) = MySqlIdentifiers;
                    break;

                default:
                    return;
            }
            SqlResult sqlResult = compiler.Compile(selectQuery);

            // Create View
            ifDatabase.Execute.Sql($"CREATE VIEW {FormatName(viewName, start, end)} AS {sqlResult.Sql}");
        }

        /// <summary>
        /// 刪除 View
        /// </summary>
        /// <param name="ifDatabase"></param>
        /// <param name="viewName"></param>
        /// <param name="dbType"></param>
        public static void DeleteView(this IIfDatabaseExpressionRoot ifDatabase,
                                      string viewName,
                                      DatabaseTypes dbType)
        {
            (string start, string end) = (string.Empty, string.Empty);
            switch (dbType)
            {
                case DatabaseTypes.SqlServer:
                    (start, end) = SqlServerIdentifiers;
                    break;

                case DatabaseTypes.Postgres:
                    (start, end) = PostgresIdentifiers;
                    break;

                case DatabaseTypes.Sqlite:
                    (start, end) = SqliteIdentifiers;
                    break;

                case DatabaseTypes.MySql:
                    (start, end) = MySqlIdentifiers;
                    break;

                default:
                    return;
            }

            string sqlStatement = $"DROP VIEW {FormatName(viewName, start, end)}";
            ifDatabase.Execute.Sql(sqlStatement);
        }

        #endregion


        #region Column

        /// <summary>
        /// MAX Unicode String
        /// </summary>
        /// <param name="column"></param>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static ICreateTableColumnOptionOrWithColumnSyntax AsMaxString(this ICreateTableColumnAsTypeSyntax column,
                                                                             DatabaseTypes dbType)
        {
            switch (dbType)
            {
                case DatabaseTypes.SqlServer:
                    return column.AsCustom(@"NVARCHAR(MAX)");

                case DatabaseTypes.Postgres:
                    return column.AsCustom(@"TEXT");

                case DatabaseTypes.Sqlite:
                    return column.AsCustom(@"TEXT");

                case DatabaseTypes.MySql:
                    return column.AsCustom(@"NVARCHAR(MAX)");

                default:
                    return column.AsString(int.MaxValue);
            }
        }

        /// <summary>
        /// MAX String
        /// </summary>
        /// <param name="column"></param>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static ICreateTableColumnOptionOrWithColumnSyntax AsMaxAnsiString(this ICreateTableColumnAsTypeSyntax column,
                                                                                 DatabaseTypes dbType)
        {
            switch (dbType)
            {
                case DatabaseTypes.SqlServer:
                    return column.AsCustom(@"VARCHAR(MAX)");

                case DatabaseTypes.Postgres:
                    return column.AsCustom(@"VARCHAR(MAX)");

                case DatabaseTypes.Sqlite:
                    return column.AsCustom(@"TEXT");

                case DatabaseTypes.MySql:
                    return column.AsCustom(@"VARCHAR(MAX)");

                default:
                    return column.AsAnsiString(int.MaxValue);
            }
        }

        /// <summary>
        /// Auto Increment
        /// </summary>
        /// <param name="column"></param>
        /// <param name="dbType"></param>
        /// <param name="isInt64"></param>
        /// <returns></returns>
        public static ICreateTableColumnOptionOrWithColumnSyntax AsAutoIncrement(this ICreateTableColumnAsTypeSyntax column,
                                                                                 DatabaseTypes dbType, 
                                                                                 bool isInt64 = false)
        {
            switch (dbType)
            {
                case DatabaseTypes.Sqlite:
                    return column.AsCustom(@"INTEGER AUTOINCREMENT");

                default:
                    if (isInt64)
                    {
                        return column.AsInt64().Identity().Unique();
                    }
                    else
                    {
                        return column.AsInt32().Identity().Unique();
                    }
            }
        }

        #endregion


        #region Private Method

        /// <summary>
        /// 格式化名稱
        /// </summary>
        /// <param name="name"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private static string FormatName(string name, string start, string end)
        {
            if (string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end))
            {
                return name;
            }

            if (!name.StartsWith(start) && !name.EndsWith(end))
            {
                return $"{start}{name}{end}";
            }

            return name;
        }

        /// <summary>
        /// SQL Server 名稱識別字
        /// </summary>
        private static (string start, string end) SqlServerIdentifiers
        {
            get
            {
                return (start: "[", end: "]");
            }
        }

        /// <summary>
        /// PostgreSql 名稱識別字
        /// </summary>
        private static (string start, string end) PostgresIdentifiers
        {
            get
            {
                return (start: "\"", end: "\"");
            }
        }

        /// <summary>
        /// SQLite 名稱識別字
        /// </summary>
        private static (string start, string end) SqliteIdentifiers
        {
            get
            {
                return (start: "\"", end: "\"");
            }
        }

        /// <summary>
        /// MySql 名稱識別字
        /// </summary>
        private static (string start, string end) MySqlIdentifiers
        {
            get
            {
                return (start: "`", end: "`");
            }
        }

        #endregion
    }
}
