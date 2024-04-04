using Microsoft.Extensions.Configuration;

namespace CommonEx.Utilities.Configurations
{
    /// <summary>
    /// App Setting 讀取
    /// </summary>
    public class AppSettingProvider
    {
        public IConfiguration Configuration { get; set; }

        public AppSettingProvider()
        {

        }
        public AppSettingProvider(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 取出連線字串
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetConnectorString(string name)
        {
            string connectorString = string.Empty;

            if (Configuration != null)
            {
                connectorString = Configuration.GetConnectionString(name) ?? string.Empty;
            }
            return connectorString;
        }

        /// <summary>
        /// 取出相關設定
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetSetting(string name)
        {
            string setting = string.Empty;

            if (Configuration != null)
            {
                setting = Configuration.GetSection(name).Value ?? string.Empty;
            }
            return setting;
        }

        /// <summary>
        /// 取出相關設定
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T GetSettings<T>(string name)
        {
            if (Configuration != null)
            {
                var currentSetting = Configuration.GetSection(name).Get<T>();

                if (currentSetting != null)
                {
                    return currentSetting;
                }
            }

            return default(T);
        }

        /// <summary>
        /// 是否存在相關設定
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool HasSetting(string name)
        {
            if (Configuration != null)
            {
                return Configuration.GetSection(name).Exists();
            }
            return false;
        }

        /// <summary>
        /// 是否為 Null Configuration
        /// </summary>
        /// <returns></returns>
        public bool IsNullConfiguration()
        {
            return (Configuration == null);
        }
    }
}
