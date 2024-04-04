using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace CommonEx.Database.Migrators.Extensions
{
    public static class FluentMigratorRunnerExtensions
    {
        /// <summary>
        /// 設定 Fluent Migrator Runner
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <param name="dbTypes"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureRunner<T>(this IServiceCollection services,
                                                            string connectionString,
                                                            string dbTypes)
        {
            return services.ConfigureRunner<T>(connectionString, DatabaseTypeParser.Parse(dbTypes));
        }

        /// <summary>
        /// 設定 Fluent Migrator Runner
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <param name="dbTypes"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureRunner<T>(this IServiceCollection services,
                                                            string connectionString,
                                                            DatabaseTypes dbTypes)
        {
            return services.ConfigureRunner(rb =>
            {
                switch (dbTypes)
                {
                    case DatabaseTypes.Sqlite :
                        rb.AddSQLite();
                        break;

                    case DatabaseTypes.SqlServer:
                        rb.AddSqlServer();
                        break;

                    case DatabaseTypes.Postgres:
                        rb.AddPostgres();
                        break;

                    case DatabaseTypes.MySql:
                        rb.AddMySql5();
                        break;

                    default:
                        throw new Exception("Not support this database.");
                }

                rb.WithGlobalConnectionString(connectionString)
                  .ScanIn(typeof(T).Assembly)
                  .For
                  .Migrations();
            });
        }
    }
}
