using Microsoft.AspNetCore;

namespace WebApiTemplate
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                          .UseWebRoot("wwwroot")
                          .UseStartup<Startup>();
        }
    }
}
