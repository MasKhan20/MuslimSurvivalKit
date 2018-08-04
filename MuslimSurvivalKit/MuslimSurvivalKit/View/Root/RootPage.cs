using MuslimSurvivalKit.Data;
using MuslimSurvivalKit.View.Download;
using MuslimSurvivalKit.View.Quran;
using MuslimSurvivalKit.View.Quran.Bookmarks;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace MuslimSurvivalKit.View.Root
{
    public class RootPage : TabbedPage
    {
        public RootPage()
        {
            ToolbarItem pageType = new ToolbarItem()
            {
                Icon = Data.Settings.ViewBySurah ? App.SurahViewIcon : App.PageViewIcon,
            };
            pageType.Clicked += PageType_Clicked;

            ToolbarItem setting = new ToolbarItem()
            {
                Icon = App.SettingsIcon,
            };
            setting.Clicked += Setting_Clicked;

            ToolbarItems.Add(pageType);
            ToolbarItems.Add(setting);

            Children.Add(new SurahListPage(true) { Title = "Surahs" });
            Children.Add(new AllBookmarksPage() { Title = "Bookmarks" });
        }

        private void PageType_Clicked(object sender, EventArgs e)
        {
            var tool = sender as ToolbarItem;

            Data.Settings.ViewBySurah = !Data.Settings.ViewBySurah;
            tool.Icon = Data.Settings.ViewBySurah
                ? new FileImageSource() { File = App.SurahViewIcon } 
                : new FileImageSource() { File = App.PageViewIcon };

            //DependencyService.Get<IMethodHelper>()
            //    .ShowToast($"View by {(Data.Settings.ViewBySurah ? "surah" : "ayah")}. {tool.Icon.File}");
        }

        private void Setting_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Settings.AppSettingsPage());
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            Title = CurrentPage?.Title ?? App.AppName;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Title = App.AppName;

            CheckIfDownloaded();
        }

        private async void CheckIfDownloaded()
        {
            var path = Path.Combine(App.DownloadPath, "pages");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var files = Directory.GetFiles(path, "*.png");

            if (files.Length < 604)
            {
                //All files not downloaded

                if (App.isDownloading)
                    //Already downloading
                    return;

                App.isDownloading = true;
                await Navigation.PushPopupAsync(new DownloadPopup());
            }
        }
    }
}
