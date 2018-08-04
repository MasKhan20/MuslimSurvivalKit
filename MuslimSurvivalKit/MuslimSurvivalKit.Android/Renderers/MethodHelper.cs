using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MuslimSurvivalKit.Data;
using MuslimSurvivalKit.Droid.Renderers;
using Plugin.CurrentActivity;
using Xamarin.Forms;

[assembly:Dependency(typeof(MethodHelper))]
namespace MuslimSurvivalKit.Droid.Renderers
{
    public class MethodHelper : IMethodHelper
    {
        public void ShowToast(string message, bool shortToast = true)
        {
            Toast.MakeText(
                CrossCurrentActivity.Current.AppContext, 
                message, 
                shortToast ? ToastLength.Short : ToastLength.Long).Show();
        }
    }
}