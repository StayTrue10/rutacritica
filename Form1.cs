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

namespace RUTACRITICA
{
    public partial class Form1 : Form
    {
        ConsultaSQL sql = new ConsultaSQL();
        public Form1()
        {
            InitializeComponent();
            sql.idsalida(cbID1);
            sql.identrada(cbID2);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            tbFecha1.Text = DateTime.Now.ToString("dd/MM/yyyy");
            tbHora1.Text = DateTime.Now.ToString("hh:mm:ss");
            tbFecha2.Text = DateTime.Now.ToString("dd/MM/yyyy");
            tbHora2.Text = DateTime.Now.ToString("hh:mm:ss");
        }
        
        private void btnRegistrar1_Click(object sender, EventArgs e)
        {
            if (cbID1.Text.Length == 0 || tbTicket1.Text.Length == 0)
            {
                MessageBox.Show("Faltan campos por rellenar", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                using (SqlConnection cn = new SqlConnection("Data Source = 10.10.50.201; Initial Catalog = RUTACRITICA; user id=sa; password=Pr0c3s0.12"))
                {
                    var cmd = new SqlCommand("SELECT COUNT(*) FROM Salidas WHERE ticket = @ticket", cn);
                    cmd.Parameters.AddWithValue("@ticket", tbTicket1.Text);
                    cn.Open();
                    if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                    {
                        if (sql.Salidas(tbTicket1.Text, cbID1.Text, tbFecha1.Text, tbHora1.Text))
                        {
                            MessageBox.Show("Registrado con exito!", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No se pudo registrar", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Este ticket ya esta registrado", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }
       private void btnRegistrar2_Click(object sender, EventArgs e)
        {
            if (cbID2.Text.Length == 0 || tbTicket2.Text.Length == 0)
            {
                MessageBox.Show("Faltan campos por rellenar", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                using (SqlConnection cn = new SqlConnection("Data Source = 10.10.50.201; Initial Catalog = RUTACRITICA; user id=sa; password=Pr0c3s0.12"))
                {
                    var cmd = new SqlCommand("SELECT COUNT(*) FROM Entradas WHERE ticket = @ticket", cn);
                    cmd.Parameters.AddWithValue("@ticket", tbTicket2.Text);
                    cn.Open();
                    if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                    {
                        if (sql.Entradas(tbTicket2.Text, cbID2.Text, tbFecha2.Text, tbHora2.Text))
                        {
                            MessageBox.Show("Registrado con exito!", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No se pudo registrar", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Este ticket ya esta registrado", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }
        private void btnNuevo1_Click(object sender, EventArgs e)
        {
            cbID1.Text = "";
            tbTicket1.Text = "";
        }
        private void btnNuevo2_Click(object sender, EventArgs e)
        {
            cbID2.Text = "";
            tbTicket2.Text = "";
        }
       


    }
}
