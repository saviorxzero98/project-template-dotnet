using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace CommonEx.HttpClients.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 加入 Http Client & Web API Client
        /// </summary>
        /// <param name="services"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static IServiceCollection AddWebApiClient(
            this IServiceCollection services,
            WebApiClientSettings settings)
        {
            if (settings == null)
            {
                settings = new WebApiClientSettings();
            }

            services.AddTransient<IWebApiClient, WebApiClient>();

            // 加入 Http Clinet
            var builder = services.AddHttpClient(WebApiClient.HttpClientName);

            // 忽略 Https 憑證檢查
            if (!settings.CertificateValidation)
            {
                builder = builder.ConfigureSelfCertificateValidation();
            }

            // 加入 Policy
            builder = builder.AddRetryPolicy(settings)
                             .AddTimeoutPolicy(settings);
            return services;
        }

        /// <summary>
        /// 加入 Http Client & Web API Client
        /// </summary>
        /// <param name="services"></param>
        /// <param name="settings"></param>
        /// <param name="configureHandler"></param>
        /// <returns></returns>
        public static IServiceCollection AddWebApiClient(
            this IServiceCollection services,
            WebApiClientSettings settings,
            Action<IHttpClientBuilder> configureHandler)
        {
            services.AddTransient<IWebApiClient, WebApiClient>();

            // 加入 Http Clinet
            var builder = services.AddHttpClient(WebApiClient.HttpClientName);

            // 忽略 Https 憑證檢查
            if (!settings.CertificateValidation)
            {
                builder = builder.ConfigureSelfCertificateValidation();
            }

            configureHandler?.Invoke(builder);

            // 加入 Policy
            builder = builder.AddRetryPolicy(settings)
                             .AddTimeoutPolicy(settings);
            return services;
        }


        #region Private: Https 憑證檢查

        /// <summary>
        /// 忽略 Https 憑證檢查
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        private static IHttpClientBuilder ConfigureSelfCertificateValidation(this IHttpClientBuilder builder)
        {
            builder.ConfigurePrimaryHttpMessageHandler(_ =>
            {
                var handler = new HttpClientHandler()
                {
                    ClientCertificateOptions = ClientCertificateOption.Manual,
                    ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) =>
                        policyErrors == System.Net.Security.SslPolicyErrors.None
                };
                return handler;
            });
            return builder;
        }

        #endregion


        #region Private: Policy

        /// <summary>
        /// 設定重試原則
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        private static IHttpClientBuilder AddRetryPolicy(this IHttpClientBuilder builder, WebApiClientSettings settings)
        {
            if (settings == null)
            {
                settings = new WebApiClientSettings();
            }
            if (settings.RetryCount <= 0)
            {
                return builder;
            }

            var retryCount = settings.RetryCount;
            var retrySecs = (settings.RetryBaseSeconds > 0) ? settings.RetryBaseSeconds : WebApiClientSettings.DefaultRetryBaseSeconds;
            var policyBuilder = HttpPolicyExtensions.HandleTransientHttpError();
            var retryPolicy = policyBuilder.WaitAndRetryAsync(retryCount,
                                                              retryAttempt => TimeSpan.FromSeconds(Math.Pow(retrySecs, retryAttempt)));
            builder.AddPolicyHandler(retryPolicy);

            return builder;
        }

        /// <summary>
        /// 設定 Request Timeout 原則
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        private static IHttpClientBuilder AddTimeoutPolicy(this IHttpClientBuilder builder, WebApiClientSettings settings)
        {
            if (settings == null)
            {
                settings = new WebApiClientSettings();
            }
            if (settings.TimeoutInSeconds <= 0)
            {
                return builder;
            }

            var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(settings.TimeoutInSeconds));
            builder.AddPolicyHandler(timeoutPolicy);
            return builder;
        }

        #endregion
    }
}
