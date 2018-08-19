using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Foundation;
using MuslimSurvivalKit.Data;
using MuslimSurvivalKit.iOS.Renderers;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace MuslimSurvivalKit.iOS.Renderers
{
    public class FileHelper : IFileHelper
    {
        public string GetDatabasePath(string databasename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, databasename);
        }

        public string GetDownloadDirectory()
        {
            return string.Empty;
        }

        public double GetDuration(string filePath)
        {
            return 0;
        }

        public string TajweedPdfPath()
        {
            
        }
    }
}
   