using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuslimSurvivalKit.Model
{
    [Table("surah")]
    public class Surah
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }

        [Column("surah_id")]
        public int SurahId { get; set; }

        [Column("page_number")]
        public int PageNumber { get; set; }

        [Column("ayah_count")]
        public int AyahCount { get; set; }

        [Column("arabic_name")]
        public string ArabicName { get; set; }

        [Column("en_ar_name")]
        public string EnArName { get; set; }

        [Column("english_name")]
        public string EnglishName { get; set; }

        public string File { get; set; }
        public double Duration { get; set; }
    }
}
