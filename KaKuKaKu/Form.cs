using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace KaKuKaKu
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        private void BOX_DragDrop(object sender, DragEventArgs e)
        {
            if (BOX.SelectedIndex != -1 && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string path = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
                string args = $"-i \"{path}\" -vf \"";
                switch (BOX.SelectedIndex)
                {
                    case 0: args += "transpose = 0"; break;
                    case 1: args += "transpose = 1"; break;
                    case 2: args += "transpose = 2"; break;
                    case 3: args += "transpose = 3"; break;
                    case 4: args += "hflip,vflip"; break;
                    case 5: args += "hflip"; break;
                    case 6: args += "vflip"; break;
                }
                args += $"\" \"{path.Insert(path.LastIndexOf('.'), "_")}\"";

                using (var process = new Process())
                {
                    process.StartInfo = new ProcessStartInfo
                    {
                        FileName = @"ffmpeg.exe",
                        Arguments = args,
                    };
                    process.Start();
                }
            }
        }

        private void BOX_DragEnter(object sender, DragEventArgs e)
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

        private void Form_Shown(object sender, EventArgs e)
        {
            if (!File.Exists("ffmpeg.exe"))
            {
                MessageBox.Show("実行ファイルが入っているフォルダにffmpeg.exeを入れてください。", "終了", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
        }
    }
}
