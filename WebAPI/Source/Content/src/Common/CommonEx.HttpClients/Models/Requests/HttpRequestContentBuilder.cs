using CommonEx.HttpClients.Constants;
using CommonEx.HttpClients.Extensions;
using CommonEx.Utilities.TextUtilities;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace CommonEx.HttpClients.Models.Requests
{
    internal static class HttpRequestContentBuilder
    {
        /// <summary>
        /// 建立 Body
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="rawData"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        internal static HttpContent? CreateBodyContent(string contentType, string rawData, Encoding? encoding = null)
        {
            if (string.IsNullOrWhiteSpace(contentType) ||
                rawData == null)
            {
                return null;
            }
            var content = new StringContent(rawData, encoding ?? Encoding.UTF8, contentType);
            content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            return content;
        }

        /// <summary>
        /// 建立 Body (Raw)
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="rawData"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        internal static HttpContent? CreateRawContent<T>(string contentType, T rawData, Encoding? encoding = null)
        {
            if (string.IsNullOrWhiteSpace(contentType) ||
                rawData == null)
            {
                return null;
            }

            switch (contentType.ToLower())
            {
                case ContentTypes.Json:
                case ContentTypes.JsonText:
                    var json = JToken.FromObject(rawData).ToString();
                    return CreateBodyContent(contentType.ToLower(), json, encoding);

                case ContentTypes.Xml:
                case ContentTypes.XmlText:
                    var rootNodeName = typeof(T).Name;
                    return CreateXmlRawContent(contentType.ToLower(), rawData, rootNodeName, encoding);

                case ContentTypes.Plain:
                case ContentTypes.CsvText:
                    return CreateTextRawContent(contentType.ToLower(), rawData, encoding);

                default:
                    throw new NotSupportedException($"Unsupported content type: {contentType}");
            }
        }

        /// <summary>
        /// 建立 Body (Text Raw)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contentType"></param>
        /// <param name="rawData"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        internal static HttpContent? CreateTextRawContent<T>(string contentType, T rawData, Encoding? encoding = null)
        {
            if (string.IsNullOrWhiteSpace(contentType) ||
                rawData == null)
            {
                return null;
            }

            var json = JToken.FromObject(rawData);
            var content = new StringContent(json.ToString(), encoding ?? Encoding.UTF8, contentType);
            content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            return content;
        }

        /// <summary>
        /// 建立 Body (XML Raw)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contentType"></param>
        /// <param name="rawData"></param>
        /// <param name="rootNodeName"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        internal static HttpContent? CreateXmlRawContent<T>(string contentType, T rawData,
                                                           string? rootNodeName = null,
                                                           Encoding? encoding = null)
        {
            if (string.IsNullOrWhiteSpace(contentType) ||
                rawData == null)
            {
                return null;
            }

            if (string.IsNullOrEmpty(rootNodeName))
            {
                rootNodeName = typeof(T).Name;
            }

            var xml = XmlConverter.SerializeObject(rawData, rootNodeName);
            return CreateBodyContent(contentType.ToLower(), xml, encoding);
        }

        /// <summary>
        /// 建立 Body 內容 (Multipart Form Data / Form Url Encoded)
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        internal static HttpContent? CreateFormContent(string contentType, Dictionary<string, string> formData)
        {
            if (string.IsNullOrWhiteSpace(contentType) ||
                formData == null)
            {
                return null;
            }

            switch (contentType.ToLower())
            {
                case ContentTypes.FormData:
                    var multipartContent = new MultipartFormDataContent().AddManyStringContent(formData);
                    multipartContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                    return multipartContent;

                case ContentTypes.FormUrlEncoded:
                    var forms = formData.Select(d => d).ToList();
                    var formUrlEncodedContent = new FormUrlEncodedContent(forms);
                    formUrlEncodedContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                    return formUrlEncodedContent;
            }

            return null;
        }

        /// <summary>
        /// 建立 Body 內容 (Multipart Form Data)
        /// </summary>
        /// <param name="formData"></param>
        /// <param name="formFiles"></param>
        /// <returns></returns>
        internal static HttpContent? CreateFormContent(Dictionary<string, string> formData,
                                                      List<StreamPartContent> formFiles)
        {
            if ((formData == null || formData.Count() == 0) &&
                (formFiles == null || formFiles.Count() == 0))
            {
                return null;
            }

            var content = new MultipartFormDataContent().AddManyStringContent(formData)
                                                        .AddManyStreamContent(formFiles);
            content.Headers.ContentType = new MediaTypeHeaderValue(ContentTypes.FormData);
            return content;
        }

        /// <summary>
        /// 建立 Body 內容 (Multipart Form Data)
        /// </summary>
        /// <param name="formFiles"></param>
        /// <returns></returns>
        internal static HttpContent? CreateStreamContent(List<StreamPartContent> formFiles)
        {
            if (formFiles == null || formFiles.Count() == 0)
            {
                return null;
            }

            var content = new MultipartFormDataContent().AddManyStreamContent(formFiles);
            content.Headers.ContentType = new MediaTypeHeaderValue(ContentTypes.FormData);
            return content;
        }
    }
}
