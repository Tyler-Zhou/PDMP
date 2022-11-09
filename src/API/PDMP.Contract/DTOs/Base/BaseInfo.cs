/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-10-24 11:22:05
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PDMP.Contract
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseInfo : INotifyPropertyChanged
    {
        #region 成员(Member)

        /// <summary>
        /// 唯一键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 属性更改事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region 公用方法(Public Method)
        /// <summary>
        /// 实现通知更新
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}

