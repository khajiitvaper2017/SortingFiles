using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SortingFiles
{
    internal class Program
    {
        private static readonly string CurDir = Environment.CurrentDirectory;
        private static readonly string CurProcess = Process.GetCurrentProcess().MainModule?.FileName;
        private const string SortDir = "\\_SortedFiles\\";


        private static void Main()
        {
            var filesWoLinks = (from string file in Directory.GetFiles(CurDir)
                where file.Split('.').Last() != "url" && file.Split('.').Last() != "lnk"
                select file).ToList();

            var extensions = new List<string>();

            foreach (var file in filesWoLinks.Where(file =>
                file != CurProcess && file.Split('.').Length > 1 && !extensions.Contains(file.Split('.').Last())))
                extensions.Add(file.Split('.').Last());
            foreach (var ext in extensions.Where(ext => !Directory.Exists(CurDir + SortDir + ext)))
            {
                Console.WriteLine($"Creating {ext} directory");
                Directory.CreateDirectory(CurDir + SortDir + ext);
            }

            foreach (var file in filesWoLinks.Where(file => file != CurProcess && file.Split('.').Length > 1))
                try
                {
                    Console.WriteLine($"Moving {file}");
                    File.Move(file,
                        $"{CurDir}" + SortDir + $"{file.Split('.').Last()}\\{file.Split('\\').Last()}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
        }
    }
}