using Newtonsoft.Json;
using PDMP.Library.Client.Common;

namespace PDMP.Library.Client.Models
{
    /// <summary>
    /// 判断题
    /// </summary>
    public class JudgeTopicModel: BaseTopicModel
    {
        #region 题型
        private EnumTopicType _TopicType = EnumTopicType.UnKnown;
        /// <summary>
        ///  题型
        /// </summary>
        [JsonProperty("TopicType")]
        public override EnumTopicType TopicType
        {
            get { return EnumTopicType.Judge; }
            set { _TopicType = value; OnPropertyChanged(); }
        }
        #endregion

        #region 选项A

        private string _ItemA = string.Empty;
        /// <summary>
        /// 选项A
        /// </summary>
        [JsonProperty("ItemA")]
        public string ItemA
        {
            get { return _ItemA; }
            set { _ItemA = value; OnPropertyChanged(); }
        }
        #endregion

        #region 选项B

        private string _ItemB = string.Empty;
        /// <summary>
        /// 选项B
        /// </summary>
        [JsonProperty("ItemB")]
        public string ItemB
        {
            get { return _ItemB; }
            set { _ItemB = value; OnPropertyChanged(); }
        }
        #endregion
    }
}
