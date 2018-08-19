using MuslimSurvivalKit.Data;
using MuslimSurvivalKit.Model.Salah;
using MuslimSurvivalKit.Services;
using MuslimSurvivalKit.View.Settings;
using PrayerTimes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MuslimSurvivalKit.ViewModel
{
    public class SalahTimesViewModel : BaseViewModel
    {
        #region Binding Commands
        public ICommand SalahSettingsCommand => new Command(SalahSettings_Command);
        #endregion

        #region Binding Properties
        private Times _salahTimes;
        public Times SalahTimes
        {
            get { return _salahTimes; }
            set
            {
                _salahTimes = value;
                RaisePropertyChanged(nameof(SalahTimes));
            }
        }
        #endregion

        INavigation Navigation;
        public SalahTimesViewModel(INavigation navigation)
        {
            Navigation = navigation;
            Init();
        }

        public async void Init()
        {
            var (success, latitude, longitude, message) = await Location.GetCurrentLocation();

            if (!success)
                DisplayAlert("Error", message);
            else
                DependencyService.Get<IMethodHelper>().ShowToast(message);

            try
            {
                GetSalahTimes(latitude, longitude);
            }
            catch (Exception exc)
            {
                DisplayAlert(exc.Message, exc.ToString());
            }
        }

        private void GetSalahTimes(double latitude, double longitude)
        {
            var calculator = new PrayerTimes.PrayerTimesCalculator(latitude, longitude)
            {
                CalculationMethod = Settings.SalahCalculationMethod,
                AsrJurusticMethod = Settings.SalahAsrJuristicMethod,
                HighLatitudeAdjustmentMethod = Settings.SalahLatitudeAdjustmentMethod
            };

            SalahTimes = calculator.GetPrayerTimes(DateTimeOffset.Now, Settings.TimeZone);
        }

        private void DisplayAlert(string title, string message)
        {
            MessagingCenter.Send(this, "Alert", (title, message));
        }

        private void SalahSettings_Command()
        {
            Navigation.PushAsync(new SalahTimesSettingsPage());
        }
    }
}
