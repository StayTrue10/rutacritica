using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace RUTACRITICA
{
    class ConsultaSQL
    {
        private SqlConnection conexion = new SqlConnection("Data Source = 10.10.50.201; Initial Catalog = RUTACRITICA; user id=sa; password=Pr0c3s0.12");
        public bool Salidas(string ticket, string id, string fecha, string hora)
        {
            conexion.Open();
            SqlCommand cmd = new SqlCommand(string.Format("Insert into Salidas values ('{0}', '{1}', '{2}', '{3}')", new string[] { ticket, id, fecha, hora }), conexion);
            int filasafectadas = cmd.ExecuteNonQuery();
            conexion.Close();
            if (filasafectadas > 0)
                return true;
            else
                return false;
        }
        public bool Entradas(string ticket, string id, string fecha, string hora)
        {
            conexion.Open();
            SqlCommand cmd = new SqlCommand(string.Format("insert into Entradas values ('{0}', '{1}', '{2}', '{3}')", new string[] { ticket, id, fecha, hora }), conexion);
            int filasafectadas = cmd.ExecuteNonQuery();
            conexion.Close();
            if (filasafectadas > 0)
                return true;
            else
                return false;
        }
    }
}
