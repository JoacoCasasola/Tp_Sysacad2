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
    public partial class InfoProfesor : Form
    {
        bool infoVisible = false;
        public InfoProfesor()
        {
            InitializeComponent();
            VisibleText();
        }

        private void GetInfo(string id)
        {
            if (Validadores.VerificarUnicidad("ID", "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Profesores.json", id))
            {
                try
                {
                    string json = File.ReadAllText("C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Profesores.json");

                    JArray jsonArray = JArray.Parse(json);

                    JObject persona = jsonArray.Children<JObject>()
                        .FirstOrDefault(p => p.Value<string>("ID") == id);

                    if (persona != null)
                    {
                        label2.Text = persona.Value<string>("Nombre");
                        label5.Text = persona.Value<string>("Apellido");
                        label7.Text = persona.Value<string>("DNI");
                        label9.Text = persona.Value<string>("Correo");
                        label11.Text = persona.Value<string>("Telefono");
                        label13.Text = persona.Value<string>("Direccion");
                        label15.Text = persona.Value<string>("Nivel");
                        label17.Text = persona.Value<string>("ID");
                        label19.Text = persona.Value<string>("Clave");

                        textBox1.Visible = false;
                        label20.Visible = false;
                        button2.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al leer o buscar en el archivo JSON: " + ex.Message);
                }
                infoVisible = true;
                VisibleText();
            }
            else
            {
                MessageBox.Show($"ID no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
            }
        }
        private void VisibleText()
        {
            label1.Visible = infoVisible;
            label2.Visible = infoVisible;
            label3.Visible = infoVisible;
            label5.Visible = infoVisible;
            label6.Visible = infoVisible;
            label7.Visible = infoVisible;
            label8.Visible = infoVisible;
            label9.Visible = infoVisible;
            label10.Visible = infoVisible;
            label11.Visible = infoVisible;
            label12.Visible = infoVisible;
            label13.Visible = infoVisible;
            label14.Visible = infoVisible;
            label15.Visible = infoVisible;
            label16.Visible = infoVisible;
            label17.Visible = infoVisible;
            label18.Visible = infoVisible;
            label19.Visible = infoVisible;
            button3.Visible = infoVisible;
            linkLabel1.Visible = infoVisible;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GetInfo(textBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GetInfo(textBox1.Text);
            VisibleText();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            NuevaClaveProfesor nuevaClaveProfesor = new NuevaClaveProfesor();
            nuevaClaveProfesor.ShowDialog();
        }
    }
}
