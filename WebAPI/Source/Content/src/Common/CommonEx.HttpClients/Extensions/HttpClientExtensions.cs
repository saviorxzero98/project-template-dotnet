using CommonEx.HttpClients.Constants;
using CommonEx.HttpClients.Models.Requests;
using System.Net.Http.Headers;

namespace CommonEx.HttpClients.Extensions
{
    public static class HttpClientExtensions
    {
        /// <summary>
        /// 設定 Http Request Header、Content Type 和 Authorization
        /// </summary>
        /// <param name="client"></param>
        /// <param name="context"></param>
        internal static void SetRequestHeaders(this HttpClient client, WebApiRequestHeaderContext context = null)
        {
            client.DefaultRequestHeaders.Clear();

            // Accept Content Type Header
            if (context.TryGetAcceptContentType(out MediaTypeWithQualityHeaderValue contentType))
            {
                client.DefaultRequestHeaders.Accept.Add(contentType);
            }

            // Authorization Header
            if (context.TryGetAuthentication(out AuthenticationHeaderValue authorization))
            {
                client.DefaultRequestHeaders.Authorization = authorization;
            }

            // Other Headers
            if (context != null)
            {
                var ignoreCase = StringComparison.InvariantCultureIgnoreCase;
                var headerNames = context.Headers
                                         .Keys
                                         .Where(key => !key.Equals(HttpHeaderNames.ContentType, ignoreCase) &&
                                                       !key.Equals(HttpHeaderNames.Authorization, ignoreCase));

                foreach (var headerName in headerNames)
                {
                    client.AddHeader(headerName, context.Headers[headerName]);
                }
            }
        }

        /// <summary>
        /// 新增 Http Request Header
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        internal static void AddHeader(this HttpClient client, string name, string value)
        {
            if (CheckHttpHeader(name, value))
            {
                client.DefaultRequestHeaders.Add(name, value);
            }
        }

        /// <summary>
        /// 新增 Http Request Header
        /// </summary>
        /// <param name="client"></param>
        /// <param name="headers"></param>
        internal static void AddHeaders(this HttpClient client, Dictionary<string, string>? headers = null)
        {
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    client.AddHeader(header.Key, header.Value);
                }
            }
        }

        /// <summary>
        /// 檢查 Request Header
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool CheckHttpHeader(string name, string value)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value))
            {
                return false;
            }
            return true;
        }
    }
}
