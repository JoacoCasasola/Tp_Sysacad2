using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

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

        public static List<Dictionary<string, string>> AgregarAListaCursos(string nombre, string id, int cupoMax, int cupoActual, string descripcion, List<string> inscriptos, bool promedioMinimo, int promedio, bool inscripcionAnterior, string cursoPreinscripto, List<string> listaEspera, string horario)
        {
            Dictionary<string, string> dictCurso = new Dictionary<string, string>();
            List<Dictionary<string, string>> listaDictCurso = new List<Dictionary<string, string>>();

            dictCurso["Nombre"] = nombre;
            dictCurso["ID"] = id;
            dictCurso["Cupo Max."] = cupoMax.ToString();
            dictCurso["Cupo Actual"] = cupoActual.ToString();
            dictCurso["Descripcion"] = descripcion;
            dictCurso["ID Inscriptos"] = string.Join(",", inscriptos);

            var condiciones = new Dictionary<string, object>
            {
                {"PromedioMinimo", promedioMinimo},
                {"Promedio", promedio},
                {"InscripcionAnterior", inscripcionAnterior},
                {"Curso preinscripto", cursoPreinscripto }
            };
            dictCurso["Condiciones"] = JsonConvert.SerializeObject(condiciones);

            dictCurso["Lista de espera"] = string.Join(",", listaEspera);
            dictCurso["Horario"] = horario;

            listaDictCurso.Add(dictCurso);

            return listaDictCurso;
        }



        public static List<Dictionary<string, string>> AgregarListaNotasExamen(string curso, string legajo, string examen, string nota, bool valido)
        {
            Dictionary<string, string> dictNotas = new Dictionary<string, string>();
            List<Dictionary<string, string>> listaDictNotas = new List<Dictionary<string, string>>();

            dictNotas["Curso"] = curso;
            dictNotas["Legajo Alumno"] = legajo;
            dictNotas["Examen"] = examen;
            dictNotas["Nota"] = nota;
            var Validacion = new Dictionary<string, bool>
            {
                {"Validado",valido }
            };
            dictNotas["Validacion"] = JsonConvert.SerializeObject(Validacion);

            listaDictNotas.Add(dictNotas);

            return listaDictNotas;
        }

        public static List<Dictionary<string, string>> AgregarListaNotasTp(string curso, string legajo, string tp, string nota, bool valido)
        {
            Dictionary<string, string> dictNotas = new Dictionary<string, string>();
            List<Dictionary<string, string>> listaDictNotas = new List<Dictionary<string, string>>();

            dictNotas["Curso"] = curso;
            dictNotas["Legajo Alumno"] = legajo;
            dictNotas["TP"] = tp;
            dictNotas["Nota"] = nota;
            var Validacion = new Dictionary<string, bool>
            {
                {"Validado",valido }
            };
            dictNotas["Validacion"] = JsonConvert.SerializeObject(Validacion);

            listaDictNotas.Add(dictNotas);

            return listaDictNotas;
        }

        public static List<Dictionary<string, string>> AgregarListaNotasTclase(string curso , string legajo, string fechaClase, string nota, bool valido)
        {
            Dictionary<string, string> dictNotas = new Dictionary<string, string>();
            List<Dictionary<string, string>> listaDictNotas = new List<Dictionary<string, string>>();

            dictNotas["Curso"] =  curso;
            dictNotas["Legajo Alumno"] = legajo;
            dictNotas["Fecha de clase"] = fechaClase;
            dictNotas["Nota"] = nota;
            var Validacion = new Dictionary<string, bool>
            {
                {"Validado",valido }
            };
            dictNotas["Validacion"] = JsonConvert.SerializeObject(Validacion);

            listaDictNotas.Add(dictNotas);

            return listaDictNotas;
        }

        public static List<Dictionary<string, string>> AgregarListaAsistencia(string curso, string legajo, string fechaClase, string estado, bool valido)
        {
            Dictionary<string, string> dictAsistencia = new Dictionary<string, string>();
            List<Dictionary<string, string>> listaDictAsistencia = new List<Dictionary<string, string>>();

            dictAsistencia["Curso"] = curso;
            dictAsistencia["Legajo Alumno"] = legajo;
            dictAsistencia["Fecha de clase"] = fechaClase;
            dictAsistencia["Estado"] = estado;

            var Validacion = new Dictionary<string, bool>
            {
                {"Validado",valido }
            };
            dictAsistencia["Validacion"] = JsonConvert.SerializeObject(Validacion);

            listaDictAsistencia.Add(dictAsistencia);

            return listaDictAsistencia;
        }



        public static bool CondicionPorPromedio(string legajo, int promedioMin, string cursoEspecifico)
        {
            try
            {
                string jsonFilePathNotas = "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\NotasExamenes.json";
                string jsonFilePathCursos = "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Cursos.json";

                if (File.Exists(jsonFilePathNotas) && File.Exists(jsonFilePathCursos))
                {
                    string jsonDataNotas = File.ReadAllText(jsonFilePathNotas);
                    JArray jsonArrayNotas = JArray.Parse(jsonDataNotas);

                    string jsonDataCursos = File.ReadAllText(jsonFilePathCursos);
                    JArray jsonArrayCursos = JArray.Parse(jsonDataCursos);

                    JObject curso = (JObject)jsonArrayCursos.FirstOrDefault(x => x["Nombre"].ToString() == cursoEspecifico);

                    var notasAlumno = jsonArrayNotas.Where(x => x["Legajo Alumno"].ToString() == legajo);

                    if (notasAlumno.Any())
                    {
                        bool promedioMinimoHabilitado = curso["Condiciones"]["PromedioMinimo"].ToObject<bool>();

                        if (promedioMinimoHabilitado)
                        {
                            double promedio = notasAlumno.Average(x => Convert.ToDouble(x["Nota"]));
                            MessageBox.Show($"Promedio: {promedio}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            if (promedio > promedioMin)
                            {
                                return true;
                            }
                            else
                            {
                                MessageBox.Show($"El promedio del alumno debe ser mayor a {promedioMin} para inscribirse a este curso.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }
                        return true;
                    }
                    else
                    {
                        MessageBox.Show($"No se encontraron notas para el alumno con legajo {legajo}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("El archivo JSON de notas no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos de notas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool CondicionPorInscripcionAnterior(string legajo, string cursoActual, string cursoEspecifico)
        {
            try
            {
                string jsonFilePathCursos = "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Cursos.json";

                if (File.Exists(jsonFilePathCursos))
                {
                    string jsonDataCursos = File.ReadAllText(jsonFilePathCursos);
                    JArray jsonArrayCursos = JArray.Parse(jsonDataCursos);

                    JObject cursoactual = (JObject)jsonArrayCursos.FirstOrDefault(x => x["Nombre"].ToString() == cursoActual);
                    JObject curso = (JObject)jsonArrayCursos.FirstOrDefault(x => x["Nombre"].ToString() == cursoEspecifico);


                    if (cursoactual != null)
                    {
                        bool condicionesHabilitadas = cursoactual["Condiciones"]["InscripcionAnterior"].ToObject<bool>();

                        if (condicionesHabilitadas)
                        {
                            string idInscriptos = curso.Value<string>("ID Inscriptos");
                            string[] inscriptos = idInscriptos.Split(',');

                            if (inscriptos.Contains(legajo))
                            {
                                return true;
                            }
                            else
                            {
                                MessageBox.Show($"Debe estar inscripto en el curso '{cursoEspecifico}' anteriormente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("El archivo JSON no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return true;
        }
    }


        
}
