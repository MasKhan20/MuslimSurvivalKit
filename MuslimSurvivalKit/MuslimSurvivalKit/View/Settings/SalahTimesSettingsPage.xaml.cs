using MuslimSurvivalKit.ViewModel;
using PrayerTimes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MuslimSurvivalKit.View.Settings
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SalahTimesSettingsPage : ContentPage
	{
		public SalahTimesSettingsPage()
		{
			InitializeComponent();
            Title = "Salah Settings";
            Init();

            var viewmodel = new SalahTimesSettingsViewModel();

            BindingContext = viewmodel;

            MessagingCenter.Subscribe<SalahTimesSettingsViewModel, (string title, string message)>(viewmodel, "Alert",
                (s, e) =>
                {
                    DisplayAlert(e.title, e.message, "OK");
                });
		}

        private void Init()
        {
            /*calcMethods.ItemsSource = new Dictionary<string, CalculationMethods>()
            {
                { "Muslim World League", CalculationMethods.MWL },
                { "Islamic Society of North America", CalculationMethods.ISNA },
                { "Egyptian General Authority of Survey", CalculationMethods.Egypt },
                { "Umm al-Qura University, Makkah", CalculationMethods.Makkah },
                { "University of Islamic Sciences, Karachi", CalculationMethods.Karachi },
                { "Institute of Geophysics, University of Tehran", CalculationMethods.Custom },
                { "Shia Ithna Ashari (Ja`fari)", CalculationMethods.Jafari },
            };
            asrJuristicMethods.ItemsSource = new Dictionary<string, AsrJuristicMethods>()
            {
                { "Shafii, Maliki, Jafari and Hanbali (shadow factor = 1)", PrayerTimes.AsrJuristicMethods.Shafii },
                { "Hanafi school of tought (shadow factor = 2)", PrayerTimes.AsrJuristicMethods.Hanafi },
            };
            latitudeAdjustmentMethods.ItemsSource = new Dictionary<string, HighLatitudeAdjustmentMethods>()
            {
                { "No adjustments", HighLatitudeAdjustmentMethods.None },
                { "The middle of the night method", HighLatitudeAdjustmentMethods.MidNight },
                { "The 1/7th of the night method", HighLatitudeAdjustmentMethods.OneSeventh },
                { "The angle-based method (recommended)", HighLatitudeAdjustmentMethods.AngleBased },
            };*/

            //calcMethods.ItemsSource = new List<string>()
            //{
            //    "Muslim World League",
            //    "Islamic Society of North America",
            //    "Egyptian General Authority of Survey",
            //    "Umm al-Qura University, Makkah",
            //    "University of Islamic Sciences, Karachi",
            //    "Institute of Geophysics, University of Tehran",
            //    "Shia Ithna Ashari (Ja`fari)",
            //};
            //asrJuristicMethods.ItemsSource = new List<string>()
            //{
            //    "Shafii, Maliki, Jafari and Hanbali (shadow factor = 1)",
            //    "Hanafi school of tought (shadow factor = 2)",
            //};
            //latitudeAdjustmentMethods.ItemsSource = new List<string>()
            //{
            //    "No adjustments",
            //    "The middle of the night method",
            //    "The 1/7th of the night method",
            //    "The angle-based method (recommended)",
            //};
        }
    }
}