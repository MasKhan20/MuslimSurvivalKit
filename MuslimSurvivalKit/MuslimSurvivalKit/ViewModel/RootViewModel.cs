using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MuslimSurvivalKit.ViewModel
{
    public class RootViewModel : BaseViewModel
    {
        #region Binding Commands
        public ICommand SettingsCommand => new Command(Settings_Command);
        public ICommand QuranPageCommand => new Command(QuranPage_Command);
        public ICommand QuranPdfCommand => new Command(QuranPdf_Command);
        public ICommand QuranAudioCommand => new Command(QuranAudio_Command);
        public ICommand SalahTimesCommand => new Command(SalahTimes_Command);
        #endregion

        #region Binding Properties

        #endregion

        bool isPushing = false;
        INavigation Navigation;
        public RootViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }

        private async void Settings_Command()
        {
            lock (this)
            {
                if (isPushing)
                    return;
                else
                    isPushing = true;
            }

            await Navigation.PushAsync(new View.Settings.AppSettingsPage());

            isPushing = false;
        }

        private async void QuranPage_Command()
        {
            lock (this)
            {
                if (isPushing)
                    return;
                else
                    isPushing = true;
            }

            await Navigation.PushAsync(new View.Root.RootQuranPage());

            isPushing = false;
        }

        private async void QuranPdf_Command()
        {
            lock (this)
            {
                if (isPushing)
                    return;
                else
                    isPushing = true;
            }

            await Navigation.PushAsync(new View.Quran.Reader.PdfView.QuranPdfPage(App.TajweedPdfPath));

            isPushing = false;
        }

        private async void QuranAudio_Command()
        {
            lock (this)
            {
                if (isPushing)
                    return;
                else
                    isPushing = true;
            }

            await Navigation.PushAsync(new View.Quran.Listener.QuranSurahAudioPage());

            isPushing = false;
        }

        private async void SalahTimes_Command()
        {
            lock (this)
            {
                if (isPushing)
                    return;
                else
                    isPushing = true;
            }

            await Navigation.PushAsync(new View.Salah.Times.SalahTimesPage());

            isPushing = false;
        }
    }
}
