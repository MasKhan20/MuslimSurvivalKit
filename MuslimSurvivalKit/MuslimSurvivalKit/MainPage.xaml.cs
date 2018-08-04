using MuslimSurvivalKit.Data;
using MuslimSurvivalKit.View.Settings;
using MuslimSurvivalKit.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MuslimSurvivalKit
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Title = "Muslim Survival Kit";

            var viewmodel = new MainViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            isOpeningPage = false;
        }

        bool isOpeningPage = false;
        private void ReadButton_Clicked(object sender, EventArgs e)
        {
            lock (this)
            {
                if (isOpeningPage)
                    return;
                else
                    isOpeningPage = true;
            }

            Navigation.PushModalAsync(new NavigationPage(new View.Quran.Reader.SurahView.QuranReaderMasterPage()));
        }

        private void DownloadButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new NavigationPage(new View.Download.AudioDownloadPage()));
            //DisplayAlert("Stay Tuned", "This feature is yet to be implemented", "OK");
        }

        private void MySettings_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new QuranReaderSettingsPage());
        }

        private void AppsSettings_Clicked(object sender, EventArgs e)
        {
            Settings.OpenSettings();
        }
    }
}
