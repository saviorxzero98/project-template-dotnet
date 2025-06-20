namespace CommonEx.Utilities.FileSystem
{
    public static class DirectoryHelper
    {
        /// <summary>
        /// 建立目錄
        /// </summary>
        /// <param name="dirName"></param>
        /// <param name="rootPath"></param>
        public static void Create(string dirName, string rootPath)
        {
            Create(FilePathHelper.ToAbsolutePath(dirName, rootPath));
        }
        /// <summary>
        /// 建立目錄
        /// </summary>
        /// <param name="dirPath"></param>
        public static void Create(string dirPath)
        {
            if (!Directory.Exists(FilePathHelper.FixPathTraversal(dirPath)))
            {
                Directory.CreateDirectory(FilePathHelper.FixPathTraversal(dirPath));
            }
        }


        /// <summary>
        /// 建立目錄資訊
        /// </summary>
        /// <param name="dirName"></param>
        /// <param name="rootPath"></param>
        /// <returns></returns>
        public static DirectoryInfo CreateInfo(string dirName, string rootPath)
        {
            return CreateInfo(FilePathHelper.ToAbsolutePath(dirName, rootPath));
        }
        /// <summary>
        /// 建立目錄資訊
        /// </summary>
        /// <param name="dirPath"></param>
        /// <returns></returns>
        public static DirectoryInfo CreateInfo(string dirPath)
        {
            return new DirectoryInfo(FilePathHelper.FixPathTraversal(dirPath));
        }


        /// <summary>
        /// 刪除目錄
        /// </summary>
        /// <param name="dirName"></param>
        /// <param name="rootPath"></param>
        /// <param name="recursive"></param>
        public static void Delete(string dirName, string rootPath, bool recursive = true)
        {
            Delete(FilePathHelper.ToAbsolutePath(dirName, rootPath), recursive);
        }
        /// <summary>
        /// 刪除目錄
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="recursive"></param>
        public static void Delete(string dirPath, bool recursive = true)
        {
            if (Directory.Exists(FilePathHelper.FixPathTraversal(dirPath)))
            {
                Directory.Delete(FilePathHelper.FixPathTraversal(dirPath), recursive);
            }
        }


        /// <summary>
        /// 搬移目錄
        /// </summary>
        /// <param name="dirName"></param>
        /// <param name="sourceRootPath"></param>
        /// <param name="destRootPath"></param>
        public static void Move(string dirName, string sourceRootPath, string destRootPath)
        {
            Move(FilePathHelper.ToAbsolutePath(dirName, sourceRootPath),
                 FilePathHelper.ToAbsolutePath(dirName, destRootPath));
        }
        /// <summary>
        /// 搬移目錄
        /// </summary>
        /// <param name="sourceDirPath"></param>
        /// <param name="destDirPath"></param>
        public static void Move(string sourceDirPath, string destDirPath)
        {
            if (Directory.Exists(FilePathHelper.FixPathTraversal(sourceDirPath)))
            {
                Directory.Move(FilePathHelper.FixPathTraversal(sourceDirPath),
                               FilePathHelper.FixPathTraversal(destDirPath));
            }
        }


        /// <summary>
        /// 複製目錄
        /// </summary>
        /// <param name="dirName"></param>
        /// <param name="sourceRootPath"></param>
        /// <param name="destRootPath"></param>
        /// <param name="copySubDirs"></param>
        public static void Copy(string dirName, string sourceRootPath, string destRootPath, bool copySubDirs = true)
        {
            Copy(FilePathHelper.ToAbsolutePath(dirName, sourceRootPath),
                 FilePathHelper.ToAbsolutePath(dirName, destRootPath),
                 copySubDirs);
        }
        /// <summary>
        /// 複製目錄
        /// </summary>
        /// <param name="sourceDirPath"></param>
        /// <param name="destDirPath"></param>
        /// <param name="copySubDirs"></param>
        public static void Copy(string sourceDirPath, string destDirPath, bool copySubDirs = true)
        {
            var dir = new DirectoryInfo(FilePathHelper.FixPathTraversal(sourceDirPath));
            if (!dir.Exists)
            {
                var errorMessage = $"Source directory does not exist or could not be found: {sourceDirPath}";
                throw new DirectoryNotFoundException(errorMessage);
            }

            // 建立新的目錄
            Create(FilePathHelper.FixPathTraversal(destDirPath));

            // 搬移目錄下的檔案
            var files = dir.GetFiles();
            foreach (var file in files)
            {
                var tempPath = FilePathHelper.Combine(FilePathHelper.FixPathTraversal(destDirPath), file.Name);
                file.CopyTo(tempPath, false);
            }

            // 搬移目錄下的子目錄
            if (copySubDirs)
            {
                var dirs = dir.GetDirectories();
                foreach (var subdir in dirs)
                {
                    var tempPath = FilePathHelper.Combine(FilePathHelper.FixPathTraversal(destDirPath), subdir.Name);
                    Copy(subdir.FullName, tempPath, copySubDirs);
                }
            }
        }
    }
}
