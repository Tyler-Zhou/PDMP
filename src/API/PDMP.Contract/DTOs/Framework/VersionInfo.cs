/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-11-09 23:27:28
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/

namespace PDMP.Contract
{
    /// <summary>
    /// 版本信息
    /// </summary>
    public class VersionInfo : BaseInfo
    {
        #region 成员(Member)

        #region 文件MD5值
        private string _FileMD5;
        /// <summary>
        /// 文件MD5值
        /// </summary>
        public string FileMD5
        {
            get { return _FileMD5; }
            set { _FileMD5 = value; OnPropertyChanged(); }
        }
        #endregion

        #region 发布时间
        private long _PubTime;
        /// <summary>
        /// 发布时间
        /// </summary>
        public long PubTime
        {
            get { return _PubTime; }
            set { _PubTime = value; OnPropertyChanged(); }
        }
        #endregion

        #region 版本号
        private string _Number;
        /// <summary>
        /// 版本号
        /// </summary>
        public string Number
        {
            get { return _Number; }
            set { _Number = value; OnPropertyChanged(); }
        }
        #endregion

        #region 更新包网页地址
        private string _Link;
        /// <summary>
        /// 更新包网页地址
        /// </summary>
        public string Link
        {
            get { return _Link; }
            set { _Link = value; OnPropertyChanged(); }
        }
        #endregion

        #region 更新包名称
        private string _Name;
        /// <summary>
        /// 更新包名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; OnPropertyChanged(); }
        }
        #endregion

        #endregion
    }
}

