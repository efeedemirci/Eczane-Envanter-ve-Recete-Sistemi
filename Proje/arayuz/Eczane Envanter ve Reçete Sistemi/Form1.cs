using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eczane_Envanter_ve_Reçete_Sistemi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            string kadi, parola;

            kadi = txtKadi.Text.Trim();
            parola = txtParola.Text.Trim();

            if (kadi == "admin" &&  parola == "1234") 
            {
                MessageBox.Show("Sisteme Giriş Yapıldı!", "Eczane Envanter ve Reçete Sistemi");

                Form2 frm2 = new Form2();
                frm2.Show();
                this.Hide();
            }
        }
    }
}
