using MuslimSurvivalKit.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuslimSurvivalKit.Data
{
    public interface IAudioManager
    {
        void Play(string filePath);
        void Play();
        void Pause();
        void Stop();
        Action OnFinishedPlaying { get; set; }
        event EventHandler<DurationChangedEventArgs> DurationChanged;
    }
}
