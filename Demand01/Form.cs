using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demand01
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();

            FormatComboBox.Text = ".jpg";
            MethodComboBox.Text = "ノーマル";
        }

        private void Form_Shown(object sender, EventArgs e)
        {
            if (!File.Exists("ffmpeg.exe"))
            {
                MessageBox.Show("実行ファイルが入っているフォルダにffmpeg.exeを入れてください。", "終了", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
        }

        private void PathListBox_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                foreach (string path in ((string[])e.Data.GetData(DataFormats.FileDrop)))
                {
                    if (Directory.Exists(path) && !PathListBox.Items.Contains(path))
                    {
                        PathListBox.Items.Add(path);
                    }
                }
            }
        }

        private void PathListBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            string extension = FormatComboBox.Text;
            if (string.IsNullOrWhiteSpace(extension))
            {
                MessageBox.Show("画像の形式を選択してください。");
                return;
            }

            bool random = RandomCheckBox.Checked;
            Random randomizer = new Random();

            Size tableSize = new Size((int)ColumnUpDown.Value, (int)RowUpDown.Value);
            Size imageSize = new Size((int)WidthUpDown.Value, (int)HeightUpDown.Value);
            Size partSize = new Size(imageSize.Width / tableSize.Width, imageSize.Height / tableSize.Height);
            SizeF scale = new SizeF(1.0f / tableSize.Width, 1.0f / tableSize.Height);

            ImageFormat format = null;
            if (extension.Equals(".jpg"))
            {
                format = ImageFormat.Jpeg;
            }
            else if (extension.Equals(".png"))
            {
                format = ImageFormat.Png;
            }
            else
            {
                format = ImageFormat.Png;
            }

            int method = 0;
            if (MethodComboBox.Text.Equals("ノーマル"))
            {
                method = 0;
            }
            else if (MethodComboBox.Text.Equals("トリミング"))
            {
                method = 1;
            }
            else if (MethodComboBox.Text.Equals("ストレッチ"))
            {
                method = 2;
            }

            foreach (string path in PathListBox.Items)
            {
                try
                {
                    string[] files = Directory.GetFiles(path, $"*{extension}");
                    int filesCount = files.Length;
                    bool[] checkBuf = new bool[filesCount];

                    int imagesNumber = tableSize.Width * tableSize.Height;
                    if (filesCount < imagesNumber)
                    {
                        imagesNumber = filesCount;
                    }

                    string tempPath = $"{path}_temp";
                    Directory.CreateDirectory(tempPath);

                    Bitmap lastFrame = new Bitmap(imageSize.Width, imageSize.Height);
                    Graphics gc = Graphics.FromImage(lastFrame);

                    for (int i = 0; i < imagesNumber; i++)
                    {
                        string file = null;

                        if (random)
                        {
                            int index = 0;
                            do
                            {
                                index = randomizer.Next(filesCount);
                            } while (checkBuf[index]);
                            checkBuf[index] = true;

                            file = files[index];
                        }
                        else
                        {
                            file = files[i];
                        }

                        if (file == null)
                        {
                            continue;
                        }

                        Image image = Image.FromFile(file);
                        Bitmap bitmap = new Bitmap(imageSize.Width, imageSize.Height);
                        Graphics graphics = Graphics.FromImage(bitmap);

                        switch (method)
                        {
                            case 0:
                                graphics.DrawImage(image, new Rectangle(new Point(), imageSize), new Rectangle(new Point((image.Width - imageSize.Width) / 2, (image.Height - imageSize.Height) / 2), imageSize), GraphicsUnit.Pixel);
                                break;

                            case 1:
                                Size recommendedSize = new Size(image.Height * 16 / 9, image.Width * 9 / 16);
                                graphics.DrawImage(image, new Rectangle(new Point(), imageSize), recommendedSize.Height < image.Height ? new Rectangle(new Point(0, (image.Height - recommendedSize.Height) / 2), new Size(image.Width, recommendedSize.Height)) : new Rectangle(new Point((image.Width - recommendedSize.Width) / 2, 0), new Size(recommendedSize.Width, image.Height)), GraphicsUnit.Pixel);
                                break;

                            case 2:
                                graphics.DrawImage(image, new Rectangle(new Point(), imageSize), new Rectangle(new Point(), image.Size), GraphicsUnit.Pixel);
                                break;
                        }

                        graphics.Dispose();
                        bitmap.Save(Path.Combine(tempPath, $"{i:0000}{extension}"), format);

                        gc.DrawImage(bitmap, new Rectangle(i == 0 ? new Point() : new Point(i % tableSize.Width * partSize.Width, i / tableSize.Width * partSize.Height), partSize), new Rectangle(new Point(), imageSize), GraphicsUnit.Pixel);

                        image.Dispose();
                        bitmap.Dispose();
                    }

                    gc.Dispose();
                    lastFrame.Save($"{path}{extension}", format);
                    lastFrame.Dispose();

                    string args = $"-r {FPSUpDown.Value} -i \"{Path.Combine(tempPath, "%04d")}{extension}\" -c:v rawvideo \"{path}.avi\"";

                    using (var process = new Process())
                    {
                        process.StartInfo = new ProcessStartInfo
                        {
                            FileName = @"ffmpeg.exe",
                            Arguments = args,
                        };
                        process.Start();
                        process.WaitForExit();
                    }

                    if (TempCheckBox.Checked)
                    {
                        for (int i = 0; i < imagesNumber; i++)
                        {
                            File.Delete(Path.Combine(tempPath, $"{i:0000}{extension}"));
                        }

                        Directory.Delete(tempPath);
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }

            PathListBox.Items.Clear();
        }
    }
}
