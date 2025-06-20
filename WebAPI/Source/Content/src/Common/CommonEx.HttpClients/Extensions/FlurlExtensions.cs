using Flurl;

namespace CommonEx.HttpClients.Extensions
{
    public static class FlurlExtensions
    {
        /// <summary>
        /// To Url String
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string ToUrlString(this Url url)
        {
            return url.ToUri().ToString();
        }
    }
}
