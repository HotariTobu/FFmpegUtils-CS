using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MusicCompressor
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
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
                if (!Directory.Exists(ToTextBox.Text))
                {
                    Directory.CreateDirectory(ToTextBox.Text);
                }

                if (!Directory.Exists(FromTextBox.Text))
                {
                    MessageBox.Show("上のテキストボックスには変換する音声ファイルが入っているフォルダのパスを入力してください。", "変換不能", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (FromTextBox.Text.Equals(ToTextBox.Text))
                {
                    MessageBox.Show("上下のテキストボックスには別々のフォルダのパスを入力してください。", "変換不能", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (FromComboBox.Text.Length == 0 || FromComboBox.Text.Equals("入力形式"))
                {
                    MessageBox.Show("上のコンボボックスには変換する音声ファイルの拡張子を入力、または選択してください。", "変換不能", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (ToComboBox.Text.Length == 0 || ToComboBox.Text.Equals("出力形式"))
                {
                    MessageBox.Show("下のコンボボックスには変換された音声ファイルに付ける拡張子を入力、または選択してください。", "変換不能", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                    string FromPath = FromTextBox.Text.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
                    string FromFormat = IsSpecified ? FromComboBox.Text : string.Empty;
                    string ToPath = ToTextBox.Text.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
                    string ToFormat = ToComboBox.Text;
                    string AudioCodec = AudioComboBox.Text;

                    string separator = new string(Path.DirectorySeparatorChar, 1);
                    int index = FromPath.Length;

                    Task = new Task(() => {
                    foreach (string FromFile in Directory.GetFiles(FromPath, $"*{FromFormat}", SearchOption.AllDirectories))
                        {
                            string toDirectory = ToPath + Path.GetDirectoryName(FromFile).Substring(index);
                            string ToFile = Path.Combine(toDirectory, Path.GetFileNameWithoutExtension(FromFile) + ToFormat);

                            Directory.CreateDirectory(toDirectory);

                            using (var process = new Process())
                            {
                                process.StartInfo = new ProcessStartInfo
                                {
                                    FileName = @"ffmpeg.exe",
                                    //Arguments = $"-i \"{FromFile}\" -map_metadata 0 -map_metadata:s:v 0:s:v -map_metadata:s:a 0:s:a -f ffmetadata metadata.txt",
                                    Arguments = $"-i \"{FromFile}\" -map_metadata 0 -map_metadata:s:a 0:s:a -f ffmetadata metadata.txt",
                                };
                                process.Start();

                                while (TaskKeeper && !process.HasExited) ;

                                if (!process.HasExited)
                                {
                                    process.CloseMainWindow();
                                    process.Close();
                                }
                            }

                            byte[] vs = File.ReadAllBytes("metadata.txt");
                            Encoding encoding = DetectEncodingFromBOM(vs);
                            if (encoding == null)
                            {
                                encoding = DetectEncoding(vs);
                            }
                            if (encoding != null)
                            {
                                string metadata = encoding.GetString(vs);
                                File.WriteAllText("metadata.txt", metadata, Encoding.UTF8);
                            }

                            using (var process = new Process())
                            {
                                process.StartInfo = new ProcessStartInfo
                                {
                                    FileName = @"ffmpeg.exe",
                                    Arguments = $"-i \"{FromFile}\" -f ffmetadata -i metadata.txt -c:a {AudioCodec} -map_metadata 1 \"{ToFile}\"",
                                };
                                process.Start();

                                while (TaskKeeper && !process.HasExited) ;

                                if (!process.HasExited)
                                {
                                    process.CloseMainWindow();
                                    process.Close();
                                }
                            }

                            File.Delete("metadata.txt");

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

                string AudioCodec = file.ReadLine();
                if (AudioCodec == null)
                {
                    AudioCodec = "";
                }

                FromTextBox.Text = FromPath;
                ToTextBox.Text = ToPath;
                FromComboBox.Text = FromFormat;
                ToComboBox.Text = ToFormat;
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

        private Encoding DetectEncodingFromBOM(byte[] bytes)
        {
            if (bytes.Length < 2)
            {
                return null;
            }
            if ((bytes[0] == 0xfe) && (bytes[1] == 0xff))
            {
                //UTF-16 BE
                return new System.Text.UnicodeEncoding(true, true);
            }
            if ((bytes[0] == 0xff) && (bytes[1] == 0xfe))
            {
                if ((4 <= bytes.Length) &&
                    (bytes[2] == 0x00) && (bytes[3] == 0x00))
                {
                    //UTF-32 LE
                    return new System.Text.UTF32Encoding(false, true);
                }
                //UTF-16 LE
                return new System.Text.UnicodeEncoding(false, true);
            }
            if (bytes.Length < 3)
            {
                return null;
            }
            if ((bytes[0] == 0xef) && (bytes[1] == 0xbb) && (bytes[2] == 0xbf))
            {
                //UTF-8
                return new System.Text.UTF8Encoding(true, true);
            }
            if (bytes.Length < 4)
            {
                return null;
            }
            if ((bytes[0] == 0x00) && (bytes[1] == 0x00) &&
                (bytes[2] == 0xfe) && (bytes[3] == 0xff))
            {
                //UTF-32 BE
                return new System.Text.UTF32Encoding(true, true);
            }

            return null;
        }

        private Encoding DetectEncoding(byte[] bytes)
        {
            const byte bEscape = 0x1B;
            const byte bAt = 0x40;
            const byte bDollar = 0x24;
            const byte bAnd = 0x26;
            const byte bOpen = 0x28;    //'('
            const byte bB = 0x42;
            const byte bD = 0x44;
            const byte bJ = 0x4A;
            const byte bI = 0x49;

            int len = bytes.Length;
            byte b1, b2, b3, b4;

            //Encode::is_utf8 は無視

            bool isBinary = false;
            for (int i = 0; i < len; i++)
            {
                b1 = bytes[i];
                if (b1 <= 0x06 || b1 == 0x7F || b1 == 0xFF)
                {
                    //'binary'
                    isBinary = true;
                    if (b1 == 0x00 && i < len - 1 && bytes[i + 1] <= 0x7F)
                    {
                        //smells like raw unicode
                        return System.Text.Encoding.Unicode;
                    }
                }
            }
            if (isBinary)
            {
                return null;
            }

            //not Japanese
            bool notJapanese = true;
            for (int i = 0; i < len; i++)
            {
                b1 = bytes[i];
                if (b1 == bEscape || 0x80 <= b1)
                {
                    notJapanese = false;
                    break;
                }
            }
            if (notJapanese)
            {
                return System.Text.Encoding.ASCII;
            }

            for (int i = 0; i < len - 2; i++)
            {
                b1 = bytes[i];
                b2 = bytes[i + 1];
                b3 = bytes[i + 2];

                if (b1 == bEscape)
                {
                    if (b2 == bDollar && b3 == bAt)
                    {
                        //JIS_0208 1978
                        //JIS
                        return System.Text.Encoding.GetEncoding(50220);
                    }
                    else if (b2 == bDollar && b3 == bB)
                    {
                        //JIS_0208 1983
                        //JIS
                        return System.Text.Encoding.GetEncoding(50220);
                    }
                    else if (b2 == bOpen && (b3 == bB || b3 == bJ))
                    {
                        //JIS_ASC
                        //JIS
                        return System.Text.Encoding.GetEncoding(50220);
                    }
                    else if (b2 == bOpen && b3 == bI)
                    {
                        //JIS_KANA
                        //JIS
                        return System.Text.Encoding.GetEncoding(50220);
                    }
                    if (i < len - 3)
                    {
                        b4 = bytes[i + 3];
                        if (b2 == bDollar && b3 == bOpen && b4 == bD)
                        {
                            //JIS_0212
                            //JIS
                            return System.Text.Encoding.GetEncoding(50220);
                        }
                        if (i < len - 5 &&
                            b2 == bAnd && b3 == bAt && b4 == bEscape &&
                            bytes[i + 4] == bDollar && bytes[i + 5] == bB)
                        {
                            //JIS_0208 1990
                            //JIS
                            return System.Text.Encoding.GetEncoding(50220);
                        }
                    }
                }
            }

            //should be euc|sjis|utf8
            //use of (?:) by Hiroki Ohzaki <ohzaki@iod.ricoh.co.jp>
            int sjis = 0;
            int euc = 0;
            int utf8 = 0;
            for (int i = 0; i < len - 1; i++)
            {
                b1 = bytes[i];
                b2 = bytes[i + 1];
                if (((0x81 <= b1 && b1 <= 0x9F) || (0xE0 <= b1 && b1 <= 0xFC)) &&
                    ((0x40 <= b2 && b2 <= 0x7E) || (0x80 <= b2 && b2 <= 0xFC)))
                {
                    //SJIS_C
                    sjis += 2;
                    i++;
                }
            }
            for (int i = 0; i < len - 1; i++)
            {
                b1 = bytes[i];
                b2 = bytes[i + 1];
                if (((0xA1 <= b1 && b1 <= 0xFE) && (0xA1 <= b2 && b2 <= 0xFE)) ||
                    (b1 == 0x8E && (0xA1 <= b2 && b2 <= 0xDF)))
                {
                    //EUC_C
                    //EUC_KANA
                    euc += 2;
                    i++;
                }
                else if (i < len - 2)
                {
                    b3 = bytes[i + 2];
                    if (b1 == 0x8F && (0xA1 <= b2 && b2 <= 0xFE) &&
                        (0xA1 <= b3 && b3 <= 0xFE))
                    {
                        //EUC_0212
                        euc += 3;
                        i += 2;
                    }
                }
            }
            for (int i = 0; i < len - 1; i++)
            {
                b1 = bytes[i];
                b2 = bytes[i + 1];
                if ((0xC0 <= b1 && b1 <= 0xDF) && (0x80 <= b2 && b2 <= 0xBF))
                {
                    //UTF8
                    utf8 += 2;
                    i++;
                }
                else if (i < len - 2)
                {
                    b3 = bytes[i + 2];
                    if ((0xE0 <= b1 && b1 <= 0xEF) && (0x80 <= b2 && b2 <= 0xBF) &&
                        (0x80 <= b3 && b3 <= 0xBF))
                    {
                        //UTF8
                        utf8 += 3;
                        i += 2;
                    }
                }
            }
            //M. Takahashi's suggestion
            //utf8 += utf8 / 2;

            System.Diagnostics.Debug.WriteLine(
                string.Format("sjis = {0}, euc = {1}, utf8 = {2}", sjis, euc, utf8));
            if (euc > sjis && euc > utf8)
            {
                //EUC
                return System.Text.Encoding.GetEncoding(51932);
            }
            else if (sjis > euc && sjis > utf8)
            {
                //SJIS
                return System.Text.Encoding.GetEncoding(932);
            }
            else if (utf8 > euc && utf8 > sjis)
            {
                //UTF8
                return System.Text.Encoding.UTF8;
            }

            return null;
        }
    }
}
