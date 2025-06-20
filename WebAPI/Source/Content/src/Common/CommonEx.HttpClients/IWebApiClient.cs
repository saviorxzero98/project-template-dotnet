using CommonEx.HttpClients.Models.Requests;
using CommonEx.HttpClients.Models.Responses;
using Flurl;

namespace CommonEx.HttpClients
{
    public interface IWebApiClient
    {
        /// <summary>
        /// 建立 Http Client
        /// </summary>
        /// <returns></returns>
        HttpClient CreateClient();


        #region Http Get

        /// <summary>
        /// Send Http Get Request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        WebApiResponseContext Get(string url, WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Http Get Request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        WebApiResponseContext Get(Url url, WebApiRequestHeaderContext? headers = null);

        /// <summary>
        /// Send Http Get Request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> GetAsync(string url, WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Http Get Request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> GetAsync(Url url, WebApiRequestHeaderContext? headers = null);

        #endregion


        #region Http Post

        /// <summary>
        /// Send Http Post Request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PostAsync(string url, WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Http Post Request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PostAsync(Url url, WebApiRequestHeaderContext? headers = null);


        /// <summary>
        /// Send Http Post Request (Raw Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PostRawDataAsync(string url, string data, string contentType,
                                                     WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Http Post Request (Raw Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PostRawDataAsync(Url url, string data, string contentType,
                                                     WebApiRequestHeaderContext? headers = null);

        /// <summary>
        /// Send Http Post Request (Raw Data)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PostRawDataAsync<T>(string url, T data, string contentType,
                                                        WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Http Post Request (Raw Data)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PostRawDataAsync<T>(Url url, T data, string contentType,
                                                        WebApiRequestHeaderContext? headers = null);

        /// <summary>
        /// Send Http Post Request (Json Raw)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PostJsonAsync(string url, string data,
                                                  WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Http Post Request (Json Raw)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PostJsonAsync(Url url, string data,
                                                  WebApiRequestHeaderContext? headers = null);

        /// <summary>
        /// Send Http Post Request (Json Raw)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PostJsonAsync<T>(string url, T data,
                                                     WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Http Post Request (Json Raw)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PostJsonAsync<T>(Url url, T data,
                                                     WebApiRequestHeaderContext? headers = null);

        /// <summary>
        /// Send Http Post Request (Xml Raw)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PostXmlAsync(string url, string data,
                                                 WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Http Post Request (Xml Raw)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PostXmlAsync(Url url, string data,
                                                 WebApiRequestHeaderContext? headers = null);

        /// <summary>
        /// Send Http Post Request (Xml Raw)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="rootNodeName"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PostXmlAsync<T>(string url, T data, string? rootNodeName = null,
                                                    WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Http Post Request (Xml Raw)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="rootNodeName"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PostXmlAsync<T>(Url url, T data, string? rootNodeName = null,
                                                    WebApiRequestHeaderContext? headers = null);

        /// <summary>
        /// Send Http Post Request (Form Url Encoded)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="form"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PostFormUrlEncodedAsync(string url, Dictionary<string, string> form,
                                                            WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Http Post Request (Form Url Encoded)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="form"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PostFormUrlEncodedAsync(Url url, Dictionary<string, string> form,
                                                            WebApiRequestHeaderContext? headers = null);

        /// <summary>
        /// Send Http Post Request (Multipart Form Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formData"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PostFormDataAsync(string url,
                                                      Dictionary<string, string> formData,
                                                      WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Http Post Request (Multipart Form Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formData"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PostFormDataAsync(Url url,
                                                      Dictionary<string, string> formData,
                                                      WebApiRequestHeaderContext? headers = null);

        /// <summary>
        /// Send Http Post Request (Multipart Form Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formData"></param>
        /// <param name="formFiles"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PostFormDataAsync(string url, Dictionary<string, string> formData,
                                                      List<StreamPartContent> formFiles,
                                                      WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Http Post Request (Multipart Form Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formData"></param>
        /// <param name="formFiles"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PostFormDataAsync(Url url, Dictionary<string, string> formData,
                                                      List<StreamPartContent> formFiles,
                                                      WebApiRequestHeaderContext? headers = null);

        /// <summary>
        /// Send Http Post Request (Multipart Form Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formFiles"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PostFormDataAsync(string url, List<StreamPartContent> formFiles,
                                                      WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Http Post Request (Multipart Form Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formFiles"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PostFormDataAsync(Url url, List<StreamPartContent> formFiles,
                                                      WebApiRequestHeaderContext? headers = null);

        #endregion


        #region Http Put

        /// <summary>
        /// Send Http Put Request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PutAsync(string url, WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Http Put Request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PutAsync(Url url, WebApiRequestHeaderContext? headers = null);


        /// <summary>
        /// Send Http Put Request (Raw Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PutRawDataAsync(string url, string data,
                                                    string contentType,
                                                    WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Http Put Request (Raw Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PutRawDataAsync(Url url, string data,
                                                    string contentType,
                                                    WebApiRequestHeaderContext? headers = null);

        /// <summary>
        /// Send Http Put Request (Raw Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PutRawDataAsync<T>(string url, T data, string contentType,
                                                       WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Http Put Request (Raw Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PutRawDataAsync<T>(Url url, T data, string contentType,
                                                       WebApiRequestHeaderContext? headers = null);

        /// <summary>
        /// Send Http Put Request (JSON Raw)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PutJsonAsync(string url, string data,
                                                 WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Http Put Request (JSON Raw)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PutJsonAsync(Url url, string data,
                                                 WebApiRequestHeaderContext? headers = null);

        /// <summary>
        /// Send Http Put Request (JSON Raw)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PutJsonAsync<T>(string url, T data,
                                                    WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Http Put Request (JSON Raw)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PutJsonAsync<T>(Url url, T data,
                                                    WebApiRequestHeaderContext? headers = null);

        /// <summary>
        /// Send Http Put Request (Xml Raw)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PutXmlAsync(string url, string data,
                                                WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Http Put Request (Xml Raw)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PutXmlAsync(Url url, string data,
                                                WebApiRequestHeaderContext? headers = null);

        /// <summary>
        /// Send Http Put Request (Xml Raw)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="rootNodeName"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PutXmlAsync<T>(string url, T data, string? rootNodeName = null,
                                                   WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Http Put Request (Xml Raw)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="rootNodeName"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PutXmlAsync<T>(Url url, T data, string? rootNodeName = null,
                                                   WebApiRequestHeaderContext? headers = null);

        /// <summary>
        /// Send Http Put Request (Form Url Encoded)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="form"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PutFormUrlEncodedAsync(string url, Dictionary<string, string> form,
                                                           WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Http Put Request (Form Url Encoded)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="form"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PutFormUrlEncodedAsync(Url url, Dictionary<string, string> form,
                                                           WebApiRequestHeaderContext? headers = null);

        /// <summary>
        /// Send Http Put Request (Multipart Form Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="form"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PutFormDataAsync(string url, Dictionary<string, string> form,
                                                     string contentType,
                                                     WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Http Put Request (Multipart Form Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="form"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PutFormDataAsync(Url url, Dictionary<string, string> form,
                                                     string contentType,
                                                     WebApiRequestHeaderContext? headers = null);

        /// <summary>
        /// Send Http Put Request (Multipart Form Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formData"></param>
        /// <param name="formFiles"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PutFormDataAsync(string url, Dictionary<string, string> formData,
                                                     List<StreamPartContent> formFiles,
                                                     WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Http Put Request (Multipart Form Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formData"></param>
        /// <param name="formFiles"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PutFormDataAsync(Url url, Dictionary<string, string> formData,
                                                     List<StreamPartContent> formFiles,
                                                     WebApiRequestHeaderContext? headers = null);

        /// <summary>
        /// Send Http Put Request (Multipart Form Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formFiles"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PutFormDataAsync(string url, List<StreamPartContent> formFiles,
                                                     WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Http Put Request (Multipart Form Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formFiles"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PutFormDataAsync(Url url, List<StreamPartContent> formFiles,
                                                     WebApiRequestHeaderContext? headers = null);

        #endregion


        #region Http Patch

        /// <summary>
        /// Send Http Patch Request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PatchAsync(string url, WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Http Patch Request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PatchAsync(Url url, WebApiRequestHeaderContext? headers = null);

        /// <summary>
        /// Send Http Patch Request (Raw Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PatchRawDataAsync(string url, string data, string contentType,
                                                      WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Http Patch Request (Raw Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PatchRawDataAsync(Url url, string data, string contentType,
                                                      WebApiRequestHeaderContext? headers = null);

        /// <summary>
        /// Send Http Patch Request (Raw Data)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PatchRawDataAsync<T>(string url, T data, string contentType,
                                                         WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Http Patch Request (Raw Data)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PatchRawDataAsync<T>(Url url, T data, string contentType,
                                                         WebApiRequestHeaderContext? headers = null);

        /// <summary>
        /// Send Http Patch Request (Json Raw)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PatchJsonAsync(string url, string data,
                                                   WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Http Patch Request (Json Raw)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PatchJsonAsync(Url url, string data,
                                                   WebApiRequestHeaderContext? headers = null);


        /// <summary>
        /// Send Http Patch Request (Json Raw)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PatchJsonAsync<T>(string url, T data,
                                                      WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Http Patch Request (Json Raw)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PatchJsonAsync<T>(Url url, T data,
                                                      WebApiRequestHeaderContext? headers = null);

        /// <summary>
        /// Send Http Patch Request (Xml Raw)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PatchXmlAsync(string url, string data,
                                                  WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Http Patch Request (Xml Raw)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PatchXmlAsync(Url url, string data,
                                                  WebApiRequestHeaderContext? headers = null);

        /// <summary>
        /// Send Http Patch Request (Xml Raw)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="rootNodeName"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PatchXmlAsync<T>(string url, T data, string? rootNodeName = null,
                                                     WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Http Patch Request (Xml Raw)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="rootNodeName"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PatchXmlAsync<T>(Url url, T data, string? rootNodeName = null,
                                                     WebApiRequestHeaderContext? headers = null);

        /// <summary>
        /// Send Http Patch Request (Form Url Encoded)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="form"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PatchFormUrlEncodedAsync(string url, Dictionary<string, string> form,
                                                             WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Http Patch Request (Form Url Encoded)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="form"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PatchFormUrlEncodedAsync(Url url, Dictionary<string, string> form,
                                                             WebApiRequestHeaderContext? headers = null);

        /// <summary>
        /// Send Http Patch Request (Multipart Form Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formData"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PatchFormDataAsync(string url, Dictionary<string, string> formData,
                                                       string contentType,
                                                       WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Http Patch Request (Multipart Form Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formData"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> PatchFormDataAsync(Url url, Dictionary<string, string> formData,
                                                       string contentType,
                                                       WebApiRequestHeaderContext? headers = null);

        #endregion


        #region Http Delete

        /// <summary>
        /// Send Http Delete Request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> DeleteAsync(string url, WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Http Delete Request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> DeleteAsync(Url url, WebApiRequestHeaderContext? headers = null);

        #endregion


        #region Send Request

        /// <summary>
        /// Send Request (Async)
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> SendAsync(HttpMethod method, string url,
                                              WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Request (Async)
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> SendAsync(HttpMethod method, Url url,
                                              WebApiRequestHeaderContext? headers = null);

        /// <summary>
        /// Send Request
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> SendAsync(HttpMethod method, string url, string data, string contentType,
                                              WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Request
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> SendAsync(HttpMethod method, Url url, string data, string contentType,
                                              WebApiRequestHeaderContext? headers = null);

        /// <summary>
        /// Send Raw Data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> SendRawDataAsync<T>(HttpMethod method, string url, T data,
                                                        string contentType,
                                                        WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Raw Data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> SendRawDataAsync<T>(HttpMethod method, Url url, T data,
                                                        string contentType,
                                                        WebApiRequestHeaderContext? headers = null);

        /// <summary>
        ///  Send Raw Data (XML)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="rootNodeName"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> SendXmlRawAsync<T>(HttpMethod method, string url, T data,
                                                       string contentType, string? rootNodeName = null,
                                                       WebApiRequestHeaderContext? headers = null);
        /// <summary>
        ///  Send Raw Data (XML)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="rootNodeName"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> SendXmlRawAsync<T>(HttpMethod method, Url url, T data,
                                                       string contentType, string? rootNodeName = null,
                                                       WebApiRequestHeaderContext? headers = null);

        /// <summary>
        /// Send Http Request (Form Url Encoded)
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> SendFormUrlEncodedAsync(HttpMethod method, string url,
                                                            Dictionary<string, string> data,
                                                            WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Http Request (Form Url Encoded)
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> SendFormUrlEncodedAsync(HttpMethod method, Url url,
                                                            Dictionary<string, string> data,
                                                            WebApiRequestHeaderContext? headers = null);

        /// <summary>
        /// Send Http Request (Multipart Form Data)
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> SendFormDataAsync(HttpMethod method, string url,
                                                      Dictionary<string, string> data,
                                                      WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Http Request (Multipart Form Data)
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> SendFormDataAsync(HttpMethod method, Url url,
                                                      Dictionary<string, string> data,
                                                      WebApiRequestHeaderContext? headers = null);

        /// <summary>
        /// Send Http Request (Multipart Form Data)
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="formData"></param>
        /// <param name="formFiles"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> SendFormDataAsync(HttpMethod method, string url,
                                                      Dictionary<string, string> formData,
                                                      List<StreamPartContent> formFiles,
                                                      WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Http Request (Multipart Form Data)
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="formData"></param>
        /// <param name="formFiles"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> SendFormDataAsync(HttpMethod method, Url url,
                                                      Dictionary<string, string> formData,
                                                      List<StreamPartContent> formFiles,
                                                      WebApiRequestHeaderContext? headers = null);

        /// <summary>
        /// Send Http Request (Multipart Form Data)
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="formFiles"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> SendFormDataAsync(HttpMethod method, string url,
                                                      List<StreamPartContent> formFiles,
                                                      WebApiRequestHeaderContext? headers = null);
        /// <summary>
        /// Send Http Request (Multipart Form Data)
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="formFiles"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<WebApiResponseContext> SendFormDataAsync(HttpMethod method, Url url,
                                                      List<StreamPartContent> formFiles,
                                                      WebApiRequestHeaderContext? headers = null);

        #endregion
    }
}
