using PDMP.Client.Common;
using PDMP.Client.Core;
using PDMP.Client.Models;
using PDMP.Client.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace PDMP.Client.ViewModels
{
    /// <summary>
    /// 主窗体视图模型
    /// </summary>
    public class MainViewModel : BindableBase
    {
        #region 成员(Members)

        #region 程序标题
        /// <summary>
        /// ApplicationTitle
        /// </summary>
        private string _ApplicationTitle = "";
        /// <summary>
        /// 程序标题
        /// </summary>
        public string ApplicationTitle
        {
            get { return _ApplicationTitle; }
            set { _ApplicationTitle = value; RaisePropertyChanged(); }
        }

        private static readonly Brush primary = (Brush)Application.Current.Resources["Primary"];
        private static readonly Brush light = (Brush)Application.Current.Resources["Light"];
        private static readonly Brush dark = (Brush)Application.Current.Resources["Dark"];
        private static readonly Brush accent = (Brush)Application.Current.Resources["Accent"];
        #endregion

        #region 用户名
        /// <summary>
        /// UserName
        /// </summary>
        private string _UserName = "";
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; RaisePropertyChanged(); }
        }
        #endregion

        #region 主菜单集
        /// <summary>
        /// 
        /// </summary>
        private ObservableCollection<MenuItemModel> menuItems;
        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<MenuItemModel> MenuItems
        {
            get => menuItems;
            set
            {
                menuItems = value;
                RaisePropertyChanged("MenuItems");
            }
        }
        #endregion

        #region 当前菜单
        /// <summary>
        /// 
        /// </summary>
        private MenuItemModel currentMenuItem;
        /// <summary>
        /// 
        /// </summary>
        public MenuItemModel CurrentMenuItem
        {
            get => currentMenuItem;
            set
            {
                currentMenuItem = value;
                RaisePropertyChanged("CurrentMenuItem");
            }
        }
        #endregion

        #region 主题集
        /// <summary>
        /// 
        /// </summary>
        private ObservableCollection<ThemeColorModel> themeColors;
        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<ThemeColorModel> ThemeColors
        {
            get => themeColors;
            set
            {
                themeColors = value;
                RaisePropertyChanged("ThemeColors");
            }
        }
        #endregion
        #endregion

        #region 服务(Service)
        /// <summary>
        /// 容器提供服务
        /// </summary>
        private readonly IContainerProvider _ContainerProvider;
        /// <summary>
        /// 区域管理器
        /// </summary>
        private readonly IRegionManager _RegionManager;
        #endregion

        #region 命令(Command)
        /// <summary>
        /// 菜单导航命令
        /// </summary>
        public DelegateCommand<object> NavigateCommand { get; private set; }
        /// <summary>
        /// 更改主题命令
        /// </summary>
        public DelegateCommand<object> ChangeThemeColor { get; set; }

        /// <summary>
        /// 打开窗口命令
        /// </summary>
        public DelegateCommand<object> OpenAboutDialog { get; set; }
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 主窗体视图模型
        /// </summary>
        /// <param name="containerProvider"></param>
        /// <param name="regionManager"></param>
        /// <param name="eventAggregator"></param>
        public MainViewModel(IContainerProvider containerProvider, IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            ApplicationTitle = "个人数据操作系统";
            _ContainerProvider = containerProvider;
            _RegionManager = regionManager;


            MenuItems = new ObservableCollection<MenuItemModel>
            {
                new MenuItemModel
                {
                    IconName="",
                    DisplayName = "首页",
                    ViewName="IndexView"
                },
                new MenuItemModel
                {
                    IconName="",
                    DisplayName = "题目",
                    ViewName="TopicView",
                    Children=
                    {
                        new MenuItemModel
                        {
                            IconName="",
                            DisplayName = "单选题",
                            ViewName="RadioTopicView",
                        },
                    },
                },
            };
            CurrentMenuItem = MenuItems[0];

            ThemeColors = new ObservableCollection<ThemeColorModel>
            {
                new ThemeColorModel
                {
                    Name = "默认蓝",
                    Primary = primary,
                    Light = light,
                    Dark = dark,
                    Accent = accent,
                    IsSeleted =true
                },
                new ThemeColorModel
                {
                    Name = "酷安绿",
                    Primary = new SolidColorBrush(Color.FromRgb(0x0B,0xA3,0x61)),
                    Light = new SolidColorBrush(Color.FromRgb(0x56,0xD5,0x8F)),
                    Dark = new SolidColorBrush(Color.FromRgb(0x00,0x73,0x36)),
                    Accent = new SolidColorBrush(Color.FromRgb(0x79,0x55,0x48)),
                    IsSeleted =false
                },
                new ThemeColorModel
                {
                    Name = "姨妈红",
                    Primary = new SolidColorBrush(Color.FromRgb(0xE5,0x39,0x35)),
                    Light = new SolidColorBrush(Color.FromRgb(0xFF,0x6F,0x60)),
                    Dark = new SolidColorBrush(Color.FromRgb(0xAB,0x00,0x0D)),
                    Accent = new SolidColorBrush(Color.FromRgb(0x39,0x49,0xAB)),
                    IsSeleted =false
                },
                new ThemeColorModel
                {
                    Name = "基佬紫",
                    Primary = new SolidColorBrush(Color.FromRgb(0x6A,0x1B,0x9A)),
                    Light = new SolidColorBrush(Color.FromRgb(0x9C,0x4D,0xCC)),
                    Dark = new SolidColorBrush(Color.FromRgb(0x38,0x00,0x6B)),
                    Accent = new SolidColorBrush(Color.FromRgb(0xE6,0x51,0x00)),
                    IsSeleted =false
                },
                new ThemeColorModel
                {
                    Name = "哔哩粉",
                    Primary = new SolidColorBrush(Color.FromRgb(0xFB,0x72,0x99)),
                    Light = new SolidColorBrush(Color.FromRgb(0xFF,0xA4,0xCA)),
                    Dark = new SolidColorBrush(Color.FromRgb(0xC4,0x40,0x6B)),
                    Accent = new SolidColorBrush(Color.FromRgb(0x73,0xC9,0xE5)),
                    IsSeleted =false
                },
                new ThemeColorModel
                {
                    Name = "高端黑",
                    Primary = new SolidColorBrush(Color.FromRgb(0x26,0x32,0x38)),
                    Light = new SolidColorBrush(Color.FromRgb(0x4F,0x5B,0x62)),
                    Dark = new SolidColorBrush(Color.FromRgb(0x00,0x0A,0x12)),
                    Accent = new SolidColorBrush(Color.FromRgb(0xAD,0x14,0x57)),
                    IsSeleted =false
                },
            };

            NavigateCommand = new DelegateCommand<object>(Navigate);
            ChangeThemeColor = new DelegateCommand<object>(ChangeThemeColorExecute);
            OpenAboutDialog = new DelegateCommand<object>(OpenAboutDialogExecute);
        }
        #endregion

        #region 方法(Method)
        /// <summary>
        /// 导航
        /// </summary>
        /// <param name="obj">菜单对象</param>
        void Navigate(object obj)
        {
            string viewName= obj as string;
            if (viewName == null || string.IsNullOrWhiteSpace(viewName))
                return;
            _RegionManager?.Regions[PrismConstant.MAIN_VIEW_REGION_NAME].RequestNavigate(viewName);
        }
        /// <summary>
        /// 改编主题
        /// </summary>
        /// <param name="obj"></param>
        private void ChangeThemeColorExecute(object obj)
        {
            ThemeColorModel themeColor = obj as ThemeColorModel;

            if (themeColor.IsSeleted)
            {
                return;
            }

            App.Current.Resources["Primary"] = themeColor.Primary;
            App.Current.Resources["Light"] = themeColor.Light;
            App.Current.Resources["Dark"] = themeColor.Dark;
            App.Current.Resources["Accent"] = themeColor.Accent;

            foreach (var item in ThemeColors)
            {
                item.IsSeleted = false;
            }

            themeColor.IsSeleted = true;
        }
        /// <summary>
        /// 打开关于窗口
        /// </summary>
        /// <param name="obj"></param>
        private async void OpenAboutDialogExecute(object obj)
        {
            var content = new AboutView();
            await DialogService.Show(content, title: "关于");
        }
        #endregion
    }
}
