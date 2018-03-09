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
        //Clase ConsultaSQL
        ConsultaSQL sql = new ConsultaSQL();
        public Form1()
        {
            InitializeComponent();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //Fecha y hora actual
            tbFecha1.Text = DateTime.Now.ToString("yyyy/MM/dd");
            tbHora1.Text = DateTime.Now.ToString("HH:mm:ss");
            tbFecha2.Text = DateTime.Now.ToString("yyyy/MM/dd");
            tbHora2.Text = DateTime.Now.ToString("HH:mm:ss");
        }
        private void btnRegistrar1_Click(object sender, EventArgs e)
        {
            //Verifica campos vacios
            if (cbID1.Text.Length == 0 || tbTicket1.Text.Length == 0)
                MessageBox.Show("Faltan campos por rellenar", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                //Conexion a sql server
                using (SqlConnection cn = new SqlConnection("Data Source = 10.10.50.201; Initial Catalog = RUTACRITICA; user id=sa; password=Pr0c3s0.12"))
                {
                    //Revisa registros de las tablas y compara con los campos
                    var cmd = new SqlCommand("SELECT COUNT(*) FROM Salidas WHERE ticket = @ticket", cn);
                    cmd.Parameters.AddWithValue("@ticket", tbTicket1.Text);
                    var control = new SqlCommand("SELECT COUNT(*) FROM Diligencieros WHERE id = @id", cn);
                    control.Parameters.AddWithValue("@id", cbID1.Text);
                    cn.Open();
                    //ExecuteScalar=0, no existe id
                    if (Convert.ToInt32(control.ExecuteScalar()) == 0)
                        MessageBox.Show("El ID no exite", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    else
                    {
                        //Verifica que no exista el ticket, ExecuteScalar=0, hace el registro
                        if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                        {
                            //Realiza el insert en la tabla
                            if (sql.Salidas(tbTicket1.Text, Convert.ToInt32(cbID1.Text), tbFecha1.Text, tbHora1.Text))
                            {
                                MessageBox.Show("Registrado con exito!", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                cbID1.Text = "";
                                tbTicket1.Text = "";
                            }
                            else
                                MessageBox.Show("No se pudo registrar", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                            MessageBox.Show("Este ticket ya esta registrado", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            } 
        }
        private void btnRegistrar2_Click(object sender, EventArgs e)
        {
            if (cbID2.Text.Length == 0 || tbTicket2.Text.Length == 0)
                MessageBox.Show("Faltan campos por rellenar", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                using (SqlConnection cn = new SqlConnection("Data Source = 10.10.50.201; Initial Catalog = RUTACRITICA; user id=sa; password=Pr0c3s0.12"))
                {
                    var cmd = new SqlCommand("SELECT COUNT(*) FROM Entradas WHERE ticket = @ticket", cn);
                    cmd.Parameters.AddWithValue("@ticket", tbTicket2.Text);
                    var control = new SqlCommand("SELECT COUNT(*) FROM Diligencieros WHERE id = @id", cn);
                    control.Parameters.AddWithValue("@id", cbID2.Text);
                    var entrada = new SqlCommand("SELECT COUNT(*) FROM Salidas WHERE ticket = @ticket", cn);
                    entrada.Parameters.AddWithValue("@ticket", tbTicket2.Text);
                    cn.Open();
                    if (Convert.ToInt32(control.ExecuteScalar()) == 0)
                        MessageBox.Show("El ID no exite", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    else
                    {
                        //ExecuteScalar=0, no existe ticket en salidas
                        if (Convert.ToInt32(entrada.ExecuteScalar()) == 0)
                            MessageBox.Show("No se ha registrado la salida de este ticket", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        else
                        {
                            if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                            {
                                if (sql.Entradas(tbTicket2.Text, Convert.ToInt32(cbID2.Text), tbFecha2.Text, tbHora2.Text))
                                {
                                    MessageBox.Show("Registrado con exito!", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    cbID2.Text = "";
                                    tbTicket2.Text = "";
                                }
                                else
                                    MessageBox.Show("No se pudo registrar", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else
                                MessageBox.Show("Este ticket ya esta registrado", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            }
        }
        private void btnNuevo1_Click(object sender, EventArgs e)
        {
            //Limpia los campos
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
