using CommonEx.HttpClients.Constants;
using CommonEx.Utilities.TextUtilities;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace CommonEx.HttpClients.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        #region Text Content

        /// <summary>
        /// Get Response Text Content 
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static string GetTextContent(this HttpResponseMessage response)
        {
            return response.GetTextContentAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Get Response Text Content 
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static async Task<string> GetTextContentAsync(this HttpResponseMessage response)
        {
            List<string> contentEncoding = response.Content.Headers.ContentEncoding.ToList();

            // 檢查是否為壓縮格式
            bool isCompression = contentEncoding.Any();
            if (isCompression)
            {
                // 解壓縮訊息
                Encoding encoding = response.GetContentEncoding();
                Stream stream = await response.GetStreamContentAsync();
                Stream? compressionStream = GetCompressionStream(stream, contentEncoding);

                if (compressionStream != null)
                {
                    using (StreamReader reader = new StreamReader(compressionStream, encoding))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            return await response.Content.ReadAsStringAsync();
        }

        #endregion


        #region Object Content

        /// <summary>
        /// Get Response Content (JToken)
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static JToken? GetJTokenContent(this HttpResponseMessage response)
        {
            Encoding encoding = response.GetContentEncoding();
            string contentType = response.GetContentType();
            string contentText = response.GetTextContent();

            if (!string.IsNullOrEmpty(contentText))
            {
                return ParseJTokenContent(contentType, contentText, encoding);
            }
            return default(JToken);
        }

        /// <summary>
        /// Get Response Content (JToken)
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static async Task<JToken?> GetJTokenContentAsync(this HttpResponseMessage response)
        {
            Encoding encoding = response.GetContentEncoding();
            string contentType = response.GetContentType();
            string contentText = await response.GetTextContentAsync();

            if (!string.IsNullOrEmpty(contentText))
            {
                return ParseJTokenContent(contentType, contentText, encoding);
            }
            return default(JToken);
        }

        /// <summary>
        /// Try Get Response Content (JToken)
        /// </summary>
        /// <param name="response"></param>
        /// <param name="contentModel"></param>
        /// <returns></returns>
        public static bool TryGetJsonContentModel(this HttpResponseMessage response, out JToken? contentModel)
        {
            try
            {
                contentModel = GetJTokenContent(response);
                return true;
            }
            catch
            {
                contentModel = default(JToken);
                return false;
            }
        }

        /// <summary>
        /// Get Response Content (Object)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        public static T? GetObjectContent<T>(this HttpResponseMessage response)
        {
            Encoding encoding = response.GetContentEncoding();
            string contentType = response.GetContentType();
            string contentText = response.GetTextContent();

            if (!string.IsNullOrWhiteSpace(contentText))
            {
                return ParseObjectContent<T>(contentType, contentText, encoding);
            }
            return default(T);
        }

        /// <summary>
        /// Get Response Content (Object)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        public static async Task<T?> GetObjectContentAsync<T>(this HttpResponseMessage response)
        {
            Encoding encoding = response.GetContentEncoding();
            string contentType = response.GetContentType();
            string contentText = await response.GetTextContentAsync();

            if (!string.IsNullOrWhiteSpace(contentText))
            {
                return ParseObjectContent<T>(contentType, contentText, encoding);
            }

            return default(T);
        }

        /// <summary>
        /// Try Get Response Content (Object)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <param name="contentModel"></param>
        /// <returns></returns>
        public static bool TryGetObjectContent<T>(this HttpResponseMessage response, out T? contentModel)
        {
            try
            {
                contentModel = GetObjectContent<T>(response);
                return (contentModel != null);
            }
            catch
            {
                contentModel = default(T);
                return false;
            }
        }

        /// <summary>
        /// Http Content (JSON, XML or Plain Text) to JToken
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="contentText"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        private static JToken? ParseJTokenContent(string contentType, string contentText, Encoding encoding)
        {
            try
            {
                // 依據 Content Type 轉換成 Model
                switch (contentType.ToLower())
                {
                    case ContentTypes.Json:
                        return JToken.Parse(contentText);

                    case ContentTypes.Xml:
                        return XmlConverter.DeserializeObject(contentText, encoding);

                    default:
                        return new JValue(contentText);
                }
            }
            catch
            {
                return default(JToken);
            }
        }

        /// <summary>
        /// Http Content (JSON, XML or Plain Text) to Object
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="contentText"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        private static T? ParseObjectContent<T>(string contentType, string contentText, Encoding encoding)
        {
            try
            {
                // 依據 Content Type 轉換成 Model
                switch (contentType.ToLower())
                {
                    case ContentTypes.Json:
                    case ContentTypes.JsonText:
                        return JsonConvert.DeserializeObject<T>(contentText);

                    case ContentTypes.Xml:
                    case ContentTypes.XmlText:
                        return XmlConverter.DeserializeObject<T>(contentText, encoding);

                    case ContentTypes.CsvText:
                        return CsvConverter.DeserializeObject<T>(contentText, encoding);
                }
            }
            catch
            {
            }
            return default(T);
        }

        #endregion


        #region Stream Content

        /// <summary>
        /// Get Response Stream Content 
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static Stream GetStreamContent(this HttpResponseMessage response)
        {
            return response.Content.ReadAsStreamAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Get Response Stream Content 
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static Task<Stream> GetStreamContentAsync(this HttpResponseMessage response)
        {
            return response.Content.ReadAsStreamAsync();
        }

        /// <summary>
        /// Get Compression Stream
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="contentEncoding"></param>
        /// <returns></returns>
        internal static Stream? GetCompressionStream(Stream stream, List<string> contentEncoding)
        {
            var ignoreCase = StringComparison.InvariantCultureIgnoreCase;

            if (contentEncoding.Any(e => e.Equals(ContentEncodingTypes.GZip, ignoreCase)))
            {
                return new GZipStream(stream, CompressionMode.Decompress);
            }
            else if (contentEncoding.Any(e => e.Equals(ContentEncodingTypes.Deflate, ignoreCase)))
            {
                return new DeflateStream(stream, CompressionMode.Decompress);
            }
            else if (contentEncoding.Any(e => e.Equals(ContentEncodingTypes.Brotli, ignoreCase)))
            {
                return new BrotliStream(stream, CompressionMode.Decompress);
            }
            return null;
        }

        #endregion


        #region Headers

        /// <summary>
        /// Get Content Type
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static string GetContentType(this HttpResponseMessage response)
        {

            bool hasMediaType = (response.Content != null &&
                                 response.Content.Headers != null &&
                                 response.Content.Headers.ContentType != null &&
                                 !string.IsNullOrEmpty(response.Content.Headers.ContentType.MediaType));

            if (hasMediaType)
            {
                return response.Content.Headers.ContentType.MediaType;
            }
            return string.Empty;
        }

        /// <summary>
        /// Get Content Encoding
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static Encoding GetContentEncoding(this HttpResponseMessage response)
        {
            string contentCharSet = response.GetContentCharSet();

            if (!string.IsNullOrEmpty(contentCharSet))
            {
                try
                {
                    Encoding encoding = Encoding.GetEncoding(contentCharSet);
                    return encoding;
                }
                catch
                {

                }
            }
            return Encoding.Default;
        }

        /// <summary>
        /// Get Content Chat Set
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static string GetContentCharSet(this HttpResponseMessage response)
        {
            bool hasCharSet = (response.Content != null &&
                               response.Content.Headers != null &&
                               response.Content.Headers.ContentType != null &&
                               !string.IsNullOrEmpty(response.Content.Headers.ContentType.CharSet));

            if (hasCharSet)
            {
                return response.Content.Headers.ContentType.CharSet;
            }
            return string.Empty;
        }

        /// <summary>
        /// Get Set Cookie Values
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static List<Cookie> GetSetCookies(this HttpResponseMessage response)
        {
            if (response.Headers.TryGetValues(HttpHeaderNames.SetCookie, out IEnumerable<string>? values) &&
                SetCookieHeaderValue.TryParseList(values.ToList(), out IList<SetCookieHeaderValue> setCookies))
            {
                var cookies = setCookies.Select(c => c.ToCookie())
                                        .ToList();
                return cookies;
            }
            return new List<Cookie>();
        }

        /// <summary>
        /// Get Many Headers
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, object> GetManyHeaders(this HttpResponseMessage response, List<string> headerNames)
        {
            var headers = new Dictionary<string, object>();

            if (headerNames != null && headerNames.Any())
            {
                foreach (var headerName in headerNames)
                {
                    if (response.Headers.TryGetValues(headerName, out IEnumerable<string>? values))
                    {
                        var headerValues = values.ToList();
                        if (headerValues.Count == 0)
                        {
                            headers.Add(headerName, string.Empty);
                        }
                        else if (headerValues.Count == 1)
                        {
                            headers.Add(headerName, headerValues.FirstOrDefault() ?? string.Empty);
                        }
                        else
                        {
                            headers.Add(headerName, headerValues);
                        }
                    }
                }
            }

            return headers;
        }

        /// <summary>
        /// Get All Headers
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, object> GetAllHeaders(this HttpResponseMessage response)
        {
            var headers = new Dictionary<string, object>();

            foreach (var responseHeader in response.Headers)
            {
                var headerName = responseHeader.Key;
                var headerValues = responseHeader.Value.ToList();

                if (headerValues.Count == 0)
                {
                    headers.Add(headerName, string.Empty);
                }
                else if (headerValues.Count == 1)
                {
                    headers.Add(headerName, headerValues.FirstOrDefault() ?? string.Empty);
                }
                else
                {
                    headers.Add(headerName, headerValues);
                }
            }
            return headers;
        }

        #endregion
    }
}
