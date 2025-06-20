using System.Text.RegularExpressions;

namespace CommonEx.Utilities.NetUtilities
{
    public static class UrlMatcher
    {
        /// <summary>
        /// Url Path 是否符合 Regex Pattern
        /// </summary>
        /// <param name="url"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool IsUrlPathMatchRegex(Uri url, string pattern)
        {
            return IsUrlPathMatchRegex(url.AbsolutePath, pattern);
        }
        /// <summary>
        /// Url Path 是否符合 Regex Pattern
        /// </summary>
        /// <param name="urlPath"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool IsUrlPathMatchRegex(string urlPath, string pattern)
        {
            return Regex.IsMatch(urlPath, pattern, RegexOptions.IgnoreCase);
        }


        /// <summary>
        /// Url Path 是否符合 Wildcard Pattern
        /// </summary>
        /// <param name="url"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool IsUrlPathMatchWildcard(Uri url, string pattern)
        {
            return IsUrlPathMatchWildcard(url.AbsolutePath, pattern);
        }
        /// <summary>
        /// Url Path 是否符合 Wildcard Pattern
        /// </summary>
        /// <param name="urlPath"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool IsUrlPathMatchWildcard(string urlPath, string pattern)
        {
            string regexPattern = Regex.Escape(pattern)
                                       .Replace(@"\*", @"[^/]+")
                                       .Replace(@"\?", @"[^/]");
            return IsUrlPathMatchRegex(urlPath, $"^{regexPattern}$");
        }
    }
}
