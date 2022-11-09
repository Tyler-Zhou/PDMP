/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-10-28 18:13:42
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/

namespace PDMP.Contract
{
    /// <summary>
    /// 
    /// </summary>
    public class TokenResponse
    {
        #region 成员(Member)
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 访问令牌
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// 刷新访问令牌时的Token
        /// </summary>
        public string RefreshToken { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime Expiration { get; set; }
        /// <summary>
        /// token 类型
        /// </summary>
        public string TokenType { get; set; } = "Bearer";

        /// <summary>
        /// 可访问资源范围
        /// </summary>
        public string Scope { get; set; }
        #endregion
    }
}

