using Newtonsoft.Json;
using PDMP.Client.Core.Models;
using PDMP.Library.Client.Common;

namespace PDMP.Library.Client.Models
{
    /// <summary>
    /// 题目
    /// </summary>
    public abstract class BaseTopicModel: BaseModel
    {
        #region 题型
        private EnumTopicType _TopicType = EnumTopicType.UnKnown;

        /// <summary>
        ///  题型
        /// </summary>
        [JsonProperty("TopicType")]
        public virtual EnumTopicType TopicType
        {
            get { return _TopicType; }
            set { _TopicType = value; OnPropertyChanged(); }
        }
        #endregion

        #region 题干

        private string _Stem = string.Empty;
        /// <summary>
        /// 题干
        /// </summary>
        [JsonProperty("Stem")]
        public string Stem
        {
            get { return _Stem; }
            set { _Stem = value; OnPropertyChanged(); }
        }
        #endregion

        #region 错误信息

        private string _ErrorMessage = string.Empty;
        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonProperty("ErrorMessage")]
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; OnPropertyChanged(); }
        }
        #endregion

        #region 答案

        private string _Answer = string.Empty;
        /// <summary>
        /// 答案
        /// </summary>
        [JsonProperty("Answer")]
        public string Answer
        {
            get { return _Answer; }
            set { _Answer = value; OnPropertyChanged(); }
        }
        #endregion
    }
}
