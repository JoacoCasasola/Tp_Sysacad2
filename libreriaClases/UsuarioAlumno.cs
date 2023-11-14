using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libreriaClases
{
    public class UsuarioAlumno
    {
        private string _legajo;
        private string _clave;

        public UsuarioAlumno(string legajo, string clave)
        {
            _legajo = legajo;
            _clave = clave;
        }
    }
}
