using CommonEx.HttpClients.Models.Requests;

namespace CommonEx.HttpClients.Extensions
{
    public static class MultipartFormDataContentExtensions
    {
        /// <summary>
        /// 加入多個 Multipart String Content
        /// </summary>
        /// <param name="content"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        internal static MultipartFormDataContent AddManyStringContent(
            this MultipartFormDataContent content,
            Dictionary<string, string> formData)
        {
            if (formData != null)
            {
                var dataKeys = new List<string>(formData.Keys);
                foreach (var dataKey in dataKeys)
                {
                    content.Add(new StringContent(formData[dataKey]), dataKey);
                }
            }
            return content;
        }

        /// <summary>
        /// 加入多個 Multipart Stream Content
        /// </summary>
        /// <param name="content"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        internal static MultipartFormDataContent AddManyStreamContent(
            this MultipartFormDataContent content,
            List<StreamPartContent> files)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    var streamContent = file.CreateContent();
                    if (streamContent != null)
                    {
                        content.Add(streamContent, file.Name, file.FileName);
                    }
                }
            }
            return content;
        }
    }
}
