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
    public partial class islemlerForm : Form
    {
        private string bakiye;
        private string tc;
        public islemlerForm()
        {
            InitializeComponent();
        }

        private void islemlerForm_Load(object sender, EventArgs e)
        {
            label10.Text = Form1.musterino;
            label11.Text = Form1.adsoyad;
            tc = Form1.musteritc;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 giris = new Form1();
            giris.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            paraYatirForm yatir = new paraYatirForm();
            yatir.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
            try
            {
                string str = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\_gokaycımen\source\repos\gorselprogramlama\gorselprogramlama\bankadatabase.mdf;Integrated Security=True";

                SqlConnection con = new SqlConnection(str);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT musteri_bakiye FROM musteriler where musteri_tc = @girilentc";
                cmd.Parameters.AddWithValue("@girilentc", tc);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    bakiye = dr["musteri_bakiye"].ToString();
                }
              
                dr.Close();
                con.Close();

                MessageBox.Show("Hesabınızda "+bakiye+" TL Para Bulunmaktadır!");
            }
            catch
            {
                MessageBox.Show("Para Yatırma İşlemi Başarısız !");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            paraCekForm cek = new paraCekForm();
            cek.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            havaleForm havale = new havaleForm();
            havale.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            sifreDegistirForm sifre = new sifreDegistirForm();
            sifre.Show();
        }
    }
}
