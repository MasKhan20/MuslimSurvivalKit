using MuslimSurvivalKit.Data;
using MuslimSurvivalKit.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MuslimSurvivalKit.ViewModel
{
    public class QuranReaderSettingsViewModel : BaseViewModel
    {
        #region Binding Command

        #endregion

        #region Binding Properties
        private bool _showArabic;
        public bool ShowArabic
        {
            get { return _showArabic; }
            set
            {
                _showArabic = value;
                RaisePropertyChanged(nameof(ShowArabic));

                Settings.ShowArabic = ShowArabic;
            }
        }

        private int _arTextSize;
        public int ArTextSize
        {
            get { return _arTextSize; }
            set
            {
                _arTextSize = value;
                RaisePropertyChanged(nameof(ArTextSize));

                if (int.TryParse(ArTextSize.ToString(), out int size) && size >= 10 && size <= 100)
                {
                    Settings.ArTextSize = size;
                }
            }
        }

        private bool _showLocation;
        public bool ShowLocation
        {
            get { return _showLocation; }
            set
            {
                _showLocation = value;
                RaisePropertyChanged(nameof(ShowLocation));

                Settings.ShowLocation = ShowLocation;
            }
        }

        private bool _showTranslation;
        public bool ShowTranslation
        {
            get { return _showTranslation; }
            set
            {
                _showTranslation = value;
                RaisePropertyChanged(nameof(ShowTranslation));

                Settings.ShowTranslation = ShowTranslation;
            }
        }

        public ObservableCollection<string> TranslationIds { get; set; } = new ObservableCollection<string>()
        {
            "EnSahih",
        };

        private string _selectedTranslation;
        public string SelectedTranslation
        {
            get { return _selectedTranslation; }
            set
            {
                _selectedTranslation = value;
                RaisePropertyChanged(nameof(SelectedTranslation));

                Settings.TranslationId = SelectedTranslation;
            }
        }

        private bool _autoSelectFirst;
        public bool AutoSelectFirst
        {
            get { return _autoSelectFirst; }
            set
            {
                _autoSelectFirst = value;
                RaisePropertyChanged(nameof(AutoSelectFirst));

                Settings.AutoSelectFirst = AutoSelectFirst;
            }
        }
        #endregion

        public QuranReaderSettingsViewModel()
        {
            Init();
        }

        private void Init()
        {
            ShowArabic = Settings.ShowArabic;
            ArTextSize = Settings.ArTextSize;
            ShowLocation = Settings.ShowLocation;
            ShowTranslation = Settings.ShowTranslation;
            //Translations
            SelectedTranslation = Settings.TranslationId;
            AutoSelectFirst = Settings.AutoSelectFirst;
        }
    }
}
