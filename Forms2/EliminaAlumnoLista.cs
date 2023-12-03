using libreriaClases;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms2
{
    public partial class EliminaAlumnoLista : Form
    {
        List<string> cursosSeleccionados = new List<string>();
        List<string> idsCursoSeleccionado = new List<string>();

        public EliminaAlumnoLista()
        {
            InitializeComponent();
            MostrarCursosConListaEspera();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void MostrarCursosConListaEspera()
        {
            try
            {
                string jsonFilePath = "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Cursos.json";
                if (File.Exists(jsonFilePath))
                {
                    string jsonData = File.ReadAllText(jsonFilePath);

                    JArray jsonArray = JArray.Parse(jsonData);

                    var cursosConListaEspera = jsonArray
                        .Where(curso => curso["Lista de espera"] != null && curso["Lista de espera"].ToString() != "")
                        .ToList();

                    if (cursosConListaEspera.Any())
                    {
                        foreach (JObject curso in cursosConListaEspera)
                        {
                            Console.WriteLine($"Curso con Lista de Espera: {curso["Nombre"]}");

                            dataGridViewCursos.Rows.Add(
                                curso["Nombre"].ToString(),
                                curso["ID"].ToString(),
                                curso["Descripcion"].ToString(),
                                curso["Cupo Actual"].ToString(),
                                curso["Cupo Max."].ToString(),
                                curso["Horario"].ToString()
                            );
                        }
                    }
                    else
                    {
                        MessageBox.Show("No hay cursos con Lista de Espera.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("El archivo JSON de alumnos no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.Name = "seleccionCurso";
            checkBoxColumn.HeaderText = "Seleccion";
            checkBoxColumn.Width = 70;
            dataGridViewCursos.Columns.Add(checkBoxColumn);
        }

        private void MostrarListaEspera(string nombreCurso)
        {
            try
            {
                List<string> legajosEnListaEspera = ObtenerListaEspera(nombreCurso);

                if (legajosEnListaEspera.Any())
                {
                    string alumnosJsonFilePath = "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Alumnos.json"; // Cambia la ruta según tu configuración
                    string alumnosJsonData = File.ReadAllText(alumnosJsonFilePath);
                    JArray alumnosJsonArray = JArray.Parse(alumnosJsonData);

                    var alumnosEnListaEspera = alumnosJsonArray
                        .Where(alumno => legajosEnListaEspera.Contains(alumno["Legajo"].ToString()))
                        .ToList();

                    if (alumnosEnListaEspera.Any())
                    {
                        dataGridViewLista.Rows.Clear();

                        foreach (JObject alumno in alumnosEnListaEspera)
                        {
                            dataGridViewLista.Rows.Add(
                                alumno["Nombre"].ToString(),
                                alumno["Apellido"].ToString(),
                                alumno["DNI"].ToString(),
                                alumno["Legajo"].ToString()
                            );
                        }
                    }
                    else
                    {
                        MessageBox.Show("No hay alumnos en la lista de espera para este curso.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show($"No hay lista de espera para {nombreCurso}.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los datos de los alumnos en lista de espera: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.Name = "seleccionAlumno";
            checkBoxColumn.HeaderText = "Seleccion";
            checkBoxColumn.Width = 70;
            dataGridViewLista.Columns.Add(checkBoxColumn);
        }

        public void GuardarSeleccion()
        {
            cursosSeleccionados.Clear();

            foreach (DataGridViewRow row in dataGridViewCursos.Rows)
            {
                if (row.Cells["seleccionCurso"].Value != null && (bool)row.Cells["seleccionCurso"].Value)
                {
                    if (row.Cells[0].Value != null)
                    {
                        cursosSeleccionados.Add(row.Cells[0].Value.ToString());
                    }
                }
            }
        }


        private List<string> ObtenerListaEspera(string nombreCurso)
        {
            try
            {
                string jsonFilePath = "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Cursos.json";
                string jsonData = File.ReadAllText(jsonFilePath);
                JArray jsonArray = JArray.Parse(jsonData);

                var cursoSeleccionado = jsonArray
                    .FirstOrDefault(curso => curso["Nombre"].ToString() == nombreCurso);

                if (cursoSeleccionado != null && cursoSeleccionado["Lista de espera"] != null)
                {
                    string listaEspera = cursoSeleccionado["Lista de espera"].ToString();
                    return listaEspera.Split(',').ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener la lista de espera: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return new List<string>();
        }

        private void EliminarAlumnoDeListaEspera(string nombreCurso, string legajoAlumno)
        {
            try
            {
                string jsonFilePath = "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Cursos.json";
                string jsonData = File.ReadAllText(jsonFilePath);
                JArray jsonArray = JArray.Parse(jsonData);

                var cursoSeleccionado = jsonArray
                    .FirstOrDefault(curso => curso["Nombre"].ToString() == nombreCurso);

                if (cursoSeleccionado != null && cursoSeleccionado["Lista de espera"] != null)
                {
                    string listaEspera = cursoSeleccionado["Lista de espera"].ToString();
                    List<string> legajosEnListaEspera = listaEspera.Split(',').ToList();

                    if (legajosEnListaEspera.Contains(legajoAlumno))
                    {
                        legajosEnListaEspera.Remove(legajoAlumno);
                        cursoSeleccionado["Lista de espera"] = string.Join(",", legajosEnListaEspera);
                        File.WriteAllText(jsonFilePath, jsonArray.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar al alumno de la lista de espera: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ObtenerAlumnoSeleccionado()
        {
            foreach (DataGridViewRow row in dataGridViewLista.Rows)
            {
                DataGridViewCheckBoxCell checkBoxCell = row.Cells["seleccionAlumno"] as DataGridViewCheckBoxCell;

                if (checkBoxCell != null && Convert.ToBoolean(checkBoxCell.Value) == true)
                {
                    return row.Cells["Legajo"].Value.ToString();
                }
            }

            return null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GuardarSeleccion();
            if (cursosSeleccionados.Count == 1)
            {
                foreach (string curso in cursosSeleccionados)
                {
                    MostrarListaEspera(curso);
                }

                dataGridViewCursos.Visible = false;
                button4.Visible = false;
            }
            else
            {
                MessageBox.Show("Seleccionar solo un curso.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nombreCurso = "";
            string alumno = ObtenerAlumnoSeleccionado();
            foreach (string curso in cursosSeleccionados)
            {
                nombreCurso = curso;
            }

            EliminarAlumnoDeListaEspera(nombreCurso, alumno);
            MessageBox.Show("Alumno eliminado de lista de espera.", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
