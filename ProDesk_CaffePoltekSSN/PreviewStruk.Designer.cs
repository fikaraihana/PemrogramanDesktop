namespace ProDesk_CaffePoltekSSN
{
    partial class PreviewStruk
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreviewStruk));
            button2 = new Button();
            button1 = new Button();
            printPreviewControl1 = new PrintPreviewControl();
            printDocument1 = new System.Drawing.Printing.PrintDocument();
            printDialog1 = new PrintDialog();
            saveFileDialog1 = new SaveFileDialog();
            openFileDialog1 = new OpenFileDialog();
            printPreviewDialog1 = new PrintPreviewDialog();
            SuspendLayout();
            // 
            // button2
            // 
            button2.Location = new Point(76, 327);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 5;
            button2.Text = "Simpan";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(76, 298);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 4;
            button1.Text = "Cetak";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // printPreviewControl1
            // 
            printPreviewControl1.Location = new Point(12, 12);
            printPreviewControl1.Name = "printPreviewControl1";
            printPreviewControl1.Size = new Size(213, 276);
            printPreviewControl1.TabIndex = 3;
            // 
            // printDocument1
            // 
            printDocument1.PrintPage += printDocument1_PrintPage;
            // 
            // printDialog1
            // 
            printDialog1.UseEXDialog = true;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // printPreviewDialog1
            // 
            printPreviewDialog1.AutoScrollMargin = new Size(0, 0);
            printPreviewDialog1.AutoScrollMinSize = new Size(0, 0);
            printPreviewDialog1.ClientSize = new Size(400, 300);
            printPreviewDialog1.Enabled = true;
            printPreviewDialog1.Icon = (Icon)resources.GetObject("printPreviewDialog1.Icon");
            printPreviewDialog1.Name = "printPreviewDialog1";
            printPreviewDialog1.Visible = false;
            // 
            // PreviewStruk
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(241, 356);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(printPreviewControl1);
            Name = "PreviewStruk";
            Text = "PreviewStruk";
            Load += PreviewStruk_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button button2;
        private Button button1;
        private PrintPreviewControl printPreviewControl1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private PrintDialog printDialog1;
        private SaveFileDialog saveFileDialog1;
        private OpenFileDialog openFileDialog1;
        private PrintPreviewDialog printPreviewDialog1;
    }
}