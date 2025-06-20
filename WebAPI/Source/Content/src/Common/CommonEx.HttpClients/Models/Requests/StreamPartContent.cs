using CommonEx.HttpClients.Constants;
using MimeTypes;

namespace CommonEx.HttpClients.Models.Requests
{
    public class StreamPartContent
    {
        /// <summary>
        /// Form Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// File Name
        /// </summary>
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// File Content Type
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// File Stream
        /// </summary>
        private Stream? _fileStream = null;
        /// <summary>
        /// File Stream
        /// </summary>
        public Stream? FileStream
        {
            get => _fileStream;
            set
            {
                _fileStream = value;
            }
        }


        public StreamPartContent(string name, Stream fileStream, string fileName = "", string? contentType = null)
        {
            Name = name;
            FileName = fileName;
            FileStream = fileStream;

            if (!string.IsNullOrEmpty(contentType))
            {
                ContentType = contentType;
            }
            else if (!string.IsNullOrEmpty(fileName))
            {
                ContentType = MimeTypeMap.GetMimeType(fileName);
            }
            else
            {
                ContentType = ContentTypes.BinaryStream;
            }
        }

        /// <summary>
        /// 建立 Http Content
        /// </summary>
        /// <returns></returns>
        public HttpContent? CreateContent()
        {
            if (FileStream == null)
            {
                return default;
            }

            // 建立 Stream Content
            var streamContent = new StreamContent(FileStream);
            streamContent.Headers.Add(HttpHeaderNames.ContentType, ContentType);

            return streamContent;
        }
    }
}
