using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Effects;

namespace PDMP.Client.Core
{
    /// <summary>
    /// 卡片
    /// </summary>
    public class Card : ContentControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Card"/> class.
        /// </summary>
        static Card()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Card), new FrameworkPropertyMetadata(typeof(Card)));
        }

        /// <summary>
        /// 圆角半径
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
              "CornerRadius", typeof(CornerRadius), typeof(Card), new PropertyMetadata(default(CornerRadius)));

        /// <summary>
        /// 圆角半径
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        /// <summary>
        /// 边框阴影
        /// </summary>
        public static readonly DependencyProperty BorderEffectProperty = DependencyProperty.Register(
             "BorderEffect", typeof(Effect), typeof(Card), new PropertyMetadata(default(Effect)));

        /// <summary>
        /// 边框阴影
        /// </summary>
        public Effect BorderEffect
        {
            get { return (Effect)GetValue(BorderEffectProperty); }
            set { SetValue(BorderEffectProperty, value); }
        }

    }
}
