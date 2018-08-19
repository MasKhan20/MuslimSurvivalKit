using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MuslimSurvivalKit.Data;
using MuslimSurvivalKit.Droid.Renderers;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace MuslimSurvivalKit.Droid.Renderers
{
    public class FileHelper : IFileHelper
    {
        public string GetDatabasePath(string databasename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, databasename);
        }

        public string GetDownloadDirectory()
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            
            var downloadDir = Path.Combine(path, "Downloads");
            
            if (!Directory.Exists(downloadDir))
                Directory.CreateDirectory(downloadDir);

            return downloadDir;
        }

        public double GetDuration(string filePath)
        {
            var reader = new MediaMetadataRetriever();

            reader.SetDataSource(filePath);

            var hasData = double.TryParse(reader.ExtractMetadata(MetadataKey.Duration), out double duration);

            return hasData ? duration / 1000 : 0.0;
        }

        public string TajweedPdfPath()
        {
            string path = Path.Combine(GetDownloadDirectory(), "Pdf");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return Path.Combine(path, "TajweedQuran.pdf");
        }
    }
}