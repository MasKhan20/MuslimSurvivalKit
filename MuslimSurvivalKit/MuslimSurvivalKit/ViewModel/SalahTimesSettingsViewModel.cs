using MuslimSurvivalKit.Data;
using MuslimSurvivalKit.Services;
using PrayerTimes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MuslimSurvivalKit.ViewModel
{
    public class SalahTimesSettingsViewModel : BaseViewModel
    {
        #region Binding Commands
        public ICommand UtcPickerCommand => new Command(UtcPicker_Command);
        #endregion

        #region Binding Proporties
        private double _desiredAccuracy;
        public double DesiredAccuracy
        {
            get { return _desiredAccuracy; }
            set
            {
                _desiredAccuracy = value;
                RaisePropertyChanged(nameof(DesiredAccuracy));

                Settings.DesiredLocationAccuracy = (int)DesiredAccuracy;
                DesiredAccuracyText = $"Desired Accuracy ({Settings.DesiredLocationAccuracy} meter{(Settings.DesiredLocationAccuracy == 1 ? "" : "s")})";
            }
        }

        private string _desiredAccuracyText;
        public string DesiredAccuracyText
        {
            get { return _desiredAccuracyText; }
            set
            {
                _desiredAccuracyText = value;
                RaisePropertyChanged(nameof(DesiredAccuracyText));
            }
        }

        private Dictionary<string, CalculationMethods> CalcMethodEnums;
        private Dictionary<string, AsrJuristicMethods> AsrJuristicMethodEnums;
        private Dictionary<string, HighLatitudeAdjustmentMethods> LatitudeAdjustmentMethodEnums;
        private Dictionary<string, int> TimeZoneInts;

        public ObservableCollection<string> Countries { get; set; } = new ObservableCollection<string>();

        public ObservableCollection<string> CalcMethods { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> AsrJuristicMethods { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> LatitudeAdjustmentMethods { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> TimeZones { get; set; } = new ObservableCollection<string>();

        private string _calcMethod;
        public string CalcMethod
        {
            get { return _calcMethod; }
            set
            {
                _calcMethod = value;
                RaisePropertyChanged(nameof(CalcMethod));

                if (CalcMethodEnums.ContainsKey(CalcMethod))
                    Settings.SalahCalculationMethod = CalcMethodEnums[CalcMethod];
            }
        }

        private string _asrJuristicMethod;
        public string AsrJuristicMethod
        {
            get { return _asrJuristicMethod; }
            set
            {
                _asrJuristicMethod = value;
                RaisePropertyChanged(nameof(AsrJuristicMethod));

                if (AsrJuristicMethodEnums.ContainsKey(AsrJuristicMethod))
                    Settings.SalahAsrJuristicMethod = AsrJuristicMethodEnums[AsrJuristicMethod];
            }
        }

        private string _latitudeAdjustmentMethod;
        public string LatitudeAdjustmentMethod
        {
            get { return _latitudeAdjustmentMethod; }
            set
            {
                _latitudeAdjustmentMethod = value;
                RaisePropertyChanged(nameof(LatitudeAdjustmentMethod));

                if (LatitudeAdjustmentMethodEnums.ContainsKey(LatitudeAdjustmentMethod))
                    Settings.SalahLatitudeAdjustmentMethod = LatitudeAdjustmentMethodEnums[LatitudeAdjustmentMethod];
            }
        }

        private string _timeZone;
        public string TimeZone
        {
            get { return _timeZone; }
            set
            {
                _timeZone = value;
                RaisePropertyChanged(nameof(TimeZone));

                if (TimeZoneInts.ContainsKey(TimeZone))
                    Settings.TimeZone = TimeZoneInts[TimeZone];
            }
        }

        private bool _openUtcPicker = false;
        public bool OpenUtcPicker
        {
            get { return _openUtcPicker; }
            set
            {
                _openUtcPicker = value;
                RaisePropertyChanged(nameof(OpenUtcPicker));
            }
        }

        /*
        private string _selectedUtcTime;
        public string SelectedUtcTime
        {
            get { return _selectedUtcTime; }
            set
            {
                _selectedUtcTime = value;
                RaisePropertyChanged(nameof(SelectedUtcTime));
            }
        }
        */
        #endregion

        public SalahTimesSettingsViewModel()
        {
            InitPickers();
            InitSettings();

            var picker = new Syncfusion.SfPicker.XForms.SfPicker();
        }

        private void InitPickers()
        {
            foreach (var country in Location.CountryNames())
            {
                Countries.Add(country);
            }

            // Picker Dictionaries
            CalcMethodEnums = new Dictionary<string, CalculationMethods>()
            {
                { "Muslim World League", CalculationMethods.MWL },
                { "Islamic Society of North America", CalculationMethods.ISNA },
                { "Egyptian General Authority of Survey", CalculationMethods.Egypt },
                { "Umm al-Qura University, Makkah", CalculationMethods.Makkah },
                { "University of Islamic Sciences, Karachi", CalculationMethods.Karachi },
                { "Institute of Geophysics, University of Tehran", CalculationMethods.Custom },
                { "Shia Ithna Ashari (Ja`fari)", CalculationMethods.Jafari },
            };
            AsrJuristicMethodEnums = new Dictionary<string, AsrJuristicMethods>()
            {
                { "Shafii, Maliki, Jafari and Hanbali (shadow factor = 1)", PrayerTimes.AsrJuristicMethods.Shafii },
                { "Hanafi school of tought (shadow factor = 2)", PrayerTimes.AsrJuristicMethods.Hanafi },
            };
            LatitudeAdjustmentMethodEnums = new Dictionary<string, HighLatitudeAdjustmentMethods>()
            {
                { "No adjustments", HighLatitudeAdjustmentMethods.None },
                { "The middle of the night method", HighLatitudeAdjustmentMethods.MidNight },
                { "The 1/7th of the night method", HighLatitudeAdjustmentMethods.OneSeventh },
                { "The angle-based method", HighLatitudeAdjustmentMethods.AngleBased },
            };
            TimeZoneInts = new Dictionary<string, int>();

            for (int i = -12; i <= 12; i++)
            {
                TimeZoneInts.Add($"UTC {(i >= 0 ? "+" : "")}{i}:00", i);
            }

            //Picker Values
            foreach (var calcMethod in CalcMethodEnums.Keys)
            {
                CalcMethods.Add(calcMethod);
            }
            foreach (var asrJuristicMethod in AsrJuristicMethodEnums.Keys)
            {
                AsrJuristicMethods.Add(asrJuristicMethod);
            }
            foreach (var highLatitudeAdjustmentMethod in LatitudeAdjustmentMethodEnums.Keys)
            {
                LatitudeAdjustmentMethods.Add(highLatitudeAdjustmentMethod);
            }
            foreach (var timeZone in TimeZoneInts.Keys)
            {
                TimeZones.Add(timeZone);
            }
        }

        private void InitSettings()
        {
            //IFDEBUG
            //Settings.DesiredLocationAccuracy = 100;
            //Settings.SalahCalculationMethod = CalculationMethods.MWL;
            //Settings.SalahAsrJuristicMethod = PrayerTimes.AsrJuristicMethods.Hanafi;
            //Settings.SalahLatitudeAdjustmentMethod = HighLatitudeAdjustmentMethods.None;
            //ENDIF

            DesiredAccuracy = Settings.DesiredLocationAccuracy;
            CalcMethod = CalcMethodEnums.FirstOrDefault((c) => c.Value == Settings.SalahCalculationMethod).Key;
            AsrJuristicMethod = AsrJuristicMethodEnums.FirstOrDefault((a) => a.Value == Settings.SalahAsrJuristicMethod).Key;
            LatitudeAdjustmentMethod = LatitudeAdjustmentMethodEnums.FirstOrDefault((l) => l.Value == Settings.SalahLatitudeAdjustmentMethod).Key;
            TimeZone = TimeZoneInts.FirstOrDefault((t) => t.Value == Settings.TimeZone).Key;
        }

        private void ShowToast(string message, bool isLong = false)
        {
            DependencyService.Get<IMethodHelper>().ShowToast(message, !isLong);
        }

        private void UtcPicker_Command()
        {
            if (OpenUtcPicker == false)
                OpenUtcPicker = true;
        }
    }
}
