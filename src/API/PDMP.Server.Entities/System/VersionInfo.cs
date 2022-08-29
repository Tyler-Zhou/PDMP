using MongoDB.Bson.Serialization.Attributes;

namespace PDMP.Server.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class VersionInfo : BaseInfo
    {
        /// <summary>
        /// 
        /// </summary>
        [BsonElement("sFileMD5")]
        [BsonDefaultValue("")]
        public string MD5
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [BsonElement("iPubTime")]
        [BsonDefaultValue("")]
        public long PubTime
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [BsonElement("sVersion")]
        [BsonDefaultValue("")]
        public string Version
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        [BsonElement("sUrl")]
        [BsonDefaultValue("")]
        public string Url
        {
            get;
            set;
        }
        /// <summary>
        /// 
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
