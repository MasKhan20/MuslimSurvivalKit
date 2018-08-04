using Microsoft.WindowsAzure.Storage;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MuslimSurvivalKit.ViewModel
{
    public class DownloadPopupViewModel : BaseViewModel
    {
        #region Binding Commands
        public ICommand CancelDownloadCommand => new Command(CancelDownload_Command);
        #endregion

        #region Binding Properties
        private string _statusLabel;
        public string StatusLabel
        {
            get { return _statusLabel; }
            set
            {
                _statusLabel = value;
                RaisePropertyChanged(nameof(StatusLabel));
            }
        }

        private double _progressValue;
        /// <summary>
        /// Value of the progress bar, set from 0 to 1
        /// </summary>
        public double ProgressValue
        {
            get { return _progressValue; }
            set
            {
                _progressValue = value;
                RaisePropertyChanged(nameof(ProgressValue));
            }
        }

        private string _buttonText = "Cancel";
        public string ButtonText
        {
            get { return _buttonText; }
            set
            {
                _buttonText = value;
                RaisePropertyChanged(nameof(ButtonText));
            }
        }
        #endregion

        private INavigation Navigation;
        public DownloadPopupViewModel(INavigation navigation)
        {
            Navigation = navigation;
            StatusLabel = "Downloading files";

            //Task.Run(() => 
            DownloadFile(); // (link);
            //);
        }

        double recievedFiles = 0;
        double listLength = 604;
        string downloadDir;
        WebClient _client;
        private async void DownloadFile()//(string link)
        {
            downloadDir = Path.Combine(App.DownloadPath, "pages");

            if (!Directory.Exists(downloadDir))
                Directory.CreateDirectory(downloadDir);

            _client = new WebClient();

            //_client.DownloadProgressChanged += Client_DownloadProgressChanged;
            _client.DownloadFileCompleted += Client_DownloadFileCompleted;

            CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;
            if (!CrossConnectivity.Current.IsConnected)
            {
                StatusLabel = "Error: Not connected";
                Current_ConnectivityChanged(this, new ConnectivityChangedEventArgs());
                _client.CancelAsync();
                return;
            }


            for (int i = 1; i < 605; i++)
            {
                var file = i.ToString().PadLeft(3, '0');
                var link = String.Format(App.QuranPageDownloadPath, file);

                try
                {
                    await _client.DownloadFileTaskAsync(new Uri(link), Path.Combine(downloadDir, $"{file}.png"));

                    recievedFiles++;
                    //StatusLabel = $"Downloaded: ({i}/604) {(int)(i/604*100)} %";
                }
                catch (Exception exc)
                {
                    Debug.WriteLine(exc.Message + Environment.NewLine + exc.ToString());
                    StatusLabel = $"Error: {exc.Message}";
                } 
            }

            //_client.DownloadProgressChanged -= Client_DownloadProgressChanged;
            _client.DownloadFileCompleted -= Client_DownloadFileCompleted;

            StatusLabel = $"Downloaded {listLength} files";
            ButtonText = "OK";
        }

        //private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        //{
        //    StatusLabel = $"Downloaded {e.BytesReceived}/{e.TotalBytesToReceive} bytes";
        //    ProgressValue = e.ProgressPercentage / 100;
        //}

        private void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            StatusLabel = $"Downloaded: ({recievedFiles} / {listLength}) files{Environment.NewLine}" +
                          $"{(int)(recievedFiles / listLength * 100)} %";
            ProgressValue = (recievedFiles / listLength);
        }

        private void Current_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                StatusLabel = "Error: No connection, please try again";
                ButtonText = "Abort";

                //if (_client.IsBusy)
                _client.CancelAsync();
            }
        }

        private async void CancelDownload_Command()
        {
            try
            {
                //if (ButtonText == "Cancel")
                //{
                _client.CancelAsync();
                //}

                _client.Dispose();

                App.isDownloading = false;
                await Navigation.PopPopupAsync();
            }
            catch (Exception exc)
            {
                Debug.WriteLine($"Exception: {exc.Message + Environment.NewLine} Full exception:{Environment.NewLine + exc.ToString()}");
            }
        }
    }
}
