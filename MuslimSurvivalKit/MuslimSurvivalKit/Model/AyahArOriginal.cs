using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuslimSurvivalKit.Model
{
    [Table("ayah_ar_original")]
    public class AyahArOriginal
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
        public int ArabicFontSize { get; set; }
        public double TotalDuration { get; set; }
        public string SurahLocation { get; set; }
    }
}
