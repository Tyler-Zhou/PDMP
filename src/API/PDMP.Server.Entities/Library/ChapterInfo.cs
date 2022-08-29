using MongoDB.Bson.Serialization.Attributes;

namespace PDMP.Server.Entities
{
    /// <summary>
    /// 章节实体
    /// </summary>
    public class ChapterInfo : BaseInfo
    {
        /// <summary>
        /// 书籍标识键
        /// </summary>
        [BsonElement("sBookKey")]
        [BsonDefaultValue("")]
        public string BookKey { get; set; }

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
        /// 内容
        /// </summary>
        [BsonElement("sContent")]
        [BsonDefaultValue("")]
        public string Content { get; set; }
        /// <summary>
        /// 链接
        /// </summary>
        [BsonElement("sLink")]
        [BsonDefaultValue("")]
        public string Link { get; set; }
        /// <summary>
        /// 排序索引
        /// </summary>
        [BsonElement("iOrderIndex")]
        [BsonDefaultValue(0)]
        public int OrderIndex { get; set; }

        #region 是否错误(章节)
        /// <summary>
        /// 是否错误(章节)
        /// </summary>
        [BsonElement("bIsError")]
        [BsonDefaultValue(false)]
        public bool IsError { get; set; } = false;
        #endregion
    }
}
