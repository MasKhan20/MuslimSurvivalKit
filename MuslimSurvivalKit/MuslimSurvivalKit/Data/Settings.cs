using Plugin.Settings;
using Plugin.Settings.Abstractions;
using PrayerTimes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuslimSurvivalKit.Data
{
    public class Settings
    {
        public const string settingsFile = "MuslimSurvivalKitSettings";

        private static ISettings AppSettings
        {
            get
            {
                if (CrossSettings.IsSupported)
                    return CrossSettings.Current; 

                return null;
            }
        }

        public static void OpenSettings()
        {
            AppSettings.OpenAppSettings();
        }

        public static bool StartupAudio
        {
            get => AppSettings.GetValueOrDefault(nameof(StartupAudio), true, settingsFile);
            set => AppSettings.AddOrUpdateValue(nameof(StartupAudio), value, settingsFile);
        }

        #region Quran Settings
        public static bool ViewBySurah
        {
            get => AppSettings.GetValueOrDefault(nameof(ViewBySurah), true, settingsFile);
            set => AppSettings.AddOrUpdateValue(nameof(ViewBySurah), value, settingsFile);
        }

        public static bool ShowArabic
        {
            get => AppSettings.GetValueOrDefault(nameof(ShowArabic), true, settingsFile);
            set => AppSettings.AddOrUpdateValue(nameof(ShowArabic), value, settingsFile);
        }

        public static int ArTextSize
        {
            get => AppSettings.GetValueOrDefault(nameof(ArTextSize), 40, settingsFile);
            set => AppSettings.AddOrUpdateValue(nameof(ArTextSize), value, settingsFile);
        }

        public static bool ShowIdBookmarks
        {
            get => AppSettings.GetValueOrDefault(nameof(ShowIdBookmarks), true, settingsFile);
            set => AppSettings.AddOrUpdateValue(nameof(ShowIdBookmarks), value, settingsFile);
        }

        public static bool ShowLocation
        {
            get => AppSettings.GetValueOrDefault(nameof(ShowLocation), true, settingsFile);
            set => AppSettings.AddOrUpdateValue(nameof(ShowLocation), value, settingsFile);
        }

        public static bool ShowTranslation
        {
            get => AppSettings.GetValueOrDefault(nameof(ShowTranslation), false, settingsFile);
            set => AppSettings.AddOrUpdateValue(nameof(ShowTranslation), value, settingsFile);
        }

        public static string TranslationId
        {
            get => AppSettings.GetValueOrDefault(nameof(TranslationId), "EnSahih", settingsFile);
            set => AppSettings.AddOrUpdateValue(nameof(TranslationId), value, settingsFile);
        }

        public static bool AutoSelectFirst
        {
            get => AppSettings.GetValueOrDefault(nameof(AutoSelectFirst), true, settingsFile);
            set => AppSettings.AddOrUpdateValue(nameof(AutoSelectFirst), value, settingsFile);
        }
        #endregion

        #region Location Settings
        public static int DesiredLocationAccuracy
        {
            get => AppSettings.GetValueOrDefault(nameof(DesiredLocationAccuracy), 100, settingsFile);
            set => AppSettings.AddOrUpdateValue(nameof(DesiredLocationAccuracy), value, settingsFile);
        }

        private static double _latitude
        {
            get => AppSettings.GetValueOrDefault(nameof(_latitude), 0, settingsFile);
            set => AppSettings.AddOrUpdateValue(nameof(_latitude), value, settingsFile);
        }

        private static double _longitude
        {
            get => AppSettings.GetValueOrDefault(nameof(_longitude), 0, settingsFile);
            set => AppSettings.AddOrUpdateValue(nameof(_longitude), value, settingsFile);
        }

        public static (double latitude, double longitude) GetLastLocation()
        {
            return (_latitude, _longitude);
        }

        public static void SetLastLocation(double latitude, double longitude)
        {
            _latitude = latitude;
            _longitude = longitude;
        }
        #endregion

        #region Salah Settings
        public static CalculationMethods SalahCalculationMethod
        {
            get => (CalculationMethods)Enum.Parse(typeof(CalculationMethods), AppSettings.GetValueOrDefault(nameof(SalahCalculationMethod), CalculationMethods.MWL.ToString(), settingsFile));
            set => AppSettings.AddOrUpdateValue(nameof(SalahCalculationMethod), value.ToString(), settingsFile);
        }

        public static AsrJuristicMethods SalahAsrJuristicMethod
        {
            get => (AsrJuristicMethods)Enum.Parse(typeof(AsrJuristicMethods), AppSettings.GetValueOrDefault(nameof(SalahAsrJuristicMethod), AsrJuristicMethods.Hanafi.ToString(), settingsFile));
            set => AppSettings.AddOrUpdateValue(nameof(SalahAsrJuristicMethod), value.ToString(), settingsFile);
        }

        public static HighLatitudeAdjustmentMethods SalahLatitudeAdjustmentMethod
        {
            get => (HighLatitudeAdjustmentMethods)Enum.Parse(typeof(HighLatitudeAdjustmentMethods), AppSettings.GetValueOrDefault(nameof(SalahLatitudeAdjustmentMethod), HighLatitudeAdjustmentMethods.None.ToString(), settingsFile));
            set => AppSettings.AddOrUpdateValue(nameof(SalahLatitudeAdjustmentMethod), value.ToString(), settingsFile);
        }

        public static int TimeZone
        {
            get => AppSettings.GetValueOrDefault(nameof(TimeZone), 1, settingsFile);
            set => AppSettings.AddOrUpdateValue(nameof(TimeZone), value, settingsFile);
        }
        #endregion

        public static void ResetSettings()
        {
            AppSettings.Clear(settingsFile);
        }
    }
}
