using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libreriaClases
{
    internal class UsuarioProfesor
    {
        private string _idProfesor;
        private string _clave;

        public UsuarioProfesor(string idProfesor, string clave)
        {
            _idProfesor = idProfesor;
            _clave = clave;
        }
    }
}
