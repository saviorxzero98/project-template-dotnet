using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApiTemplate.Test
{
    public class TestHostBaseFixture
    {
        public ServiceProvider ServiceProvider { get; private set; }

        public TestServer Server { get; private set; }

        public HttpClient Client { get; private set; }

        public TestHostBaseFixture()
        {
            var serviceCollection = new ServiceCollection();

            // 讀取 appsettings.json
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json",
                                                                       optional: true,
                                                                       reloadOnChange: true)
                                                          .Build();
            serviceCollection.AddSingleton<IConfiguration>(configuration);

            // 設定 Web Host，執行環境、Configutation、Web API 的 Startup 類別
            var builder = new WebHostBuilder().UseEnvironment("yest")
                                              .UseConfiguration(configuration)
                                              .UseStartup<Startup>();

            // 建立 Test Server 和 Http Client
            Server = new TestServer(builder);
            Client = Server.CreateClient();

            // 設定 Service Provider
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
