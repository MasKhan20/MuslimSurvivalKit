using MuslimSurvivalKit.Data;
using MuslimSurvivalKit.View.Download;
using MuslimSurvivalKit.View.Settings;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MuslimSurvivalKit.ViewModel
{
    public class AppSettingsViewModel : BaseViewModel
    {
        #region Binding Commands
        public ICommand QuranSettingsCommand => new Command(QuranSettings_Command);
        public ICommand SalahSettingsCommand => new Command(SalahSettings_Command);
        public ICommand DownloadCompulsoryCommand => new Command(DownloadCompulsory_Command);
        public ICommand DownloadAudioCommand => new Command(DownloadAudio_Command);
        #endregion

        #region Binding Properties
        private bool _startUpAudio;
        public bool StartUpAudio
        {
            get { return _startUpAudio; }
            set
            {
                _startUpAudio = value;
                RaisePropertyChanged(nameof(StartUpAudio));

                Settings.StartupAudio = StartUpAudio;
            }
        }
        #endregion
        
        bool isPushing;
        INavigation Navigation;
        public AppSettingsViewModel(INavigation navigation)
        {
            Navigation = navigation;
            Init();
        }

        private void Init()
        {
            StartUpAudio = Settings.StartupAudio;
        }

        private void DisplayAlert(string title, string message)
        {
            MessagingCenter.Send(this, "Alert", (title, message));
        }

        private async void QuranSettings_Command()
        {
            lock (this)
            {
                if (isPushing)
                    return;
                else
                    isPushing = true;
            }

            await Navigation.PushAsync(new QuranReaderSettingsPage());

            isPushing = false;
        }

        private async void SalahSettings_Command()
        {
            lock (this)
            {
                if (isPushing)
                    return;
                else
                    isPushing = true;
            }

            await Navigation.PushAsync(new SalahTimesSettingsPage());

            isPushing = false;
        }

        private async void DownloadCompulsory_Command()
        {
            lock (this)
            {
                if (isPushing)
                    return;
                else
                    isPushing = true;
            }

            await Navigation.PushPopupAsync(new DownloadPopup());

            isPushing = false;
        }

        private void DownloadAudio_Command()
        {
            lock (this)
            {
                if (isPushing)
                    return;
                else
                    isPushing = true;
            }

            DisplayAlert("Stay Tuned!", "This feature is yet to be implemented.");

            isPushing = false;
        }
    }
}
