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
    public partial class ConsultaDeHorarios : Form
    {
        public ConsultaDeHorarios()
        {
            InitializeComponent();
        }

        private void MostrarEnTabla(string legajo)
        {
            
            try
            {
                string jsonFilePath = "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Cursos.json";
                if (File.Exists(jsonFilePath))
                {
                    string jsonData = File.ReadAllText(jsonFilePath);

                    JArray jsonArray = JArray.Parse(jsonData);

                    bool encontrado = false; 

                    foreach (JObject curso in jsonArray)
                    {
                        string idInscriptos = curso.Value<string>("ID Inscriptos");
                        string[] inscriptos = idInscriptos.Split(',');

                        if (inscriptos.Contains(legajo))
                        {
                            dataGridView1.Rows.Add(
                                curso["Nombre"].ToString(),
                                curso["Descripcion"].ToString(),
                                curso["Horario"].ToString()
                            );
                            
                            encontrado = true;
                        }
                    }

                    if (!encontrado)
                    {
                        MessageBox.Show("No se encontraron cursos para el legajo especificado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InscripcionACurso curso = new InscripcionACurso();
            if (curso.VerificarLegajo(textBox1.Text))
            {
                panel1.Visible = false;
                panel2.Visible = false;
                button3.Visible = false;
                textBox1.Visible = false;
                label2.Visible = false;

                try
                {
                    string json = File.ReadAllText("C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Alumnos.json");

                    JArray jsonArray = JArray.Parse(json);

                    JObject persona = jsonArray.Children<JObject>()
                        .FirstOrDefault(p => p.Value<string>("Legajo") == textBox1.Text);

                    if (persona != null)
                    {
                        string nombre = persona.Value<string>("Nombre");
                        label3.Text = $"Hola {nombre}, estos son los Horarios de tus cursos";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al leer o buscar en el archivo JSON: " + ex.Message);
                }

                MostrarEnTabla(textBox1.Text);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
