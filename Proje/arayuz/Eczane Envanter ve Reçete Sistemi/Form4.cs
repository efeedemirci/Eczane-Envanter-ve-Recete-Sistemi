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
using System.Data.SqlClient;

namespace Eczane_Envanter_ve_Reçete_Sistemi
{
    public partial class Form4 : Form
    {

        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;

        public Form4()
        {
            InitializeComponent();
        }

        void DoktorGetir()
        {
            baglanti = new SqlConnection("server=DMRC-COMPTUER\\SQLEXPRESS01;Initial Catalog=Eczane;Integrated Security=SSPI");
            baglanti.Open();
            da = new SqlDataAdapter("SELECT * FROM doktorlar", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
            this.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            DoktorGetir();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string sorgu = "INSERT INTO doktorlar(Ad,Soyad,Uzmanlik) VALUES (@ad,@soyad,@uzmanlik)";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@ad", txtAd.Text);
            komut.Parameters.AddWithValue("@soyad", txtSoyad.Text);
            komut.Parameters.AddWithValue("@uzmanlik", txtUzmanlik.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            DoktorGetir();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            string sorgu = "DELETE FROM doktorlar WHERE DoktorID = @doktorid";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@doktorid", Convert.ToInt32(txtId.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            DoktorGetir();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE doktorlar SET Ad=@ad,Soyad=@soyad,Uzmanlik=@uzmanlik WHERE DoktorID=@doktorid";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@doktorid", Convert.ToInt32(txtId.Text));
            komut.Parameters.AddWithValue("@ad", txtAd.Text);
            komut.Parameters.AddWithValue("@soyad", txtSoyad.Text);
            komut.Parameters.AddWithValue("@uzmanlik", txtUzmanlik.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            DoktorGetir();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtUzmanlik.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }
    }
}
