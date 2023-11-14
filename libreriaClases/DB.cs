using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libreriaClases
{
    internal class DB
    {
        private static SqlConnection conexion;
        private static string cadenaConexion;

        static DB()
        {
            cadenaConexion = @"Server=.;Database=myDataBase;Trusted_Connection=True;";
            conexion = new SqlConnection(cadenaConexion);
        }

        public static void Query()
        {
            Console.WriteLine("Query");
        }
    }
}
