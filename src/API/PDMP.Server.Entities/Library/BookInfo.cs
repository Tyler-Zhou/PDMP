using MongoDB.Bson.Serialization.Attributes;

namespace PDMP.Server.Entities
{
    /// <summary>
    /// 书籍实体
    /// </summary>
    public class BookInfo : BaseInfo
    {
        /// <summary>
        /// 标识键
        /// </summary>
        [BsonElement("sKey")]
        [BsonDefaultValue("")]
        public string Key { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [BsonElement("sName")]
        [BsonDefaultValue("")]
        public string Name { get; set; }
        /// <summary>
        /// 链接
        /// </summary>
        [BsonElement("sLink")]
        [BsonDefaultValue("")]
        public string Link { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        [BsonElement("sAuthor")]
        [BsonDefaultValue("")]
        public string Author { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        [BsonElement("sTag")]
        [BsonDefaultValue("")]
        public string Tag { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        [BsonElement("sIntroduction")]
        [BsonDefaultValue("")]
        public string Introduction { get; set; }
        /// <summary>
        /// 完本状态
        /// </summary>
        [BsonElement("tStatus")]
        [BsonDefaultValue(0)]
        public int Status { get; set; }
    }
}
