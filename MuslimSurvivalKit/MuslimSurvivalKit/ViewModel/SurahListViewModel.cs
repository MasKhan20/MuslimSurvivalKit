using MuslimSurvivalKit.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MuslimSurvivalKit.ViewModel
{
    public class SurahListViewModel : BaseViewModel
    {
        #region Binding Commands
        public ICommand SearchCommand => new Command<string>((text) => Search_Command(text));
        #endregion

        #region Binding Properties
        public ObservableCollection<Surah> Surahs { get; set; } = new ObservableCollection<Surah>();

        private string _searchMessage;
        /// <summary>
        /// Text displayed when search is empty
        /// </summary>
        public string SearchMessage
        {
            get { return _searchMessage; }
            set
            {
                _searchMessage = value;
                RaisePropertyChanged(nameof(SearchMessage));
            }
        }
        #endregion

        public SurahListViewModel()
        {
            Load();
        }

        public async void Load()
        {
            Surahs.Clear();

            var surahs = await App.Database.GetSurahList();

            SearchMessage = surahs.Count == 0 ? "No surahs matching search query" : String.Empty;

            foreach (var surah in surahs)
            {
                Surahs.Add(surah);
            }
        }

        private async void Search_Command(string text)
        {
            if (String.IsNullOrEmpty(text) || String.IsNullOrWhiteSpace(text))
            {
                Load();
                return;
            }

            var surahs = await App.Database.GetSurahList();
            var query = surahs
                .Where(s => s.Id.ToString().Contains(text) || s.EnArName.ToLower().Contains(text.ToLower()))
                .OrderBy(s => s.PageNumber)
                .OrderBy(s => s.SurahId)
                .ToList();

            SearchMessage = surahs.Count == 0 ? "No surahs matching search query" : String.Empty;

            Surahs.Clear();

            foreach (var surah in query)
            {
                Surahs.Add(surah);
            }
        }
    }
}
