using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SortingFiles
{
    internal class Program
    {
        private static void Main()
        {
            string curDir = Environment.CurrentDirectory;
            string curProcess = Process.GetCurrentProcess().MainModule.FileName;
            var files = Directory.GetFiles(curDir);
            List<string> filesWOLinks = new List<string>();
            foreach (var file in files)
            {
                if (file.Split('.').Last() != "url" &&
                    file.Split('.').Last() != "lnk")
                {
                    filesWOLinks.Add(file);
                }
            }
            List<string> extensions = new List<string>();
            foreach (var file in filesWOLinks)
            {
                if (file != curProcess &&
                    file.Split('.').Length > 1 &&
                    !extensions.Contains(file.Split('.').Last()))
                {
                    extensions.Add(file.Split('.').Last());
                }
            }
            foreach (var ext in extensions)
            {
                if (!Directory.Exists(curDir + "\\" + ext))
                {
                    Console.WriteLine($"Creating {ext} directory");
                    Directory.CreateDirectory(curDir + "\\_SortedFiles\\" + ext);
                }
            }
            foreach (var file in filesWOLinks)
            {
                if (file != curProcess &&
                    file.Split('.').Length > 1)
                {
                    try
                    {
                        Console.WriteLine($"Moving {file}");
                        File.Move(file, curDir + "\\_SortedFiles\\" + file.Split('.').Last() + "\\" + file.Split('\\').Last());
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("File exists");
                    }
                }
            }
        }
    }
}