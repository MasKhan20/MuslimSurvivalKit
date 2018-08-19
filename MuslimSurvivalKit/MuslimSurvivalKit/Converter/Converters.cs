using System;
using System.Collections.Generic;
using System.Text;

namespace MuslimSurvivalKit.Converter
{
    public static class Converters
    {
        public static string ConvertSize(float byteSize)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB", "PB" };
            //{ "Bytes", "Kilobytes", "Megabytes", "Gigabytes", "Terabytes", "Petabytes" };
            int value = 0;
            while (byteSize >= 1024 && value < sizes.Length - 1)
            {
                value++;
                byteSize /= 1024;
            }

            return String.Format("{0:0.##} {1}", byteSize, sizes[value]);
        }
    }
}
