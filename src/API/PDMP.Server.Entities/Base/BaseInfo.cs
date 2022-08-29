using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PDMP.Server.Entities
{
    /// <summary>
    /// 实体基类
    /// </summary>
    public class BaseInfo
    {
        /// <summary>
        /// 唯一键
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public Guid Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("tCreateDate")]
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("tUpdateDate")]
        public DateTime? UpdateDate { get; set; }
    }
}
