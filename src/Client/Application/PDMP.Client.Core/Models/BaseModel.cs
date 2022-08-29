using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PDMP.Client.Core.Models
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseModel : INotifyPropertyChanged
    {
        /// <summary>
        /// 唯一键
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 属性更改事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 实现通知更新
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
