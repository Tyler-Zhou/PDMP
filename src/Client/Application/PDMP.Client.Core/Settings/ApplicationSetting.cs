namespace PDMP.Client.Core.Settings
{
    /// <summary>
    /// 程序设置
    /// </summary>
    public class ApplicationSetting
    {
        #region 成员(Member)
        /// <summary>
        /// 账号
        /// </summary>
        public static string Account { get; set; }

        /// <summary>
        /// 个性化设置
        /// </summary>
        public PersonalizationSetting Personalization { get; set; }

        /// <summary>
        /// 缓存设置
        /// </summary>
        public CacheSetting Cache { get; set; }
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 程序设置
        /// </summary>
        public ApplicationSetting()
        {
            Personalization = new PersonalizationSetting();
            Cache = new CacheSetting();
        } 
        #endregion
    }
}
