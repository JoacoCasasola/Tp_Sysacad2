using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libreriaClases
{
    public class Alumno
    {
        public string _nombre { get; set; }
        public string _apellido { get; set; }
        public string _Dni { get; set; }
        public string _correo { get; set; }
        public string _telefono { get; set; }
        public string _direccion {  get; set; }
        public string _carrera { get; set; }
        public string _clave { get; set; }
        public string _legajo { get; set; }

        public Alumno(string nombre, string apellido, string Dni, string telefono, string direccion, string correo, string carrera, string clave, string legajo)
        {
            _nombre = nombre;
            _apellido = apellido;
            _Dni = Dni;
            _telefono = telefono;
            _direccion = direccion;
            _correo = correo;
            _carrera = carrera;
            _clave = clave;
            _legajo = legajo;
        }
    }
}
