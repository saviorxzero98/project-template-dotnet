using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;

namespace CommonEx.HttpClients.Extensions
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// 加入 Http Client
        /// </summary>
        /// <param name="services"></param>
        /// <param name="settings"></param>
        /// <param name="clientName"></param>
        /// <returns></returns>
        public static IServiceCollection AddHttpClient(this IServiceCollection services, 
                                                       HttpApiSettings settings,
                                                       string clientName = HttpApiSettings.HttpClientName)
        {
            // 加入 Http Clinet
            var builder = services.AddHttpClient(clientName, (httpClient) =>
            {
                httpClient.BaseAddress = new Uri(settings.HostUrl);
            });


            // 忽略 Https 憑證檢查
            if (!settings.CertificateValidation)
            {
                builder.ConfigurePrimaryHttpMessageHandler(_ =>
                {
                    var handler = new HttpClientHandler()
                    {
                        ClientCertificateOptions = ClientCertificateOption.Manual,
                        ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => true
                    };
                    return handler;
                });
            }

            builder.AddRetryPolicy(settings)
                   .AddTimeoutPolicy(settings);

            return services;
        }

        /// <summary>
        /// 設定重試原則
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        private static IHttpClientBuilder AddRetryPolicy(this IHttpClientBuilder builder, HttpApiSettings settings)
        {
            if (settings == null)
            {
                return builder;
            }

            if (settings.RetryCount > 0 && settings.RetryWaitMilliseconds > 0)
            {
                var hasTimeout = settings.TimeoutSeconds > 0;
                var retryCount = settings.RetryCount;
                var waitTime = settings.RetryWaitMilliseconds;

                var policyBuilder = HttpPolicyExtensions.HandleTransientHttpError();

                if (hasTimeout)
                {
                    policyBuilder.Or<TimeoutRejectedException>();
                }

                var policy = policyBuilder.WaitAndRetryAsync(retryCount, _ => TimeSpan.FromMilliseconds(waitTime));
                builder.AddPolicyHandler(policy);
            }

            return builder;
        }

        /// <summary>
        /// 設定 Request Timeout 原則
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        private static IHttpClientBuilder AddTimeoutPolicy(this IHttpClientBuilder builder, HttpApiSettings settings)
        {
            if (settings == null || settings.TimeoutSeconds <= 0)
            {
                return builder;
            }

            var policy = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(settings.TimeoutSeconds));
            builder.AddPolicyHandler(policy);
            return builder;
        }
    }
}
