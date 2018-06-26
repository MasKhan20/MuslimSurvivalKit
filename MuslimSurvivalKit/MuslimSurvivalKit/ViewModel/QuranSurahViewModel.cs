using MuslimSurvivalKit.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MuslimSurvivalKit.ViewModel
{
    public class QuranSurahViewModel : BaseViewModel
    {
        #region Binding Commands
        public ICommand ExitCommand => new Command(Exit_Command);
        #endregion

        #region Binding Properties
        public ObservableCollection<AyahArOriginal> Surah { get; set; } = new ObservableCollection<AyahArOriginal>();
        #endregion

        ListView _listView;
        INavigation Navigation;
        public QuranSurahViewModel(INavigation navigation, ListView listView, int surahId, int ayahId)
        {
            Navigation = navigation;
            _listView = listView;

            Load(surahId, ayahId);
        }

        private async void Load(int surahId, int ayahId)
        {
            var surah = await App.Database.GetArSurahByAyahs(surahId);

            Surah.Clear();
            foreach (var ayah in surah)
            {
                var translation = await App.Database.GetAyahEnSahih(ayah.SurahId, ayah.AyahId);

                ayah.TranslationText = translation.TranslatedText;
                ayah.ArabicFontSize = 40;
                ayah.SurahLocation = $"[{ayah.SurahId}:{ayah.AyahId}]";

                Surah.Add(ayah);
            }

            //surah.RemoveAt(0);
        }

        private void Exit_Command()
        {
            Navigation.PopModalAsync();
        }
    }
}
