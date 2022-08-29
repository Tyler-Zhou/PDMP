using DryIoc;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using PDMP.Client.Core;
using PDMP.Client.Core.Settings;
using PDMP.Client.Helpers;
using PDMP.Client.ViewModels;
using PDMP.Client.Views;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace PDMP.Client
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : PrismApplication
    {
        #region 成员(Member)
        /// <summary>
        /// 
        /// </summary>
        Mutex mutex;
        #endregion

        #region 服务(Service)
        /// <summary>
        /// 日志服务
        /// </summary>
        ILogger _Logger;
        #endregion

        #region 事件(Event)
        /// <summary>
        /// 调度程序未处理的异常
        /// </summary>
        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            //通常全局异常捕捉的都是致命信息
            _Logger.LogCritical($"{e.Exception.StackTrace},{e.Exception.Message}");
        }

        /// <summary>
        /// 未观察到的任务异常
        /// </summary>
        private void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            _Logger.LogCritical($"{e.Exception.StackTrace},{e.Exception.Message}");
        }
        /// <summary>
        /// 未处理的异常
        /// </summary>
        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            if (ex != null)
                _Logger.LogCritical($"{ex.StackTrace},{ex.Message}");

            //记录dump文件
            DumpHelper.TryDump($"dumps\\PDMP_{DateTime.Now.ToString("HH-mm-ss-ms")}.dmp");
        }
        #endregion

        #region 重写方法(Override)
        /// <summary>
        /// 创建外壳
        /// </summary>
        /// <returns></returns>
        protected override Window CreateShell()
        {
            bool createNew;
            mutex = new Mutex(true, "pdOS", out createNew);
            if (!createNew)
                Environment.Exit(0);
            //UI线程未捕获异常处理事件
            DispatcherUnhandledException += OnDispatcherUnhandledException;
            //Task线程内未捕获异常处理事件
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
            //多线程异常
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            return Container.Resolve<MainView>();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected override async void OnInitialized()
        {
            base.OnInitialized();
            var settingService = Container.Resolve<ISettingService>();
            if(await settingService.GetAsync<ApplicationSetting>("Setting") == null)
            {
                await settingService.SaveAsync("Setting",new ApplicationSetting());
            }
        }
        /// <summary>
        /// 加载模块
        /// </summary>
        /// <returns></returns>
        protected override IModuleCatalog CreateModuleCatalog()
        {
            //指定模块加载方式为从文件夹中以反射发现并加载module(推荐用法)
            return new DirectoryModuleCatalog() { ModulePath = @".\Modules" };
        }
        /// <summary>
        /// 注册 (视图、视图模型、服务)
        /// </summary>
        /// <param name="containerRegistry"></param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var factory = new NLogLoggerFactory();
            _Logger = factory.CreateLogger("PDMP");


            //注入到Prism DI容器中
            containerRegistry.RegisterInstance(_Logger);

            //serviceKey

            //Service
            containerRegistry.Register<ISettingService, SettingService>();
            containerRegistry.Register<ICacheService, CacheService>();

            //View & ViewModel
            containerRegistry.RegisterForNavigation<IndexView, IndexViewModel>();

        }
        #endregion

        #region 方法(Method)
        #region 程序更新(Application Update)

        #endregion
        #endregion
    }
}
