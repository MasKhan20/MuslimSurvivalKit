using MuslimSurvivalKit.Data;
using MuslimSurvivalKit.Model;
//using Plugin.MediaManager.Abstractions;
//using Plugin.MediaManager.Abstractions.Enums;
//using Plugin.MediaManager.Abstractions.EventArguments;
//using Plugin.MediaManager.Abstractions.Implementations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MuslimSurvivalKit.ViewModel
{
    public class QuranSurahViewModel : BaseViewModel
    {
        #region Binding Commands
        //public ICommand ShowHidePlayerCommand => new Command(ShowHidePlayer_Command);
        public ICommand SettingsCommand => new Command(Settings_Command);
        public ICommand ExitCommand => new Command(Exit_Command);

        public ICommand ToggleBookmarkedCommand => new Command((s) => ToggleBookmarked_Command(s));

        //public ICommand StopCommand => new Command(Stop_Command);
        //public ICommand PreviousCommand => new Command(Previous_Command);
        //public ICommand PlayPauseCommand => new Command(PlayPause_Command);
        //public ICommand NextCommand => new Command(Next_Command);
        //public ICommand AudioSettingsCommand => new Command(AudioSettings_Command);
        #endregion

        #region Binding Properties
        public ObservableCollection<AyahArOriginal> Surah { get; set; } = new ObservableCollection<AyahArOriginal>();

        private AyahArOriginal _selectedAyah;
        public AyahArOriginal SelectedAyah
        {
            get { return _selectedAyah; }
            set
            {
                _selectedAyah = value;
                RaisePropertyChanged(nameof(SelectedAyah));

                if (SelectedAyah != null)
                    HandleAyahChanged();
            }
        }

        //private bool _showPlayer;
        //public bool ShowPlayer
        //{
        //    get { return _showPlayer; }
        //    set
        //    {
        //        _showPlayer = value;
        //        RaisePropertyChanged(nameof(ShowPlayer));
        //
        //        ToggleIcon = ShowPlayer ? pauseIcon : playIcon;
        //    }
        //}

        //private string _currentAyahLabel;
        //public string CurrentAyahLabel
        //{
        //    get { return _currentAyahLabel; }
        //    set
        //    {
        //        _currentAyahLabel = value;
        //        RaisePropertyChanged(nameof(CurrentAyahLabel));
        //    }
        //}

        //private string _playPauseIcon;
        //public string PlayPauseIcon
        //{
        //    get { return _playPauseIcon; }
        //    set
        //    {
        //        _playPauseIcon = value;
        //        RaisePropertyChanged(nameof(PlayPauseIcon));
        //    }
        //}

        //private string _toggleIcon;
        //public string ToggleIcon
        //{
        //    get { return _toggleIcon; }
        //    set
        //    {
        //        _toggleIcon = value;
        //        RaisePropertyChanged(nameof(ToggleIcon));
        //    }
        //}

        //private bool _showDownload;
        //public bool ShowDownload
        //{
        //    get { return _showDownload; }
        //    set
        //    {
        //        _showDownload = value;
        //        RaisePropertyChanged(nameof(ShowDownload));
        //    }
        //}
        #endregion

        #region Player Vars
        /*
        private IMediaManager _manager = Plugin.MediaManager.CrossMediaManager.Current;
        private IPlaybackController _player = Plugin.MediaManager.CrossMediaManager.Current.PlaybackController;

        private bool isPlaying = false;
        private bool isLoading = false;
        private int expectedLength = 0;
        private bool isLoaded;

        private readonly string playIcon = "ic_play_arrow_white_24dp";
        private readonly string pauseIcon = "ic_pause_white_24dp";
        */
        #endregion

        List<Bookmark> bookmarks;
        int _surahId;
        ListView _listView;
        INavigation Navigation;
        public QuranSurahViewModel(INavigation navigation, ListView listView, int surahId, int ayahId, bool jumpTo)
        {
            Init(navigation, listView, surahId, ayahId, jumpTo);
        }

        private async void Init(INavigation navigation, ListView listView, int surahId, int ayahId, bool jumpTo)
        {
            Navigation = navigation;
            _listView = listView;
            _surahId = surahId;

            //PlayPauseIcon = playIcon;
            //ShowDownload = false;
            //ShowPlayer = false;

            bookmarks = await App.Database.GetBookmarks();
            await Load(surahId, ayahId, jumpTo);

            if (Surah.Count > ayahId)
            {
                _listView.ScrollTo(Surah[ayahId], ScrollToPosition.MakeVisible, false);
            }

            _listView.ItemTapped += ListView_ItemTapped;
            //_manager.MediaFinished += Manager_MediaFinished;
        }

        public async Task Load(int surahId, int ayahId, bool jumpTo)
        {
            //if (isLoading)
            //{
            //    DependencyService.Get<IMethodHelper>().ShowToast("Already Loading");
            //    return;
            //}
            //isLoading = true;
            //expectedLength = await App.Database.GetAyahCount(surahId) + 1;
            
            async Task UpdateAyah(AyahArOriginal ayah)
            {
                ayah.ShowArabic = Settings.ShowArabic;
                ayah.ArabicFontSize = Settings.ArTextSize;
                ayah.ShowLocation = Settings.ShowLocation;
                ayah.ShowBookmarks = Settings.ShowIdBookmarks;
                ayah.BookmarkedIcon = App.BookmarkHollowBlackIcon;
                foreach (var bookmark in bookmarks)
                {
                    if (ayah.SurahId == bookmark.SurahId && ayah.AyahId == bookmark.AyahId && bookmark.BookmarkType == BookmarkType.Id)
                    {
                        ayah.BookmarkedIcon = App.BookmarkFilledBlackIcon;
                    }
                }
                ayah.ShowTranslation = Settings.ShowTranslation;

                var translation = await App.Database.GetAyahEnSahih(ayah.SurahId, ayah.AyahId);

                ayah.TranslationText = translation.TranslatedText;
                ayah.ArabicFontSize = Settings.ArTextSize;
                ayah.SurahLocation = $"[{ayah.SurahId}:{ayah.AyahId}]";

                var fileName = $"{ayah.SurahId.ToString().PadLeft(3, '0')}{ayah.AyahId.ToString().PadLeft(3, '0')}.mp3";

                ayah.File = Path.Combine(
                    DependencyService.Get<IFileHelper>().GetDownloadDirectory(),
                    "Audio",
                    "Ayahs",
                    Reciters.MisharyAlAfasy,
                    fileName);
            }

            var surah = await App.Database.GetArSurahByAyahs(surahId);

            Surah.Clear();
            //_manager.MediaQueue.Clear();

            // Surah Tawbah does not start with bismillah!
            if (surahId != 9)
            {
                var bismillah = await App.Database.GetAyah(0, 0);
                await UpdateAyah(bismillah);
                bismillah.ShowBookmarks = false;
                bismillah.ShowLocation = false;
                //_manager.MediaQueue.Add(new MediaFile(bismillah.File, MediaFileType.Audio, ResourceAvailability.Local));
                Surah.Add(bismillah);
            }

            foreach (var ayah in surah)
            {
                await UpdateAyah(ayah);

                //_manager.MediaQueue.Add(new MediaFile(ayah.File, MediaFileType.Audio, ResourceAvailability.Local));
                Surah.Add(ayah);
            }

            //if (Settings.AutoSelectFirst)
            //{
                SelectedAyah = Surah.Count != 0 ? Surah[0] : null;
            //}

            //isLoading = false;
            //isLoaded = true;
        }

        private async void HandleAyahChanged()
        {
            await App.Database.UpdateLastId(
                _surahId,
                SelectedAyah.SurahId == 0 ? 1 : SelectedAyah.AyahId);
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ////isPlaying = false;
            //SelectedAyah = e.Item as AyahArOriginal;
        }

        private void ShowToast(string message, bool longToast = false)
        {
            DependencyService.Get<IMethodHelper>().ShowToast(message, !longToast);
        }

        private void Exit_Command()
        {
            Navigation.PopModalAsync();
        }

        private async void ToggleBookmarked_Command(object sender)
        {
            var ayah = sender as AyahArOriginal;
            var bookmarks = await App.Database.GetBookmarks();
            var bookmark = bookmarks
                .Where(b => b.AyahId == ayah.AyahId)
                .Where(b => b.SurahId == ayah.SurahId)
                .FirstOrDefault();

            var isNotMarked = bookmark == null; //default(Bookmark);

            if (isNotMarked)
            {
                var surah = await App.Database.GetSurah(ayah.SurahId);

                await App.Database.AddBookmark(new Bookmark()
                {
                    SurahId = ayah.SurahId,
                    AyahId = ayah.AyahId,
                    BookmarkType = BookmarkType.Id,
                    DateCreated = DateTime.Now,
                    Description = $"{surah.EnArName} - {surah.ArabicName}{Environment.NewLine}[{ayah.SurahId}:{ayah.AyahId}]",
                });

                if (Surah.Count != 0)
                {
                    Surah[Surah.IndexOf(ayah)].BookmarkedIcon = App.BookmarkFilledBlackIcon;
                }
            }
            else
            {
                await App.Database.DeleteBookmark(bookmark);
                if (Surah.Count != 0)
                {
                    Surah[Surah.IndexOf(ayah)].BookmarkedIcon = App.BookmarkHollowBlackIcon;
                }
            }
        }

        private void Settings_Command()
        {
            Navigation.PushAsync(new View.Settings.QuranReaderSettingsPage());
        }

        #region Player Commands
        /*
        private void HandleAyahChanged()
        {
            //if (Surah.IndexOf(SelectedAyah) != _manager.MediaQueue.Index)
            _manager.MediaQueue.SetIndexAsCurrent(Surah.IndexOf(SelectedAyah));
            //ShowToast($"{_manager.MediaQueue.Index}, [{SelectedAyah.SurahId}:{SelectedAyah.AyahId}]");

            CurrentAyahLabel = SelectedAyah.SurahLocation;

            if (ShowPlayer)
            {
                _manager.Play(_manager.MediaQueue[_manager.MediaQueue.Index]);
                isPlaying = true;
            }

            ShowToast($"{(isPlaying ? "Now" : "Not")} playing {(isPlaying ? $": {SelectedAyah.SurahLocation}" : " media")}");
        }

        private async void Manager_MediaFinished(object sender, MediaFinishedEventArgs e)
        {
            if (e.File == _manager.MediaQueue.LastOrDefault())
            {
                await _player.Stop();
                isPlaying = false;
                ShowPlayer = false;
                PlayPauseIcon = playIcon;
            }
            else
            {
                if (Surah.Count > _manager.MediaQueue.IndexOf(e.File) + 1)// && Surah.IndexOf(SelectedAyah) != _manager.MediaQueue.Index)
                {
                    try
                    {
                        SelectedAyah = Surah[_manager.MediaQueue.IndexOf(e.File) + 1];
                        _listView.ScrollTo(SelectedAyah, ScrollToPosition.MakeVisible, false);
                    }
                    catch (Exception)
                    {
                        
                    }
                }
            }
        }

        private void ShowHidePlayer_Command()
        {
            ShowToast($"isPlaying: {isPlaying}");

            if (!isLoaded)
            {
                ShowToast($"Loading Queue: ({_manager.MediaQueue.Count}/{expectedLength})");
                //return;
            }
            if (!File.Exists(_manager.MediaQueue.Current.Url))
            {
                ShowToast("Error: File does not exist");
                //DowloadFilesInSurah();
                return;
            }

            ShowPlayer = !ShowPlayer;

            if (isPlaying)
            {
                _player.Stop();

                _manager.Play(_manager.MediaQueue[_manager.MediaQueue.Index]);
                isPlaying = true;
            }
            else
            {
                isPlaying = false;
                _player.Stop();
            }
        }

        private void Stop_Command()
        {
            _player.Stop();
            ShowPlayer = false;
            isPlaying = false;
        }

        private async void Previous_Command()
        {
            ShowToast($"isPlaying: {isPlaying}");
            if (ShowPlayer && _manager.MediaQueue.Index > 0)
            {
                //_manager.MediaQueue.SetIndexAsCurrent(_manager.MediaQueue.IndexOf(_manager.MediaQueue.Current) - 1);
                await _player.PlayPrevious();
                //DependencyService.Get<IMethodHelper>().ShowToast(_manager.MediaQueue.Index.ToString());
                //_manager.MediaQueue.SetIndexAsCurrent(_manager.MediaQueue.Index - 1);
                SelectedAyah = Surah[_manager.MediaQueue.Index];
                isPlaying = true;
                _listView.ScrollTo(SelectedAyah, ScrollToPosition.MakeVisible, false);
            }
        }

        //private void PlayPause_Command()
        //{
        //    if (!isLoaded)
        //    {
        //        DependencyService.Get<IMethodHelper>().ShowToast($"Queue is loading ... ({_manager.MediaQueue.Count}/{expectedLength})");
        //        return;
        //    }
        //
        //    if (!isPlaying)
        //    {
        //        // Play
        //        if (SelectedAyah == null)
        //        {
        //            DependencyService.Get<IMethodHelper>().ShowToast("Error: Select an ayah");
        //            return;
        //        }
        //        if (_manager.MediaQueue.Count != Surah.Count)
        //        {
        //            DependencyService.Get<IMethodHelper>().ShowToast($"Loading: ({_manager.MediaQueue.Count}/{expectedLength})", false);
        //            
        //            return;
        //        }
        //
        //        _player.Play();
        //        PlayPauseIcon = pauseIcon;
        //        isPlaying = true;
        //
        //        DependencyService.Get<IMethodHelper>().ShowToast($"Playing Index: {_manager.MediaQueue.Index}{Environment.NewLine}{_manager.MediaQueue[_manager.MediaQueue.Index].Url}");
        //    }
        //    else
        //    {
        //        // Pause
        //        _manager.Pause();
        //        PlayPauseIcon = playIcon;
        //        isPlaying = false;
        //    }
        //}

        private async void Next_Command()
        {
            ShowToast($"isPlaying: {isPlaying}");
            if (_manager.MediaQueue.Index < _manager.MediaQueue.Count - 1)
            {
                //_manager.MediaQueue.SetIndexAsCurrent(_manager.MediaQueue.IndexOf(_manager.MediaQueue.Current) + 1);
                await _player.PlayNext();
                //DependencyService.Get<IMethodHelper>().ShowToast(_manager.MediaQueue.Index.ToString());
                //_manager.MediaQueue.SetIndexAsCurrent(_manager.MediaQueue.Index + 1);
                SelectedAyah = Surah[_manager.MediaQueue.Index];
                isPlaying = true;
                _listView.ScrollTo(SelectedAyah, ScrollToPosition.MakeVisible, false);
            }
        }

        private void AudioSettings_Command()
        {

        }
        */
        #endregion
    }
}
