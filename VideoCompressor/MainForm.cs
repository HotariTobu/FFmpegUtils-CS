using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VideoCompressor
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form_Shown(object sender, EventArgs e)
        {
            if (!File.Exists("ffmpeg.exe"))
            {
                MessageBox.Show("実行ファイルが入っているフォルダにffmpeg.exeを入れてください。","終了",MessageBoxButtons.OK,MessageBoxIcon.Information);

                this.Close();
            }
        }

        private void FormButton_Click(object sender, EventArgs e)
        {
            if (FolderBrowserDialog.ShowDialog() != DialogResult.Cancel)
            {
                FromTextBox.Text = FolderBrowserDialog.SelectedPath;
            }

        }

        private void FromTextBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                if (Directory.Exists(((string[])e.Data.GetData(DataFormats.FileDrop))[0]))
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void FromTextBox_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                FromTextBox.Text = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            }
        }

        private void ToButton_Click(object sender, EventArgs e)
        {
            if (FolderBrowserDialog.ShowDialog() != DialogResult.Cancel)
            {
                ToTextBox.Text = FolderBrowserDialog.SelectedPath;
            }
        }

        private void ToTextBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                if (Directory.Exists(((string[])e.Data.GetData(DataFormats.FileDrop))[0]))
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void ToTextBox_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                ToTextBox.Text = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            }
        }

        private Task Task;
        private bool TaskKeeper;
        
        private delegate void f();

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (Task == null || Task.Status != TaskStatus.Running)
            {
                if (!Directory.Exists(ToTextBox.Text) && !string.IsNullOrWhiteSpace(ToTextBox.Text))
                {
                    Directory.CreateDirectory(ToTextBox.Text);
                }

                if (!Directory.Exists(FromTextBox.Text))
                {
                    MessageBox.Show("上のテキストボックスには変換する動画ファイルが入っているフォルダのパスを入力してください。", "変換不能", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (FromTextBox.Text.Equals(ToTextBox.Text) && FromComboBox.Text.Equals(ToComboBox.Text))
                {
                    MessageBox.Show("上下のテキストボックスには別々のフォルダのパスを入力してください。", "変換不能", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (FromComboBox.Text.Length == 0 || FromComboBox.Text.Equals("入力形式"))
                {
                    MessageBox.Show("上のコンボボックスには変換する動画ファイルの拡張子を入力、または選択してください。", "変換不能", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (ToComboBox.Text.Length == 0 || ToComboBox.Text.Equals("出力形式"))
                {
                    MessageBox.Show("下のコンボボックスには変換された動画ファイルに付ける拡張子を入力、または選択してください。", "変換不能", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (VideoComboBox.Text.Length == 0 || AudioComboBox.Text.Length == 0)
                {
                    MessageBox.Show("真ん中のコンボボックスには変換に使うエンコーダを選択してください。", "変換不能", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (Task != null)
                    {
                        Task.Dispose();
                    }

                    TaskKeeper = true;

                    bool IsSpecified = !FromComboBox.Text.Equals("指定なし");
                    Regex regex = new Regex(FromComboBox.Text + @"$", RegexOptions.IgnoreCase);
                    bool IsRawVideo = Regex.IsMatch(ToComboBox.Text, @"\.avi", RegexOptions.IgnoreCase);

                    string FromPath = FromTextBox.Text;
                    string ToPath = ToTextBox.Text;
                    string ToFormat = ToComboBox.Text;
                    string VideoCodec = VideoComboBox.Text;
                    string AudioCodec = AudioComboBox.Text;

                    Task = new Task(() => {
                        foreach (string FromFile in Directory.GetFiles(FromPath))
                        {
                            string ToFile = ToPath + "\\" + Path.GetFileNameWithoutExtension(FromFile) + ToFormat;

                            if (IsSpecified && !regex.IsMatch(FromFile))
                            {
                                continue;
                            }

                            string arguments = $"-i \"{FromFile}\" -c:v ";
                            if (IsRawVideo)
                            {
                                arguments += "rawvideo";
                            }
                            else
                            {
                                arguments += VideoCodec;
                            }
                            arguments += $" -c:a {AudioCodec} -copyts -vsync 0 -map_metadata 0 \"{ToFile}\"";

                            using (var process = new Process
                            {
                                EnableRaisingEvents = true,
                            })
                            {
                                process.StartInfo = new ProcessStartInfo
                                {
                                    FileName = @"ffmpeg.exe",
                                    Arguments = arguments,
                                };

                                /*process.StartInfo = new ProcessStartInfo
                                {
                                    //FileName = @"ffmpeg.exe",
                                    //Arguments = arguments,
                                    FileName = @"ConsoleTest.exe",
                                    //CreateNoWindow = true,
                                    UseShellExecute = false,
                                    RedirectStandardOutput = true,
                                };*/

                                /*process.OutputDataReceived += (sen, ev) =>
                                {
                                    Console.WriteLine(ev.Data);
                                    Debugger.Break();
                                };

                                process.Exited += (sen, ev) =>
                                {
                                    Debugger.Break();
                                };*/

                                /*using (ProgressForm progressForm = new ProgressForm())
                                {
                                    progressForm.Process = process;
                                    progressForm.ShowDialog();
                                }*/

                                process.Start();
                                //process.BeginOutputReadLine();

                                process.WaitForExit();

                                /*while (TaskKeeper && !process.HasExited) ;

                                if (!process.HasExited)
                                {
                                    process.CloseMainWindow();
                                    process.Close();
                                }*/
                            }

                            if (!TaskKeeper)
                            {
                                break;
                            }
                        }

                        this.Invoke(new f(() =>
                        {
                            if (SaveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                StreamWriter file = new StreamWriter(SaveFileDialog.OpenFile(), Encoding.UTF8);

                                file.WriteLine(FromTextBox.Text);
                                file.WriteLine(ToTextBox.Text);
                                file.WriteLine(FromComboBox.Text);
                                file.WriteLine(ToComboBox.Text);
                                file.WriteLine(VideoComboBox.Text);
                                file.WriteLine(AudioComboBox.Text);

                                file.Close();
                            }

                            StartButton.Text = "↓\r\n変換開始\r\n↓";
                        }));
                    });

                    Task.Start();

                    StartButton.Text = "変換停止";
                }
            }
            else
            {
                TaskKeeper = false;
            }
        }

        private void Form_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                StreamReader file = new StreamReader(((string[])e.Data.GetData(DataFormats.FileDrop))[0], Encoding.UTF8);

                string FromPath = file.ReadLine();
                if (FromPath == null)
                {
                    FromPath = "";
                }

                string ToPath = file.ReadLine();
                if (ToPath == null)
                {
                    ToPath = "";
                }

                string FromFormat = file.ReadLine();
                if (FromFormat == null || FromFormat.Length == 0)
                {
                    FromFormat = "";
                }

                string ToFormat = file.ReadLine();
                if (ToFormat == null || ToFormat.Length == 0)
                {
                    ToFormat = "入力形式";
                }

                string VideoCodec = file.ReadLine();
                if (VideoCodec == null || VideoCodec.Length == 0)
                {
                    VideoCodec = "出力形式";
                }

                string AudioCodec = file.ReadLine();
                if (AudioCodec == null || AudioCodec.Length == 0)
                {
                    AudioCodec = "";
                }

                FromTextBox.Text = FromPath;
                ToTextBox.Text = ToPath;
                FromComboBox.Text = FromFormat;
                ToComboBox.Text = ToFormat;
                VideoComboBox.Text = VideoCodec;
                AudioComboBox.Text = AudioCodec;

                file.Close();
            }
        }

        private void Form_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                if (File.Exists(((string[])e.Data.GetData(DataFormats.FileDrop))[0]))
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Task == null)
            {
                return;
            }

            if (Task.Status == TaskStatus.Running)
            {
                if (MessageBox.Show("変換が完了していませんが終了しますか？", "終了確認", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    TaskKeeper = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
