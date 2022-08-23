using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace PDMP.Library.Client.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class TopicData
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("RadioTopics")]
        public ObservableCollection<RadioTopicModel> RadioTopics { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("MultipleChoiceTopics")]
        public ObservableCollection<MultipleChoiceTopicModel> MultipleChoiceTopics { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("JudgeTopics")]
        public ObservableCollection<JudgeTopicModel> JudgeTopics { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("ShortAnswerTopics")]
        public ObservableCollection<ShortAnswerTopicModel> ShortAnswerTopics { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TopicData()
        {
            RadioTopics = new ObservableCollection<RadioTopicModel>();
            MultipleChoiceTopics = new ObservableCollection<MultipleChoiceTopicModel>();
            JudgeTopics = new ObservableCollection<JudgeTopicModel>();
            ShortAnswerTopics = new ObservableCollection<ShortAnswerTopicModel>();
        }
    }
}
