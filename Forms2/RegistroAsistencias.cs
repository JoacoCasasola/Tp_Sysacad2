using libreriaClases;
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
    public partial class RegistroAsistencias : Form
    {
        List<string> alumnosSeleccionados = new List<string>();

        public enum EstadoAlumno
        {
            Presente,
            Ausente
        }

        public RegistroAsistencias()
        {
            InitializeComponent();
            dataGridViewAlumnos.Visible = false;
            button1.Visible = false;    
            dateTimePicker1.Visible = false;
            comboBox1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool VerificarCurso(string nombreCurso)
        {
            bool CursoVerifica = false;
            try
            {
                string jsonFilePath = "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Cursos.json";
                if (File.Exists(jsonFilePath))
                {
                    string jsonData = File.ReadAllText(jsonFilePath);

                    JArray jsonArray = JArray.Parse(jsonData);

                    JObject curso = (JObject)jsonArray.FirstOrDefault(x => x["Nombre"].ToString() == nombreCurso);

                    if (curso != null)
                    {
                        CursoVerifica = true;
                    }
                    else
                    {
                        MessageBox.Show("El Curso no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            return CursoVerifica;
        }

        private string ObtenerDatoAlumno(string dato, string idAlumno)
        {
            string texto = "";

            try
            {
                string json = File.ReadAllText("C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Alumnos.json");

                JArray jsonArray = JArray.Parse(json);

                JObject persona = jsonArray.Children<JObject>()
                    .FirstOrDefault(p => p.Value<string>("Legajo") == idAlumno);

                if (persona != null)
                {
                    string valor = persona.Value<string>(dato);

                    if (!string.IsNullOrEmpty(valor))
                    {
                        texto = valor;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al leer o buscar en el archivo JSON: " + ex.Message);
            }

            return texto;
        }

        private void MostrarEnTabla(string nombreCurso, DataGridView dataGrid)
        {
            try
            {
                string jsonFilePath = "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Cursos.json";
                if (File.Exists(jsonFilePath))
                {
                    string jsonData = File.ReadAllText(jsonFilePath);

                    JArray jsonArray = JArray.Parse(jsonData);

                    JObject curso = (JObject)jsonArray.FirstOrDefault(x => x["Nombre"].ToString() == nombreCurso);

                    if (curso != null)
                    {
                        string idInscriptos = curso.Value<string>("ID Inscriptos");
                        string[] inscriptos = idInscriptos.Split(',');

                        dataGrid.Columns.Add("Nombre", "Nombre");
                        dataGrid.Columns.Add("Apellido", "Apellido");
                        dataGrid.Columns.Add("DNI", "DNI");
                        dataGrid.Columns.Add("Legajo", "Legajo");

                        foreach (string idAlumno in inscriptos)
                        {
                            dataGrid.Rows.Add(
                                ObtenerDatoAlumno("Nombre", idAlumno),
                                ObtenerDatoAlumno("Apellido", idAlumno),
                                ObtenerDatoAlumno("DNI", idAlumno),
                                ObtenerDatoAlumno("Legajo", idAlumno)
                                );
                        }
                        DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                        checkBoxColumn.Name = "seleccionAlumno";
                        checkBoxColumn.HeaderText = "Seleccion";
                        checkBoxColumn.Width = 70;
                        dataGrid.Columns.Add(checkBoxColumn);
                    }
                }
                else
                {
                    MessageBox.Show("El archivo JSON de cursos no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void GuardarSeleccionados(DataGridView dataGrid)
        {
            alumnosSeleccionados.Clear();

            foreach (DataGridViewRow row in dataGrid.Rows)
            {
                if (row.Cells["seleccionAlumno"].Value != null && (bool)row.Cells["seleccionAlumno"].Value)
                {
                    if (row.Cells[0].Value != null)
                    {
                        alumnosSeleccionados.Add(row.Cells[3].Value.ToString());
                    }
                }
            }

        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (VerificarCurso(textBox1.Text))
            {
                dataGridViewAlumnos.Visible = true;
                panel1.Visible = false;
                textBox1.Visible = false;
                label1.Visible = false;
                btnSeleccionar.Visible = false;
                button1.Visible = true;
                dateTimePicker1.Visible = true;
                comboBox1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;

                MostrarEnTabla(textBox1.Text, dataGridViewAlumnos);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GuardarSeleccionados(dataGridViewAlumnos);

            if (alumnosSeleccionados.Count == 1)
            {
                if (ValidarAsistencia())
                {
                    List<Dictionary<string, string>> ListDictAsistencia= Administrador.AgregarListaAsistencia(textBox1.Text, alumnosSeleccionados[0], dateTimePicker1.Text, comboBox1.Text, false);
                    Archivar("Asistencias", ListDictAsistencia);
                    MessageBox.Show("Asistencia cargada con exito.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un solo alumno para cargar la nota.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Archivar(string nombreArchivo, List<Dictionary<string, string>> listaDict)
        {
            //DELEGADO
            ManejadorArchivos.GuardarListaDiccionariosDelegado guardarLista = ManejadorArchivos.GuardarListaDiccionariosJSON;
            guardarLista(listaDict, nombreArchivo, "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\");
        }

        private bool ValidarAsistencia()
        {
            bool valido = true;
            if (comboBox1.Text != EstadoAlumno.Presente.ToString() && comboBox1.Text != EstadoAlumno.Ausente.ToString())
            {
                valido = false;
                MessageBox.Show("Estado de alumno incorrecto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return valido;
        }
    }
}
