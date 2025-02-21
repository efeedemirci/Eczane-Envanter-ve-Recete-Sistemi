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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;

        void HastaGetir()
        {
            baglanti = new SqlConnection("server=DMRC-COMPTUER\\SQLEXPRESS01;Initial Catalog=Eczane;Integrated Security=SSPI");
            baglanti.Open();
            da = new SqlDataAdapter("SELECT * FROM Hastalar", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            HastaGetir();
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
            this.Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string sorgu = "INSERT INTO Hastalar(Ad,Soyad,DogumTarihi,ReceteID,DoktorID,EczaciID) VALUES (@ad,@soyad,@dtarihi,@receteid,@doktorid,@eczaciid)";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@ad", txtAd.Text);
            komut.Parameters.AddWithValue("@soyad", txtSoyad.Text);
            komut.Parameters.AddWithValue("@dtarihi", dateTimePicker1.Value);
            komut.Parameters.AddWithValue("@receteid", Convert.ToInt32(txtReceteID.Text));
            komut.Parameters.AddWithValue("@doktorid", Convert.ToInt32(txtDoktorID.Text));
            komut.Parameters.AddWithValue("@eczaciid", Convert.ToInt32(txtEczaciID.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            HastaGetir();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            string sorgu = "DELETE FROM Hastalar WHERE HastaID = @hastaid";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@hastaid", Convert.ToInt32(txtId.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            HastaGetir();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE Hastalar SET Ad=@ad,Soyad=@soyad,DogumTarihi=@dtarihi,ReceteID=@receteid,DoktorID=@doktorid,EczaciID=@eczaciid WHERE HastaID=@hastaid";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@hastaid", Convert.ToInt32(txtId.Text));
            komut.Parameters.AddWithValue("@ad", txtAd.Text);
            komut.Parameters.AddWithValue("@soyad", txtSoyad.Text);
            komut.Parameters.AddWithValue("@dtarihi", dateTimePicker1.Value);
            komut.Parameters.AddWithValue("@receteid", Convert.ToInt32(txtReceteID.Text));
            komut.Parameters.AddWithValue("@doktorid", Convert.ToInt32(txtDoktorID.Text));
            komut.Parameters.AddWithValue("@eczaciid", Convert.ToInt32(txtEczaciID.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            HastaGetir();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtReceteID.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtDoktorID.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtEczaciID.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
        }
    }
}
