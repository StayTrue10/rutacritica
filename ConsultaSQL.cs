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
        //Conexion a sql server
        private SqlConnection conexion = new SqlConnection("Data Source = 10.10.50.201; Initial Catalog = RUTACRITICA; user id=sa; password=Pr0c3s0.12");
        //Insert datos a bd (varchar, int, date, time)
        public bool Salidas(string ticket, int id, string fecha, string hora)
        {
            //Abre la conexion e inserta en las columas correspondientes
            conexion.Open();
            SqlCommand cmd = new SqlCommand(string.Format("Insert into Salidas values ('{0}', {1}, '{2}', '{3}')", ticket, id, fecha, hora), conexion);
            //Filas afectadas mayores a 0, registradas con exito
            int n = cmd.ExecuteNonQuery();
            conexion.Close();
            if (n > 0)
                return true;
            else
                return false;
        }
        public bool Entradas(string ticket, int id, string fecha, string hora)
        {
            conexion.Open();
            SqlCommand cmd = new SqlCommand(string.Format("Insert into Entradas values ('{0}', {1}, '{2}', '{3}')", ticket, id, fecha, hora), conexion);
            int n = cmd.ExecuteNonQuery();
            conexion.Close();
            if (n > 0)
                return true;
            else
                return false;
        }
    }
}
