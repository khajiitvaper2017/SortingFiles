using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace SortingFiles
{
    internal class Program
    {
        private static readonly string curDir = Environment.CurrentDirectory;
        private static readonly string curProcess = Process.GetCurrentProcess().MainModule.FileName;

        private static void Main()
        {
            List<string> filesWOLinks = (from string file in Directory.GetFiles(curDir)
                                         where file.Split('.').Last() != "url" && file.Split('.').Last() != "lnk"
                                         select file).ToList();
            List<string> extensions = new List<string>();
            foreach (string file in filesWOLinks)
            {
                if (file != curProcess &&
                    file.Split('.').Length > 1 &&
                    !extensions.Contains(file.Split('.').Last()))
                {
                    extensions.Add(file.Split('.').Last());
                }
            }
            foreach (string ext in extensions)
            {
                if (!Directory.Exists(curDir + "\\" + ext))
                {
                    Console.WriteLine($"Creating {ext} directory");
                    Directory.CreateDirectory(curDir + "\\_SortedFiles\\" + ext);
                }
            }
            foreach (string file in filesWOLinks)
            {
                if (file != curProcess &&
                    file.Split('.').Length > 1)
                {
                    try
                    {
                        Console.WriteLine($"Moving {file}");
                        File.Move(file, destFileName: $"{curDir}\\_SortedFiles\\{file.Split('.').Last()}\\{file.Split('\\').Last()}");
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