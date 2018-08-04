using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MuslimSurvivalKit.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

//[assembly: ExportRenderer(typeof(NavigationPage), typeof(CustomNavPage))]
namespace MuslimSurvivalKit.Droid.Renderers
{
    public class CustomNavPage : NavigationPageRenderer
    {
        public CustomNavPage(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<NavigationPage> e)
        {
            base.OnElementChanged(e);

            //var bar = (Android.Support.V7.Widget.Toolbar)typeof(NavigationPageRenderer)
            //        .GetField("_toolbar", BindingFlags.NonPublic | BindingFlags.Instance)
            //        .GetValue(this);
            //bar.SetLogo(Resource.Drawable.kabah_logo);
        }
    }
}