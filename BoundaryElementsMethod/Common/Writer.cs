using System.IO;

namespace BEM.Common
{
    public class Writer
    {
        private static bool outputAllowed = true;

        public static void OutputIfAllowed(object o, string fileName)
        {
            if (!outputAllowed)
            {
                return;
            }
            Output(o, fileName);
        }

        public static void Output(object o, string fileName)
        {
            var directory = Path.GetDirectoryName(fileName);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            StreamWriter sw = new StreamWriter(fileName);
            sw.WriteLine(o);
            sw.Close();
        }
    }

    public class CopyOfWriter
    {
        private static bool outputAllowed = true;

        public static void OutputIfAllowed(object o, string fileName)
        {
            if (!outputAllowed)
            {
                return;
            }
            Output(o, fileName);
        }

        public static void Output(object o, string fileName)
        {
            var directory = Path.GetDirectoryName(fileName);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            StreamWriter sw = new StreamWriter(fileName);
            sw.WriteLine(o);
            sw.Close();
        }
    }
}