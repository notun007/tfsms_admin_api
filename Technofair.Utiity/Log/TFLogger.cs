using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Utiity.Log
{
    public class TFLogger: ITFLogger
    {
        private readonly string _wwwRootPath;

        // Constructor to accept the wwwroot path directly
        public TFLogger(string wwwRootPath)
        {
            _wwwRootPath = wwwRootPath;
        }

        // Method to retrieve the wwwroot folder path
        public string GetWwwRootPath()
        {
            return _wwwRootPath;
        }

        public void LogError(string message)
        {
            string folder = @"Logs";
            string uploadDir = Path.Combine(GetWwwRootPath(), folder);
            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }
            string fileName = "error_log.txt";
            string filePath = Path.Combine(uploadDir, fileName);

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
