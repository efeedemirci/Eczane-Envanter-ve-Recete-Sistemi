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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;

        void FirmaGetir()
        {
            baglanti = new SqlConnection("server=DMRC-COMPTUER\\SQLEXPRESS01;Initial Catalog=Eczane;Integrated Security=SSPI");
            baglanti.Open();
            da = new SqlDataAdapter("SELECT * FROM Firmalar", baglanti);
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

        private void Form6_Load(object sender, EventArgs e)
        {
            FirmaGetir();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string sorgu = "INSERT INTO Firmalar(FirmaAdi,Sehir) VALUES (@ad,@sehir)";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@ad", txtAd.Text);
            komut.Parameters.AddWithValue("@sehir", txtSehir.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            FirmaGetir();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            string sorgu = "DELETE FROM Firmalar WHERE FirmaID = @firmaid";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@firmaid", Convert.ToInt32(txtId.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            FirmaGetir();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE Firmalar SET FirmaAdi=@ad,Sehir=@sehir WHERE FirmaID=@firmaid";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@firmaid", Convert.ToInt32(txtId.Text));
            komut.Parameters.AddWithValue("@ad", txtAd.Text);
            komut.Parameters.AddWithValue("@sehir", txtSehir.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            FirmaGetir();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtSehir.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }
    }
}
