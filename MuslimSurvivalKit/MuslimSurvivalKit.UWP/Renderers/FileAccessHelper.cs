using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace MuslimSurvivalKit.UWP.Renderers
{
    public class FileAccessHelper
    {
        public static string GetLocalFilePath(string filename)
        {
            //string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            var path = ApplicationData.Current.LocalFolder;
            string dbPath = Path.Combine(path.Path, filename);

            CopyDatabaseIfNotExists(dbPath);

            return dbPath;
        }

        private static void CopyDatabaseIfNotExists(string dbPath)
        {
            if (!File.Exists(dbPath))
            {
                //var file = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                Windows.Storage.StorageFolder installedLocation = Windows.ApplicationModel.Package.Current.InstalledLocation;
                var appDB = Path.Combine(installedLocation.Path, Path.GetFileName(dbPath));
                File.Copy(appDB, dbPath);
            }
        }
    }
}
