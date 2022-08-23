using PDMP.Client.Core;
using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace PDMP.Client.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : ApplicationWindow
    {
        public MainView()
        {
            InitializeComponent();
            Loaded += MainView_Loaded;
        }

        private void MainView_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void BlackSwitch_Click(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton toggleButton)
            {
                Color lightForegroundColor = (Color)Application.Current.FindResource("LightForegroundColor");
                Color lightBackgroundColor = (Color)Application.Current.FindResource("LightBackgroundColor");
                Color darkForegroundColor = (Color)Application.Current.FindResource("DarkForegroundColor");
                Color darkBackgroundColor = (Color)Application.Current.FindResource("DarkBackgroundColor");

                var background = new SolidColorBrush(toggleButton.IsChecked == false ? darkBackgroundColor : lightBackgroundColor);
                var backgroundAnimation = new ColorAnimation
                {
                    Duration = TimeSpan.FromMilliseconds(600),
                    To = toggleButton.IsChecked == false ? lightBackgroundColor : darkBackgroundColor,
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
                };
                background.BeginAnimation(SolidColorBrush.ColorProperty, backgroundAnimation);

                var foreground = new SolidColorBrush(toggleButton.IsChecked == false ? darkForegroundColor : lightForegroundColor);
                var foregroundAnimation = new ColorAnimation
                {
                    Duration = TimeSpan.FromMilliseconds(600),
                    To = toggleButton.IsChecked == false ? lightForegroundColor : darkForegroundColor,
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
                };
                foreground.BeginAnimation(SolidColorBrush.ColorProperty, foregroundAnimation);

                Application.Current.Resources["DefaultForeground"] = foreground;
                Application.Current.Resources["DefaultBackground"] = background;
            }
        }
    }
}
