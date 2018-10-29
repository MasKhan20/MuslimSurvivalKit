using System;
using System.Collections.Generic;
using System.Text;

namespace MuslimSurvivalKit.Events
{
    public class DurationChangedEventArgs : EventArgs
    {
        public DurationChangedEventArgs(int seconds)
        {
            Seconds = seconds;
        }

        public int Seconds { get; set; }
    }
}
