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
    public partial class Form1 : Form
    {
        public static string musteritc;
        public static string musterino;
        public static string adsoyad;
        public static string  sifre;
        public Form1()
        {
            InitializeComponent();
        }
        private void baglanti()
        {
            string str = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\_gokaycımen\source\repos\gorselprogramlama\gorselprogramlama\bankadatabase.mdf;Integrated Security=True";
  
            SqlConnection con = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT musteri_no,musteri_sifre,musteri_adsoyad FROM musteriler where musteri_tc = @girilentc";
            cmd.Parameters.AddWithValue("@girilentc", textBox1.Text);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
          

            while (dr.Read())
            {
                sifre = dr["musteri_sifre"].ToString();
                musterino = dr["musteri_no"].ToString();
                adsoyad = dr["musteri_adsoyad"].ToString();
            }          
            dr.Close();
            con.Close();

        }

        private void Form1_Load(object sender, EventArgs e)
        {          
            textBox2.PasswordChar = '*';        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti();
            kontrol();
        }

        private void kontrol()
        {           
            string sifre_kontrol = textBox2.Text;
            if(sifre_kontrol.Equals(sifre))
            {
                musteritc = textBox1.Text;
                islemlerForm islem = new islemlerForm();
                islem.Show();
                this.Hide();
                
            }
            else
            {
                MessageBox.Show("T.C veya Şifre Hatalı !");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
