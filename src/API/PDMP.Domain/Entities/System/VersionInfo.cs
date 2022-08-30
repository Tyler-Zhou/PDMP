using MongoDB.Bson.Serialization.Attributes;

namespace PDMP.Domain.Entities
{
    /// <summary>
    /// 版本实体
    /// </summary>
    public class VersionInfo : BaseInfo
    {
        /// <summary>
        /// 文件MD5值
        /// </summary>
        [BsonElement("sFileMD5")]
        [BsonDefaultValue("")]
        public string FileMD5
        {
            get;
            set;
        }
        /// <summary>
        /// 发布时间
        /// </summary>
        [BsonElement("iPubTime")]
        [BsonDefaultValue("")]
        public long PubTime
        {
            get;
            set;
        }
        /// <summary>
        /// 版本号
        /// </summary>
        [BsonElement("sVersion")]
        [BsonDefaultValue("")]
        public string Version
        {
            get;
            set;
        }
        /// <summary>
        /// 网页地址
        /// </summary>
        [BsonElement("sUrl")]
        [BsonDefaultValue("")]
        public string Url
        {
            get;
            set;
        }
        /// <summary>
        /// 更新包名称
        /// </summary>
        [BsonElement("sName")]
        [BsonDefaultValue("")]
        public string Name
        {
            get;
            set;
        }
    }
}
