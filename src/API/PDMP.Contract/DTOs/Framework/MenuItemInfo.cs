/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-10-27 13:39:57
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/

namespace PDMP.Contract
{
    /// <summary>
    /// 
    /// </summary>
    public class MenuItemInfo : BaseInfo
    {
        #region 成员(Member)

        #region 上级Id
        private long _ParentId;
        /// <summary>
        /// 上级Id
        /// </summary>
        public long ParentId
        {
            get { return _ParentId; }
            set { _ParentId = value; OnPropertyChanged(); }
        }
        #endregion

        #region 名称
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

        #region 命令
        private string _Command;
        /// <summary>
        /// 命令
        /// </summary>
        public string Command
        {
            get { return _Command; }
            set { _Command = value; OnPropertyChanged(); }
        }
        #endregion

        #region 命令参数
        private string _CommandParameter;
        /// <summary>
        /// 命令参数
        /// </summary>
        public string CommandParameter
        {
            get { return _CommandParameter; }
            set { _CommandParameter = value; OnPropertyChanged(); }
        }
        #endregion

        #region 子菜单
        ICollection<MenuItemInfo> _Children;
        /// <summary>
        /// 子菜单
        /// </summary>
        public ICollection<MenuItemInfo> Children
        {
            get { return _Children; }
            set { _Children = value; OnPropertyChanged(); }
        } 
        #endregion

        #endregion
    }
}

