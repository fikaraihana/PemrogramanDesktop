using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static Org.BouncyCastle.Math.EC.ECCurve;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace ProDesk_CaffePoltekSSN
{
    public partial class AdminPage : Form
    {
        private string username;
        private string email;
        private string level;
        private string menu;
        private decimal menuPrice;
        private decimal sizePrice;
        private string size = string.Empty;
        private string ice_lv = string.Empty;
        private string sugar_lv = string.Empty;
        private decimal addonTotal = 0;
        private string addonOrder = string.Empty;
        private string struk = string.Empty;
        private string filePath = @"..\net6.0-windows\csv file\menu.csv";
        private List<Menu> menus;
        private List<Order> records;

        Dictionary<string, decimal> addonPrices = new Dictionary<string, decimal>
        {
            { "Bubble", 3000 },
            { "Grass Jelly", 3500 },
            { "Nata de Coco", 2000 },
            { "Whipped Cream", 1000 },
            { "Choco Chips", 1500 },
            { "Oreo", 2000 }
        };

        public AdminPage(string username, string email, string level)
        {
            InitializeComponent();
            this.username = username;
            this.email = email;
            this.level = level;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChooseSize();
            SugarIce(out sugar_lv, out ice_lv);
            ChooseAddon(out addonOrder, out addonTotal);
            ChooseMenu(out menu, out menuPrice);

            var guid = Guid.NewGuid();
            DateTime waktu1 = DateTime.Now;

            //set
            var newOrder = new Order
            {
                Id = guid,
                Username = username,
                Email = email,
                Item = menu,
                ItemPrice = menuPrice,
                Size = size,
                SizePrice = sizePrice,
                Sugarlvl = sugar_lv,
                Icelvl = ice_lv,
                AddonPrice = addonTotal,
                Waktu = waktu1
            };

            newOrder.TotalHarga();

            records = new List<Order>
            {
                newOrder
            };

            WriteCsv();
            CetakStruk(records, addonOrder);
            ToCheckOut();
            ResetFields();
        }

        private void ToCheckOut()
        {
            CheckOut co = new(
                records[0].Id,
                records[0].Username,
                records[0].Email,
                records[0].Item,
                records[0].ItemPrice,
                records[0].Size,
                records[0].SizePrice,
                records[0].Sugarlvl,
                records[0].Icelvl,
                addonOrder,
                records[0].AddonPrice,
                records[0].Bill,
                struk,
                records[0].Waktu
                );
            this.Hide();
            co.Show();
            co.Closed += (s, args) => this.Close();
        }

        private void ChooseMenu(out string menu, out decimal menuPrice)
        {
            Menu selectedMenu = (Menu)comboBox1.SelectedItem;
            menu = selectedMenu.Name;
            menuPrice = selectedMenu.Price;
        }

        private string ChooseSize()
        {
            if (radioButton5.Checked == true)
            {
                size = "Large";
                sizePrice = 3000;
            }
            if (radioButton4.Checked == true)
            {
                size = "Medium";
                sizePrice = 0;
            }
            if (size == string.Empty)
            {
                size = "Large";
                sizePrice = 3000;
            }
            return size;
        }

        private void SugarIce(out string sugar_lv, out string ice_lv)
        {
            //Pilih sugar level
            sugar_lv = comboBox2.Text;
            if (comboBox2.Text == "")
            {
                sugar_lv = "100%";
            }

            //Pilih ice level
            ice_lv = comboBox3.Text;
            if (comboBox3.Text == "")
            {
                ice_lv = "100%";
            }
        }

        private void ChooseAddon(out string listAddon, out decimal hargaAddon)
        {
            var addon = checkedListBox1.CheckedItems.Cast<KeyValuePair<string, decimal>>().ToList();
            listAddon = string.Empty;
            hargaAddon = 0;
            for (int i = 0; i < addon.Count; i++)
            {
                string addonName = addon[i].Key;
                decimal addonPrice = addon[i].Value;
                listAddon += $"{addonName}\t(Rp{addonPrice})\n";
                hargaAddon += addonPrice;
            }
        }

        private void WriteCsv()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };
            using var writer = new StreamWriter(@"..\net6.0-windows\csv file\order_record.csv", append: true);
            using var csv = new CsvWriter(writer, config);
            csv.WriteRecords(records);
        }

        private void CetakStruk(List<Order> orders, string addonOrder)
        {
            foreach (var order in orders)
            {
                struk = $"DETAIL ORDER" +
                    $"\n{records[0].Waktu}" +
                    $"\nPelanggan:{records[0].Email}" +
                    $"\nMenu: \t\t {records[0].Item} (Rp{records[0].ItemPrice})" +
                    $"\nSize: \t\t {records[0].Size} (Rp{records[0].SizePrice})" +
                    $"\nSugar Level: \t{records[0].Sugarlvl}" +
                    $"\nIce Level: \t{records[0].Icelvl}" +
                    $"\nAddon:\n{addonOrder}" +
                    $"\nTotal Belanja: \tRp{records[0].Bill}";
            }
            MessageBox.Show(struk);
        }

        private void LoadProductList()
        {
            // Membaca file teks untuk mendapatkan daftar produk
            string[] lines = File.ReadAllLines(filePath);

            menus = new List<Menu>();

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');

                if (parts.Length == 2)
                {
                    string menuName = parts[0];
                    decimal menuPrice;

                    if (decimal.TryParse(parts[1], out menuPrice))
                    {
                        Menu menu = new Menu(menuName, menuPrice);
                        menus.Add(menu);
                    }
                }
            }

            comboBox1.DataSource = menus;
            comboBox1.DisplayMember = "Name";
            checkedListBox1.DataSource = new BindingSource(addonPrices, null);
            checkedListBox1.DisplayMember = "Key";
            checkedListBox1.ValueMember = "Value";
            checkedListBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }

        private void UserPage_Load(object sender, EventArgs e)
        {
            LoadProductList();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Menu selectedMenu = (Menu)comboBox1.SelectedItem;
            label7.Text = selectedMenu.Price.ToString("C", new CultureInfo("id-ID"));
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Closed += (s, args) => this.Close();
            login.Show();
            this.Hide();
        }
        private void ResetFields()
        {
            radioButton5.Checked = false;
            radioButton4.Checked = false;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            checkedListBox1.ClearSelected();
            addonTotal = 0;
            addonOrder = string.Empty;

            records.Clear();
        }
    }
}

//notes: handle exception kalau menu null