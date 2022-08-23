using Prism.Mvvm;
using System.Windows.Media;

namespace PDMP.Client.Models
{
    /// <summary>
    /// 主题颜色
    /// </summary>
    public class ThemeColorModel : BindableBase
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 基本
        /// </summary>
        public Brush Primary { get; set; }
        /// <summary>
        /// 亮色
        /// </summary>
        public Brush Light { get; set; }
        /// <summary>
        /// 暗色
        /// </summary>
        public Brush Dark { get; set; }
        /// <summary>
        /// 强调
        /// </summary>
        public Brush Accent { get; set; }
        /// <summary>
        /// 是否选择
        /// </summary>
        private bool _IsSeleted;
        /// <summary>
        /// 是否选择
        /// </summary>
        public bool IsSeleted
        {
            get => _IsSeleted;
            set
            {
                _IsSeleted = value;
                RaisePropertyChanged("IsSeleted");
            }
        }
    }
}
