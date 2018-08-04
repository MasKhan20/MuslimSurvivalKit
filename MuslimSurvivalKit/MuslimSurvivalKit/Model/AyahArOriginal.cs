using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MuslimSurvivalKit.Model
{
    [Table("ayah_ar_original")]
    public class AyahArOriginal : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }

        [Column("surah_id")]
        public int SurahId { get; set; }

        [Column("ayah_id")]
        public int AyahId { get; set; }

        [Column("arabic_text")]
        public string ArabicText { get; set; }

        public string TranslationText { get; set; }
        public double TotalDuration { get; set; }
        public string File { get; set; }

        public bool ShowArabic { get; set; }
        public int ArabicFontSize { get; set; }
        public bool ShowBookmarks { get; set; }
        public bool ShowLocation { get; set; }
        public string SurahLocation { get; set; }
        public bool ShowTranslation { get; set; }
        public bool TranslationId { get; set; }

        private string _bookmarkedIcon;
        public string BookmarkedIcon
        {
            get { return _bookmarkedIcon; }
            set
            {
                _bookmarkedIcon = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BookmarkedIcon)));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
