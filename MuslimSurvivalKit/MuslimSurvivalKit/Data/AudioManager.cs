using MuslimSurvivalKit.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace MuslimSurvivalKit.Data
{
    public class AudioManager
    {
        private IAudioManager _player;
        public IAudioManager Player
        {
            get
            {
                if (_player == null)
                {
                    _player = DependencyService.Get<IAudioManager>();
                    _player.DurationChanged += Player_DurationChanged;
                }

                return _player;
            }
        }

        private void Player_DurationChanged(object sender, DurationChangedEventArgs args)
        {
            PlayerDurationChanged?.Invoke(sender, args);
        }

        //public ObservableCollection<string> AudioList { get; set; } = new ObservableCollection<string>();
        public string AudioFilePath { get; set; }

        public AudioManager() { }

        public AudioManager(string filePath)
        {
            AudioFilePath = filePath;
        }

        //public AudioManager(IEnumerable<string> files)
        //{
        //    foreach (var file in files)
        //    {
        //        AudioList.Add(file);
        //    }
        //}

        public event EventHandler<DurationChangedEventArgs> PlayerDurationChanged;

        /// <summary>
        /// Starts playing audio file, warning: does not validate
        /// </summary>
        /// <param name="filePath"></param>
        public void Start(string filePath) {
            Player.Play(filePath);
        }

        public void Resume() {
            Player.Play();
        }

        public void Stop() {
            Player.Stop();
        }
    }
}
