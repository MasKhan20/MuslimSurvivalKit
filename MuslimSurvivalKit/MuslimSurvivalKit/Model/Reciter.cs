using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuslimSurvivalKit.Model
{
    [Table("reciter")]
    public class Reciter
    {
        //Info about reciter e.g. name, age, biography etc.

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Column("full_name")]
        public string FullName { get; set; }
    }
}
