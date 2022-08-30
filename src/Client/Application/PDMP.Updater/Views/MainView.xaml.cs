using PDMP.Updater.ViewModels;
using System.Windows;

namespace PDMP.Updater.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public MainView(string args)
        {
            InitializeComponent();
            DataContext = new MainViewModel(args);
        }
    }
}
