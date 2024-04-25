using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace WebApiTemplate.Test.Controllers
{
    public class VersionControllerTest : IClassFixture<TestHostBaseFixture>
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public VersionControllerTest(TestHostBaseFixture fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
            _server = fixture.Server;
            _client = fixture.Client;
        }

        [Fact]
        public Task Test1Async()
        {
            return Task.CompletedTask;
        }
    }
}
