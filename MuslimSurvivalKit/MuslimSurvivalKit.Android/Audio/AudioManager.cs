using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MuslimSurvivalKit.Data;
using MuslimSurvivalKit.Events;
using Xamarin.Forms;

[assembly: Dependency(typeof(MuslimSurvivalKit.Droid.Audio.AudioManager))]
namespace MuslimSurvivalKit.Droid.Audio
{
    public class AudioManager : IAudioManager
    {
        private MediaPlayer _mediaPlayer;
        private MediaPlayer MediaPlayer
        {
            get
            {
                if (_mediaPlayer == null)
                    _mediaPlayer = new MediaPlayer();

                return _mediaPlayer;
            }
        }

        public TimeSpan FileDuration { get; private set;}
        public Action OnFinishedPlaying { get; set; }

        public event EventHandler<DurationChangedEventArgs> DurationChanged;

        public void Play(string filePath)
        {
            MediaPlayer.Reset();
            MediaPlayer.SetDataSource(filePath);
            MediaPlayer.Prepare();

            MediaPlayer.Start();
        }

        public void Play()
        {
            MediaPlayer.Start();
        }

        public void Pause()
        {
            MediaPlayer.Pause();
        }

        public void Stop()
        {
            MediaPlayer.Stop();
        }
    }
}