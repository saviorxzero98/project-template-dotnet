using Microsoft.Extensions.Configuration;

namespace CommonEx.Utilities.Configurations
{
    /// <summary>
    /// App Setting 管理
    /// </summary>
    public class AppSettingManager : AppSettingProvider
    {
        public AppSettingManager()
        {

        }
        public AppSettingManager(IConfiguration configuration) : base(configuration)
        {

        }
    }
}
