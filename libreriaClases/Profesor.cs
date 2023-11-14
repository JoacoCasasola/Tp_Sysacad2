using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libreriaClases
{
    public enum NivelProfesor
    {
        ayudante,
        titular,
        jefeDeCatedra,
    }

    public class Profesor
    {
        public string _nombre { get; set; }
        public string _apellido { get; set; }
        public string _dni { get; set; }
        public string _correo { get; set; }
        public string _telefono { get; set; }
        public string _direccion { get; set; }

        [JsonProperty("ID")]
        public string _idProfesor { get; set; }
        public string _clave { get; set; }
        public string _nivel { get; set; }

        public Profesor(string nombre, string apellido, string dni, string correo, string telefono, string direccion, string idProfesor, string nivel, string clave)
        {
            _nombre = nombre;
            _apellido = apellido;
            _dni = dni;
            _correo = correo;
            _telefono = telefono;
            _direccion = direccion;
            _idProfesor = idProfesor;
            _nivel = nivel;
            _clave = clave;
        }
    }
}
