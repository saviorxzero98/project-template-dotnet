using CommonEx.Utilities.Configurations;
using Microsoft.Extensions.Configuration;

namespace CommonEx.Database.Settings
{
    public class DatabaseSetting
    {
        public const string SettingName = "DatabaseSettings";

        /// <summary>
        /// Database Type
        /// </summary>
        public string DatabaseType { get; set; } = string.Empty;

        /// <summary>
        /// Connection String Name
        /// </summary>
        public string ConnectionName { get; set; } = string.Empty;


        /// <summary>
        /// Load Database Setting
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static DatabaseSetting Load(IConfiguration configuration)
        {
            var manager = new AppSettingProvider();
            return manager.GetSettings<DatabaseSetting>(SettingName);
        }

        /// <summary>
        /// Get Connection String
        /// </summary>
        /// <returns></returns>
        public string GetConnectionString()
        {
            var manager = new AppSettingProvider();
            return manager.GetConnectorString(ConnectionName);
        }
    
        /// <summary>
        /// Get Database Type
        /// </summary>
        /// <returns></returns>
        public DatabaseTypes GetDatabaseType()
        {
            return DatabaseTypeParser.Parse(DatabaseType);
        }
    }
}
