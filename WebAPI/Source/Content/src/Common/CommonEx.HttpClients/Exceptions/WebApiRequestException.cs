namespace CommonEx.HttpClients.Exceptions
{
    public class WebApiRequestException : Exception
    {
        /// <summary>
        /// Http Request
        /// </summary>
        public HttpRequestMessage Request { get; protected set; }

        /// <summary>
        /// Http Request Info
        /// </summary>
        public string ErrorInfo { get; protected set; } = string.Empty;


        public WebApiRequestException()
        {
        }
        public WebApiRequestException(string message) : base(message)
        {
        }
        public WebApiRequestException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
        public WebApiRequestException(Exception innerException)
            : base(innerException.Message, innerException)
        {
        }


        /// <summary>
        /// With Data
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public WebApiRequestException WithData(HttpRequestMessage request)
        {
            Request = request;

            if (request != null && request.RequestUri != null)
            {
                ErrorInfo = $"{request.Method.Method} {request.RequestUri.ToString()}";
            }
            return this;
        }
    }
}
