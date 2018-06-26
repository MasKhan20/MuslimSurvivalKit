using MuslimSurvivalKit.Data;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace MuslimSurvivalKit
{
    public partial class App : Application
    {
        public const string DatabaseFile = "MuslimSurvivalKitSQLite.db3";

        public static string DataBasePath = DependencyService.Get<IFileHelper>().GetDatabasePath(App.DatabaseFile);

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());//(new View.Quran.Reader.QuranReaderMasterPage());
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
    }
}
