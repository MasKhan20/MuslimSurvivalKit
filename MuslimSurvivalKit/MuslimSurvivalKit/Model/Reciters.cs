using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MuslimSurvivalKit.Model
{
    public struct Reciters
    {
        public static ObservableCollection<string> GetAll()
        {
            return new ObservableCollection<string>()
            {
                AbdulBasit,
                AbuBakrAlShatri,
                MisharyAlAfasy,
            };
        }

        public const string AbdulBasit = "Abdul Basit";
        public const string AbuBakrAlShatri = "AbuBakr Al Shatri";
        public const string MisharyAlAfasy = "Mishary Al Afasy";
    }
}
