namespace CommonEx.Utilities.FileSystem
{
    public static class FilePathHelper
    {
        /// <summary>
        /// 轉換成絕對路徑
        /// </summary>
        /// <param name="rootPath"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ToAbsolutePath(string path, string rootPath = "")
        {
            // Releation File Path
            if (path.StartsWith("~/") ||
                path.StartsWith("~\\"))
            {
                path = path.Replace("~/", string.Empty).Replace("~\\", string.Empty);
            }

            // Get Root Path
            if (string.IsNullOrWhiteSpace(rootPath))
            {
                rootPath = AppDomain.CurrentDomain.BaseDirectory;
            }

            // Combine Base Path
            path = FixPathTraversal(Combine(rootPath, path));

            return Path.GetFullPath(path);
        }

        /// <summary>
        /// 取得相對路徑
        /// </summary>
        /// <param name="path"></param>
        /// <param name="rootPath"></param>
        /// <returns></returns>
        public static string GetRelativePath(string path, string rootPath)
        {
            Uri baseUri = new Uri(AppendDirectorySeparatorChar(rootPath));
            Uri fullUri = new Uri(path);

            Uri relativeUri = baseUri.MakeRelativeUri(fullUri);
            string relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            string slash = $"~{Path.DirectorySeparatorChar}";
            relativePath = relativePath.Replace("..\\", slash)
                                       .Replace("../", slash);
            return relativePath;
        }

        /// <summary>
        /// 合併路徑
        /// </summary>
        /// <param name="pathList"></param>
        /// <returns></returns>
        public static string Combine(params string[] pathList)
        {
            // Combine Path
            string path = Path.Combine(pathList);

            // Directory Separator
            path = FormatDirectorySeparatorChar(path);

            return Path.GetFullPath(path);
        }

        /// <summary>
        /// 調整檔案系統目錄分隔符號
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string FormatDirectorySeparatorChar(string path)
        {
            string slash = Path.DirectorySeparatorChar.ToString();
            path = path.Replace("\\", slash)
                       .Replace("/", slash);

            return Path.GetFullPath(path);
        }

        /// <summary>
        /// 加入目錄分割字元
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string AppendDirectorySeparatorChar(string path)
        {
            if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                return path + Path.DirectorySeparatorChar;
            }
            return path;
        }

        /// <summary>
        /// 修復 Path Traversal Issue
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string FixPathTraversal(string path)
        {
            string slash = Path.DirectorySeparatorChar.ToString();

            return path.Replace("..\\", slash)
                       .Replace("../", slash)
                       .Replace("..", string.Empty);
        }
    }
}
