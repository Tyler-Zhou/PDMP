namespace PDMP.Contract.Interfaces
{
    /// <summary>
    /// 文件服务
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// 文件是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool Exists(string name);
        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        FileStream Open(string name);
        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        long GetLength(string name);
    }
}
