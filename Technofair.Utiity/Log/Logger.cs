using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Utiity.Log
{
    public static class Logger
    {

        //Log: Start- Call from outer world
        //            try
        //            {
        //                string webRootPath = hostingEnvironment.WebRootPath;
        //        string folder = @"Logs";
        //        string uploadDir = Path.Combine(webRootPath, folder);
        //                if (!Directory.Exists(uploadDir))
        //                {
        //                    Directory.CreateDirectory(uploadDir);
        //                }
        //    string fileName = "error_log.txt";
        //    string fullPath = Path.Combine(uploadDir, fileName);
        //    Technofair.Utiity.Log.Logger.LogError(fullPath, "Hello Bangladesh!");
        //            }
        //            catch
        //            {
        //}
        //Log: End

        public static void LogError(string filePath, string message)
        {            
            // Create the directory if it doesn't exist
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            // Write the error to the log file
            using (StreamWriter writer = new StreamWriter(filePath, append: true))
            {
                writer.WriteLine(new string('-', 80)); // Add a separator for readability
                writer.WriteLine($"Time: {DateTime.Now}");
                writer.WriteLine($"Message: {message}");
                //writer.WriteLine($"Stack Trace: {ex.StackTrace}");
                writer.WriteLine(new string('-', 80)); // Add a separator for readability
            }
        }
    }
}
