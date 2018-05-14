using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gorselprogramlama
{
    public partial class sifreDegistirForm : Form
    {
        private string tc;
        private string eskisifre;
        public sifreDegistirForm()
        {
            InitializeComponent();
        }

        private void sifreDegistirForm_Load(object sender, EventArgs e)
        {
            textBox1.PasswordChar = '*';
            textBox2.PasswordChar = '*';
            tc = Form1.musteritc;
            eskisifre = Form1.sifre;
        }
        private void sifreDegistir()
        {
            if (eskisifre.Equals(textBox1.Text))
            {
                try
                {
                    string str = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\_gokaycımen\source\repos\gorselprogramlama\gorselprogramlama\bankadatabase.mdf;Integrated Security=True";

                    SqlConnection con = new SqlConnection(str);
                    string komut = "update musteriler set musteri_sifre = @yenisifre where musteri_tc =@tc";
                    SqlCommand cmd = new SqlCommand(komut, con);
                    cmd.Parameters.AddWithValue("@yenisifre", textBox2.Text);
                    cmd.Parameters.AddWithValue("@tc", tc);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Şifre Değiştirildi !");
                }
                catch
                {
                    MessageBox.Show("Şifre Değiştirme İşlemi Başarısız !");
                }
            }
            else
            {
                MessageBox.Show("Eski Şifre Yanlış Girildi !");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sifreDegistir();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            islemlerForm islem = new islemlerForm();
            islem.Show();
        }
    }
}
