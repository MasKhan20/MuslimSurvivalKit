using MuslimSurvivalKit.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MuslimSurvivalKit.View.Quran.Listener
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuranSurahAudioPage : ContentPage
    {
        QuranSurahAudioViewModel _viewmodel;
        public QuranSurahAudioPage(int surahId = 1)
        {
            InitializeComponent();

            _viewmodel = new QuranSurahAudioViewModel(Navigation, surahId);

            BindingContext = _viewmodel;

            MessagingCenter.Subscribe<QuranSurahAudioViewModel, (string title, string message)>(this, "Alert",
                (sender, args) =>
                {
                    DisplayAlert(args.title, args.message, "OK");
                });
        }
    }
}