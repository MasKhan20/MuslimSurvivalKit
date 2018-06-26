using System;
using System.Collections.Generic;
using System.Text;

namespace MuslimSurvivalKit.Data
{
    public interface IFileHelper
    {
        string GetDatabasePath(string databaseName);
        string GetDownloadDirectory();
        double GetDuration(string filePath);
    }
}
