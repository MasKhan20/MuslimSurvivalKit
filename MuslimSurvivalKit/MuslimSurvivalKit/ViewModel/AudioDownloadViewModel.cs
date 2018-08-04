using MuslimSurvivalKit.Data;
using MuslimSurvivalKit.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MuslimSurvivalKit.ViewModel
{
    public class AudioDownloadViewModel : BaseViewModel
    {
        #region Binding Commands
        public ICommand DownloadAyahsCommand => new Command(DownloadAyahs_Command);
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

        private async void DownloadAyahs_Command()
        {
            var reciter = Reciters.MisharyAlAfasy;

            var path = Path.Combine(DependencyService.Get<IFileHelper>().GetDownloadDirectory(), "Audio", "Ayahs", reciter);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            try
            {
                using (WebClient client = new WebClient())
                {
                    var address = $"{linkPrefix}/Ayah/{reciter.Replace(" ", "%20")}";
                    var filesString = await client.DownloadStringTaskAsync($"{address}/files.txt");
                    var files = filesString.Split('\n');

                    foreach (var file in files)
                    {
                        StatusText = $"Downloading: {file}.mp3";

                        var fullFile = Path.Combine(path, $"{file}.mp3");
                        await Task.Run(async () =>
                        {
                            await client.DownloadFileTaskAsync($"{address}/{file}.mp3", fullFile);
                        });
                    }

                    StatusText = $"Downloaded {files.Length} files";
                }
            }
            catch (Exception exc)
            {
                MessagingCenter.Send(this, "Alert", (exc.Message, exc.ToString()));
            }
        }

        private async void DownloadAll_Command()
        {
            var reciter = Reciters.MisharyAlAfasy;

            //await DownloadFromGit(reciter);

            //using (WebClient client = new WebClient())
            //{
            //     DownloadFromDrive(client, reciter);
            //}

            var path = Path.Combine(DependencyService.Get<IFileHelper>().GetDownloadDirectory(), "Audio", reciter);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var files = Directory.GetFiles(path, "*.mp4");
            foreach (var file in files)
            {
                System.IO.File.Delete(file);
            }
            
            using (WebClient client = new WebClient())
            {
                for (int i = 1; i < 115; i++)
                {
                    var file = $"{i.ToString().PadLeft(3, '0')}.mp3";
            
                    var link = $"{linkPrefix}/{reciter.Replace(" ", "%20")}/{file}";

                    var fullFile = Path.Combine(path, file);
                    await client.DownloadFileTaskAsync(new Uri(link), fullFile);
            
                    StatusText = $"Downloaded: {i.ToString().PadLeft(3, '0')}";
                }
            }
            
            StatusText = "Downloaded All";
        }
    }
}
