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
    public partial class paraYatirForm : Form
    {
        private int yatirilacakpara;
        private string tc;
        public paraYatirForm()
        {
            InitializeComponent();
        }
        private void paraYatirForm_Load(object sender, EventArgs e)
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
            cmd.Parameters.AddWithValue("@girilentc",tc);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                yatirilacakpara = Convert.ToInt32(dr["musteri_bakiye"].ToString());
            }
            yatirilacakpara += Convert.ToInt32(textBox1.Text);
            dr.Close();
            con.Close();
        }
        private void parayatir()
        {
            try
            {
                string str = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\_gokaycımen\source\repos\gorselprogramlama\gorselprogramlama\bankadatabase.mdf;Integrated Security=True";

                SqlConnection con = new SqlConnection(str);
               
                string komut = "update musteriler set musteri_bakiye = @yenibakiye where musteri_tc =@tc";
                SqlCommand cmd = new SqlCommand(komut, con);
                cmd.Parameters.AddWithValue("@yenibakiye", yatirilacakpara);
                cmd.Parameters.AddWithValue("@tc",tc);
                con.Open();
                cmd.ExecuteNonQuery();
               

                MessageBox.Show("Hesabınıza "+textBox1.Text+" TL yatırıldı!");
                con.Close();
            }
            catch
            {
                MessageBox.Show("Para Yatırma İşlemi Başarısız !");
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
            parayatir();
        }
    }
}
