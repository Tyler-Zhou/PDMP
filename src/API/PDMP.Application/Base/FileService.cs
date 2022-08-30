using Microsoft.Extensions.Options;
using PDMP.Contract.Interfaces;
using PDMP.Domain.Entities;

namespace PDMP.Application
{
    /// <summary>
    /// 文件服务
    /// </summary>
    public class FileService: IFileService
    {
        private FileSetting _FileSetting { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileSetting"></param>
        public FileService(IOptions<FileSetting> fileSetting)
        {
            _FileSetting = fileSetting.Value;
        }

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Exists(string name)
        {
            string file = Directory.GetFiles(_FileSetting.DownloadDir, name, SearchOption.TopDirectoryOnly).FirstOrDefault();
            if(string.IsNullOrWhiteSpace(file))
                return false;
            return true;
        }


        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public FileStream Open(string name)
        {
            var fullFilePath = Path.Combine(_FileSetting.DownloadDir, name);
            return File.Open(fullFilePath,
                FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        /// <summary>
        /// 获取文件长度
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public long GetLength(string name)
        {
            var fullFilePath = Path.Combine(_FileSetting.DownloadDir, name);
            return new FileInfo(fullFilePath).Length;
        }
    }
}
