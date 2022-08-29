using MongoDB.Bson.Serialization.Attributes;

namespace PDMP.Server.Entities
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserInfo : BaseInfo
    {
        /// <summary>
        /// 账号
        /// </summary>
        [BsonElement("sAccount")]
        [BsonDefaultValue("")]
        public string Account { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [BsonElement("sUserName")]
        [BsonDefaultValue("")]
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [BsonElement("sPassword")]
        [BsonDefaultValue("")]
        public string Password { get; set; }
    }
}
