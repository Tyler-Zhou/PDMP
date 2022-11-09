/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-10-24 17:02:35
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/

namespace PDMP.Contract
{
    /// <summary>
    /// 
    /// </summary>
    public class PermissionInfo : BaseInfo
    {
        #region 成员(Member)

        #region 名称
        /// <summary>
        /// Name
        /// </summary>
        private string _Name;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; OnPropertyChanged(); }
        }
        #endregion

        #endregion
    }
}

