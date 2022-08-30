namespace PDMP.Domain.Entities
{
    /// <summary>
    /// MongoDB设置
    /// </summary>
    public class MongoDBSetting
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// 数据库名
        /// </summary>
        public string DatabaseName { get; set; }
    }
}
