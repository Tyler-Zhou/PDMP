using Prism.Regions;
using System.Collections.ObjectModel;

namespace PDMP.Client.Models
{
    /// <summary>
    /// 菜单项
    /// </summary>
    public class MenuItemModel
    {
        #region 成员(Member)
        #region 图标
        /// <summary>
        /// 图标
        /// </summary>
        public string IconName { get; set; }
        #endregion

        #region 名称
        /// <summary>
        /// 名称
        /// </summary>
        public string DisplayName { get; set; }
        #endregion

        #region 内容
        /// <summary>
        /// 内容
        /// </summary>
        public string ViewName { get; set; }
        #endregion

        #region 导航参数
        /// <summary>
        /// 导航参数
        /// </summary>
        public NavigationParameters NavigationParams { get; set; }
        #endregion

        #region 子项
        /// <summary>
        /// 子项
        /// </summary>
        public ObservableCollection<MenuItemModel> Children { get; set; }
        #endregion
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 
        /// </summary>
        public MenuItemModel()
        {
            NavigationParams = new NavigationParameters();
            Children = new ObservableCollection<MenuItemModel>();
        } 
        #endregion
    }
}
