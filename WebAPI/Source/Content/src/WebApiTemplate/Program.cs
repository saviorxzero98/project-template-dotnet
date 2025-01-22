using Microsoft.AspNetCore;
using NLog;
using NLog.Web;

namespace WebApiTemplate
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                          .UseWebRoot("wwwroot")
                          .UseStartup<Startup>()
                          .UseNLog();
        }
    }
}
