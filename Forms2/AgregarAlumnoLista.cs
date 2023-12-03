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
using System.Web.ModelBinding;
using System.Windows.Forms;

namespace Forms2
{
    public partial class AgregarAlumnoLista : Form
    {
        public AgregarAlumnoLista()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                AgregarAlumnoAListaEspera(textBox1.Text, textBox2.Text);
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AgregarAlumnoAListaEspera(string nombreCurso, string legajoAlumno)
        {
            try
            {
                string jsonFilePath = "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Cursos.json";
                string jsonData = File.ReadAllText(jsonFilePath);
                JArray jsonArray = JArray.Parse(jsonData);

                var cursoSeleccionado = jsonArray
                    .FirstOrDefault(curso => curso["Nombre"].ToString() == nombreCurso);

                if (cursoSeleccionado != null)
                {
                    string listaEspera = cursoSeleccionado["Lista de espera"]?.ToString();
                    List<string> legajosEnListaEspera = !string.IsNullOrEmpty(listaEspera) ? listaEspera.Split(',').ToList() : new List<string>();

                    if (!legajosEnListaEspera.Contains(legajoAlumno))
                    {
                        legajosEnListaEspera.Add(legajoAlumno);
                        cursoSeleccionado["Lista de espera"] = string.Join(",", legajosEnListaEspera);
                        File.WriteAllText(jsonFilePath, jsonArray.ToString());
                        MessageBox.Show("Alumno agregado a la lista de espera.", "Agregado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("El alumno ya está en la lista de espera para este curso.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show($"No se encontró el curso '{nombreCurso}'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar al alumno a la lista de espera: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool Validar()
        {
            bool valido = true;
            string mensaje = "";

            if (textBox1.Text == "" || textBox2.Text == "")
            {
                valido = false;
                mensaje += "- Areas incompletas\n";
            }
            else
            {
                if (!Validadores.VerificarUnicidad("Nombre", "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Cursos.json", textBox1.Text))
                {
                    valido = false;
                    mensaje += "- Curso incorrecto\n";
                }
                if (!Validadores.VerificarUnicidad("Legajo", "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Alumnos.json", textBox2.Text))
                {
                    valido = false;
                    mensaje += "- Legajo incorrecto\n";
                }
            }
            
            if (!valido)
            {
                MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return valido;
        }
    }
}
