using CommonEx.HttpClients.Constants;
using CommonEx.HttpClients.Exceptions;
using CommonEx.HttpClients.Extensions;
using CommonEx.HttpClients.Models.Requests;
using CommonEx.HttpClients.Models.Responses;
using Flurl;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace CommonEx.HttpClients
{
    public class WebApiClient : IWebApiClient
    {
        public const string HttpClientName = nameof(WebApiClient);

        /// <summary>
        /// Logger
        /// </summary>
        protected readonly ILogger Logger;

        /// <summary>
        /// Http Client Factory
        /// </summary>
        protected readonly IHttpClientFactory HttpClientFactory;

        public WebApiClient(IHttpClientFactory httpClientFactory, ILoggerFactory loggerFactory)
        {
            HttpClientFactory = httpClientFactory;
            Logger = CreateLogger<WebApiClient>(loggerFactory);
        }


        /// <summary>
        /// Create Logger
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <returns></returns>
        protected virtual ILogger CreateLogger<T>(ILoggerFactory? loggerFactory = null) where T : IWebApiClient
        {
            if (loggerFactory == null)
            {
                return NullLoggerFactory.Instance.CreateLogger<T>();
            }
            else
            {
                return loggerFactory.CreateLogger<T>();
            }
        }

        /// <summary>
        /// 取得 Http Client
        /// </summary>
        /// <returns></returns>
        public virtual HttpClient CreateClient()
        {
            return HttpClientFactory.CreateClient(HttpClientName);
        }

        #region Http Get

        /// <summary>
        /// Send Http Get Request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual WebApiResponseContext Get(
            string url, WebApiRequestHeaderContext? headers = null)
        {
            return GetAsync(url, headers).GetAwaiter().GetResult();
        }
        /// <summary>
        /// Send Http Get Request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual WebApiResponseContext Get(
            Url url, WebApiRequestHeaderContext? headers = null)
        {
            return Get(url.ToUrlString(), headers);
        }


        /// <summary>
        /// Send Http Get Request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> GetAsync(
            string url, WebApiRequestHeaderContext? headers = null)
        {
            return SendAsync(HttpMethod.Get, url, headers);
        }
        /// <summary>
        /// Send Http Get Request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> GetAsync(
            Url url, WebApiRequestHeaderContext? headers = null)
        {
            return GetAsync(url.ToUrlString(), headers);
        }

        #endregion


        #region Http Post

        /// <summary>
        /// Send Http Post Request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PostAsync(
            string url, WebApiRequestHeaderContext? headers = null)
        {
            return SendAsync(HttpMethod.Post, url, headers);
        }
        /// <summary>
        /// Send Http Post Request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PostAsync(
            Url url, WebApiRequestHeaderContext? headers = null)
        {
            return PostAsync(url.ToUrlString(), headers);
        }


        /// <summary>
        /// Send Http Post Request (Raw Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PostRawDataAsync(
            string url, string data, string contentType,
            WebApiRequestHeaderContext? headers = null)
        {
            return SendAsync(HttpMethod.Post, url, data, contentType, headers);
        }
        /// <summary>
        /// Send Http Post Request (Raw Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PostRawDataAsync(
            Url url, string data, string contentType,
            WebApiRequestHeaderContext? headers = null)
        {
            return PostRawDataAsync(url.ToUrlString(), data, contentType, headers);
        }


        /// <summary>
        /// Send Http Post Request (Raw Data)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PostRawDataAsync<T>(
            string url, T data, string contentType,
            WebApiRequestHeaderContext? headers = null)
        {
            return SendRawDataAsync(HttpMethod.Post, url, data, contentType, headers);
        }
        /// <summary>
        /// Send Http Post Request (Raw Data)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PostRawDataAsync<T>(
            Url url, T data, string contentType,
            WebApiRequestHeaderContext? headers = null)
        {
            return PostRawDataAsync(url.ToUrlString(), data, contentType, headers);
        }


        /// <summary>
        /// Send Http Post Request (Json Raw)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PostJsonAsync(
            string url, string data,
            WebApiRequestHeaderContext? headers = null)
        {
            return SendAsync(HttpMethod.Post, url, data, ContentTypes.Json, headers);
        }
        /// <summary>
        /// Send Http Post Request (Json Raw)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PostJsonAsync(
            Url url, string data,
            WebApiRequestHeaderContext? headers = null)
        {
            return PostJsonAsync(url.ToUrlString(), data, headers);
        }


        /// <summary>
        /// Send Http Post Request (Json Raw)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PostJsonAsync<T>(
            string url, T data,
            WebApiRequestHeaderContext? headers = null)
        {
            return SendRawDataAsync(HttpMethod.Post, url, data, ContentTypes.Json, headers);
        }
        /// <summary>
        /// Send Http Post Request (Json Raw)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PostJsonAsync<T>(
            Url url, T data,
            WebApiRequestHeaderContext? headers = null)
        {
            return PostJsonAsync<T>(url.ToUrlString(), data, headers);
        }


        /// <summary>
        /// Send Http Post Request (Xml Raw)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PostXmlAsync(
            string url, string data,
            WebApiRequestHeaderContext? headers = null)
        {
            return SendAsync(HttpMethod.Post, url, data, ContentTypes.Xml, headers);
        }
        /// <summary>
        /// Send Http Post Request (Xml Raw)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PostXmlAsync(
            Url url, string data,
            WebApiRequestHeaderContext? headers = null)
        {
            return PostXmlAsync(url.ToUrlString(), data, headers);
        }


        /// <summary>
        /// Send Http Post Request (Xml Raw)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="rootNodeName"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PostXmlAsync<T>(
            string url, T data, string? rootNodeName = null,
            WebApiRequestHeaderContext? headers = null)
        {
            return SendXmlRawAsync(HttpMethod.Post, url, data, ContentTypes.Xml, rootNodeName, headers);
        }
        /// <summary>
        /// Send Http Post Request (Xml Raw)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="rootNodeName"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PostXmlAsync<T>(
            Url url, T data, string? rootNodeName = null,
            WebApiRequestHeaderContext? headers = null)
        {
            return PostXmlAsync(url.ToUrlString(), data, rootNodeName, headers);
        }


        /// <summary>
        /// Send Http Post Request (Form Url Encoded)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="form"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PostFormUrlEncodedAsync(
            string url, Dictionary<string, string> form,
            WebApiRequestHeaderContext? headers = null)
        {
            return SendFormUrlEncodedAsync(HttpMethod.Post, url, form, headers);
        }
        /// <summary>
        /// Send Http Post Request (Form Url Encoded)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="form"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PostFormUrlEncodedAsync(
            Url url, Dictionary<string, string> form,
            WebApiRequestHeaderContext? headers = null)
        {
            return PostFormUrlEncodedAsync(url.ToUrlString(), form, headers);
        }


        /// <summary>
        /// Send Http Post Request (Multipart Form Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formData"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PostFormDataAsync(
            string url, Dictionary<string, string> formData,
            WebApiRequestHeaderContext? headers = null)
        {
            return SendFormDataAsync(HttpMethod.Post, url, formData, headers);
        }
        /// <summary>
        /// Send Http Post Request (Multipart Form Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formData"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PostFormDataAsync(
            Url url, Dictionary<string, string> formData,
            WebApiRequestHeaderContext? headers = null)
        {
            return PostFormDataAsync(url.ToUrlString(), formData, headers);
        }


        /// <summary>
        /// Send Http Post Request (Multipart Form Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formData"></param>
        /// <param name="formFiles"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PostFormDataAsync(
            string url, Dictionary<string, string> formData,
            List<StreamPartContent> formFiles,
            WebApiRequestHeaderContext? headers = null)
        {
            return SendFormDataAsync(HttpMethod.Post, url, formData, formFiles, headers);
        }
        /// <summary>
        /// Send Http Post Request (Multipart Form Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formData"></param>
        /// <param name="formFiles"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PostFormDataAsync(
            Url url, Dictionary<string, string> formData,
            List<StreamPartContent> formFiles,
            WebApiRequestHeaderContext? headers = null)
        {
            return PostFormDataAsync(url.ToUrlString(), formData, formFiles, headers);
        }


        /// <summary>
        /// Send Http Post Request (Multipart Form Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formFiles"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PostFormDataAsync(
            string url, List<StreamPartContent> formFiles,
            WebApiRequestHeaderContext? headers = null)
        {
            return SendFormDataAsync(HttpMethod.Post, url, formFiles, headers);
        }
        /// <summary>
        /// Send Http Post Request (Multipart Form Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formFiles"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PostFormDataAsync(
            Url url, List<StreamPartContent> formFiles,
            WebApiRequestHeaderContext? headers = null)
        {
            return PostFormDataAsync(url.ToUrlString(), formFiles, headers);
        }

        #endregion


        #region Http Put

        /// <summary>
        /// Send Http Put Request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PutAsync(
            string url, WebApiRequestHeaderContext? headers = null)
        {
            return SendAsync(HttpMethod.Put, url, headers);
        }
        /// <summary>
        /// Send Http Put Request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PutAsync(
            Url url, WebApiRequestHeaderContext? headers = null)
        {
            return PutAsync(url.ToUrlString(), headers);
        }


        /// <summary>
        /// Send Http Put Request (Raw Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PutRawDataAsync(
            string url, string data, string contentType,
            WebApiRequestHeaderContext? headers = null)
        {
            return SendAsync(HttpMethod.Put, url, data, contentType, headers);
        }
        /// <summary>
        /// Send Http Put Request (Raw Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PutRawDataAsync(
            Url url, string data, string contentType,
            WebApiRequestHeaderContext? headers = null)
        {
            return PutRawDataAsync(url.ToUrlString(), data, contentType, headers);
        }


        /// <summary>
        /// Send Http Put Request (Raw Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PutRawDataAsync<T>(
            string url, T data, string contentType,
            WebApiRequestHeaderContext? headers = null)
        {
            return SendRawDataAsync(HttpMethod.Put, url, data, contentType, headers);
        }
        /// <summary>
        /// Send Http Put Request (Raw Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PutRawDataAsync<T>(
            Url url, T data, string contentType,
            WebApiRequestHeaderContext? headers = null)
        {
            return PutRawDataAsync(url.ToUrlString(), data, contentType, headers);
        }


        /// <summary>
        /// Send Http Put Request (JSON Raw)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PutJsonAsync(
            string url, string data,
            WebApiRequestHeaderContext? headers = null)
        {
            return SendAsync(HttpMethod.Put, url, data, ContentTypes.Json, headers);
        }
        /// <summary>
        /// Send Http Put Request (JSON Raw)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PutJsonAsync(
            Url url, string data,
            WebApiRequestHeaderContext? headers = null)
        {
            return PutJsonAsync(url.ToUrlString(), data, headers);
        }


        /// <summary>
        /// Send Http Put Request (JSON Raw)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PutJsonAsync<T>(
            string url, T data,
            WebApiRequestHeaderContext? headers = null)
        {
            return SendRawDataAsync(HttpMethod.Put, url, data, ContentTypes.Json, headers);
        }
        /// <summary>
        /// Send Http Put Request (JSON Raw)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PutJsonAsync<T>(
            Url url, T data,
            WebApiRequestHeaderContext? headers = null)
        {
            return PutJsonAsync(url.ToUrlString(), data, headers);
        }


        /// <summary>
        /// Send Http Put Request (Xml Raw)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PutXmlAsync(
            string url, string data,
            WebApiRequestHeaderContext? headers = null)
        {
            return SendAsync(HttpMethod.Put, url, data, ContentTypes.Xml, headers);
        }
        /// <summary>
        /// Send Http Put Request (Xml Raw)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PutXmlAsync(
            Url url, string data,
            WebApiRequestHeaderContext? headers = null)
        {
            return PutXmlAsync(url.ToUrlString(), data, headers);
        }


        /// <summary>
        /// Send Http Put Request (Xml Raw)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="rootNodeName"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PutXmlAsync<T>(
            string url, T data, string? rootNodeName = null,
            WebApiRequestHeaderContext? headers = null)
        {
            return SendXmlRawAsync(HttpMethod.Put, url, data, ContentTypes.Xml, rootNodeName, headers);
        }
        /// <summary>
        /// Send Http Put Request (Xml Raw)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="rootNodeName"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PutXmlAsync<T>(
            Url url, T data, string? rootNodeName = null,
            WebApiRequestHeaderContext? headers = null)
        {
            return PutXmlAsync(url.ToUrlString(), data, rootNodeName, headers);
        }


        /// <summary>
        /// Send Http Put Request (Form Url Encoded)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="form"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PutFormUrlEncodedAsync(
            string url, Dictionary<string, string> form,
            WebApiRequestHeaderContext? headers = null)
        {
            return SendFormUrlEncodedAsync(HttpMethod.Put, url, form, headers);
        }
        /// <summary>
        /// Send Http Put Request (Form Url Encoded)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="form"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PutFormUrlEncodedAsync(
            Url url, Dictionary<string, string> form,
            WebApiRequestHeaderContext? headers = null)
        {
            return PutFormUrlEncodedAsync(url.ToUrlString(), form, headers);
        }


        /// <summary>
        /// Send Http Put Request (Multipart Form Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="form"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PutFormDataAsync(
            string url, Dictionary<string, string> form,
            string contentType, WebApiRequestHeaderContext? headers = null)
        {
            return SendFormDataAsync(HttpMethod.Put, url, form, headers);
        }
        /// <summary>
        /// Send Http Put Request (Multipart Form Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="form"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PutFormDataAsync(
            Url url, Dictionary<string, string> form,
            string contentType, WebApiRequestHeaderContext? headers = null)
        {
            return PutFormDataAsync(url.ToUrlString(), form, contentType, headers);
        }


        /// <summary>
        /// Send Http Put Request (Multipart Form Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formData"></param>
        /// <param name="formFiles"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PutFormDataAsync(
            string url, Dictionary<string, string> formData,
            List<StreamPartContent> formFiles,
            WebApiRequestHeaderContext? headers = null)
        {
            return SendFormDataAsync(HttpMethod.Put, url, formData, formFiles, headers);
        }
        /// <summary>
        /// Send Http Put Request (Multipart Form Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formData"></param>
        /// <param name="formFiles"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PutFormDataAsync(
            Url url, Dictionary<string, string> formData,
            List<StreamPartContent> formFiles,
            WebApiRequestHeaderContext? headers = null)
        {
            return PutFormDataAsync(url.ToUrlString(), formData, formFiles, headers);
        }


        /// <summary>
        /// Send Http Put Request (Multipart Form Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formFiles"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PutFormDataAsync(
            string url, List<StreamPartContent> formFiles,
            WebApiRequestHeaderContext? headers = null)
        {
            return SendFormDataAsync(HttpMethod.Put, url, formFiles, headers);
        }
        /// <summary>
        /// Send Http Put Request (Multipart Form Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formFiles"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PutFormDataAsync(
            Url url, List<StreamPartContent> formFiles,
            WebApiRequestHeaderContext? headers = null)
        {
            return PutFormDataAsync(url.ToUrlString(), formFiles, headers);
        }

        #endregion


        #region Http Patch

        /// <summary>
        /// Send Http Patch Request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PatchAsync(
            string url, WebApiRequestHeaderContext? headers = null)
        {
            return SendAsync(HttpExtendMethod.Patch, url, headers);
        }
        /// <summary>
        /// Send Http Patch Request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PatchAsync(
            Url url, WebApiRequestHeaderContext? headers = null)
        {
            return PatchAsync(url.ToUrlString(), headers);
        }

        /// <summary>
        /// Send Http Patch Request (Raw Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PatchRawDataAsync(
            string url, string data, string contentType,
            WebApiRequestHeaderContext? headers = null)
        {
            return SendAsync(HttpExtendMethod.Patch, url, data, contentType, headers);
        }
        /// <summary>
        /// Send Http Patch Request (Raw Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PatchRawDataAsync(
            Url url, string data, string contentType,
            WebApiRequestHeaderContext? headers = null)
        {
            return PatchRawDataAsync(url.ToUrlString(), data, contentType, headers);
        }


        /// <summary>
        /// Send Http Patch Request (Raw Data)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PatchRawDataAsync<T>(
            string url, T data, string contentType,
            WebApiRequestHeaderContext? headers = null)
        {
            return SendRawDataAsync(HttpExtendMethod.Patch, url, data, contentType, headers);
        }
        /// <summary>
        /// Send Http Patch Request (Raw Data)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PatchRawDataAsync<T>(
            Url url, T data, string contentType,
            WebApiRequestHeaderContext? headers = null)
        {
            return PatchRawDataAsync(url.ToUrlString(), data, contentType, headers);
        }


        /// <summary>
        /// Send Http Patch Request (Json Raw)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PatchJsonAsync(
            string url, string data,
            WebApiRequestHeaderContext? headers = null)
        {
            return SendAsync(HttpExtendMethod.Patch, url, data, ContentTypes.Json, headers);
        }
        /// <summary>
        /// Send Http Patch Request (Json Raw)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PatchJsonAsync(
            Url url, string data,
            WebApiRequestHeaderContext? headers = null)
        {
            return PatchJsonAsync(url.ToUrlString(), data, headers);
        }


        /// <summary>
        /// Send Http Patch Request (Json Raw)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PatchJsonAsync<T>(
            string url, T data,
            WebApiRequestHeaderContext? headers = null)
        {
            return SendRawDataAsync(HttpExtendMethod.Patch, url, data, ContentTypes.Json, headers);
        }
        /// <summary>
        /// Send Http Patch Request (Json Raw)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PatchJsonAsync<T>(
            Url url, T data,
            WebApiRequestHeaderContext? headers = null)
        {
            return PatchJsonAsync(url.ToUrlString(), data, headers);
        }


        /// <summary>
        /// Send Http Patch Request (Xml Raw)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PatchXmlAsync(
            string url, string data,
            WebApiRequestHeaderContext? headers = null)
        {
            return SendAsync(HttpExtendMethod.Patch, url, data, ContentTypes.Xml, headers);
        }
        /// <summary>
        /// Send Http Patch Request (Xml Raw)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PatchXmlAsync(
            Url url, string data,
            WebApiRequestHeaderContext? headers = null)
        {
            return PatchXmlAsync(url.ToUrlString(), data, headers);
        }


        /// <summary>
        /// Send Http Patch Request (Xml Raw)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="rootNodeName"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PatchXmlAsync<T>(
            string url, T data, string? rootNodeName = null,
            WebApiRequestHeaderContext? headers = null)
        {
            return SendXmlRawAsync(HttpExtendMethod.Patch, url, data, ContentTypes.Xml, rootNodeName, headers);
        }
        /// <summary>
        /// Send Http Patch Request (Xml Raw)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="rootNodeName"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PatchXmlAsync<T>(
            Url url, T data, string? rootNodeName = null,
            WebApiRequestHeaderContext? headers = null)
        {
            return PatchXmlAsync(url.ToUrlString(), data, rootNodeName, headers);
        }


        /// <summary>
        /// Send Http Patch Request (Form Url Encoded)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="form"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PatchFormUrlEncodedAsync(
            string url, Dictionary<string, string> form,
            WebApiRequestHeaderContext? headers = null)
        {
            return SendFormUrlEncodedAsync(HttpExtendMethod.Patch, url, form, headers);
        }
        /// <summary>
        /// Send Http Patch Request (Form Url Encoded)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="form"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PatchFormUrlEncodedAsync(
            Url url, Dictionary<string, string> form,
            WebApiRequestHeaderContext? headers = null)
        {
            return PatchFormUrlEncodedAsync(url.ToUrlString(), form, headers);
        }


        /// <summary>
        /// Send Http Patch Request (Multipart Form Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formData"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PatchFormDataAsync(
            string url, Dictionary<string, string> formData, string contentType,
            WebApiRequestHeaderContext? headers = null)
        {
            return SendFormDataAsync(HttpExtendMethod.Patch, url, formData, headers);
        }
        /// <summary>
        /// Send Http Patch Request (Multipart Form Data)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formData"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> PatchFormDataAsync(
            Url url, Dictionary<string, string> formData, string contentType,
            WebApiRequestHeaderContext? headers = null)
        {
            return PatchFormDataAsync(url.ToUrlString(), formData, contentType, headers);
        }

        #endregion


        #region Http Delete

        /// <summary>
        /// Send Http Delete Request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> DeleteAsync(
            string url, WebApiRequestHeaderContext? headers = null)
        {
            return SendAsync(HttpMethod.Delete, url, headers);
        }
        /// <summary>
        /// Send Http Delete Request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> DeleteAsync(
            Url url, WebApiRequestHeaderContext? headers = null)
        {
            return DeleteAsync(url.ToUrlString(), headers);
        }

        #endregion


        #region Send Request

        /// <summary>
        /// Send Http Request
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual async Task<WebApiResponseContext> SendAsync(
            HttpMethod method, string url,
            WebApiRequestHeaderContext? headers = null)
        {
            using (var request = CreateRequest(method, url))
            using (HttpClient client = CreateClient())
            {
                // 設定 Header
                if (headers != null)
                {
                    client.SetRequestHeaders(headers);
                }

                // 發送 HTTP Request
                return await SendRequestAsync(client, request);
            }
        }
        /// <summary>
        /// Send Http Request
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> SendAsync(
            HttpMethod method, Url url,
            WebApiRequestHeaderContext? headers = null)
        {
            return SendAsync(method, url.ToUrlString(), headers);
        }


        /// <summary>
        /// Send Http Request (Raw Data)
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual async Task<WebApiResponseContext> SendAsync(
            HttpMethod method, string url,
            string data, string contentType,
            WebApiRequestHeaderContext? headers = null)
        {
            using (var request = CreateRequest(method, url))
            using (HttpClient client = CreateClient())
            {
                // Set Headers
                headers = (headers != null) ? headers.SetContentType(contentType)
                                            : new WebApiRequestHeaderContext(contentType);
                client.SetRequestHeaders(headers);

                // Add Body Content
                var content = HttpRequestContentBuilder.CreateBodyContent(headers.ContentType,
                                                                          data,
                                                                          headers.ContentEncoding);
                request.Content = content;

                // Send HTTP Request
                return await SendRequestAsync(client, request);
            }
        }
        /// <summary>
        /// Send Http Request (Raw Data)
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> SendAsync(
            HttpMethod method, Url url,
            string data, string contentType,
            WebApiRequestHeaderContext? headers = null)
        {
            return SendAsync(method, url.ToUrlString(), data, contentType, headers);
        }


        /// <summary>
        /// Send Http Request (Raw Data)
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual async Task<WebApiResponseContext> SendRawDataAsync<T>(
            HttpMethod method, string url,
            T data, string contentType,
            WebApiRequestHeaderContext? headers = null)
        {
            using (var request = CreateRequest(method, url))
            using (HttpClient client = CreateClient())
            {
                // Set Headers
                headers = (headers != null) ? headers.SetContentType(contentType)
                                            : new WebApiRequestHeaderContext(contentType);
                client.SetRequestHeaders(headers);

                // Add Body Content
                var content = HttpRequestContentBuilder.CreateRawContent(headers.ContentType,
                                                                         data,
                                                                         headers.ContentEncoding);
                request.Content = content;

                // Send HTTP Request
                return await SendRequestAsync(client, request);
            }
        }
        /// <summary>
        /// Send Http Request (Raw Data)
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> SendRawDataAsync<T>(
            HttpMethod method, Url url,
            T data, string contentType,
            WebApiRequestHeaderContext? headers = null)
        {
            return SendRawDataAsync(method, url.ToUrlString(), data, contentType, headers);
        }

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
        public virtual async Task<WebApiResponseContext> SendXmlRawAsync<T>(
            HttpMethod method, string url, T data,
            string contentType, string? rootNodeName = null,
            WebApiRequestHeaderContext? headers = null)
        {
            using (var request = CreateRequest(method, url))
            using (HttpClient client = CreateClient())
            {
                // Set Headers
                headers = (headers != null) ? headers.SetContentType(contentType)
                                            : new WebApiRequestHeaderContext(contentType);
                client.SetRequestHeaders(headers);

                // Add Body Content
                var content = HttpRequestContentBuilder.CreateXmlRawContent(headers.ContentType,
                                                                            data, rootNodeName,
                                                                            headers.ContentEncoding);
                request.Content = content;

                // Send HTTP Request
                return await SendRequestAsync(client, request);
            }
        }
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
        public virtual Task<WebApiResponseContext> SendXmlRawAsync<T>(
            HttpMethod method, Url url, T data,
            string contentType, string? rootNodeName = null,
            WebApiRequestHeaderContext? headers = null)
        {
            return SendXmlRawAsync(method, url.ToUrlString(), data, contentType, rootNodeName, headers);
        }


        /// <summary>
        /// Send Http Request (Form Url Encoded)
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual async Task<WebApiResponseContext> SendFormUrlEncodedAsync(
            HttpMethod method, string url,
            Dictionary<string, string> data,
            WebApiRequestHeaderContext? headers = null)
        {
            using (var request = CreateRequest(method, url))
            using (HttpClient client = CreateClient())
            {
                // Set Headers
                headers = (headers != null) ? headers.SetContentType(ContentTypes.FormUrlEncoded)
                                            : WebApiRequestHeaderContext.FormUrlEncoded();
                client.SetRequestHeaders(headers);

                // Add Body Content
                var content = HttpRequestContentBuilder.CreateFormContent(headers.ContentType, data);
                request.Content = content;

                // Send HTTP Request
                return await SendRequestAsync(client, request);
            }
        }
        /// <summary>
        /// Send Http Request (Form Url Encoded)
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> SendFormUrlEncodedAsync(
            HttpMethod method, Url url,
            Dictionary<string, string> data,
            WebApiRequestHeaderContext? headers = null)
        {
            return SendFormUrlEncodedAsync(method, url.ToUrlString(), data, headers);
        }


        /// <summary>
        /// Send Http Request (Multipart Form Data)
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual async Task<WebApiResponseContext> SendFormDataAsync(
            HttpMethod method, string url,
            Dictionary<string, string> data,
            WebApiRequestHeaderContext? headers = null)
        {
            using (var request = CreateRequest(method, url))
            using (HttpClient client = CreateClient())
            {
                // Set Headers
                headers = (headers != null) ? headers.SetContentType(ContentTypes.FormData)
                                            : WebApiRequestHeaderContext.FormData();
                client.SetRequestHeaders(headers);

                // Add Body Content
                var content = HttpRequestContentBuilder.CreateFormContent(headers.ContentType, data);
                request.Content = content;

                // Send HTTP Request
                return await SendRequestAsync(client, request);
            }
        }
        /// <summary>
        /// Send Http Request (Multipart Form Data)
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> SendFormDataAsync(
            HttpMethod method, Url url,
            Dictionary<string, string> data,
            WebApiRequestHeaderContext? headers = null)
        {
            return SendFormDataAsync(method, url.ToUrlString(), data, headers);
        }


        /// <summary>
        /// Send Http Request (Multipart Form Data)
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="formData"></param>
        /// <param name="formFiles"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual async Task<WebApiResponseContext> SendFormDataAsync(
            HttpMethod method, string url,
            Dictionary<string, string> formData,
            List<StreamPartContent> formFiles,
            WebApiRequestHeaderContext? headers = null)
        {
            using (var request = CreateRequest(method, url))
            using (HttpClient client = CreateClient())
            {
                // Set Headers
                headers = (headers != null) ? headers.SetContentType(ContentTypes.FormData)
                                            : WebApiRequestHeaderContext.FormData();
                client.SetRequestHeaders(headers);

                // Add Body Content
                var content = HttpRequestContentBuilder.CreateFormContent(formData, formFiles);
                request.Content = content;

                // Send HTTP Request
                return await SendRequestAsync(client, request);
            }
        }
        /// <summary>
        /// Send Http Request (Multipart Form Data)
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="formData"></param>
        /// <param name="formFiles"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> SendFormDataAsync(
            HttpMethod method, Url url,
            Dictionary<string, string> formData,
            List<StreamPartContent> formFiles,
            WebApiRequestHeaderContext? headers = null)
        {
            return SendFormDataAsync(method, url.ToUrlString(), formData, formFiles, headers);
        }


        /// <summary>
        /// Send Http Request (Multipart Form Data)
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="formFiles"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual async Task<WebApiResponseContext> SendFormDataAsync(
            HttpMethod method, string url,
            List<StreamPartContent> formFiles,
            WebApiRequestHeaderContext? headers = null)
        {
            using (var request = CreateRequest(method, url))
            using (HttpClient client = CreateClient())
            {
                // Set Headers
                headers = (headers != null) ? headers.SetContentType(ContentTypes.FormData)
                                             : WebApiRequestHeaderContext.FormData();
                client.SetRequestHeaders(headers);

                // Add Body Content
                var content = HttpRequestContentBuilder.CreateStreamContent(formFiles);
                request.Content = content;

                // Send HTTP Request
                return await SendRequestAsync(client, request);
            }
        }
        /// <summary>
        /// Send Http Request (Multipart Form Data)
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="formFiles"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public virtual Task<WebApiResponseContext> SendFormDataAsync(
            HttpMethod method, Url url,
            List<StreamPartContent> formFiles,
            WebApiRequestHeaderContext? headers = null)
        {
            return SendFormDataAsync(method, url.ToUrlString(), formFiles, headers);
        }


        /// <summary>
        /// Send HTTP Request
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        protected virtual async Task<WebApiResponseContext> SendRequestAsync(
            HttpClient client,
            HttpRequestMessage request)
        {
            try
            {
                var response = await client.SendAsync(request);
                return new WebApiResponseContext(response);
            }
            catch (Exception exception)
            {
                var apiException = new WebApiRequestException(exception).WithData(request);
                Logger?.LogError(exception, apiException.Message);
                throw apiException;
            }
        }

        /// <summary>
        /// Create HTTP Request
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        protected virtual HttpRequestMessage CreateRequest(HttpMethod method, string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("URL cannot be null or empty.", nameof(url));
            }
            return new HttpRequestMessage(method, url);
        }

        #endregion
    }
}
