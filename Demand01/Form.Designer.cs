namespace Demand01
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
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
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
            this.RandomCheckBox = new System.Windows.Forms.CheckBox();
            this.RowUpDown = new System.Windows.Forms.NumericUpDown();
            this.ColumnUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.WidthUpDown = new System.Windows.Forms.NumericUpDown();
            this.HeightUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.FormatComboBox = new System.Windows.Forms.ComboBox();
            this.FPSUpDown = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.TempCheckBox = new System.Windows.Forms.CheckBox();
            this.PathListBox = new System.Windows.Forms.ListBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.MethodComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.RowUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColumnUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WidthUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeightUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPSUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // RandomCheckBox
            // 
            this.RandomCheckBox.AutoSize = true;
            this.RandomCheckBox.Checked = true;
            this.RandomCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RandomCheckBox.Location = new System.Drawing.Point(12, 12);
            this.RandomCheckBox.Name = "RandomCheckBox";
            this.RandomCheckBox.Size = new System.Drawing.Size(60, 16);
            this.RandomCheckBox.TabIndex = 0;
            this.RandomCheckBox.Text = "ランダム";
            this.RandomCheckBox.UseVisualStyleBackColor = true;
            // 
            // RowUpDown
            // 
            this.RowUpDown.Location = new System.Drawing.Point(93, 36);
            this.RowUpDown.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.RowUpDown.Name = "RowUpDown";
            this.RowUpDown.Size = new System.Drawing.Size(35, 19);
            this.RowUpDown.TabIndex = 1;
            this.RowUpDown.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            // 
            // ColumnUpDown
            // 
            this.ColumnUpDown.Location = new System.Drawing.Point(93, 11);
            this.ColumnUpDown.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.ColumnUpDown.Name = "ColumnUpDown";
            this.ColumnUpDown.Size = new System.Drawing.Size(35, 19);
            this.ColumnUpDown.TabIndex = 2;
            this.ColumnUpDown.Value = new decimal(new int[] {
            24,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(134, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "行";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(134, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "列";
            // 
            // WidthUpDown
            // 
            this.WidthUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.WidthUpDown.Location = new System.Drawing.Point(12, 61);
            this.WidthUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.WidthUpDown.Name = "WidthUpDown";
            this.WidthUpDown.Size = new System.Drawing.Size(58, 19);
            this.WidthUpDown.TabIndex = 5;
            this.WidthUpDown.Value = new decimal(new int[] {
            1920,
            0,
            0,
            0});
            // 
            // HeightUpDown
            // 
            this.HeightUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.HeightUpDown.Location = new System.Drawing.Point(93, 61);
            this.HeightUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.HeightUpDown.Name = "HeightUpDown";
            this.HeightUpDown.Size = new System.Drawing.Size(58, 19);
            this.HeightUpDown.TabIndex = 6;
            this.HeightUpDown.Value = new decimal(new int[] {
            1080,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(76, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "x";
            // 
            // FormatComboBox
            // 
            this.FormatComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FormatComboBox.FormattingEnabled = true;
            this.FormatComboBox.Items.AddRange(new object[] {
            ".jpg",
            ".png"});
            this.FormatComboBox.Location = new System.Drawing.Point(12, 35);
            this.FormatComboBox.Name = "FormatComboBox";
            this.FormatComboBox.Size = new System.Drawing.Size(73, 20);
            this.FormatComboBox.TabIndex = 8;
            // 
            // FPSUpDown
            // 
            this.FPSUpDown.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.FPSUpDown.Location = new System.Drawing.Point(93, 87);
            this.FPSUpDown.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.FPSUpDown.Name = "FPSUpDown";
            this.FPSUpDown.Size = new System.Drawing.Size(35, 19);
            this.FPSUpDown.TabIndex = 9;
            this.FPSUpDown.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(134, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "fps";
            // 
            // TempCheckBox
            // 
            this.TempCheckBox.AutoSize = true;
            this.TempCheckBox.Location = new System.Drawing.Point(12, 88);
            this.TempCheckBox.Name = "TempCheckBox";
            this.TempCheckBox.Size = new System.Drawing.Size(73, 16);
            this.TempCheckBox.TabIndex = 11;
            this.TempCheckBox.Text = "temp削除";
            this.TempCheckBox.UseVisualStyleBackColor = true;
            // 
            // PathListBox
            // 
            this.PathListBox.AllowDrop = true;
            this.PathListBox.FormattingEnabled = true;
            this.PathListBox.ItemHeight = 12;
            this.PathListBox.Location = new System.Drawing.Point(161, 11);
            this.PathListBox.Name = "PathListBox";
            this.PathListBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.PathListBox.Size = new System.Drawing.Size(116, 88);
            this.PathListBox.TabIndex = 12;
            this.PathListBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.PathListBox_DragDrop);
            this.PathListBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.PathListBox_DragEnter);
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(12, 110);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(139, 23);
            this.StartButton.TabIndex = 13;
            this.StartButton.Text = "スタート";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // MethodComboBox
            // 
            this.MethodComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MethodComboBox.FormattingEnabled = true;
            this.MethodComboBox.Items.AddRange(new object[] {
            "ノーマル",
            "トリミング",
            "ストレッチ"});
            this.MethodComboBox.Location = new System.Drawing.Point(161, 112);
            this.MethodComboBox.Name = "MethodComboBox";
            this.MethodComboBox.Size = new System.Drawing.Size(116, 20);
            this.MethodComboBox.TabIndex = 14;
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 139);
            this.Controls.Add(this.MethodComboBox);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.PathListBox);
            this.Controls.Add(this.TempCheckBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.FPSUpDown);
            this.Controls.Add(this.FormatComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.HeightUpDown);
            this.Controls.Add(this.WidthUpDown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ColumnUpDown);
            this.Controls.Add(this.RowUpDown);
            this.Controls.Add(this.RandomCheckBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form";
            this.Text = "Demand01";
            this.Shown += new System.EventHandler(this.Form_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.RowUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColumnUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WidthUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeightUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPSUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox RandomCheckBox;
        private System.Windows.Forms.NumericUpDown RowUpDown;
        private System.Windows.Forms.NumericUpDown ColumnUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown WidthUpDown;
        private System.Windows.Forms.NumericUpDown HeightUpDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox FormatComboBox;
        private System.Windows.Forms.NumericUpDown FPSUpDown;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox TempCheckBox;
        private System.Windows.Forms.ListBox PathListBox;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.ComboBox MethodComboBox;
    }
}

