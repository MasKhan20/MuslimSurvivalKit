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
        public ICommand SalahTimesCommand => new Command(SalahTimes_Command);
        #endregion

        #region Binding Properties

        #endregion

        INavigation Navigation;
        public RootViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }

        private async void Settings_Command()
        {
            await Navigation.PushAsync(new View.Settings.AppSettingsPage());
        }

        private async void QuranPage_Command()
        {
            await Navigation.PushAsync(new View.Root.RootQuranPage());
        }

        private async void QuranPdf_Command()
        {
            await Navigation.PushAsync(new View.Quran.Reader.PdfView.QuranPdfPage(App.TajweedPdfPath));
        }

        private async void SalahTimes_Command()
        {
            await Navigation.PushAsync(new View.Salah.Times.SalahTimesPage());                                   
        }
    }
}
