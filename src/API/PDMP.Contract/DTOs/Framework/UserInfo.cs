/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-10-24 11:31:04
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/

namespace PDMP.Contract
{
    /// <summary>
    /// 
    /// </summary>
    public class UserInfo : BaseInfo
    {
        #region 成员(Member)

        #region 用户
        private string _Name;
        /// <summary>
        /// 用户
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; OnPropertyChanged(); }
        }
        #endregion

        #region 密码
        /// <summary>
        /// Password
        /// </summary>
        private string _Password;
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get { return _Password; }
            set { _Password = value; OnPropertyChanged(); }
        }
        #endregion

        #region 邮箱
        private string _Email;
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email
        {
            get { return _Email; }
            set { _Email = value; OnPropertyChanged(); }
        }
        #endregion

        #region 手机号
        private string _PhoneNumber;
        /// <summary>
        /// 手机号
        /// </summary>
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; OnPropertyChanged(); }
        }
        #endregion

        #region 刷新Token
        private string _RefreshToken;
        /// <summary>
        /// 刷新Token
        /// </summary>
        public string RefreshToken
        {
            get { return _RefreshToken; }
            set { _RefreshToken = value; OnPropertyChanged(); }
        }
        #endregion

        #region 刷新Token 有效期
        private DateTime _RefreshTokenExpiryTime;
        /// <summary>
        /// 刷新Token 有效期
        /// </summary>
        public DateTime RefreshTokenExpiryTime
        {
            get { return _RefreshTokenExpiryTime; }
            set { _RefreshTokenExpiryTime = value; OnPropertyChanged(); }
        }
        #endregion
        #endregion
    }
}

