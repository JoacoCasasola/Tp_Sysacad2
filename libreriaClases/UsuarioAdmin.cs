using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libreriaClases
{
    internal class UsuarioAdmin
    {
        private string _idAdmin;
        private string _clave;

        public UsuarioAdmin(string idAdmin, string clave)
        {
            _idAdmin = idAdmin;
            _clave = clave;
        }
    }
}
