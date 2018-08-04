using MuslimSurvivalKit.Model;
using MuslimSurvivalKit.View.Ads;
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
        private bool isPushing = false;
        private bool _isRoot;
        public ListView SurahList;
        public SurahListPage(bool isRoot = false)
        {
            _isRoot = isRoot;

            InitializeComponent();

            if (isRoot)
            {
                var ad = new AdMobBannerView("ca-app-pub-4025243320631804/8161981113");
                    //"ca-app-pub-3940256099942544/6300978111"
                
                grid.Children.Add(ad, 0, 1); 
            }

            //Title = "Al Quran - Surahs";

            var viewmodel = new SurahListViewModel(); //(Navigation, isRoot);

            BindingContext = viewmodel;

            SurahList = surahList;

            MessagingCenter.Subscribe<SurahListViewModel, (string title, string message)>(viewmodel, "Alert",
                (sender, args) =>
                {
                    DisplayAlert(args.title, args.message, "OK");
                });
        }

        private void SurahList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            lock (this)
            {
                if (isPushing == true)
                    return;
                else
                    isPushing = false;
            }

            var surah = e.SelectedItem as Surah;

            if (_isRoot)
            {
                if (Data.Settings.ViewBySurah)
                    Navigation.PushModalAsync(new NavigationPage(new Reader.SurahView.QuranReaderMasterPage(surah.SurahId)));
                else
                    Navigation.PushAsync(new Reader.PageView.QuranPages(surah.PageNumber));
            }

            isPushing = false;
            surahList.SelectedItem = null;
        }
    }
}