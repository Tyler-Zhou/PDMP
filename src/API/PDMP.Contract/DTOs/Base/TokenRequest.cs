/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-10-29 1:30:18
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/

namespace PDMP.Contract
{
    /// <summary>
    /// 
    /// </summary>
    public class TokenRequest
    {
        #region 成员(Member)
        /// <summary>
        /// 访问令牌
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// 刷新访问令牌时的Token
        /// </summary>
        public string RefreshToken { get; set; }
        #endregion
    }
}

