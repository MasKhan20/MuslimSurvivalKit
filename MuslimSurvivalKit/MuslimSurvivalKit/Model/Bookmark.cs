using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuslimSurvivalKit.Model
{
    [Table("bookmark")]
    public class Bookmark
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("date_created")]
        public DateTime DateCreated { get; set; }

        [Column("bookmark_type")]
        public BookmarkType BookmarkType { get; set; }

        [Column("surah_id")]
        public int SurahId { get; set; }

        [Column("ayah_id")]
        public int AyahId { get; set; }

        [Column("page_number")]
        public int PageNumber { get; set; }

        public string BookmarkIdHeader { get; set; }
        public string BookmarkIdDetail { get; set; }
        public string BookmarkPageHeader { get; set; }
        public string BookmarkPageDetail { get; set; }
        public string BookmarkDetail { get; set; }
        public string BookmarkDate { get; set; }
    }
}
