using PDMP.Client.Core;
using PDMP.Client.Core.Settings;
using PDMP.Library.Client.Common;
using PDMP.Library.Client.Models;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;

namespace PDMP.Library.Client.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class TopicViewModel : BindableBase
    {
        #region 成员(Members)
        /// <summary>
        /// 是否启用缓存
        /// </summary>
        bool _IsEnableCache { get; set; } = true;

        #region 题型
        /// <summary>
        /// 题型
        /// </summary>
        private EnumTopicType _TopicType = EnumTopicType.Radio;
        #endregion

        #region 单选题目集合
        private ObservableCollection<RadioTopicModel> _RadioTopics = new ObservableCollection<RadioTopicModel>();
        /// <summary>
        /// 单选题目集合
        /// </summary>
        public ObservableCollection<RadioTopicModel> RadioTopics
        {
            get { return _RadioTopics; }
            set { _RadioTopics = value; RaisePropertyChanged(); }
        }
        #endregion

        #region 当前编辑单选题
        private RadioTopicModel _CurrentRadio = new RadioTopicModel();
        /// <summary>
        /// 当前编辑单选题
        /// </summary>
        public RadioTopicModel CurrentRadio
        {
            get { return _CurrentRadio; }
            set { _CurrentRadio = value; RaisePropertyChanged(); }
        }
        #endregion
        #endregion 成员(Members)

        #region 服务(Service)
        /// <summary>
        /// 设置服务
        /// </summary>
        ISettingService _SettingService;
        /// <summary>
        /// 缓存服务
        /// </summary>
        ICacheService _CacheService;
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 
        /// </summary>
        public TopicViewModel(ISettingService settingService, ICacheService cacheService)
        {
            _SettingService = settingService;
            _CacheService = cacheService;
            InitData();
        }
        #endregion 构造函数(Constructor)

        #region 方法(Method)
        private async void InitData()
        {
            try
            {
                var setting = await _SettingService.GetAsync<ApplicationSetting>("Setting");
                if (setting != null)
                    _IsEnableCache= setting.Cache.IsEnableTopicCache;
                try
                {
                    if (_IsEnableCache)
                    {
                        RadioTopics = await _CacheService.GetAsync<ObservableCollection<RadioTopicModel>>("RadioData");
                    }
                    else
                    {
                        //TODO:从服务端获取题目
                    }
                }
                catch (Exception ex)
                {

                }
                if (RadioTopics == null)
                {
                    RadioTopics = new ObservableCollection<RadioTopicModel>()
                                {
                                    new RadioTopicModel
                                            {
                                                Id = "010101001",
                                                ErrorMessage = "",
                                                Stem = "<p>（ ）被誉为“现代电子计算机之父”。</p>",
                                                Answer = "D",
                                                ItemA = "<p>A．查尔斯·巴贝</p>",
                                                ItemB = "<p>B．阿塔诺索夫</p>",
                                                ItemC = "<p>C．图灵</p>",
                                                ItemD = "<p>D．冯·诺依曼</p>",
                                                TopicType = EnumTopicType.Radio,
                                            },
                                };
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
