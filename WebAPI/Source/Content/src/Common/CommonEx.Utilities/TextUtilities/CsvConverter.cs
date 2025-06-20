using CsvHelper;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Text;

namespace CommonEx.Utilities.TextUtilities
{
    public static class CsvConverter
    {
        /// <summary>
        /// CSV Deserialize
        /// </summary>
        /// <param name="csvString">CSV String</param>
        /// <param name="encoding">CSV String Encoding</param>
        /// <returns></returns>
        public static JToken? DeserializeObject(string csvString, Encoding? encoding = null)
        {
            if (string.IsNullOrEmpty(csvString))
            {
                return default(JToken);
            }

            if (encoding != null)
            {
                var csvBytes = encoding.GetBytes(csvString);
                csvString = Encoding.UTF8.GetString(csvBytes);
            }

            using (var reader = new StringReader(csvString))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<dynamic>();
                JToken json = JToken.FromObject(records);
                return json;
            }
        }

        /// <summary>
        /// CSV Deserialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="csvString"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static T? DeserializeObject<T>(string csvString, Encoding? encoding = null)
        {
            var json = DeserializeObject(csvString, encoding);

            if (json == null)
            {
                return default(T);
            }
            return json.ToObject<T>();
        }
    }
}
