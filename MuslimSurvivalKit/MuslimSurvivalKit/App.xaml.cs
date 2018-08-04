using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.WindowsAzure.MobileServices;
using MuslimSurvivalKit.Data;
//using Plugin.MediaManager.Forms;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace MuslimSurvivalKit
{
    public partial class App : Application
    {
        public static string AppName = "Muslim Survival Kit";

        public const string DatabaseFile = "MuslimSurvivalKitSQLite.db3";

        public static string DataBasePath = DependencyService.Get<IFileHelper>().GetDatabasePath(App.DatabaseFile);

        public static string DownloadPath = DependencyService.Get<IFileHelper>().GetDownloadDirectory();

        public static bool isDownloading = false;

        public static MobileServiceClient MobileService 
            = new MobileServiceClient("https://muslimsurvivalkit.azurewebsites.net");

        public App()
        {
            InitializeComponent();

            CurrentPlatform.Init();

            AppCenter.Start("9657bb54-236b-4cab-8f40-401cfa4834c1", typeof(Analytics), typeof(Crashes));

            // Workaround
            //var view = typeof(VideoView);

            MainPage = new NavigationPage(new View.Root.RootPage());//(new View.Quran.Reader.QuranReaderMasterPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private static MuslimKitDataBase database;
        public static MuslimKitDataBase Database
        {
            get
            {
                if (database == null)
                    database = new MuslimKitDataBase(DataBasePath);

                return database;
            }
        }

        public const string QuranPageDownloadPath = "https://muslimsurvivalkitstorage.file.core.windows.net/quranaudio/pages/{0}.png?sv=2017-11-09&ss=bf&srt=sco&sp=r&se=2020-12-31T23:06:35Z&st=2018-07-31T14:06:35Z&spr=https&sig=J8NPhZ89HekXLUH7F7ugnUiir6RZHHq9gKguez0fVAQ%3D";

        #region App Constants
        ///Icons
        public static string SettingsIcon = "ic_settings_white_36dp.png";
        public static string DownloadWhiteIcon = "ic_file_download_white_236p.png";
        public static string BookmarkFilledBlackIcon = "ic_bookmark_black_36dp.png";
        public static string BookmarkHollowBlackIcon = "ic_bookmark_border_black_36dp.png";
        public static string BookmarkFilledWhiteIcon = "ic_bookmark_white_36dp.png";
        public static string BookmarkHollowWhiteIcon = "ic_bookmark_border_white_36dp.png";
        public static string SurahViewIcon = "view_stream_white_36dp.png";
        public static string PageViewIcon = "view_carousel_white_36dp.png";
        #endregion
    }
}
