using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libreriaClases
{
    public class Condiciones
    {
        public Condiciones(bool promedioMinimo, int promedio, bool inscripcionAnterior, int curso)
        {
            PromedioMinimo = promedioMinimo;
            Promedio = promedio;
            InscripcionAnterior = inscripcionAnterior;
            Curso = curso;
        }

        public bool PromedioMinimo { get; set; }
        public int Promedio { get; set; }
        public bool InscripcionAnterior { get; set; }
        public int Curso { get; set; }
    }
}
