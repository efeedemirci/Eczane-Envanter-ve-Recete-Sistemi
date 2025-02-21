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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;

        void IlacGetir()
        {
            baglanti = new SqlConnection("server=DMRC-COMPTUER\\SQLEXPRESS01;Initial Catalog=Eczane;Integrated Security=SSPI");
            baglanti.Open();
            da = new SqlDataAdapter("SELECT * FROM Ilaclar", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            IlacGetir();
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
            this.Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string sorgu = "INSERT INTO Ilaclar(IlacAdi,Adet,Ucret,FirmaID) VALUES (@ad,@adet,@ucret,@firmaid)";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@ad", txtAd.Text);
            komut.Parameters.AddWithValue("@adet", Convert.ToInt32(txtAdet.Text));
            komut.Parameters.AddWithValue("@ucret", Convert.ToDecimal(txtUcret.Text));
            komut.Parameters.AddWithValue("@firmaid", Convert.ToInt32(txtFirmaID.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            IlacGetir();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            string sorgu = "DELETE FROM Ilaclar WHERE IlacID = @ilacid";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@ilacid", Convert.ToInt32(txtId.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            IlacGetir();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE Ilaclar SET IlacAdi=@ad,Adet=@adet,Ucret=@ucret,FirmaID=@firmaid WHERE IlacID=@ilacid";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@ilacid", Convert.ToInt32(txtId.Text));
            komut.Parameters.AddWithValue("@ad", txtAd.Text);
            komut.Parameters.AddWithValue("@adet", Convert.ToInt32(txtAdet.Text));
            komut.Parameters.AddWithValue("@ucret", Convert.ToDecimal(txtUcret.Text));
            komut.Parameters.AddWithValue("@firmaid", Convert.ToInt32(txtFirmaID.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            IlacGetir();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtAdet.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtUcret.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtFirmaID.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }
    }
}
