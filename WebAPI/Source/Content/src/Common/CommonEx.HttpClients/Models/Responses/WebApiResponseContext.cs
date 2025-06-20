using CommonEx.HttpClients.Extensions;
using Newtonsoft.Json.Linq;
using System.Net;

namespace CommonEx.HttpClients.Models.Responses
{
    public class WebApiResponseContext
    {
        /// <summary>
        /// Http Response Message
        /// </summary>
        public HttpResponseMessage Response { get; protected set; }

        /// <summary>
        /// Http Status Code
        /// </summary>
        public HttpStatusCode StatusCode
        {
            get
            {
                return Response?.StatusCode ?? default;
            }
        }

        /// <summary>
        /// Gets or sets the reason phrase which typically is sent by servers together with the status code.
        /// </summary>
        public string ReasonPhrase
        {
            get
            {
                return Response?.ReasonPhrase ?? string.Empty;
            }
        }

        /// <summary>
        /// Gets a value that indicates if the HTTP response was successful.
        /// </summary>
        public bool IsSuccessStatusCode
        {
            get
            {
                return Response?.IsSuccessStatusCode ?? false;
            }
        }

        /// <summary>
        /// Http Response Body (Json Object)
        /// </summary>
        public JToken? Body
        {
            get
            {
                if (Response != null &&
                    Response.TryGetObjectContent(out JToken? body))
                {
                    return body;
                }
                return default(JToken);
            }
        }

        /// <summary>
        /// Http Response Body (Raw String)
        /// </summary>
        public string BodyRaw
        {
            get
            {
                return Response?.GetTextContent() ?? string.Empty;
            }
        }


        public WebApiResponseContext(HttpResponseMessage response)
        {
            Response = response;
        }


        /// <summary>
        /// 讀取 Stream
        /// </summary>
        /// <returns></returns>
        public Task<Stream> ReadAsStreamAsync()
        {
            return Response.Content.ReadAsStreamAsync();
        }

        /// <summary>
        /// 取得 Cookie
        /// </summary>
        /// <returns></returns>
        public List<Cookie> GetSetCookies()
        {
            return Response.GetSetCookies();
        }

        /// <summary>
        /// 取得 Headers
        /// </summary>
        /// <param name="headerNames"></param>
        /// <returns></returns>
        public Dictionary<string, object> GetManyHeaders(params string[] headerNames)
        {
            return Response.GetManyHeaders(headerNames.ToList());
        }
    }
}
