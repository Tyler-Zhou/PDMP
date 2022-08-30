using GeneralUpdate.Core;
using GeneralUpdate.Core.Strategys;
using GeneralUpdate.Core.Update;
using System;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace PDMP.Updater.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _tips1, _tips2, _tips3, _tips4, _tips5, _tips6;
        private double _progressVal, _progressMin, _progressMax;
        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// 
        /// </summary>
        public string Tips1
        {
            get
            {
                return _tips1;
            }
            set
            {
                _tips1 = value;
                NotifyPropertyChanged("Tips1");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Tips2
        {
            get
            {
                return _tips2;
            }
            set
            {
                _tips2 = value;
                NotifyPropertyChanged("Tips2");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Tips3
        {
            get
            {
                return _tips3;
            }
            set
            {
                _tips3 = value;
                NotifyPropertyChanged("Tips3");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Tips4
        {
            get
            {
                return _tips4;
            }
            set
            {
                _tips4 = value;
                NotifyPropertyChanged("Tips4");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Tips5
        {
            get
            {
                return _tips5;
            }
            set
            {
                _tips5 = value;
                NotifyPropertyChanged("Tips5");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Tips6
        {
            get
            {
                return _tips6;
            }
            set
            {
                _tips6 = value;
                NotifyPropertyChanged("Tips6");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public double ProgressVal
        {
            get
            {
                return _progressVal;
            }
            set
            {
                _progressVal = value;
                NotifyPropertyChanged("ProgressVal");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public double ProgressMin
        {
            get
            {
                return _progressMin;
            }
            set
            {
                _progressMin = value;
                NotifyPropertyChanged("ProgressMin");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public double ProgressMax
        {
            get
            {
                return _progressMax;
            }
            set
            {
                _progressMax = value;
                NotifyPropertyChanged("ProgressMax");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public MainViewModel(string args)
        {
            ProgressMin = 0;
            Task.Run(async () =>
            {
                var bootStrap = new GeneralUpdateBootstrap();
                bootStrap.MutiAllDownloadCompleted += OnMutiAllDownloadCompleted;
                bootStrap.MutiDownloadCompleted += OnMutiDownloadCompleted;
                bootStrap.MutiDownloadError += OnMutiDownloadError;
                bootStrap.MutiDownloadProgressChanged += OnMutiDownloadProgressChanged;
                bootStrap.MutiDownloadStatistics += OnMutiDownloadStatistics;
                bootStrap.Exception += OnException;
                bootStrap.Strategy<DefaultStrategy>().
                Option(UpdateOption.Encoding, Encoding.Default).
                Option(UpdateOption.DownloadTimeOut, 60).
                Option(UpdateOption.Format, "zip").
                RemoteAddressBase64(args);
                await bootStrap.LaunchTaskAsync();
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMutiDownloadStatistics(object sender, MutiDownloadStatisticsEventArgs e)
        {
            Tips1 = $" {e.Speed} , {e.Remaining.ToShortTimeString()}";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMutiDownloadProgressChanged(object sender, MutiDownloadProgressChangedEventArgs e)
        {
            switch (e.Type)
            {
                case ProgressType.Check:
                    break;

                case ProgressType.Donwload:
                    ProgressVal = e.BytesReceived;
                    if (ProgressMax != e.TotalBytesToReceive)
                    {
                        ProgressMax = e.TotalBytesToReceive;
                    }
                    Tips2 = $" {Math.Round(e.ProgressValue * 100, 2)}% ， Receivedbyte：{e.BytesReceived}M ，Totalbyte：{e.TotalBytesToReceive}M";
                    break;

                case ProgressType.Updatefile:
                    break;

                case ProgressType.Done:
                    break;

                case ProgressType.Fail:
                    break;

                default:
                    break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMutiDownloadCompleted(object sender, MutiDownloadCompletedEventArgs e)
        {
            //Tips3 = $"{ e.Version.Name } download completed.";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMutiAllDownloadCompleted(object sender, MutiAllDownloadCompletedEventArgs e)
        {
            if (e.IsAllDownloadCompleted)
            {
                Tips4 = "AllDownloadCompleted";
            }
            else
            {
                //foreach (var version in e.FailedVersions)
                //{
                //    Debug.Write($"{ version.Item1.Name }");
                //}
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMutiDownloadError(object sender, MutiDownloadErrorEventArgs e)
        {
            //Tips5 = $"{ e.Version.Name },{ e.Exception.Message }.";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnException(object sender, ExceptionEventArgs e)
        {
            Tips6 = $"{e.Exception.Message}";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
