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
    public partial class LoginProfesor : Form
    {
        public string id { get; set; }
        public string clave { get; set; }

        PanelCarga panelCarga;
        public LoginProfesor()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
            id = textBox1.Text;
            clave = textBox2.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormRegistrarProfesor registrarProfesor = new FormRegistrarProfesor();
            registrarProfesor.ShowDialog();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Espacios obligatorios incompletos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (ValidarIdAdmin() && ValidarClaveAdmin())
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
                    MessageBox.Show("Clave o ID incorrecto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            MessageBox.Show($"{GetNombreProfesor(textBox1.Text, "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Profesores.json")}, bienvenido/a al panel de Profesores!", "SysAcad", MessageBoxButtons.OK, MessageBoxIcon.Information);
            PanelAlumno panelAlumno = new PanelAlumno();
            panelAlumno.Show();
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


        public bool ValidarIdAdmin()
        {
            string claveBuscada = textBox1.Text;
            string ruta = "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Profesores.json";
            bool claveExiste = Validadores.VerificarUnicidad("ID", ruta, claveBuscada);

            return claveExiste;
        }
        private bool ValidarClaveAdmin()
        {
            string claveBuscada = textBox2.Text;
            string ruta = "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Profesores.json";
            bool claveExiste = Validadores.VerificarUnicidad("Clave", ruta, claveBuscada);

            return claveExiste;
        }

        private string GetNombreProfesor(string id, string pathJson)
        {
            string nombre = "";
            try
            {
                string json = File.ReadAllText(pathJson);

                JArray jsonArray = JArray.Parse(json);

                JObject persona = jsonArray.Children<JObject>()
                    .FirstOrDefault(p => p.Value<string>("ID") == id);

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
