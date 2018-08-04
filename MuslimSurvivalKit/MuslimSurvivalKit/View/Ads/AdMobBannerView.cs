using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MuslimSurvivalKit.View.Ads
{
    public class AdMobBannerView : Xamarin.Forms.View
    {
        public static readonly BindableProperty AdUnitIdProperty = BindableProperty.Create(
            nameof(AdUnitId),
            typeof(string),
            typeof(AdMobBannerView),
            string.Empty);
        
        public string AdUnitId
        {
        	get => (string)GetValue(AdUnitIdProperty);
        	set => SetValue(AdUnitIdProperty, value);
        }

        public AdMobBannerView() { }

        public AdMobBannerView(string adUnitId)
        {
            AdUnitId = adUnitId;
        }
    }
}
