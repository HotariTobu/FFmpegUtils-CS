using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoCompressor
{
    public partial class ProgressForm : Form
    {
        public ProgressForm()
        {
            InitializeComponent();
        }

        public Process Process { set; private get; }

        private void Timer_Tick(object sender, EventArgs e)
        {
            string output = Process.StandardOutput.ReadLine();
            output = output.Replace("\r\r\n", "\n");
            textBox1.Text = output;

            if (Process.HasExited)
            {
                Close();
            }
        }
    }
}
