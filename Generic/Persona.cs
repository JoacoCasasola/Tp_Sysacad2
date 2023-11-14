using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic
{
    internal class Persona
    {
        public string nombre { get; set; }
        public int id { get; set; }

        public Persona(string nombre, int id)
        {
            this.nombre = nombre;
            this.id = id;
        }

        public override string ToString()
        {
            return $"Nombre: {nombre}  ID: {id}";
        }
    }
}
