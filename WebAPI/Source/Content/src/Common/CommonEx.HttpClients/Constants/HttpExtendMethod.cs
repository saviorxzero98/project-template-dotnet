namespace CommonEx.HttpClients.Constants
{
    public class HttpExtendMethod
    {
        public static HttpMethod Patch
        {
            get
            {
                return new HttpMethod(HttpMethodTypes.Patch);
            }
        }
    }
}
