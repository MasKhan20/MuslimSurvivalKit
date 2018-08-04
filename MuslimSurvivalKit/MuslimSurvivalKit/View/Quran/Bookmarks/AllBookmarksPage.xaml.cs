using MuslimSurvivalKit.Data;
using MuslimSurvivalKit.Model;
using MuslimSurvivalKit.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MuslimSurvivalKit.View.Quran.Bookmarks
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AllBookmarksPage : ContentPage
	{
        private bool isPushing;
        private AllBookmarksViewModel viewmodel;
		public AllBookmarksPage()
		{
			InitializeComponent();

            viewmodel = new AllBookmarksViewModel(Navigation);

            BindingContext = viewmodel;
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewmodel.LoadLastPage();
            viewmodel.LoadBookmarks();
        }

        private async void BookmarksList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var bookmark = e.Item as Bookmark;
            if (bookmark == null)
                return;

            lock (this)
            {
                if (isPushing)
                    return;
                else
                    isPushing = false;
            }

            if (bookmark.BookmarkType == BookmarkType.Id)
                await Navigation.PushModalAsync(new NavigationPage(new Reader.SurahView.QuranReaderMasterPage(bookmark.SurahId, bookmark.AyahId)));
            else
                await Navigation.PushAsync(new Reader.PageView.QuranPages(bookmark.PageNumber, true));

            isPushing = false;
        }


        private async void RemoveBookmark_Clicked(object sender, EventArgs e)
        {
            var bookmark = (Bookmark)((MenuItem)sender).CommandParameter;

            await App.Database.DeleteBookmark(bookmark);
            
            viewmodel.Bookmarks.Remove(bookmark);
        }
    }
}