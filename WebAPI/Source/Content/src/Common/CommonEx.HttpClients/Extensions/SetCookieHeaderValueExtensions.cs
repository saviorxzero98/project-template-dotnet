using Microsoft.Net.Http.Headers;
using System.Net;

namespace CommonEx.HttpClients.Extensions
{
    public static class SetCookieHeaderValueExtensions
    {
        /// <summary>
        /// Set Cookie Header Value to Cookie
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Cookie ToCookie(this SetCookieHeaderValue value)
        {
            var cookie = new Cookie()
            {
                Name = value.Name.Value ?? string.Empty,
                Value = value.Value.Value,
                Domain = value.Domain.Value,
                Secure = value.Secure,
                HttpOnly = value.HttpOnly,
                Path = value.Path.Value
            };

            if (value.Expires != null)
            {
                cookie.Expires = ((DateTimeOffset)value.Expires).LocalDateTime;
            }
            return cookie;
        }
    }
}
