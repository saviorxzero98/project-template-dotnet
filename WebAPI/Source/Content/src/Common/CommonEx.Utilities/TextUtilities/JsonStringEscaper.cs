using System.Text;

namespace CommonEx.Utilities.TextUtilities
{
    /// <summary>
    /// 處理 JSON 字串的 Escape 和 Unescape
    /// </summary>
    public static class JsonStringEscaper
    {
        /// <summary>
        /// Escape，處理 JSON String Value 保留字 (", \)
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string EscapeJsonStringValue(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return text;
            }

            var builder = new StringBuilder();
            foreach (char c in text)
            {
                switch (c)
                {
                    case '\\':
                    case '"':
                        builder.Append('\\');
                        builder.Append(c);
                        break;

                    default:
                        builder.Append(c);
                        break;
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// Unescape，處理 JSON String Value (", \)
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string UnescapeJsonStringValue(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }
            string unescapedText = text.Replace("\\\"", "\"")
                                       .Replace("\\\\", "\\");
            return unescapedText;
        }

        /// <summary>
        /// 替換掉 JsonPath 保留字
        /// </summary>
        /// <param name="jsonpath"></param>
        /// <returns></returns>
        public static string UnescapeJsonPath(string jsonpath)
        {
            if (string.IsNullOrWhiteSpace(jsonpath))
            {
                return jsonpath;
            }
            return jsonpath.Replace("\\\"", "'");
        }

        /// <summary>
        /// Unescape，處理 Script 保留字 (")
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string UnescapeScriptExpression(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }
            string unescapedText = text.Replace("\\\"", "\"");
            return unescapedText;
        }

        /// <summary>
        /// Escape，處理 JSON 保留字 (", \, /, \b, \t, \n, \r, \f)
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string EscapeJson(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return text;
            }

            var builder = new StringBuilder();
            foreach (char c in text)
            {
                switch (c)
                {
                    case '\\':
                    case '"':
                    case '/':
                        builder.Append('\\');
                        builder.Append(c);
                        break;

                    case '\b':
                        builder.Append("\\b");
                        break;
                    case '\t':
                        builder.Append("\\t");
                        break;
                    case '\n':
                        builder.Append("\\n");
                        break;
                    case '\f':
                        builder.Append("\\f");
                        break;
                    case '\r':
                        builder.Append("\\r");
                        break;

                    default:
                        builder.Append(c);
                        break;
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// Unescape，處理 JSON 保留字 (", \, /, \b, \t, \n, \r, \f)
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string UnescapeJson(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }
            string unescapedText = text.Replace("\\\"", "\"")
                                       .Replace("\\/", "/")
                                       .Replace("\\b", "\b")
                                       .Replace("\\t", "\t")
                                       .Replace("\\n", "\n")
                                       .Replace("\\r", "\r")
                                       .Replace("\\f", "\f")
                                       .Replace("\\\\", "\\");
            return unescapedText;
        }
    }
}
