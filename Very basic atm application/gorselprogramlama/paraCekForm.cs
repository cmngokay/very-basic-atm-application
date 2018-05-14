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
    public partial class paraCekForm : Form
    {
        private int cekilecekpara;
        private string tc;
        public paraCekForm()
        {
            InitializeComponent();
        }

        private void paraCekForm_Load(object sender, EventArgs e)
        {
            tc = Form1.musteritc;
        }
        private void bakiyeHesapla()
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
                cekilecekpara = Convert.ToInt32(dr["musteri_bakiye"].ToString());
            }
            cekilecekpara -= Convert.ToInt32(textBox1.Text);
            dr.Close();
            con.Close();
        }
        private void paraCek()
        {
            try
            {
                string str = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\_gokaycımen\source\repos\gorselprogramlama\gorselprogramlama\bankadatabase.mdf;Integrated Security=True";

                SqlConnection con = new SqlConnection(str);

                string komut = "update musteriler set musteri_bakiye = @yenibakiye where musteri_tc =@tc";
                SqlCommand cmd = new SqlCommand(komut, con);
                cmd.Parameters.AddWithValue("@yenibakiye", cekilecekpara);
                cmd.Parameters.AddWithValue("@tc", tc);
                con.Open();
                cmd.ExecuteNonQuery();


                MessageBox.Show("Hesabınızdan " + textBox1.Text + " TL para çekildi!");
                con.Close();
            }
            catch
            {
                MessageBox.Show("Para Çekme İşlemi Başarısız !");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            islemlerForm islem = new islemlerForm();
            islem.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bakiyeHesapla();
            paraCek();
        }
    }
}
