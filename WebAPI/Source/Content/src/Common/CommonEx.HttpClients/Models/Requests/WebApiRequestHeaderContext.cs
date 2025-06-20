using CommonEx.HttpClients.Constants;
using CommonEx.Utilities.Cryptography;
using CommonEx.Utilities.Cryptography.Extensions;
using System.Net.Http.Headers;
using System.Security;
using System.Text;

namespace CommonEx.HttpClients.Models.Requests
{
    public class WebApiRequestHeaderContext
    {
        /// <summary>
        /// Header
        /// </summary>
        public Dictionary<string, string> Headers { get; protected set; } = new Dictionary<string, string>();

        /// <summary>
        /// Content Type
        /// </summary>
        public string ContentType { get; protected set; } = string.Empty;

        /// <summary>
        /// Content Encoding
        /// </summary>
        public Encoding ContentEncoding { get; protected set; } = Encoding.UTF8;

        /// <summary>
        /// Authorization
        /// </summary>
        public SecureString AuthorizationValue { get; protected set; }

        /// <summary>
        /// Authorization Scheme
        /// </summary>
        public string AuthorizationScheme { get; protected set; }

        public WebApiRequestHeaderContext()
        {
            AuthorizationValue = string.Empty.ToSecureString();
        }

        public WebApiRequestHeaderContext(string contentType)
        {
            ContentType = contentType;
            AuthorizationValue = string.Empty.ToSecureString();
        }


        #region Builder Pattern

        /// <summary>
        /// Raw (JSON)
        /// </summary>
        /// <returns></returns>
        public static WebApiRequestHeaderContext JsonRaw()
        {
            return new WebApiRequestHeaderContext(ContentTypes.Json);
        }

        /// <summary>
        /// Raw (XML)
        /// </summary>
        /// <returns></returns>
        public static WebApiRequestHeaderContext XmlRaw()
        {
            return new WebApiRequestHeaderContext(ContentTypes.Xml);
        }

        /// <summary>
        /// Form Url Encoded
        /// </summary>
        /// <returns></returns>
        public static WebApiRequestHeaderContext FormUrlEncoded()
        {
            return new WebApiRequestHeaderContext(ContentTypes.FormUrlEncoded);
        }

        /// <summary>
        /// Multipart Form Data
        /// </summary>
        /// <returns></returns>
        public static WebApiRequestHeaderContext FormData()
        {
            return new WebApiRequestHeaderContext(ContentTypes.FormData);
        }

        /// <summary>
        /// 設定 Content Type
        /// </summary>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public WebApiRequestHeaderContext SetContentType(string contentType)
        {
            ContentType = contentType;
            return this;
        }

        /// <summary>
        /// 設定 Content Encoding
        /// </summary>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public WebApiRequestHeaderContext SetContentEncoding(Encoding encoding)
        {
            ContentEncoding = encoding;
            return this;
        }

        /// <summary>
        /// 新增 Http Request Header
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public WebApiRequestHeaderContext AddHeader(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return this;
            }

            if (Headers.ContainsKey(key))
            {
                Headers[key] = value;
            }
            else
            {
                Headers.Add(key, value);
            }
            return this;
        }
        /// <summary>
        /// 新增 Http Request Header
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        public WebApiRequestHeaderContext AddHeaders(Dictionary<string, string> headers)
        {
            foreach (var header in headers)
            {
                AddHeader(header.Key, header.Value);
            }
            return this;
        }


        /// <summary>
        /// 設定 Basic Authorization
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <returns></returns>
        public WebApiRequestHeaderContext SetBasicAuthorization(string clientId, string clientSecret)
        {
            return SetBasicAuthorization(clientId, clientSecret, Encoding.UTF8);
        }
        /// <summary>
        /// 設定 Basic Authorization
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public WebApiRequestHeaderContext SetBasicAuthorization(string clientId, string clientSecret, Encoding encoding)
        {
            if (string.IsNullOrEmpty(clientId))
            {
                throw new ArgumentException("Client ID cannot be null or empty.", nameof(clientId));
            }

            if (string.IsNullOrEmpty(clientSecret))
            {
                throw new ArgumentException("Client Secret cannot be null or empty.", nameof(clientSecret));
            }

            AuthorizationScheme = AuthorizationSchemes.Basic;
            AuthorizationValue = new Base64Convert(encoding).Encode($"{clientId}:{clientSecret}")
                                                            .ToSecureString();
            return this;
        }

        /// <summary>
        /// 設定 JWT Authorization
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public WebApiRequestHeaderContext SetJwtAuthenticator(string accessToken)
        {
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                AuthorizationScheme = AuthorizationSchemes.Bearer;

                var ignoreCase = StringComparison.InvariantCultureIgnoreCase;
                if (accessToken.StartsWith($"{AuthorizationSchemes.Bearer} ", ignoreCase))
                {
                    AuthorizationValue = accessToken.Replace(AuthorizationSchemes.Bearer, string.Empty)
                                                    .TrimStart()
                                                    .ToSecureString();
                }
                else
                {
                    AuthorizationValue = accessToken.TrimStart()
                                                    .ToSecureString();
                }
            }
            return this;
        }

        #endregion


        #region Get Header Value

        /// <summary>
        /// 取得 Content Type
        /// </summary>
        /// <param name="headerValue"></param>
        /// <returns></returns>
        public bool TryGetAcceptContentType(out MediaTypeWithQualityHeaderValue? headerValue)
        {
            if (!string.IsNullOrWhiteSpace(ContentType))
            {
                headerValue = new MediaTypeWithQualityHeaderValue(ContentType);
                return true;
            }

            headerValue = default(MediaTypeWithQualityHeaderValue);
            return false;
        }

        /// <summary>
        /// 取得 Authorization Header
        /// </summary>
        /// <param name="headerValue"></param>
        /// <returns></returns>
        public bool TryGetAuthentication(out AuthenticationHeaderValue? headerValue)
        {
            if (!string.IsNullOrWhiteSpace(AuthorizationScheme) &&
                !AuthorizationValue.IsNullOrEmpty())
            {
                headerValue = new AuthenticationHeaderValue(AuthorizationScheme, AuthorizationValue.ToPlainString());
                return true;
            }

            headerValue = default(AuthenticationHeaderValue);
            return false;
        }

        #endregion
    }
}
