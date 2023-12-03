using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libreriaClases
{
    public enum NivelAdmin
    {
        basico,
        intermedio,
        avanzado,
    }

    public class Administrador
    {
        public string _nombre { get; set; }
        public string _apellido { get; set; }
        public string _dni { get; set; }
        public string _correo { get; set; }
        public string _telefono { get; set; }
        public string _direccion { get; set; }

        [JsonProperty("ID")]
        public string _idAdmin { get; set; }
        public string _clave { get; set; }
        public NivelAdmin _nivel { get; set; }

        public Administrador(string nombre, string apellido, string dni, string correo, string telefono, string direccion,string idAdmin, NivelAdmin nivel, string clave)
        {
            _nombre = nombre;
            _apellido = apellido;
            _dni = dni;
            _correo = correo;
            _telefono = telefono;
            _direccion = direccion;
            _idAdmin = idAdmin;
            _nivel = nivel;
            _clave = clave;
        }



        public static List<Dictionary<string, string>> AgregarAListaProfesores(string nombre, string apellido, string dni, string correo, string telefono, string direccion, string idProfesor, string nivel, string clave)
        {
            Dictionary<string, string> dictProfesor;
            List<Dictionary<string, string>> listaDictProfesores = new List<Dictionary<string, string>>();

            dictProfesor = new Dictionary<string, string>();
            dictProfesor["Nombre"] = nombre;
            dictProfesor["Apellido"] = apellido;
            dictProfesor["DNI"] = dni;
            dictProfesor["Correo"] = correo;
            dictProfesor["Telefono"] = telefono;
            dictProfesor["Direccion"] = direccion;
            dictProfesor["ID"] = idProfesor;
            dictProfesor["Clave"] = clave;
            dictProfesor["Nivel"] = nivel;

            listaDictProfesores.Add(dictProfesor);

            return listaDictProfesores;
        }

        public static List<Dictionary<string, string>> AgregarAListaAlumnos(string nombre, string apellido, string dni, string correo, string telefono, string direccion, string carrera, string legajo, string clave)
        {
            Dictionary<string, string> dictAlumno;
            List<Dictionary<string, string>> listaDictAlumnos = new List<Dictionary<string, string>>();

            dictAlumno = new Dictionary<string, string>();
            dictAlumno["Nombre"] = nombre;
            dictAlumno["Apellido"] = apellido;
            dictAlumno["DNI"] = dni;
            dictAlumno["Correo"] = correo;
            dictAlumno["Telefono"] = telefono;
            dictAlumno["Direccion"] = direccion;
            dictAlumno["Carrera"] = carrera;
            dictAlumno["Legajo"] = legajo;
            dictAlumno["Clave"] = clave;

            listaDictAlumnos.Add(dictAlumno);

            return listaDictAlumnos;
        }

        public static List<Dictionary<string, string>> AgregarAListaAdmin(string nombre, string apellido, string dni, string correo, string telefono, string direccion, string nivel, string idAdmin, string clave)
        {
            Dictionary<string, string> dictAdmin;
            List<Dictionary<string, string>> listaDictAdmin = new List<Dictionary<string, string>>();

            dictAdmin = new Dictionary<string, string>();
            dictAdmin["Nombre"] = nombre;
            dictAdmin["Apellido"] = apellido;
            dictAdmin["DNI"] = dni;
            dictAdmin["Correo"] = correo;
            dictAdmin["Telefono"] = telefono;
            dictAdmin["Direccion"] = direccion;
            dictAdmin["ID"] = idAdmin;
            dictAdmin["Clave"] = clave;
            dictAdmin["Nivel"] = nivel;

            listaDictAdmin.Add(dictAdmin);

            return listaDictAdmin;
        }

        public static List<Dictionary<string, string>> AgregarAListaCursos(string nombre, string id, int cupoMax, int cupoActual, string descripcion, List<string> inscriptos, List<string> listaEspera, string horario)
        {
            Dictionary<string, string> dictCurso = new Dictionary<string, string>();
            List<Dictionary<string, string>> listaDictCurso = new List<Dictionary<string, string>>();

            dictCurso["Nombre"] = nombre;
            dictCurso["ID"] = id;
            dictCurso["Cupo Max."] = cupoMax.ToString();
            dictCurso["Cupo Actual"] = cupoActual.ToString();
            dictCurso["Descripcion"] = descripcion;
            dictCurso["ID Inscriptos"] = string.Join(",", inscriptos);
            dictCurso["Lista de espera"] = string.Join(",", listaEspera);
            dictCurso["Horario"] = horario;

            listaDictCurso.Add(dictCurso);

            return listaDictCurso;
        }





        public static List<Dictionary<string, string>> AgregarListaNotasExamen(string examen, string nota)
        {
            Dictionary<string, string> dictNotas = new Dictionary<string, string>();
            List<Dictionary<string, string>> listaDictNotas = new List<Dictionary<string, string>>();

            dictNotas["Examen"] = examen;
            dictNotas["Nota"] = nota;

            listaDictNotas.Add(dictNotas);

            return listaDictNotas;
        }

        public static List<Dictionary<string, string>> AgregarListaNotasTp(string tp, string nota)
        {
            Dictionary<string, string> dictNotas = new Dictionary<string, string>();
            List<Dictionary<string, string>> listaDictNotas = new List<Dictionary<string, string>>();

            dictNotas["TP"] = tp;
            dictNotas["Nota"] = nota;

            listaDictNotas.Add(dictNotas);

            return listaDictNotas;
        }

        public static List<Dictionary<string, string>> AgregarListaNotasTclase(string curso , string legajo, string fechaClase, string nota)
        {
            Dictionary<string, string> dictNotas = new Dictionary<string, string>();
            List<Dictionary<string, string>> listaDictNotas = new List<Dictionary<string, string>>();

            dictNotas["Curso"] =  curso;
            dictNotas["Legajo Alumno"] = legajo;
            dictNotas["Fecha de clase"] = fechaClase;
            dictNotas["Nota"] = nota;

            listaDictNotas.Add(dictNotas);

            return listaDictNotas;
        }
    }


        
}
