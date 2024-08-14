using Newtonsoft.Json;
using System.IO.Compression;

namespace CommonEx.Utilities.TextUtilities
{
    public enum TextCompressorType
    {
        Deflate,
        GZip,
        Brotli,
        ZLib
    }


    public class TextCompressor
    {
        public TextCompressorType Type { get; set; } = TextCompressorType.Brotli;

        public TextCompressor() : this(TextCompressorType.Brotli)
        {

        }
        public TextCompressor(TextCompressorType type)
        {
            Type = type;
        }

        public static TextCompressor Deflate { get => new TextCompressor(TextCompressorType.Deflate); }
        public static TextCompressor GZip { get => new TextCompressor(TextCompressorType.GZip); }
        public static TextCompressor Brotli { get => new TextCompressor(TextCompressorType.Brotli); }
        public static TextCompressor ZLib { get => new TextCompressor(TextCompressorType.ZLib); }


        /// <summary>
        /// 壓縮字串
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string Compress(string text)
        {
            using (var memoryStream = new MemoryStream())
            using (var compressionStream = CreateCompressionStream(memoryStream, CompressionMode.Compress))
            using (var streamWriter = new StreamWriter(compressionStream))
            {
                streamWriter.Write(text);
                streamWriter.Close();
                compressionStream.Close();

                var bytes = memoryStream.ToArray();
                return Convert.ToBase64String(bytes);
            }
        }
        /// <summary>
        /// 壓縮物件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Compress<T>(T data) where T : class
        {
            if (data == null)
            {
                return string.Empty;
            }

            var settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.None,
                NullValueHandling = NullValueHandling.Ignore
            };
            string serializedJson = JsonConvert.SerializeObject(data, settings);
            return Compress(serializedJson);
        }


        /// <summary>
        /// 解壓縮
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string Decompress(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            byte[] bytes = Convert.FromBase64String(text);

            using (var memoryStream = new MemoryStream(bytes))
            using (var compressionStream = CreateCompressionStream(memoryStream, CompressionMode.Decompress))
            using (var streamReader = new StreamReader(compressionStream))
            {
                return streamReader.ReadToEnd();
            }
        }

        /// <summary>
        /// 解壓縮
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text"></param>
        /// <returns></returns>
        public T Decompress<T>(string text) where T : class
        {
            if (string.IsNullOrEmpty(text))
            {
                return default(T);
            }

            var deserializeJson = Decompress(text);
            return JsonConvert.DeserializeObject<T>(deserializeJson);
        }


        /// <summary>
        /// 建立 Compression Stream
        /// </summary>
        /// <param name="inStream"></param>
        /// <param name="mode"></param>
        /// <param name="leaveOpen"></param>
        /// <returns></returns>
        protected Stream CreateCompressionStream(Stream inStream, CompressionMode mode, bool leaveOpen = false)
        {
            switch (Type)
            {
                case TextCompressorType.Deflate:
                    return new DeflateStream(inStream, mode, leaveOpen);

                case TextCompressorType.GZip:
                    return new GZipStream(inStream, mode, leaveOpen);

                case TextCompressorType.ZLib:
                    return new ZLibStream(inStream, mode, leaveOpen);

                case TextCompressorType.Brotli:
                default:
                    return new BrotliStream(inStream, mode, leaveOpen);
            }
        }
    }
}
