using PDMP.Contract;

namespace PDMP.Application
{
    /// <summary>
    /// 文件服务
    /// </summary>
    public class FileService: IFileService
    {
        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool Exists(string name, string path)
        {
            string file = Directory.GetFiles(path, name, SearchOption.TopDirectoryOnly).FirstOrDefault();
            if(string.IsNullOrWhiteSpace(file))
                return false;
            return true;
        }


        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public FileStream Open(string name, string path)
        {
            var fullFilePath = Path.Combine(path, name);
            return File.Open(fullFilePath,
                FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        /// <summary>
        /// 获取文件长度
        /// </summary>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public long GetLength(string name, string path)
        {
            var fullFilePath = Path.Combine(path, name);
            return new FileInfo(fullFilePath).Length;
        }
    }
}
