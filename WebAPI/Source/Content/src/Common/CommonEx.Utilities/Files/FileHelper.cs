using CommonEx.Utilities.Cryptography;
using MimeTypes;

namespace CommonEx.Utilities.Files
{
    public static class FileHelper
    {
        /// <summary>
        /// 取得檔案資訊
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="rootPath"></param>
        /// <returns></returns>
        public static FileInfo CreateInfo(string fileName, string rootPath)
        {
            return CreateInfo(PathHelper.ToAbsolutePath(fileName, rootPath));
        }
        /// <summary>
        /// 取得檔案資訊
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static FileInfo CreateInfo(string filePath)
        {
            return new FileInfo(PathHelper.FixPathTraversal(filePath));
        }


        /// <summary>
        /// 取得檔案 Content Type
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="rootPath"></param>
        /// <param name="defaultContentType"></param>
        /// <returns></returns>
        public static string GetContentType(string fileName, string rootPath,
                                            string defaultContentType = "application/octet-stream")
        {
            return GetContentType(PathHelper.ToAbsolutePath(fileName, rootPath),
                                  defaultContentType);
        }
        /// <summary>
        /// 取得檔案 Content Type
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="rootPath"></param>
        /// <param name="defaultContentType"></param>
        /// <returns></returns>
        public static string GetContentType(string filePath,
                                            string defaultContentType = "application/octet-stream")
        {
            var contentType = MimeTypeMap.GetMimeType(PathHelper.FixPathTraversal(filePath));
            return contentType ?? defaultContentType;
        }

        /// <summary>
        /// 刪除檔案
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="rootPath"></param>
        public static void Delete(string fileName, string rootPath)
        {
            Delete(PathHelper.ToAbsolutePath(fileName, rootPath));
        }
        /// <summary>
        /// 刪除檔案
        /// </summary>
        /// <param name="filePath"></param>
        public static void Delete(string filePath)
        {
            if (File.Exists(PathHelper.FixPathTraversal(filePath)))
            {
                File.Delete(PathHelper.FixPathTraversal(filePath));
            }
        }


        /// <summary>
        /// 取得檔案雜湊值
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="rootPath"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetFileHash(string fileName, string rootPath,
                                         HashAlgorithmType type = HashAlgorithmType.MD5)
        {
            return GetFileHash(PathHelper.ToAbsolutePath(fileName, rootPath), type);
        }
        /// <summary>
        /// 取得檔案雜湊值
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetFileHash(string filePath, HashAlgorithmType type = HashAlgorithmType.MD5)
        {
            if (!File.Exists(PathHelper.FixPathTraversal(filePath)))
            {
                return string.Empty;
            }

            using (var hash = HashCrypto.GetHashAlgorithm(type))
            using (var stream = File.OpenRead(PathHelper.FixPathTraversal(filePath).ToString()))
            {
                byte[] checksum = hash.ComputeHash(stream);
                string bits = BitConverter.ToString(checksum);
                return bits.Replace("-", string.Empty).ToLower();
            }
        }
    }
}
