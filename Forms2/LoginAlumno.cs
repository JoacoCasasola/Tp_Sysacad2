using Forms2;
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

namespace libreriaForms
{
    public partial class LoginAlumno : Form
    {
        public string legajo { get; set; }
        public string clave { get; set; }

        PanelCarga panelCarga;
        public LoginAlumno()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
            legajo = textBox1.Text;
            clave = textBox2.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormRegistrarAlumno registrarAlumno = new FormRegistrarAlumno();
            registrarAlumno.ShowDialog();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Espacios obligatorios incompletos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (ValidarUsuario("Legajo", textBox1.Text) && ValidarUsuario("Clave", textBox2.Text))
                {
                    Hide();
                    Task tarea = new Task(Espera);
                    tarea.Start();
                    await Task.Delay(2000);
                    Cerrar();
                    MostrarPanelAlumno();
                   
                }
                else
                {
                    MessageBox.Show("Clave o Legajo incorrecto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Clear();
                    textBox2.Clear();
                }
            }
        }
        public void Espera()
        {
            MostrarPanelEspera();
        }

        public void MostrarPanelAlumno()
        {
            PanelAlumno panelAlumno = new PanelAlumno();
            panelAlumno.Show();
            MessageBox.Show($"{GetNombreAlumno(textBox1.Text, "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Alumnos.json")}, bienvenido/a al panel de alumno!", "SysAcad", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void MostrarPanelEspera()
        {
            panelCarga = new PanelCarga();
            panelCarga.ShowDialog();
        }

        public void Cerrar()
        {
            if (panelCarga != null)
            {
                panelCarga.Close();
            }
        }

        private bool ValidarUsuario(string key, string textBox)
        {
            string ruta = "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Alumnos.json";
            bool claveExiste = Validadores.VerificarUnicidad(key, ruta, textBox);

            return claveExiste;
        }

        private string GetNombreAlumno(string legajo, string pathJson)
        {
            string nombre = "";
            try
            {
                string json = File.ReadAllText(pathJson);

                JArray jsonArray = JArray.Parse(json);

                JObject persona = jsonArray.Children<JObject>()
                    .FirstOrDefault(p => p.Value<string>("Legajo") == legajo);

                if (persona != null)
                {
                    nombre = persona.Value<string>("Nombre");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al leer o buscar en el archivo JSON: " + ex.Message);
            }
            return nombre;
        }
    }
}
