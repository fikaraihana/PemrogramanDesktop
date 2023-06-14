using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace ProDesk_CaffePoltekSSN
{
    public partial class Login : Form
    {

        private const string FilePath = "user.csv";

        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            if (CheckCredentials(username, password, out string level, out string email))
            {
                MessageBox.Show("Login berhasil!");

                if (level == "admin")
                {
                    AdminPage adminlogin = new AdminPage(username, email, level);
                    this.Hide();
                    adminlogin.Show();
                    adminlogin.Closed += (s, args) => this.Close();
                }
                else if (level == "user")
                {
                    UserPage userlogin = new UserPage(username, email, level);
                    this.Hide();
                    userlogin.Show();
                    userlogin.Closed += (s, args) => this.Close();
                }
            }
            else
            {
                MessageBox.Show("Username atau password salah.");
                ClearFields();
            }
        }

        private bool CheckCredentials(string username, string password, out string level, out string email)
        {
            string[] lines = File.ReadAllLines(FilePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');

                string storedUsername = parts[0].Trim();
                string storedPassword = parts[1].Trim();
                string storedLevel = parts[2].Trim();
                string storedEmail = parts[3].Trim();

                if (username == storedUsername && password == storedPassword)
                {
                    level = storedLevel;
                    email = storedEmail;
                    return true;
                }
            }

            level = string.Empty;
            email = string.Empty;
            return false;
        }

        private void ClearFields()
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
        }
    }
}
