using MuslimSurvivalKit.Data;
using MuslimSurvivalKit.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MuslimSurvivalKit.ViewModel
{
    public class AllBookmarksViewModel : BaseViewModel
    {
        #region Binding Commands
        public ICommand LastIdCommand => new Command(LastId_Command);
        public ICommand LastPageCommand => new Command(LastPage_Command);
        #endregion

        #region Binding Properties
        public ObservableCollection<Bookmark> Bookmarks { get; set; } = new ObservableCollection<Bookmark>();

        private Bookmark _lastBookmark;
        public Bookmark LastBookmark
        {
            get { return _lastBookmark; }
            set
            {
                _lastBookmark = value;
                RaisePropertyChanged(nameof(LastBookmark));
            }
        }

        private string _bookmarksMessage;
        public string BookmarksMessage
        {
            get { return _bookmarksMessage; }
            set
            {
                _bookmarksMessage = value;
                RaisePropertyChanged(nameof(BookmarksMessage));
            }
        }
        #endregion

        private bool isPushing = false;
        INavigation Navigation;
        public AllBookmarksViewModel(INavigation navigation)
        {
            Navigation = navigation;

            LoadLastPage();
            LoadBookmarks();

            //Bookmarks.Add(
            //    new Bookmark()
            //    {
            //        Description = "Surah Najm sajda ayah",
            //        DateCreated = DateTime.Now.Subtract(TimeSpan.FromHours(3.2)),
            //        BookmarkType = BookmarkType.Id,
            //        SurahId = 53,
            //        AyahId = 62,
            //    });
            //Bookmarks.Add(
            //    new Bookmark()
            //    {
            //        Description = "Surah Baqarh",
            //        DateCreated = DateTime.Now.Subtract(TimeSpan.FromHours(3.2)),
            //        BookmarkType = BookmarkType.Id,
            //        SurahId = 2,
            //        AyahId = 1,
            //    });
            //Bookmarks.Add(
            //    new Bookmark()
            //    {
            //        Description = "Surah Najm",
            //        DateCreated = DateTime.Now.Subtract(TimeSpan.FromHours(3.2)),
            //        BookmarkType = BookmarkType.Page,
            //        PageNumber = 526,
            //    });
        }

        public async void LoadLastPage()
        {
            var last = await App.Database.GetLast();

            if (last == null)
            {
                Debug.WriteLine($"Error: last bookmark not found {last}");
                return;
            }

            last.BookmarkIdHeader = "Location Id: ";
            last.BookmarkIdDetail = $"Surah {last.SurahId} Ayah {last.AyahId}";
            last.BookmarkPageHeader = "Page Number: ";
            last.BookmarkPageDetail = $"Page {last.PageNumber}";
            
            LastBookmark = last;
        }

        public async void LoadBookmarks()
        {
            Bookmarks.Clear();

            var bookmarks = await App.Database.GetBookmarks();

            foreach (var bookmark in bookmarks)
            {
                if (bookmark.BookmarkType == BookmarkType.Id)
                {
                    bookmark.BookmarkDetail = $"Surah {bookmark.SurahId} Ayah {bookmark.AyahId}";
                }
                else
                    bookmark.BookmarkDetail = $"Page {bookmark.PageNumber}";

                bookmark.BookmarkDate = bookmark.DateCreated.ToString().Replace(" ", Environment.NewLine);

                Bookmarks.Add(bookmark);
            }

            BookmarksMessage = Bookmarks.Count == 0 ? "No bookmarks, time to start reading!" : String.Empty;
        }

        private async void LastId_Command()
        {
            lock (this)
            {
                if (isPushing)
                    return;
                else
                    isPushing = true;
            }

            await Navigation.PushModalAsync(new NavigationPage(new View.Quran.Reader.SurahView.QuranReaderMasterPage(LastBookmark.SurahId, LastBookmark.AyahId)));

            isPushing = false;
        }

        private async void LastPage_Command()
        {
            lock (this)
            {
                if (isPushing)
                    return;
                else
                    isPushing = true;
            }

            await Navigation.PushAsync(new View.Quran.Reader.PageView.QuranPages(LastBookmark.PageNumber, true));

            isPushing = false;
        }
    }
}
