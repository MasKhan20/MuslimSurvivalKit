using MuslimSurvivalKit.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MuslimSurvivalKit.View.Quran
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SurahListPage : ContentPage
    {
        public ListView SurahList;

        public SurahListPage()
        {
            InitializeComponent();

            Title = "Al Quran - Surahs";

            var viewmodel = new SurahListViewModel();

            BindingContext = viewmodel;

            SurahList = surahList;

            MessagingCenter.Subscribe<SurahListViewModel, (string title, string message)>(viewmodel, "Alert",
                (sender, args) =>
                {
                    DisplayAlert(args.title, args.message, "OK");
                });
        }
    }
}