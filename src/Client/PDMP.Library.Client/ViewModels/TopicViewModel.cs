using PDMP.Client.Core;
using PDMP.Library.Client.Common;
using PDMP.Library.Client.Models;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace PDMP.Library.Client.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class TopicViewModel : BindableBase
    {
        #region 成员(Members)
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

        #region 构造函数(Constructor)
        /// <summary>
        /// 
        /// </summary>
        public TopicViewModel()
        {
            InitData();
        }
        #endregion 构造函数(Constructor)

        #region 方法(Method)
        private void InitData()
        {
            try
            {
                string appPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                string jsonfile = Path.Combine(appPath, "TopicData.json");

                string json = File.ReadAllText(jsonfile);

                TopicData data = JsonSerializerHelper.DeserializeObject<TopicData>(json);

                RadioTopics = data.RadioTopics;
            }
            catch (Exception ex)
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
        #endregion
    }
}
