using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MuslimSurvivalKit.ViewModel
{
    public class QuranSurahAudioViewModel : BaseViewModel
    {
        private INavigation Navigation;
        public QuranSurahAudioViewModel(INavigation navigation, int surahId)
        {
            Navigation = navigation;
        }
    }
}
