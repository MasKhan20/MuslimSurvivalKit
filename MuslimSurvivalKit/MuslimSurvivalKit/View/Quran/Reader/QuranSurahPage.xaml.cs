using MuslimSurvivalKit.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MuslimSurvivalKit.View.Quran.Reader
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuranSurahPage : ContentPage
    {
        int _surahId;
        QuranSurahViewModel _viewmodel;
        public QuranSurahPage(int surahId, int ayahId = 1)
        {
            InitializeComponent();

            _surahId = surahId;

            _viewmodel = new QuranSurahViewModel(Navigation, _listView, surahId, ayahId);

            BindingContext = _viewmodel;
        }

        private async void JumpToAyah_Clicked(object sender, EventArgs e)
        {
            //var ayahCount = await App.Database.GetAyahCount(_surahId);

            //fix to jump to ayah
            var buttons = new string[_viewmodel.Surah.Count];
            for (int i = 0; i < _viewmodel.Surah.Count; i++)
                buttons[i] = (i + 1).ToString();

            var result = await DisplayActionSheet("Jump to ayah number", "Cancel", null, buttons);
            if (result == "Cancel")
                return;

            try
            {
                var jumpTo = _viewmodel.Surah[int.Parse(result) - 1];
                _listView.ScrollTo(jumpTo, ScrollToPosition.Start, false);
            }
            catch (Exception exc)
            {
                await DisplayAlert(exc.Message, exc.ToString(), "OK");
            }
            //_listView.ScrollTo(jumpTo, ScrollToPosition.Start, true);
        }
    }
}