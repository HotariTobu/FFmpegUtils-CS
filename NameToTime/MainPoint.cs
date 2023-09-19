using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NameToTime
{
    class MainPoint
    {
        [STAThread]
        static void Main()
        {
            if (!File.Exists("exiftool.exe"))
            {
                Console.WriteLine("実行ファイルが入っているフォルダにexiftool.exeを入れてください。");
                Console.ReadLine();
                return;
            }

            void scan(string path)
            {
                foreach (string file in Directory.GetFiles(path))
                {
                    try
                    {
                        string[] lines = null;

                        using (var process = new Process())
                        {
                            process.StartInfo = new ProcessStartInfo
                            {
                                FileName = @"exiftool.exe",
                                Arguments = $"\"{file}\"",
                                CreateNoWindow = true,
                                UseShellExecute = false,
                                RedirectStandardOutput = true,
                            };

                            process.Start();
                            lines = process.StandardOutput.ReadToEnd().Split('\n');
                            process.WaitForExit();
                        }

                        if (lines == null)
                        {
                            continue;
                        }

                        DateTime createDate = new DateTime();
                        DateTime modifyDate = new DateTime();
                        TimeSpan duration = new TimeSpan();
                        int offsetTime = 0;

                        foreach (string line in lines)
                        {
                            int index = line.IndexOf(':');
                            if (index == -1)
                            {
                                continue;
                            }

                            string key = line.Substring(0, index).Trim();
                            string data = line.Substring(index + 1).Trim();

                            switch (key)
                            {
                                case "Create Date":
                                    {
                                        Regex regex = new Regex(@"\d{4}|\d{2}");
                                        Match match = regex.Match(data);
                                        int year = int.Parse(match.Value); match = match.NextMatch();
                                        int month = int.Parse(match.Value); match = match.NextMatch();
                                        int day = int.Parse(match.Value); match = match.NextMatch();
                                        int hour = int.Parse(match.Value); match = match.NextMatch();
                                        int minute = int.Parse(match.Value); match = match.NextMatch();
                                        int second = int.Parse(match.Value);
                                        createDate = new DateTime(year, month, day, hour, minute, second).ToLocalTime();
                                    }
                                    break;
                                case "Modify Date":
                                    {
                                        Regex regex = new Regex(@"\d{4}|\d{2}");
                                        Match match = regex.Match(data);
                                        int year = int.Parse(match.Value); match = match.NextMatch();
                                        int month = int.Parse(match.Value); match = match.NextMatch();
                                        int day = int.Parse(match.Value); match = match.NextMatch();
                                        int hour = int.Parse(match.Value); match = match.NextMatch();
                                        int minute = int.Parse(match.Value); match = match.NextMatch();
                                        int second = int.Parse(match.Value);
                                        modifyDate = new DateTime(year, month, day, hour, minute, second).ToLocalTime();
                                    }
                                    break;
                                case "Duration":
                                    {
                                        Regex regex = new Regex(@"\d{2}|\d");
                                        Match match = regex.Match(data);
                                        if (data.EndsWith("s"))
                                        {
                                            int second = int.Parse(match.Value); match = match.NextMatch();
                                            int millisecond = int.Parse(match.Value) * 10;
                                            duration = new TimeSpan(0, 0, 0, second, millisecond);
                                        }
                                        else
                                        {
                                            int hour = int.Parse(match.Value); match = match.NextMatch();
                                            int minute = int.Parse(match.Value); match = match.NextMatch();
                                            int second = int.Parse(match.Value);
                                            duration = new TimeSpan(hour, minute, second);
                                        }
                                    }
                                    break;
                                case "Offset Time":
                                    {
                                        Regex regex = new Regex(@"(\+|-)\d{2}");
                                        Match match = regex.Match(data);
                                        int hour = int.Parse(match.Value);
                                        offsetTime = -hour;
                                    }
                                    break;
                            }
                        }

                        if (offsetTime != 0)
                        {
                            createDate = createDate.AddHours(offsetTime);
                            modifyDate = modifyDate.AddHours(offsetTime);
                        }

                        if (createDate.CompareTo(modifyDate) == 0)
                        {
                            createDate = modifyDate.Subtract(duration);
                        }

                        if (createDate.Ticks != 0)
                        {
                            string name = null;
                            if (duration.TotalMinutes < 1)
                            {
                                name = Path.Combine(Path.GetDirectoryName(file), $"{createDate.ToShortTimeString()}".Replace(':', '_') + Path.GetExtension(file));
                            }
                            else
                            {
                                name = Path.Combine(Path.GetDirectoryName(file), $"{createDate.ToShortTimeString()} - {modifyDate.ToShortTimeString()}".Replace(':', '_') + Path.GetExtension(file));
                            }
                            while (File.Exists(name))
                            {
                                name = Path.Combine(Path.GetDirectoryName(file), Path.GetFileNameWithoutExtension(name) + "_" + Path.GetExtension(name));
                            }
                            File.Move(file, name);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"@ - {file}");
                        Console.WriteLine(e.Message);
                        Console.WriteLine();
                    }
                }

                foreach (string directory in Directory.GetDirectories(path))
                {
                    scan(directory);
                }
            }

            string rootPath = Clipboard.GetText();

            if (Directory.Exists(rootPath))
            {
                scan(rootPath);
            }

            Console.WriteLine("Press Enter key to quit...");
            Console.ReadLine();
        }
    }
}
