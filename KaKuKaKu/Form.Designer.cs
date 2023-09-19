namespace KaKuKaKu
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
            this.BOX = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // BOX
            // 
            this.BOX.AllowDrop = true;
            this.BOX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BOX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BOX.FormattingEnabled = true;
            this.BOX.Items.AddRange(new object[] {
            "90度反時計回り上下反転",
            "90度時計回り",
            "90度反時計回り",
            "90度時計回り上下反転",
            "180度回転",
            "左右反転",
            "上下反転"});
            this.BOX.Location = new System.Drawing.Point(0, 0);
            this.BOX.Name = "BOX";
            this.BOX.Size = new System.Drawing.Size(137, 20);
            this.BOX.TabIndex = 0;
            this.BOX.DragDrop += new System.Windows.Forms.DragEventHandler(this.BOX_DragDrop);
            this.BOX.DragEnter += new System.Windows.Forms.DragEventHandler(this.BOX_DragEnter);
            // 
            // Form
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(137, 20);
            this.Controls.Add(this.BOX);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form";
            this.Text = "かくかく";
            this.Shown += new System.EventHandler(this.Form_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox BOX;
    }
}

