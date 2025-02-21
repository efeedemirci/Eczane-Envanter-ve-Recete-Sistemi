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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;

        void ReceteGetir()
        {
            baglanti = new SqlConnection("server=DMRC-COMPTUER\\SQLEXPRESS01;Initial Catalog=Eczane;Integrated Security=SSPI");
            komut = new SqlCommand("GetAllReceteler", baglanti);
            komut.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter(komut);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            ReceteGetir();
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
            this.Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string sorgu = "AddRecete";
            komut = new SqlCommand(sorgu, baglanti);
            komut.CommandType = CommandType.StoredProcedure;
            komut.Parameters.AddWithValue("@Tarih", dateTimePicker1.Value);
            komut.Parameters.AddWithValue("@Recete", txtRecete.Text);
            komut.Parameters.AddWithValue("@IlacID", Convert.ToInt32(txtIlacID.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            ReceteGetir();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            string sorgu = "DeleteRecete";
            komut = new SqlCommand(sorgu, baglanti);
            komut.CommandType = CommandType.StoredProcedure;
            komut.Parameters.AddWithValue("@ReceteID", Convert.ToInt32(txtId.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            ReceteGetir();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            string sorgu = "UpdateRecete";
            komut = new SqlCommand(sorgu, baglanti);
            komut.CommandType = CommandType.StoredProcedure;
            komut.Parameters.AddWithValue("@ReceteID", Convert.ToInt32(txtId.Text));
            komut.Parameters.AddWithValue("@Tarih", dateTimePicker1.Value);
            komut.Parameters.AddWithValue("@Recete", txtRecete.Text);
            komut.Parameters.AddWithValue("@IlacID", Convert.ToInt32(txtIlacID.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            ReceteGetir();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtIlacID.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            if (DateTime.TryParse(dataGridView1.CurrentRow.Cells[2].Value.ToString(), out DateTime tarih))
            {
                dateTimePicker1.Value = tarih;
            }
            txtRecete.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
