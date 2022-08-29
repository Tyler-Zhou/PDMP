using PDMP.Game.ProgressQuest.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace PDMP.Game.ProgressQuest
{
    /// <summary>
    /// 无限进度条
    /// </summary>
    [Module(ModuleName = "Game.ProgressQuest")]
    public class ProgressQuestModule : IModule
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
            //View & ViewModel
            containerRegistry.RegisterForNavigation<ProgressQuestView>();
        }
    }
}
