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
        }

        private void ReadButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new NavigationPage(new View.Quran.Reader.QuranReaderMasterPage()));
        }

        private void ListenButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new NavigationPage(new View.Quran.Listener.QuranSurahAudioPage()));
            //DisplayAlert("Stay Tuned", "This feature is yet to be implemented", "OK");
        }
    }
}
