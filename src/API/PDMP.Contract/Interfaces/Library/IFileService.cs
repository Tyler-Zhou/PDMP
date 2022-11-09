/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-11-09 23:43:19
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/

namespace PDMP.Contract
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
        /// <param name="path"></param>
        /// <returns></returns>
        bool Exists(string name, string path);
        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        FileStream Open(string name, string path);
        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        long GetLength(string name, string path);

    }
}

