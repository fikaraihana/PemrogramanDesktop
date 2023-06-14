using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Drawing.Imaging;
using System.Formats.Asn1;
using System.Globalization;

namespace ProDesk_CaffePoltekSSN
{
    public partial class CheckOut : Form
    {
        private string email;
        private string level;
        private List<Order> records;
        private Guid id;
        private string username;
        private string item;
        private decimal itemPrice;
        private string size;
        private decimal sizePrice;
        private string sugarlvl;
        private string icelvl;
        private decimal addonPrice;
        private string addonOrder;
        private decimal bill;
        private string struk;
        private DateTime waktu;

        private string bayar;
        private decimal bayarValue;
        private decimal kembali;
        private string qrcodePath;

        public CheckOut(Guid id,
            string username,
            string email,
            string level,
            string item,
            decimal itemPrice,
            string size,
            decimal sizePrice,
            string sugarlvl,
            string icelvl,
            string addonOrder,
            decimal addonPrice,
            decimal bill,
            string struk,
            DateTime waktu
            )
        {
            InitializeComponent();
            this.id = id;
            this.username = username;
            this.email = email;
            this.level = level;
            this.item = item;
            this.itemPrice = itemPrice;
            this.size = size;
            this.sizePrice = sizePrice;
            this.sugarlvl = sugarlvl;
            this.icelvl = icelvl;
            this.addonPrice = addonPrice;
            this.waktu = waktu;
            this.addonOrder = addonOrder;
            this.struk = struk;
            this.bill = bill;
        }

        private void CheckOut_Load(object sender, EventArgs e)
        {
            label1.Text = item;
            label6.Text = "Rp" + itemPrice.ToString();
            label2.Text = size;
            label7.Text = "Rp" + sizePrice.ToString();
            label8.Text = sugarlvl;
            label9.Text = icelvl;
            label10.Text = addonOrder;
            label14.Text = "Rp" + bill.ToString();
            button1.Visible = false;
            button2.Visible = true;
            label13.Visible = false;
            label15.Visible = false;
            label16.Visible = false;
            MessageBox.Show(records[0].Icelvl);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            bayar = textBox1.Text;
            bayarValue = int.Parse(bayar);
            if (bayarValue > bill)
            {
                kembali = bayarValue - bill;
                label13.Visible =
                label15.Visible =
                label16.Visible = true;
                label15.Text = "Rp" + bayar;
                label16.Text = "Rp" + kembali.ToString();
                button1.Visible = true;
                button2.Visible = false;
                label13.Visible = true;
                label15.Visible = true;
                textBox1.Visible = false;

                struk += $"\nBayar: \t\tRp{bayarValue}" +
                    $"\nKembali: \tRp{kembali}";

                qrcodePath = @"..\net6.0-windows\qrcode\" + id+"_CaffePoltekSSN.png";

                GenerateQrcode();

                /*var emailCaffe = "atlantastuff100@gmail.com";

                // Mengirim email dengan QR code sebagai lampiran menggunakan SendGrid
                var apiKey = "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"; // Ganti dengan SendGrid API key Anda
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress(emailCaffe, "Caffe PoltekSSN"); // Ganti dengan alamat email pengirim dan nama pengirim
                var to = new EmailAddress(email, username); // Alamat email penerima diambil dari textBox1 dan nama penerima bisa diganti sesuai kebutuhan
                var subject = "History Pemesanan";
                var plainTextContent = "Terima kasih telah melakukan pemesanan melalui aplikasi Caffe PoltekSSN. silahkan tunjukkan barcode kepada kasir untuk mengambil pesanan.";
                var htmlContent = "<strong>Ini adalah QR Code yang dihasilkan:</strong>";

                var message = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var bytes = File.ReadAllBytes(qrcodePath); // Membaca file QR code
                var base64QRCode = Convert.ToBase64String(bytes);
                message.AddAttachment("qr_code.png", base64QRCode); // Menambahkan QR code sebagai lampiran

                var response = await client.SendEmailAsync(message);
                MessageBox.Show("Email dengan QR Code telah terkirim.");*/
            }
            else
            {
                MessageBox.Show("Uang anda tidak cukup!");
            }
            
            Execute();
        }

        private void GenerateQrcode()
        {
            QRCodeGenerator qrGenerator = new();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(id.ToString(), QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            pictureBox2.Image = qrCodeImage;

            qrCodeImage.Save(qrcodePath, ImageFormat.Png);
        }


        private async void Execute()
        {
            var emailCaffe = "atlantastuff100@gmail.com";
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(emailCaffe, "Caffe PoltekSSN");
            var subject = "History Pemesanan";
            var to = new EmailAddress(email, username);
            var plainTextContent = "Terima kasih telah melakukan pemesanan melalui aplikasi Caffe PoltekSSN. silahkan tunjukkan barcode kepada kasir untuk mengambil pesanan.";
            var htmlContent = $"<strong>Qr Code Order</strong>" +
                $"\n{plainTextContent}";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var bytes = File.ReadAllBytes(qrcodePath);
            var base64QRCode = Convert.ToBase64String(bytes);
            msg.AddAttachment("CaffePoltekSSN_qrcode.png", base64QRCode);

            var response = await client.SendEmailAsync(msg);

            string status = response.IsSuccessStatusCode ? "Qr code berhasil dikirm ke email kamu!" : "Ada Masalah, coba lagi!";
            MessageBox.Show(status);
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            PreviewStruk preview = new(struk, pictureBox2.Image);
            preview.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UserPage user = new UserPage(username, email, level);
            user.Closed += (s, args) => this.Close();
            user.Show();
            this.Hide();
        }
    }
}
