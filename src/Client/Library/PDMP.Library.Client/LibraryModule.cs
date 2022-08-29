using PDMP.Library.Client.Interfaces;
using PDMP.Library.Client.Services;
using PDMP.Library.Client.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace PDMP.Library.Client
{
    /// <summary>
    /// 图书馆模块
    /// </summary>
    [Module(ModuleName = "Library")]
    public class LibraryModule : IModule
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="containerProvider"></param>
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }
        /// <summary>
        /// 注入 服务、视图/视图模型
        /// </summary>
        /// <param name="containerRegistry"></param>
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Service
            containerRegistry.Register<ITopicService, TopicService>();
            //containerRegistry.Register<IBookService, BookService>();
            //containerRegistry.Register<IChapterService, ChapterService>();
            //View & ViewModel
            containerRegistry.RegisterForNavigation<TopicView>();
            containerRegistry.RegisterForNavigation<RadioTopicView>();
            //containerRegistry.RegisterForNavigation<BookView>();
            //containerRegistry.RegisterForNavigation<ChapterView>();
        }
    }
}
