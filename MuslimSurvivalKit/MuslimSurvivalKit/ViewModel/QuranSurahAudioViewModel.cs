using MuslimSurvivalKit.Data;
using MuslimSurvivalKit.Model;
using MuslimSurvivalKit.View.Download;
using Plugin.MediaManager.Abstractions;
using Plugin.MediaManager.Abstractions.Enums;
using Plugin.MediaManager.Abstractions.EventArguments;
using Plugin.MediaManager.Abstractions.Implementations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MuslimSurvivalKit.ViewModel
{
    public class QuranSurahAudioViewModel : BaseViewModel
    {
        #region Binding Commands
        public ICommand ExitCommand => new Command(Exit_Command);
        public ICommand DownloadCommand => new Command(Download_Command);
        public ICommand PlayPauseCommand => new Command(PlayPause_Command);
        #endregion

        #region Binding Properties
        public ObservableCollection<Reciter> Reciters { get; set; } = new ObservableCollection<Reciter>();
        public ObservableCollection<Surah> Surahs { get; set; } = new ObservableCollection<Surah>();

        private Surah _selectedSurah;
        public Surah SelectedSurah
        {
            get { return _selectedSurah; }
            set
            {
                _selectedSurah = value;
                RaisePropertyChanged(nameof(SelectedSurah));

                if (SelectedSurah != null)
                    HandleSurahChanged();
            }
        }

        private string _reciterImage;
        public string ReciterImage
        {
            get { return _reciterImage; }
            set
            {
                _reciterImage = value;
                RaisePropertyChanged(nameof(ReciterImage));
            }
        }

        private string _playerLabel;
        public string PlayerLabel
        {
            get { return _playerLabel; }
            set
            {
                _playerLabel = value;
                RaisePropertyChanged(nameof(PlayerLabel));
            }
        }

        private double _playedDuration;
        public double PlayedDuration
        {
            get { return _playedDuration; }
            set
            {
                _playedDuration = value;
                RaisePropertyChanged(nameof(PlayedDuration));
            }
        }

        private double _playedValue;
        public double PlayedValue
        {
            get { return _playedValue; }
            set
            {
                _playedValue = value;
                RaisePropertyChanged(nameof(PlayedValue));

                UpdateDurationFromSlider();
            }
        }

        private double _totalDuration = 1;
        public double TotalDuration
        {
            get { return _totalDuration; }
            set
            {
                _totalDuration = value;
                RaisePropertyChanged(nameof(TotalDuration));
            }
        }

        private string _stopIcon;
        public string StopIcon
        {
            get { return _stopIcon; }
            set
            {
                _stopIcon = value;
                RaisePropertyChanged(nameof(StopIcon));
            }
        }

        private string _previousIcon;
        public string PreviousIcon
        {
            get { return _previousIcon; }
            set
            {
                _previousIcon = value;
                RaisePropertyChanged(nameof(PreviousIcon));
            }
        }

        private string _playPauseIcon;
        public string PlayPauseIcon
        {
            get { return _playPauseIcon; }
            set
            {
                _playPauseIcon = value;
                RaisePropertyChanged(nameof(PlayPauseIcon));
            }
        }

        private string _nextIcon;
        public string NextIcon
        {
            get { return _nextIcon; }
            set
            {
                _nextIcon = value;
                RaisePropertyChanged(nameof(NextIcon));
            }
        }

        private string _settingIcon;
        public string SettingIcon
        {
            get { return _settingIcon; }
            set
            {
                _settingIcon = value;
                RaisePropertyChanged(nameof(SettingIcon));
            }
        }
        #endregion

        private readonly string stopIcon = "ic_stop_white_24dp";
        private readonly string previousIcon = "ic_skip_previous_white_24dp";
        private readonly string playIcon = "ic_play_arrow_white_24dp";
        private readonly string pauseIcon = "ic_pause_white_24dp";
        private readonly string nextIcon = "ic_skip_next_white_24dp";
        private readonly string settingIcon = "ic_settings_white_24dp";

        IMediaManager _manager = Plugin.MediaManager.CrossMediaManager.Current;
        IPlaybackController _player = Plugin.MediaManager.CrossMediaManager.Current.PlaybackController;
        bool _isPlaying;

        INavigation Navigation;
        public QuranSurahAudioViewModel(INavigation navigation, int surahId)
        {
            Navigation = navigation;

            StopIcon = stopIcon;
            PreviousIcon = previousIcon;
            PlayPauseIcon = playIcon;
            NextIcon = nextIcon;
            SettingIcon = settingIcon;

            _manager.BufferingChanged += Manager_BufferingChanged;
            _manager.PlayingChanged += Manager_PlayingChanged;
            _manager.StatusChanged += Manager_StatusChanged;

            GetReciters();
            LoadSurahs(Model.Reciters.MisharyAlAfasy, surahId);

            LoadMediaQueue();
        }

        private void DisplayAlert(string title, string message)
        {
            MessagingCenter.Send(this, "Alert", (title, message));
        }

        private async void GetReciters()
        {
            var reciters = await App.Database.GetReciters();

            foreach (var reciter in reciters)
            {
                Reciters.Add(reciter);
            }
        }

        private async void LoadSurahs(string reciter, int surahId)
        {
            var path = Path.Combine(DependencyService.Get<IFileHelper>().GetDownloadDirectory(), "Audio", reciter);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var files = Directory.GetFiles(path, "*.mp3");
            
            foreach (var file in files)
            {
                var surah = await App.Database.GetSurah(int.Parse(Path.GetFileNameWithoutExtension(file)));

                try
                {
                    surah.Duration = DependencyService.Get<IFileHelper>().GetDuration(file);
                }
                catch (Exception exc)
                {
                    DisplayAlert(exc.Message, exc.ToString());
                }

                surah.File = file;

                Surahs.Add(surah);
            }

            //if (Surahs.Count != 0)
            //{
            //    SelectedSurah = Surahs[surahId <= Surahs.Count ? surahId - 1 : 0];
            //
            //    TotalDuration = SelectedSurah.Duration;
            //}
        }

        private void LoadMediaQueue()
        {
            if (_manager.MediaQueue.Count < Surahs.Count)
            {
                _manager.MediaQueue.CollectionChanged -= MediaQueue_CollectionChanged;

                _manager.MediaQueue.Clear();
                foreach (var surah in Surahs)
                {

                    IMediaFile audio = new MediaFile()
                    {
                        Url = surah.File,
                        Type = MediaFileType.Audio,
                        Availability = ResourceAvailability.Local
                    };

                    audio.Metadata.Duration = (int)surah.Duration;

                    _manager.MediaQueue.Add(audio);
                }

                _manager.MediaQueue.CollectionChanged += MediaQueue_CollectionChanged; 
            }
        }

        private void HandleSurahChanged()
        {
            PlayerLabel = $"Now Playing: {SelectedSurah.SurahId.ToString().PadLeft(3, '0')} Surah {SelectedSurah.ArabicName} - {SelectedSurah.EnglishName}";
        }

        private void UpdateDurationFromSlider()
        {
            PlayedDuration = PlayedValue;

            _manager.AudioPlayer.Seek(TimeSpan.FromSeconds(PlayedValue));
        }

        //private void PlaySurah(Surah surah)
        //{
        //    
        //}

        private void Manager_BufferingChanged(object sender, BufferingChangedEventArgs e)
        {
            Debug.WriteLine($"MediaManager BufferingChanged");
        }

        private void Manager_PlayingChanged(object sender, PlayingChangedEventArgs e)
        {
            Debug.WriteLine($"MediaPlayer PlayingChanged");

            try
            {
                var time = _manager.MediaQueue[_manager.MediaQueue.Index].Metadata.Duration;
                Debug.WriteLine($"Progress: {e.Progress}");

                TotalDuration = time;
                if (e.Progress != 0)
                {
                    PlayedValue = e.Progress;
                }
                    //_manager.Position.TotalSeconds > 0 ? Convert.ToInt32(_manager.Position.TotalSeconds) : 0;
            }
            catch (Exception exc)
            {
                DisplayAlert(exc.Message, exc.ToString());
            }
        }

        private void MediaQueue_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_manager.MediaQueue.Count > Surahs.Count)
            {
                LoadMediaQueue();
            }
        }

        private void Manager_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            Debug.WriteLine($"MediaPlayer Status = {e.Status}");
        }

        private void PlayPause_Command()
        {
            //Exception
            LoadMediaQueue();

            Debug.WriteLine("Loaded MediaQueue, Contents: ");

            foreach (var item in _manager.MediaQueue)
            {
                Debug.WriteLine($"Queue Item: {item.Url}");
            }

            if (SelectedSurah == null)
            {
                DisplayAlert("No surah selected", "Please select a surah to begin playing");
                return;
            }

            if (!_isPlaying)
            {
                _manager.MediaQueue.SetIndexAsCurrent(Surahs.IndexOf(SelectedSurah));

                _player.Play();

                PlayPauseIcon = pauseIcon;
                _isPlaying = true;
            }
            else
            {
                _player.Pause();

                PlayPauseIcon = playIcon;
                _isPlaying = false;
            }

            //switch (_manager.Status)
            //{
            //    case MediaPlayerStatus.Stopped:
            //        PlaySurah(SelectedSurah);
            //        break;
            //    case MediaPlayerStatus.Paused:
            //        PlaySurah(SelectedSurah);
            //        break;
            //    case MediaPlayerStatus.Playing:
            //        _manager.Pause();
            //        break;
            //    case MediaPlayerStatus.Loading:
            //        break;
            //    case MediaPlayerStatus.Buffering:
            //        break;
            //    case MediaPlayerStatus.Failed:
            //        break;
            //    default:
            //        break;
            //}
        }

        private void Exit_Command()
        {
            Navigation.PopModalAsync();
        }

        private void Download_Command()
        {
            Navigation.PushAsync(new AudioDownloadPage());
        }
    }
}
