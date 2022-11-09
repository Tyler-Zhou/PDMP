/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-11-09 22:02:52
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/

namespace PDMP.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class VersionEntity:BaseEntity
    {
        #region 成员(Member)
        /// <summary>
        /// 文件MD5值
        /// </summary>
        public string FileMD5
        {
            get;
            set;
        }
        /// <summary>
        /// 发布时间
        /// </summary>
        public long PubTime
        {
            get;
            set;
        }
        /// <summary>
        /// 版本号
        /// </summary>
        public string Number
        {
            get;
            set;
        }
        /// <summary>
        /// 网页地址
        /// </summary>
        public string Link
        {
            get;
            set;
        }
        /// <summary>
        /// 更新包名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }
        #endregion
    }
}

