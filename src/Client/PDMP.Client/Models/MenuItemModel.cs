using Prism.Regions;

namespace PDMP.Client.Models
{
    /// <summary>
    /// 菜单项
    /// </summary>
    public class MenuItemModel
    {
        /// <summary>
        /// 图标
        /// </summary>
        public string IconName { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string ViewName { get; set; }

        /// <summary>
        /// NavigationParams
        /// </summary>
        private NavigationParameters _NavigationParams = new NavigationParameters();
        /// <summary>
        /// 导航参数
        /// </summary>
        public NavigationParameters NavigationParams
        {
            get { return _NavigationParams; }
            set { _NavigationParams = value; }
        }
    }
}
