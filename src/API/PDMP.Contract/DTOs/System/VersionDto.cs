namespace PDMP.Contract.DTOs
{
    /// <summary>
    /// 版本传输对象
    /// </summary>
    public class VersionDto : BaseDto
    {
        private string _FileMD5;
        /// <summary>
        /// 文件MD5值
        /// </summary>
        public string FileMD5
        {
            get { return _FileMD5; }
            set { _FileMD5 = value; OnPropertyChanged(); }
        }
        private long _PubTime;
        /// <summary>
        /// 发布时间
        /// </summary>
        public long PubTime
        {
            get { return _PubTime; }
            set { _PubTime = value; OnPropertyChanged(); }
        }
        private string _Version;
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version
        {
            get { return _Version; }
            set { _Version = value; OnPropertyChanged(); }
        }
        private string _Url;
        /// <summary>
        /// 更新包网页地址
        /// </summary>
        public string Url
        {
            get { return _Url; }
            set { _Url = value; OnPropertyChanged(); }
        }
        private string _Name;
        /// <summary>
        /// 更新包名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; OnPropertyChanged(); }
        }
    }
}
