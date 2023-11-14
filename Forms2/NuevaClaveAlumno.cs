using Generic;
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
    public partial class NuevaClaveAlumno : Form
    {

        public NuevaClaveAlumno()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (verificacion())
            {
                Close();
                CambiarClave(textBox1.Text, textBox2.Text);
                DB.LimpiarTablaSql("Alumnos");
                DB.GuardarJsonAlumnosSql();
            }
        }

        private bool verificacion()
        {
            bool verifica = true;

            if (textBox2.Text != textBox3.Text)
            {
                verifica = false;
                MessageBox.Show($"Error de verificasion", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                verifica = false;
                MessageBox.Show($"Espacios obligatorios vacios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return verifica;
        }

        private void CambiarClave(string claveActual, string newClave)
        {
            try
            {
                string json = File.ReadAllText("C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Alumnos.json");

                JArray jsonArray = JArray.Parse(json);

                JObject persona = jsonArray.Children<JObject>()
                    .FirstOrDefault(p => p.Value<string>("Clave") == claveActual);

                if (persona != null)
                {
                    persona["Clave"] = newClave;
                    File.WriteAllText("C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Alumnos.json", jsonArray.ToString());
                    MessageBox.Show($"Nueva clave guardada con exito.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show($"Error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
