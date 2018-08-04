using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace MuslimSurvivalKit.View.Quran.Reader.PageView
{
    public class QuranPages : CarouselPage
    {
        public QuranPages(int surahIdOrPage = 1, bool isPage = false, string quranId = "default_604")
        {
            var imageDir = Path.Combine(App.DownloadPath, quranId);
            if (!Directory.Exists(imageDir))
                quranId = "default_604";

            Build(surahIdOrPage, isPage, quranId);
        }

        private async void Build(int surahIdOrPage, bool isPage, string quranId)
        {
            List<QuranPage> pages = new List<QuranPage>();
            var bookmarks = await App.Database.GetBookmarks();

            for (int i = 1; i < 605; i++)
            {
                var file = Path.Combine(App.DownloadPath, "pages", $"{i.ToString().PadLeft(3, '0')}.png");
                var page = new QuranPage(this, file, i, bookmarks)
                {
                    Title = $"Quran - Page {i}",
                };

                //this.SetBinding(NavigationPage.HasNavigationBarProperty, nameof(page._viewmodel.ShowBar));
                //NavigationPage.SetHasNavigationBar(this, page._viewmodel.ShowBar);
                pages.Add(page);
            }

            pages.Reverse();

            foreach (var page in pages)
            {
                Children.Add(page);
            }

            //var index = isPage
            //    ? 
            //    : ;

            CurrentPage = Children[pages.Count - surahIdOrPage];
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            Title = CurrentPage?.Title ?? "Quran Pages";

            var valid = int.TryParse(CurrentPage.Title.Split(' ').LastOrDefault(), out int pageNumber);

            if (valid)
            {
                UpdateLastPage(pageNumber); 
            }
        }

        private async void UpdateLastPage(int pageNumber)
        {
            await App.Database.UpdateLastPage(pageNumber);
        }
    }
}
