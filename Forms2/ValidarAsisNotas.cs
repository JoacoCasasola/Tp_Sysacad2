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
    public partial class ValidarAsisNotas : Form
    {
        List<string> ListaAValidar = new List<string>();

        public ValidarAsisNotas()
        {
            InitializeComponent();
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            button4.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CargarDatosNotas();
            dataGridView1.Visible = true;
            panel1.Visible = false;
            button3.Visible = false;
            button2.Visible = false;
            button4.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CargarDatosAsist();
            dataGridView2.Visible = true;
            dataGridView1.Visible = false;
            panel1.Visible = false;
            button3.Visible = false;
            button2.Visible = false;
            button4.Visible = true;
        }

        private void CargarDatosNotas()
        {
            string jsonFilePathNotas = "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\NotasExamenes.json";
            string jsonFilePathAlumnos = "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Alumnos.json";

            if (File.Exists(jsonFilePathNotas))
            {
                string jsonDataNotas = File.ReadAllText(jsonFilePathNotas);
                string jsonDataAlumnos = File.ReadAllText(jsonFilePathAlumnos);

                var notas = JsonConvert.DeserializeObject<dynamic[]>(jsonDataNotas);
                var alumnos = JsonConvert.DeserializeObject<dynamic[]>(jsonDataAlumnos);

                var resultados = from nota in notas
                                 join alumno in alumnos on (string)nota["Legajo Alumno"] equals (string)alumno["Legajo"]
                                 select new
                                 {
                                     Curso = (string)nota["Curso"],
                                     Nota = (string)nota["Nota"],
                                     Examen = (string)nota["Examen"],
                                     NombreAlumno = $"{alumno["Nombre"]} {alumno["Apellido"]}"
                                 };

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Curso", typeof(string));
                dataTable.Columns.Add("Nota", typeof(string));
                dataTable.Columns.Add("Examen", typeof(string));
                dataTable.Columns.Add("NombreAlumno", typeof(string));

                foreach (var resultado in resultados)
                {
                    dataTable.Rows.Add(resultado.Curso, resultado.Nota, resultado.Examen, resultado.NombreAlumno);
                }

                dataGridView1.DataSource = dataTable;

                DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                checkBoxColumn.Name = "seleccionCurso";
                checkBoxColumn.HeaderText = "Seleccion";
                checkBoxColumn.Width = 70;
                dataGridView1.Columns.Add(checkBoxColumn);
            }
        }

        private void CargarDatosAsist()
        {
            string jsonFilePathAsist = "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Asistencias.json";
            string jsonFilePathAlumnos = "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Alumnos.json";

            if (File.Exists(jsonFilePathAsist))
            {
                string jsonDataAsist = File.ReadAllText(jsonFilePathAsist);
                string jsonDataAlumnos = File.ReadAllText(jsonFilePathAlumnos);


                var alumnos = JsonConvert.DeserializeObject<dynamic[]>(jsonDataAlumnos);
                var asistencias = JsonConvert.DeserializeObject<dynamic[]>(jsonDataAsist);

                var asistenciasResultados = from asistencia in asistencias
                                            join alumno in alumnos on (string)asistencia["Legajo Alumno"] equals (string)alumno["Legajo"]
                                            select new
                                            {
                                                Curso = (string)asistencia["Curso"],
                                                Fecha = (string)asistencia["Fecha de clase"],
                                                Estado = (string)asistencia["Estado"],
                                                NombreAlumno = $"{alumno["Nombre"]} {alumno["Apellido"]}"
                                            };

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Curso", typeof(string));
                dataTable.Columns.Add("Fecha de clase", typeof(string));
                dataTable.Columns.Add("Estado", typeof(string));
                dataTable.Columns.Add("NombreAlumno", typeof(string));

                foreach (var asistenciaResultado in asistenciasResultados)
                {
                    dataTable.Rows.Add(asistenciaResultado.Curso, asistenciaResultado.Fecha, asistenciaResultado.Estado, asistenciaResultado.NombreAlumno);
                }

                dataGridView2.DataSource = dataTable;

                DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                checkBoxColumn.Name = "seleccionCurso";
                checkBoxColumn.HeaderText = "Seleccion";
                checkBoxColumn.Width = 70;
                dataGridView2.Columns.Add(checkBoxColumn);
            }
        }



        private void GuardarSeleccionados(DataGridView dataGridView)
        {
            ListaAValidar.Clear();

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells["seleccionCurso"].Value != null && (bool)row.Cells["seleccionCurso"].Value)
                {
                    if (row.Cells[0].Value != null)
                    {
                        ListaAValidar.Add(row.Cells[0].Value.ToString());
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Visible)
            {
                GuardarSeleccionados(dataGridView1);
                MessageBox.Show("Nota validada con exito", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (dataGridView2.Visible)
            {
                GuardarSeleccionados(dataGridView1);
                MessageBox.Show("Asistencia validada con exito", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
