using PDMP.Client.Core;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PDMP.Client.Converters
{
    /// <summary>
    /// 
    /// </summary>
    public class IsSelectedToIconConverter : IValueConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return new Icon { Type = IconType.CheckboxFill };
            }
            else
            {
                return new Icon { Type = IconType.CheckboxBlankFill };
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
