using Newtonsoft.Json;

namespace CommonEx.Utilities.ObjectUtilities
{
    public static class DeepCloneHelper
    {
        /// <summary>
        /// Deep Clone
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceData"></param>
        /// <returns></returns>
        public static T Clone<T>(T sourceData)
        {
            if (sourceData == null)
            {
                return default(T);
            }

            var jsonText = JsonConvert.SerializeObject(sourceData);
            if (!string.IsNullOrEmpty(jsonText))
            {
                return JsonConvert.DeserializeObject<T>(jsonText);
            }
            return default(T);
        }
    }
}
