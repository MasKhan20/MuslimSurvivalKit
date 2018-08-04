using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuslimSurvivalKit.Data
{
    public class Settings
    {
        public const string settingsFile = "MuslimSurvivalKitSettings";

        public static ISettings AppSettings
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
    }
}
