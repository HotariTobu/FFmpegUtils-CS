using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace NoAudio
{
    class Program
    {
        public static void Main(string[] args)
        {
            string file = Path.Combine(Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName, "ffmpeg.exe");

            if (!File.Exists(file))
            {
                Console.WriteLine("実行ファイルが入っているフォルダにffmpeg.exeを入れてください。");
                Console.ReadKey();
                return;
            }

            string folder = "Result";
            Directory.CreateDirectory(folder);
            folder = Path.GetFullPath(folder);

            foreach (string arg in args)
            {
                using var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = file,
                        Arguments = $"-i \"{arg}\" -vcodec copy -an \"{folder}\\{Path.GetFileName(arg)}\"",
                    }
                };
                process.Start();
                process.WaitForExit();
            }
        }
    }
}
