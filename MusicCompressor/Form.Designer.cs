namespace MusicCompressor
{
    partial class Form
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.FromTextBox = new System.Windows.Forms.TextBox();
            this.ToTextBox = new System.Windows.Forms.TextBox();
            this.FormButton = new System.Windows.Forms.Button();
            this.ToButton = new System.Windows.Forms.Button();
            this.FromComboBox = new System.Windows.Forms.ComboBox();
            this.ToComboBox = new System.Windows.Forms.ComboBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.FolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.AudioComboBox = new System.Windows.Forms.ComboBox();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // FromTextBox
            // 
            this.FromTextBox.AllowDrop = true;
            this.FromTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FromTextBox.Location = new System.Drawing.Point(12, 12);
            this.FromTextBox.Name = "FromTextBox";
            this.FromTextBox.Size = new System.Drawing.Size(422, 19);
            this.FromTextBox.TabIndex = 0;
            this.FromTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.FromTextBox_DragDrop);
            this.FromTextBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.FromTextBox_DragEnter);
            // 
            // ToTextBox
            // 
            this.ToTextBox.AllowDrop = true;
            this.ToTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ToTextBox.Location = new System.Drawing.Point(12, 92);
            this.ToTextBox.Name = "ToTextBox";
            this.ToTextBox.Size = new System.Drawing.Size(422, 19);
            this.ToTextBox.TabIndex = 1;
            this.ToTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.ToTextBox_DragDrop);
            this.ToTextBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.ToTextBox_DragEnter);
            // 
            // FormButton
            // 
            this.FormButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FormButton.Location = new System.Drawing.Point(440, 10);
            this.FormButton.Name = "FormButton";
            this.FormButton.Size = new System.Drawing.Size(75, 23);
            this.FormButton.TabIndex = 2;
            this.FormButton.Text = "参照";
            this.FormButton.UseVisualStyleBackColor = true;
            this.FormButton.Click += new System.EventHandler(this.FormButton_Click);
            // 
            // ToButton
            // 
            this.ToButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ToButton.Location = new System.Drawing.Point(440, 90);
            this.ToButton.Name = "ToButton";
            this.ToButton.Size = new System.Drawing.Size(75, 23);
            this.ToButton.TabIndex = 3;
            this.ToButton.Text = "参照";
            this.ToButton.UseVisualStyleBackColor = true;
            this.ToButton.Click += new System.EventHandler(this.ToButton_Click);
            // 
            // FromComboBox
            // 
            this.FromComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FromComboBox.FormattingEnabled = true;
            this.FromComboBox.Items.AddRange(new object[] {
            "指定なし",
            ".wav",
            ".aiff",
            ".aif",
            ".aifc",
            ".mp3",
            ".ogg",
            ".oga",
            ".ogv",
            ".oga",
            ".ogx",
            ".spx",
            ".opus",
            ".3gp",
            ".3g2",
            ".aac",
            ".avi",
            ".m2ts",
            ".m4a",
            ".m4b",
            ".m4p",
            ".mov",
            ".mp4",
            ".wav",
            ".mkv",
            ".mka",
            ".wma",
            ".asf",
            ".omg",
            ".oma",
            ".aa3",
            ".flac",
            ".fla",
            ".ape",
            ".mac",
            ".tta",
            ".wv",
            ".la",
            ".alac"});
            this.FromComboBox.Location = new System.Drawing.Point(521, 12);
            this.FromComboBox.Name = "FromComboBox";
            this.FromComboBox.Size = new System.Drawing.Size(121, 20);
            this.FromComboBox.TabIndex = 4;
            this.FromComboBox.Text = "入力形式";
            // 
            // ToComboBox
            // 
            this.ToComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ToComboBox.FormattingEnabled = true;
            this.ToComboBox.Items.AddRange(new object[] {
            ".mp3",
            ".wav",
            ".aiff",
            ".aif",
            ".aifc",
            ".ogg",
            ".oga",
            ".ogv",
            ".oga",
            ".ogx",
            ".spx",
            ".opus",
            ".3gp",
            ".3g2",
            ".aac",
            ".avi",
            ".m2ts",
            ".m4a",
            ".m4b",
            ".m4p",
            ".mov",
            ".mp4",
            ".wav",
            ".mkv",
            ".mka",
            ".wma",
            ".asf",
            ".omg",
            ".oma",
            ".aa3",
            ".flac",
            ".fla",
            ".ape",
            ".mac",
            ".tta",
            ".wv",
            ".la",
            ".alac"});
            this.ToComboBox.Location = new System.Drawing.Point(521, 92);
            this.ToComboBox.Name = "ToComboBox";
            this.ToComboBox.Size = new System.Drawing.Size(121, 20);
            this.ToComboBox.TabIndex = 5;
            this.ToComboBox.Text = "出力形式";
            // 
            // StartButton
            // 
            this.StartButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StartButton.Location = new System.Drawing.Point(12, 39);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(503, 47);
            this.StartButton.TabIndex = 6;
            this.StartButton.Text = "↓\r\n変換開始\r\n↓";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // AudioComboBox
            // 
            this.AudioComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AudioComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AudioComboBox.FormattingEnabled = true;
            this.AudioComboBox.Items.AddRange(new object[] {
            "libmp3lame",
            "libfdk_aac",
            "libopus",
            "libvorbis",
            "libfaac",
            "eac3",
            "ac3",
            "aac",
            "libtwolame",
            "vorbis",
            "mp2",
            "wmav2",
            "wmav1",
            "libvo_aacenc"});
            this.AudioComboBox.Location = new System.Drawing.Point(521, 53);
            this.AudioComboBox.Name = "AudioComboBox";
            this.AudioComboBox.Size = new System.Drawing.Size(121, 20);
            this.AudioComboBox.TabIndex = 8;
            // 
            // SaveFileDialog
            // 
            this.SaveFileDialog.DefaultExt = "txt";
            this.SaveFileDialog.Filter = "プレーンテキスト|*.txt";
            this.SaveFileDialog.Title = "変換情報を保存";
            // 
            // Form
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 123);
            this.Controls.Add(this.AudioComboBox);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.ToComboBox);
            this.Controls.Add(this.FromComboBox);
            this.Controls.Add(this.ToButton);
            this.Controls.Add(this.FormButton);
            this.Controls.Add(this.ToTextBox);
            this.Controls.Add(this.FromTextBox);
            this.MinimumSize = new System.Drawing.Size(370, 162);
            this.Name = "Form";
            this.Text = "MusicCompressor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_FormClosing);
            this.Shown += new System.EventHandler(this.Form_Shown);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form_DragEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox FromTextBox;
        private System.Windows.Forms.TextBox ToTextBox;
        private System.Windows.Forms.Button FormButton;
        private System.Windows.Forms.Button ToButton;
        private System.Windows.Forms.ComboBox FromComboBox;
        private System.Windows.Forms.ComboBox ToComboBox;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog;
        private System.Windows.Forms.ComboBox AudioComboBox;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog;
    }
}

