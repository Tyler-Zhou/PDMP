/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-10-29 1:25:42
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/

namespace PDMP.Contract
{
    /// <summary>
    /// 登录信息
    /// </summary>
    public class LoginInfo
    {
        #region 成员(Member)
        /// <summary>
        /// 用户
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        #endregion
    }
}

