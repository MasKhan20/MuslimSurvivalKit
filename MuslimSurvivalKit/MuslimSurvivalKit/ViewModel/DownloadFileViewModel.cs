using static MuslimSurvivalKit.Converter.Converters;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MuslimSurvivalKit.ViewModel
{
    public class DownloadFileViewModel : BaseViewModel
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
        public DownloadFileViewModel(INavigation navigation, string link, string path)
        {
            Navigation = navigation;
            StatusLabel = "Downloading file";
            
            DownloadFile(link, path);
        }
        
        string downloadDir;
        WebClient _client;
        private async void DownloadFile(string link, string path)
        {
            _client = new WebClient();

            _client.DownloadProgressChanged += Client_DownloadProgressChanged;
            _client.DownloadFileCompleted += Client_DownloadFileCompleted;

            CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;
            if (!CrossConnectivity.Current.IsConnected)
            {
                StatusLabel = "Error: Not connected";
                Current_ConnectivityChanged(this, new ConnectivityChangedEventArgs());
                _client.CancelAsync();
                return;
            }

            try
            {
                await _client.DownloadFileTaskAsync(link, path);
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.Message + Environment.NewLine + exc.ToString());
                StatusLabel = $"Error: {exc.Message}";
                MessagingCenter.Send(this, "Alert", (exc.Message, exc.ToString()));
            }

            _client.DownloadProgressChanged -= Client_DownloadProgressChanged;
            _client.DownloadFileCompleted -= Client_DownloadFileCompleted;

            ButtonText = "OK";
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            StatusLabel = $"Downloading: {e.ProgressPercentage} %{Environment.NewLine}" +
                          $"({ConvertSize(e.BytesReceived)} / {ConvertSize(e.TotalBytesToReceive)})";
            ProgressValue = (e.BytesReceived / e.TotalBytesToReceive);
        }

        private void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            StatusLabel = $"Downloaded: 100 %{Environment.NewLine}";
            ProgressValue = 1;
        }

        private void Current_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                StatusLabel = "Error: No connection, please try again";
                ButtonText = "Abort";
                
                _client.CancelAsync();
            }
        }

        private async void CancelDownload_Command()
        {
            try
            {
                _client.CancelAsync();

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
