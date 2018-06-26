using MuslimSurvivalKit.Data;
using MuslimSurvivalKit.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MuslimSurvivalKit.ViewModel
{
    public class AudioDownloadViewModel : BaseViewModel
    {
        #region Binding Commands
        public ICommand DownloadAllCommand => new Command(DownloadAll_Command);
        #endregion

        #region Binding Properties
        private string _statusText = "Download";
        public string StatusText
        {
            get { return _statusText; }
            set
            {
                _statusText = value;
                RaisePropertyChanged(nameof(StatusText));
            }
        }
        #endregion

        private string linkPrefix = "https://raw.githubusercontent.com/MasKhan20/MuslimSurvivalKit-Files/master/Audio";
        INavigation Navigation;
        public AudioDownloadViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }

        private async void DownloadAll_Command()
        {
            var reciter = Reciters.MisharyAlAfasy;

            using (WebClient client = new WebClient())
            {
                for (int i = 1; i < 115; i++)
                {
                    var file = $"{i.ToString().PadLeft(3, '0')}.mp3";

                    var link = $"{linkPrefix}/{reciter.Replace(" ", "%20")}/{file}";
                    var path = Path.Combine(DependencyService.Get<IFileHelper>().GetDownloadDirectory(), "Audio", reciter);

                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    var fullFile = Path.Combine(path, file);
                    await client.DownloadFileTaskAsync(new Uri(link), fullFile);

                    StatusText = $"Downloaded: {i.ToString().PadLeft(3, '0')}";
                }
            }

            StatusText = "Downloaded All";
        }
    }
}
