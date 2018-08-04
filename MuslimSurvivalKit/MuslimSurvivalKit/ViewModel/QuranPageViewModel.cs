using MuslimSurvivalKit.Data;
using MuslimSurvivalKit.Model;
using MuslimSurvivalKit.View.Quran.Reader.PageView;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MuslimSurvivalKit.ViewModel
{
    public class QuranPageViewModel : BaseViewModel
    {
        #region Binding Commands
        public ICommand BookmarkCommand => new Command(Bookmark_Command);
        public ICommand DownloadPageCommand => new Command(DownloadPage_Command);
        public ICommand RefreshCommand => new Command(Refresh_Command);
        public ICommand ToggleVisibilityCommand => new Command(ToggleVisibility_Command);
        #endregion

        #region Binding Properties
        private string _bookmarkedIcon;
        public string BookmarkedIcon
        {
            get { return _bookmarkedIcon; }
            set
            {
                _bookmarkedIcon = value;
                RaisePropertyChanged(nameof(BookmarkedIcon));
            }
        }

        private bool _showBar;
        public bool ShowBar
        {
            get { return _showBar; }
            set
            {
                _showBar = value;
                RaisePropertyChanged(nameof(ShowBar));
            }
        }

        private Color _bgColor;
        public Color BgColor
        {
            get { return _bgColor; }
            set
            {
                _bgColor = value;
                RaisePropertyChanged(nameof(BgColor));
            }
        }

        private ImageSource _pageSource;
        /// <summary>
        /// The source for the current image to display
        /// </summary>
        public ImageSource PageSource
        {
            get { return _pageSource; }
            set
            {
                _pageSource = value;
                RaisePropertyChanged(nameof(PageSource));
            }
        }
        #endregion

        bool isDownloading = false;

        int _pageNumber;
        string _imagePath;
        QuranPages _parent;
        List<Bookmark> _bookmarks;
        public QuranPageViewModel(QuranPages parent, string imagePath, int pageNumber, List<Bookmark> bookmarks)
        {
            Init(parent, imagePath, pageNumber, bookmarks);
        }

        private void Init(QuranPages parent, string imagePath, int pageNumber, List<Bookmark> bookmarks)
        {
            _parent = parent;
            _imagePath = imagePath;
            _pageNumber = pageNumber;
            BgColor = Color.LightGray;
            PageSource = GetSource(imagePath);
            ShowBar = true;
            _bookmarks = bookmarks;

            var bookmark = _bookmarks
                .Where(b => b.PageNumber == _pageNumber)
                .FirstOrDefault();

            BookmarkedIcon = bookmark != null 
                ? App.BookmarkFilledWhiteIcon 
                : App.BookmarkHollowWhiteIcon;
        }

        private void DisplayAlert(string title, string message)
        {
            MessagingCenter.Send(this, "Alert", (title, message));
        }

        private ImageSource GetSource(string path)
        {
            if (!File.Exists(path))
            {
                System.Diagnostics.Debug.WriteLine($"Error: File does not exist - {Environment.NewLine}{path}");
            }

            return ImageSource.FromFile(path);
        }

        private async void Bookmark_Command()
        {
            _bookmarks = await App.Database.GetBookmarks();
            var bookmark = _bookmarks
                .Where(b => b.PageNumber == _pageNumber)
                .FirstOrDefault();

            var isNotMarked = bookmark == null;

            if (isNotMarked)
            {
                await App.Database.AddBookmark(new Bookmark()
                {
                    PageNumber = _pageNumber,
                    BookmarkType = BookmarkType.Page,
                    DateCreated = DateTime.Now,
                    Description = $"Page {_pageNumber}",
                });

                BookmarkedIcon = App.BookmarkFilledWhiteIcon;
            }
            else
            {
                await App.Database.DeleteBookmark(bookmark);

                BookmarkedIcon = App.BookmarkHollowWhiteIcon;
            }
        }

        private async void DownloadPage_Command()
        {
            lock (this)
            {
                if (isDownloading)
                    return;
                else
                    isDownloading = true;
            }

            if (!CrossConnectivity.Current.IsConnected)
            {
                DisplayAlert("Error not connected", "Please reconnect and try again");
                return;
            }

            try
            {
                using (WebClient client = new WebClient())
                {
                    string url = String.Format(App.QuranPageDownloadPath, _pageNumber.ToString().PadLeft(3, '0'));

                    await client.DownloadFileTaskAsync(url, _imagePath);
                }
            }
            catch (Exception exc)
            {
                DependencyService.Get<IMethodHelper>().ShowToast($"Error: {exc.Message}");
            }

            Refresh_Command();

            isDownloading = false;
        }

        private void Refresh_Command()
        {
            PageSource = GetSource(_imagePath);
        }

        private void ToggleVisibility_Command()
        {
            ShowBar = !ShowBar;
            NavigationPage.SetHasNavigationBar(_parent, ShowBar);
        }
    }
}
