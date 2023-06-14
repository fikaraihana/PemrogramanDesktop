using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Reflection.Metadata;
using Document = iTextSharp.text.Document;

namespace ProDesk_CaffePoltekSSN
{
    public partial class PreviewStruk : Form
    {
        private string struk;
        private System.Drawing.Image image;

        public PreviewStruk(string struk, System.Drawing.Image image)
        {
            this.image = image;
            this.struk = struk;
            InitializeComponent();
        }

        private void PreviewStruk_Load(object sender, EventArgs e)
        {
            printPreviewControl1.Document = printDocument1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            printDialog1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.InitialDirectory = @"D:\";
            saveFileDialog1.Title = "Struk CaffePoltekSSN";
            saveFileDialog1.DefaultExt = "pdf";
            saveFileDialog1.Filter = "pdf files (*.pdf)|*.pdf|All files (*.*)|*.*";
            //reset dir
            //saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Document document = new Document();

                // Menyimpan ke file
                try
                {
                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(saveFileDialog1.FileName, FileMode.Create));
                    document.Open();

                    // Tambahkan konten ke file PDF
                    // Contoh: Menambahkan teks
                    document.Add(new Paragraph(struk));

                    document.Close();

                    MessageBox.Show("File PDF berhasil disimpan.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan saat menyimpan file PDF: " + ex.Message);
                }
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            System.Drawing.Image qrcode = image;
            int height = 200;
            int width = 200;
            Bitmap resizedImage = new Bitmap(qrcode, width, height);
            e.Graphics.DrawString(struk, new System.Drawing.Font("Arial", 20, FontStyle.Bold), Brushes.Black, new PointF(10, 10));
            if (resizedImage != null)
            {
                e.Graphics.DrawImage(resizedImage, new PointF(50, 500));
            }
        }
    }
}
