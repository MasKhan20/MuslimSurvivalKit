using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuslimSurvivalKit.Model
{
    [Table("ayah_en_sahih")]
    public class AyahEnSahih
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }

        [Column("surah_id")]
        public int SurahId { get; set; }

        [Column("ayah_id")]
        public int AyahId { get; set; }

        [Column("translated_text")]
        public string TranslatedText { get; set; }
    }
}
