using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace PDMP.Client.Core
{
    /// <summary>
    /// 转换动画
    /// </summary>
    public class Transition : ContentControl
    {
        static Transition()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Transition), new FrameworkPropertyMetadata(typeof(Transition)));
        }

        /// <inheritdoc/>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Loaded += Transition_Loaded;
        }

        private void Transition_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsShow)
            {
                ShowAnimation(this);
            }
        }

        #region 命令

        /// <summary>
        /// 显示完命令
        /// </summary>
        public static readonly DependencyProperty ShowedCommandProperty =
            DependencyProperty.Register("ShowedCommand", typeof(ICommand), typeof(Transition), new PropertyMetadata(default(ICommand)));

        /// <summary>
        /// 显示完命令
        /// </summary>
        public ICommand ShowedCommand
        {
            get { return (ICommand)GetValue(ShowedCommandProperty); }
            set { SetValue(ShowedCommandProperty, value); }
        }

        /// <summary>
        /// 关闭完命令
        /// </summary>
        public static readonly DependencyProperty ClosedCommandProperty =
            DependencyProperty.Register("ClosedCommand", typeof(ICommand), typeof(Transition), new PropertyMetadata(default(ICommand)));

        /// <summary>
        /// 关闭完命令
        /// </summary>
        public ICommand ClosedCommand
        {
            get { return (ICommand)GetValue(ClosedCommandProperty); }
            set { SetValue(ClosedCommandProperty, value); }
        }

        #endregion 命令

        #region 事件

        /// <summary>
        /// 显示完事件
        /// </summary>
        public static readonly RoutedEvent ShowedEvent =
           EventManager.RegisterRoutedEvent("Showed", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Transition));

        /// <summary>
        /// 显示完事件 handler
        /// </summary>
        public event RoutedEventHandler Showed
        {
            add { AddHandler(ShowedEvent, value); }
            remove { RemoveHandler(ShowedEvent, value); }
        }

        /// <summary>
        /// 关闭完事件
        /// </summary>
        public static readonly RoutedEvent ClosedEvent =
          EventManager.RegisterRoutedEvent("Closed", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Transition));

        /// <summary>
        /// 关闭完事件 handler
        /// </summary>
        public event RoutedEventHandler Closed
        {
            add { AddHandler(ClosedEvent, value); }
            remove { RemoveHandler(ClosedEvent, value); }
        }

        #endregion 事件

        #region 依赖属性

        /// <summary>
        /// 是否显示
        /// </summary>
        public static readonly DependencyProperty IsShowProperty =
            DependencyProperty.Register("IsShow", typeof(bool), typeof(Transition), new PropertyMetadata(default(bool), OnIsShwowChanged));

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow
        {
            get { return (bool)GetValue(IsShowProperty); }
            set { SetValue(IsShowProperty, value); }
        }

        /// <summary>
        /// 转换类型
        /// </summary>
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(TransitionType), typeof(Transition), new PropertyMetadata(default(TransitionType)));

        /// <summary>
        /// 转换类型
        /// </summary>
        public TransitionType Type
        {
            get { return (TransitionType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        /// <summary>
        /// 动画时长
        /// </summary>
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(Duration), typeof(Transition), new PropertyMetadata(default(Duration)));

        /// <summary>
        /// 动画时长
        /// </summary>
        public Duration Duration
        {
            get { return (Duration)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        /// <summary>
        /// 动画位置偏移
        /// </summary>
        public static readonly DependencyProperty OffsetProperty =
            DependencyProperty.Register("Offset", typeof(double), typeof(Transition), new PropertyMetadata(default(double)));

        /// <summary>
        /// 动画位置偏移
        /// </summary>
        public double Offset
        {
            get { return (double)GetValue(OffsetProperty); }
            set { SetValue(OffsetProperty, value); }
        }

        /// <summary>
        /// 动画初始缩放
        /// </summary>
        public static readonly DependencyProperty InitialScaleProperty =
            DependencyProperty.Register("InitialScale", typeof(double), typeof(Transition), new PropertyMetadata(default(double)));

        /// <summary>
        /// 动画初始缩放
        /// </summary>
        public double InitialScale
        {
            get { return (double)GetValue(InitialScaleProperty); }
            set { SetValue(InitialScaleProperty, value); }
        }

        /// <summary>
        /// 显示缓动
        /// </summary>
        public static readonly DependencyProperty ShowEasingFunctionProperty =
            DependencyProperty.Register("ShowEasingFunction", typeof(IEasingFunction), typeof(Transition), new PropertyMetadata(default(IEasingFunction)));

        /// <summary>
        /// 显示缓动
        /// </summary>
        public IEasingFunction ShowEasingFunction
        {
            get { return (IEasingFunction)GetValue(ShowEasingFunctionProperty); }
            set { SetValue(ShowEasingFunctionProperty, value); }
        }

        /// <summary>
        /// 关闭缓动
        /// </summary>
        public static readonly DependencyProperty CloseEasingFunctionProperty =
            DependencyProperty.Register("CloseEasingFunction", typeof(IEasingFunction), typeof(Transition), new PropertyMetadata(default(IEasingFunction)));

        /// <summary>
        /// 关闭缓动
        /// </summary>
        public IEasingFunction CloseEasingFunction
        {
            get { return (IEasingFunction)GetValue(CloseEasingFunctionProperty); }
            set { SetValue(CloseEasingFunctionProperty, value); }
        }

        #endregion 依赖属性

        private static void OnIsShwowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Transition transition && transition.IsLoaded)
            {
                if (transition.IsShow)
                {
                    ShowAnimation(transition);
                }
                else
                {
                    CloseAnimation(transition);
                }
            }
        }

        private static void ShowAnimation(Transition transition)
        {
            Storyboard storyboard = new Storyboard();
            storyboard.Completed += (sender, e) =>
            {
                var args = new RoutedEventArgs();
                args.RoutedEvent = Transition.ShowedEvent;
                transition.RaiseEvent(args);
                transition.ShowedCommand?.Execute(null);
            };

            DoubleAnimation opacityAnimation = GetOpacityAnimation(transition, 0, 1, transition.ShowEasingFunction);

            switch (transition.Type)
            {
                case TransitionType.Fade:
                case TransitionType.CollapseUp:
                case TransitionType.CollapseDown:
                case TransitionType.CollapseLeft:
                case TransitionType.CollapseRight:
                default:
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.FadeLeft:
                    DoubleAnimation translateAnimation = GetTranslateXAnimation(transition, transition.Offset, 0, transition.ShowEasingFunction);
                    storyboard.Children.Add(translateAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.FadeRight:
                    translateAnimation = GetTranslateXAnimation(transition, -transition.Offset, 0, transition.ShowEasingFunction);
                    storyboard.Children.Add(translateAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.FadeUp:
                    translateAnimation = GetTranslateYAnimation(transition, transition.Offset, 0, transition.ShowEasingFunction);
                    storyboard.Children.Add(translateAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.FadeDown:
                    translateAnimation = GetTranslateYAnimation(transition, -transition.Offset, 0, transition.ShowEasingFunction);
                    storyboard.Children.Add(translateAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.Zoom:
                    DoubleAnimation scaleXAnimation = GetScaleXAnimation(transition, transition.InitialScale, 1, transition.ShowEasingFunction);
                    DoubleAnimation scaleYAnimation = GetScaleYAnimation(transition, transition.InitialScale, 1, transition.ShowEasingFunction);
                    storyboard.Children.Add(scaleXAnimation);
                    storyboard.Children.Add(scaleYAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.ZoomX:
                    scaleXAnimation = GetScaleXAnimation(transition, transition.InitialScale, 1, transition.ShowEasingFunction);
                    storyboard.Children.Add(scaleXAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.ZoomY:
                    scaleYAnimation = GetScaleYAnimation(transition, transition.InitialScale, 1, transition.ShowEasingFunction);
                    storyboard.Children.Add(scaleYAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.ZoomLeft:
                    transition.RenderTransformOrigin = new Point(0, 0.5);
                    scaleXAnimation = GetScaleXAnimation(transition, transition.InitialScale, 1, transition.ShowEasingFunction);
                    storyboard.Children.Add(scaleXAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.ZoomRight:
                    transition.RenderTransformOrigin = new Point(1, 0.5);
                    scaleXAnimation = GetScaleXAnimation(transition, transition.InitialScale, 1, transition.ShowEasingFunction);
                    storyboard.Children.Add(scaleXAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.ZoomUp:
                    transition.RenderTransformOrigin = new Point(0.5, 0);
                    scaleYAnimation = GetScaleYAnimation(transition, transition.InitialScale, 1, transition.ShowEasingFunction);
                    storyboard.Children.Add(scaleYAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.ZoomDown:
                    transition.RenderTransformOrigin = new Point(0.5, 1);
                    scaleYAnimation = GetScaleYAnimation(transition, transition.InitialScale, 1, transition.ShowEasingFunction);
                    storyboard.Children.Add(scaleYAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;
            }

            storyboard.Begin();
        }

        private static void CloseAnimation(Transition transition)
        {
            Storyboard storyboard = new Storyboard();
            storyboard.Completed += (sender, e) =>
            {
                var args = new RoutedEventArgs();
                args.RoutedEvent = Transition.ClosedEvent;
                transition.RaiseEvent(args);
                transition.ClosedCommand?.Execute(null);
            };

            DoubleAnimation opacityAnimation = GetOpacityAnimation(transition, 1, 0, transition.CloseEasingFunction);

            switch (transition.Type)
            {
                case TransitionType.Fade:
                case TransitionType.CollapseUp:
                case TransitionType.CollapseDown:
                case TransitionType.CollapseLeft:
                case TransitionType.CollapseRight:
                default:
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.FadeLeft:
                    DoubleAnimation translateAnimation = GetTranslateXAnimation(transition, 0, transition.Offset, transition.CloseEasingFunction);
                    storyboard.Children.Add(translateAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.FadeRight:
                    translateAnimation = GetTranslateXAnimation(transition, 0, -transition.Offset, transition.CloseEasingFunction);
                    storyboard.Children.Add(translateAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.FadeUp:
                    translateAnimation = GetTranslateYAnimation(transition, 0, transition.Offset, transition.CloseEasingFunction);
                    storyboard.Children.Add(translateAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.FadeDown:
                    translateAnimation = GetTranslateYAnimation(transition, 0, -transition.Offset, transition.CloseEasingFunction);
                    storyboard.Children.Add(translateAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.Zoom:
                    DoubleAnimation scaleXAnimation = GetScaleXAnimation(transition, 1, transition.InitialScale, transition.CloseEasingFunction);
                    DoubleAnimation scaleYAnimation = GetScaleYAnimation(transition, 1, transition.InitialScale, transition.CloseEasingFunction);
                    storyboard.Children.Add(scaleXAnimation);
                    storyboard.Children.Add(scaleYAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.ZoomX:
                    scaleXAnimation = GetScaleXAnimation(transition, 1, transition.InitialScale, transition.CloseEasingFunction);
                    storyboard.Children.Add(scaleXAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.ZoomY:
                    scaleYAnimation = GetScaleYAnimation(transition, 1, transition.InitialScale, transition.CloseEasingFunction);
                    storyboard.Children.Add(scaleYAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.ZoomLeft:
                    transition.RenderTransformOrigin = new Point(0, 0.5);
                    scaleXAnimation = GetScaleXAnimation(transition, 1, transition.InitialScale, transition.CloseEasingFunction);
                    storyboard.Children.Add(scaleXAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.ZoomRight:
                    transition.RenderTransformOrigin = new Point(1, 0.5);
                    scaleXAnimation = GetScaleXAnimation(transition, 1, transition.InitialScale, transition.CloseEasingFunction);
                    storyboard.Children.Add(scaleXAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.ZoomUp:
                    transition.RenderTransformOrigin = new Point(0.5, 0);
                    scaleYAnimation = GetScaleYAnimation(transition, 1, transition.InitialScale, transition.CloseEasingFunction);
                    storyboard.Children.Add(scaleYAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;

                case TransitionType.ZoomDown:
                    transition.RenderTransformOrigin = new Point(0.5, 1);
                    scaleYAnimation = GetScaleYAnimation(transition, 1, transition.InitialScale, transition.CloseEasingFunction);
                    storyboard.Children.Add(scaleYAnimation);
                    storyboard.Children.Add(opacityAnimation);
                    break;
            }

            storyboard.Begin();
        }

        private static DoubleAnimation GetOpacityAnimation(Transition transition, double from, double to, IEasingFunction easing)
        {
            DoubleAnimation opacityAnimation = new DoubleAnimation()
            {
                From = from,
                To = to,
                Duration = transition.Duration,
                EasingFunction = easing,
            };

            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath(OpacityProperty));
            Storyboard.SetTarget(opacityAnimation, transition);
            return opacityAnimation;
        }

        private static DoubleAnimation GetTranslateXAnimation(Transition transition, double from, double to, IEasingFunction easing)
        {
            DoubleAnimation transformAnimation = new DoubleAnimation()
            {
                From = from,
                To = to,
                Duration = transition.Duration,
                EasingFunction = easing,
            };

            Storyboard.SetTargetProperty(transformAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)"));
            Storyboard.SetTarget(transformAnimation, transition);
            return transformAnimation;
        }

        private static DoubleAnimation GetTranslateYAnimation(Transition transition, double from, double to, IEasingFunction easing)
        {
            DoubleAnimation transformAnimation = new DoubleAnimation()
            {
                From = from,
                To = to,
                Duration = transition.Duration,
                EasingFunction = easing,
            };

            Storyboard.SetTargetProperty(transformAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)"));
            Storyboard.SetTarget(transformAnimation, transition);
            return transformAnimation;
        }

        private static DoubleAnimation GetScaleXAnimation(Transition transition, double from, double to, IEasingFunction easing)
        {
            DoubleAnimation transformAnimation = new DoubleAnimation()
            {
                From = from,
                To = to,
                Duration = transition.Duration,
                EasingFunction = easing,
            };

            Storyboard.SetTargetProperty(transformAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
            Storyboard.SetTarget(transformAnimation, transition);
            return transformAnimation;
        }

        private static DoubleAnimation GetScaleYAnimation(Transition transition, double from, double to, IEasingFunction easing)
        {
            DoubleAnimation transformAnimation = new DoubleAnimation()
            {
                From = from,
                To = to,
                Duration = transition.Duration,
                EasingFunction = easing,
            };

            Storyboard.SetTargetProperty(transformAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)"));
            Storyboard.SetTarget(transformAnimation, transition);
            return transformAnimation;
        }
    }

    /// <summary>
    /// 转换类型
    /// </summary>
    public enum TransitionType
    {
        /// <summary>
        /// 淡入
        /// </summary>
        Fade = 0,

        /// <summary>
        /// 向左淡入
        /// </summary>
        FadeLeft,

        /// <summary>
        /// 向右淡入
        /// </summary>
        FadeRight,

        /// <summary>
        /// 向上淡入
        /// </summary>
        FadeUp,

        /// <summary>
        /// 向下淡入
        /// </summary>
        FadeDown,

        /// <summary>
        /// 缩放
        /// </summary>
        Zoom,

        /// <summary>
        /// X 轴缩放
        /// </summary>
        ZoomX,

        /// <summary>
        /// Y 轴缩放
        /// </summary>
        ZoomY,

        /// <summary>
        /// 向左缩放
        /// </summary>
        ZoomLeft,

        /// <summary>
        /// 向右缩放
        /// </summary>
        ZoomRight,

        /// <summary>
        /// 向上缩放
        /// </summary>
        ZoomUp,

        /// <summary>
        /// 向下缩放
        /// </summary>
        ZoomDown,

        /// <summary>
        /// 向上折叠
        /// </summary>
        CollapseUp,

        /// <summary>
        /// 向下折叠
        /// </summary>
        CollapseDown,

        /// <summary>
        /// 向左折叠
        /// </summary>
        CollapseLeft,

        /// <summary>
        /// 向右折叠
        /// </summary>
        CollapseRight,
    }
}