using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Eczane_Envanter_ve_Reçete_Sistemi
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;

        void EczaciGetir()
        {
            baglanti = new SqlConnection("server=DMRC-COMPTUER\\SQLEXPRESS01;Initial Catalog=Eczane;Integrated Security=SSPI");
            baglanti.Open();
            da = new SqlDataAdapter("SELECT * FROM Eczacilar", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            EczaciGetir();
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
            this.Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string sorgu = "INSERT INTO Eczacilar(Ad,Soyad,LisansNo) VALUES (@ad,@soyad,@lisansno)";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@ad", txtAd.Text);
            komut.Parameters.AddWithValue("@soyad", txtSoyad.Text);
            komut.Parameters.AddWithValue("@lisansno", txtLisans.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            EczaciGetir();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            string sorgu = "DELETE FROM Eczacilar WHERE EczaciID = @eczaciid";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@eczaciid", Convert.ToInt32(txtId.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            EczaciGetir();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE Eczacilar SET Ad=@ad,Soyad=@soyad,LisansNo=@lisansno WHERE EczaciID = @eczaciid";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@eczaciid", Convert.ToInt32(txtId.Text));
            komut.Parameters.AddWithValue("@ad", txtAd.Text);
            komut.Parameters.AddWithValue("@soyad", txtSoyad.Text);
            komut.Parameters.AddWithValue("@lisansno", txtLisans.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            EczaciGetir();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtLisans.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }
    }
}
