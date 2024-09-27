using Newtonsoft.Json.Linq;

namespace CommonEx.Utilities.TextUtilities.Extensions
{
    public static class JTokenExtensions
    {
        /// <summary>
        /// Is Null or Empty
        /// </summary>
        /// <param name="jToken"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this JToken jToken)
        {
            switch (jToken.Type)
            {
                case JTokenType.String:
                    string text = jToken.ToString();
                    return string.IsNullOrEmpty(text);

                case JTokenType.Array:
                    return (((JArray)jToken).Count == 0);

                case JTokenType.Object:
                    return (((JObject)jToken).Count == 0);

                case JTokenType.Undefined:
                case JTokenType.Null:
                case JTokenType.None:
                    return true;

            }
            return false;
        }

        /// <summary>
        /// To String，僅 Escape JToken 為一個非 JSON String 的 String
        /// </summary>
        /// <param name="jToken"></param>
        /// <returns></returns>
        public static string ConvertToString(this JToken jToken)
        {
            string jsonString = Convert.ToString(jToken);

            // 針對 JToken 為一個非 JSON String 的 String 處理 Escape
            if (jToken.Type == JTokenType.String && !IsValidJson(jsonString))
            {
                return JsonStringEscaper.EscapeJsonStringValue(jsonString);
            }
            return jsonString;
        }

        /// <summary>
        /// 檢查是否為 JSON String
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static bool IsValidJson(string jsonString)
        {
            if (string.IsNullOrWhiteSpace(jsonString))
            {
                return false;
            }

            try
            {
                var jToken = JToken.Parse(jsonString);

                switch (jToken.Type)
                {
                    case JTokenType.Object:
                    case JTokenType.Array:
                        return true;

                    default:
                        return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Replace Value by JsonPath
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root"></param>
        /// <param name="path"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public static JToken ReplacePath<T>(this JToken root, string path, T newValue)
        {
            if (root == null || path == null)
            {
                throw new ArgumentNullException();
            }

            foreach (var value in root.SelectTokens(path).ToList())
            {
                if (value == root)
                {
                    root = JToken.FromObject(newValue);
                }

                else
                {
                    value.Replace(JToken.FromObject(newValue));
                }
            }

            return root;
        }

        /// <summary>
        /// Remove Value by JsonPath
        /// </summary>
        /// <param name="root"></param>
        /// <param name="path"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void RemovePath(this JToken root, string path)
        {
            if (root == null || path == null)
            {
                throw new ArgumentNullException();
            }

            foreach (var value in root.SelectTokens(path).ToList())
            {
                value.Remove();
            }
        }
    }
}
