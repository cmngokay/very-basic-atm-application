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
    public partial class havaleForm : Form
    {
        private string tc;
        private string gonderilecektc;
        private int gonderilecekpara;
        private int gonderenbakiye;
        public havaleForm()
        {
            InitializeComponent();
        }

        private void havaleForm_Load(object sender, EventArgs e)
        {
            tc = Form1.musteritc;        
        }
        private void havaleHesapIslemi()
        {
            gonderilecektc = textBox1.Text;
            string str = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\_gokaycımen\source\repos\gorselprogramlama\gorselprogramlama\bankadatabase.mdf;Integrated Security=True";

            SqlConnection con = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT musteri_bakiye FROM musteriler where musteri_tc = @girilentc";
            cmd.Parameters.AddWithValue("@girilentc", gonderilecektc);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                gonderilecekpara = Convert.ToInt32(dr["musteri_bakiye"].ToString());
            }
            gonderilecekpara += Convert.ToInt32(textBox2.Text);
            dr.Close();
            con.Close();
        }

        private void havaleGerceklestir()
        {
            try
            {
                string str = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\_gokaycımen\source\repos\gorselprogramlama\gorselprogramlama\bankadatabase.mdf;Integrated Security=True";

                SqlConnection con = new SqlConnection(str);

                string komut = "update musteriler set musteri_bakiye = @yenibakiye where musteri_tc =@tc";
                SqlCommand cmd = new SqlCommand(komut, con);
                cmd.Parameters.AddWithValue("@yenibakiye", gonderilecekpara);
                cmd.Parameters.AddWithValue("@tc",gonderilecektc);
                con.Open();
                cmd.ExecuteNonQuery();


                MessageBox.Show("Havale İşlemi Gerçekleştirildi !");
                con.Close();
            }
            catch
            {
                MessageBox.Show("Havale İşlemi Başarısız !");
            }
        }

        private void gonderenIslem()
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
                gonderenbakiye = Convert.ToInt32(dr["musteri_bakiye"].ToString());
            }
            gonderenbakiye -= Convert.ToInt32(textBox2.Text);
            dr.Close();
            con.Close();
        }

        private void gonderenGuncelle()
        {
            try
            {
                string str = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\_gokaycımen\source\repos\gorselprogramlama\gorselprogramlama\bankadatabase.mdf;Integrated Security=True";

                SqlConnection con = new SqlConnection(str);

                string komut = "update musteriler set musteri_bakiye = @yenibakiye where musteri_tc =@tc";
                SqlCommand cmd = new SqlCommand(komut, con);
                cmd.Parameters.AddWithValue("@yenibakiye", gonderenbakiye);
                cmd.Parameters.AddWithValue("@tc", tc);
                con.Open();
                cmd.ExecuteNonQuery();
              
                con.Close();
            }
            catch
            {
                MessageBox.Show("gönderen kişinin hesap güncellemesi başarısız!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            havaleHesapIslemi();
            havaleGerceklestir();
            gonderenIslem();
            gonderenGuncelle();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            islemlerForm islem = new islemlerForm();
            islem.Show();
                
        }
    }
}
