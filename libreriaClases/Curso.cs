using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libreriaClases
{
    public class Curso
    {
        public string _nombre { get; set; }

        [JsonProperty("ID")]
        public string _idCurso { get; set; }

        [JsonProperty("Cupo Max.")]
        public int _cupoMax { get; set; }

        [JsonProperty("Cupo Actual")]
        public int _cupoActual { get; set; }
        public string _descripcion { get; set; }
        public string _horario { get; set; }

        [JsonProperty("Id_Inscriptos")]
        public List<string> _inscriptos { get; set; }

        [JsonProperty("Lista_espera")]
        public List<string> _listaEspera { get; set; }

        public Condiciones Condiciones { get; set; }

        public Curso(string nombre, string idCurso, int cupoMax, int cupoActual,string descripcion,string horario,List<string> inscriptos,List<string> listaEspera)
        {
            _nombre = nombre;
            _idCurso = idCurso;
            _cupoMax = cupoMax;
            _cupoActual = cupoActual;
            _descripcion = descripcion;
            _horario = horario;
            _inscriptos = inscriptos;
            _listaEspera = listaEspera;
        }
    }
}
