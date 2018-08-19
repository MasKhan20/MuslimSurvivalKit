using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ImageCircle.Forms.Plugin.Droid;
//using Plugin.MediaManager;
//using Plugin.MediaManager.Abstractions.Enums;
using Android.Media;
using MuslimSurvivalKit.Data;

namespace MuslimSurvivalKit.Droid
{
    [Activity(Label = "Muslim Survival Kit", Icon = "@drawable/kabah_logo", Theme = "@style/splashscreen", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            if (Settings.StartupAudio)
            {
                MediaPlayer player = MediaPlayer.Create(this, Resource.Raw.bismillah);
                player.Start();
            }

            base.SetTheme(Resource.Style.MainTheme);

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, bundle);
            Rg.Plugins.Popup.Popup.Init(this, bundle);

            Android.Gms.Ads.MobileAds.Initialize(ApplicationContext, "ca-app-pub-4025243320631804~3716319700");
            global::Xamarin.Forms.Forms.Init(this, bundle);

            ImageCircleRenderer.Init();
            string dbPath = Renderers.FileAccessHelper.GetLocalFilePath(App.DatabaseFile);

            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

