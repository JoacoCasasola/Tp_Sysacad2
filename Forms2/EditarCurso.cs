using Generic;
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
    public partial class EditarCurso : Form
    {
        string jsonFilePath = "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Cursos.json";
        bool infoVisible = false;

        public EditarCurso()
        {
            InitializeComponent();
            Visivilidad();
        }

        private void Visivilidad()
        {
            label1.Visible = infoVisible;
            label2.Visible = infoVisible;
            label3.Visible = infoVisible;
            label5.Visible = infoVisible;

            textBox1.Visible = infoVisible;
            textBox2.Visible = infoVisible;
            textBox3.Visible = infoVisible;
            textBox4.Visible = infoVisible;

            button2.Visible = infoVisible;
            button3.Visible = infoVisible;
        }

       
        

        public void EncontrarCurso(string nombre)
        {
            if (Validadores.VerificarUnicidad("Nombre", jsonFilePath, nombre))
            {
                try
                {
                    string json = File.ReadAllText(jsonFilePath);

                    JArray jsonArray = JArray.Parse(json);

                    JObject persona = jsonArray.Children<JObject>()
                        .FirstOrDefault(p => p.Value<string>("Nombre") == nombre);

                    if (persona != null)
                    {
                        textBox1.Text = persona.Value<string>("Nombre");
                        textBox2.Text = persona.Value<string>("ID");
                        textBox3.Text = persona.Value<string>("Cupo Max.");
                        textBox4.Text = persona.Value<string>("Descripcion");
                        textBox6.Text = persona.Value<string>("Horario");


                        infoVisible = true;
                        label6.Visible = false;
                        button4.Visible = false;
                        panel1.Visible = false;
                        Visivilidad();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al leer o buscar en el archivo JSON: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show($"Nombre del Curso no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        public bool Validar()
        {
            bool valido = true;
            string mensajeError = "";

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox6.Text == "")
            {
                valido = false;
                mensajeError += "Espacios obligatprios incompletos.\n";
            }
            else
            {
                if (!Validadores.VerificaSoloAlfanumerico(textBox1.Text))
                {
                    valido = false;
                    mensajeError += "- El nombre solo debe contener caracteres alfanumericos\n";
                }
                if (!Validadores.VerificaSoloNumero(textBox2.Text))
                {
                    valido = false;
                    mensajeError += "- El ID solo debe contener numeros\n";
                }
                if (!Validadores.VerificaSoloNumero(textBox3.Text))
                {
                    valido = false;
                    mensajeError += "- El cupo solo debe contener numeros\n";
                }
                try
                {
                    int numeroEntero = int.Parse(textBox3.Text);
                    if (numeroEntero < 0)
                    {
                        valido = false;
                        mensajeError += "- El cupo no puede ser menor a 0\n";
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("La cadena no es un número entero válido.");
                }
                if (!Validadores.VerificarDiaHorario(textBox6.Text))
                {
                    valido = false;
                    mensajeError += "- El horario debe tener esta estructura: 'Lunes 18:30 - 22:00'\n"; ;
                }
            }
            if (!valido)
            {
                MessageBox.Show(mensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return valido;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            EncontrarCurso(textBox5.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                string json = File.ReadAllText(jsonFilePath);

                JArray jsonArray = JArray.Parse(json);

                string nombreACambiar = textBox5.Text;

                JObject elemento = jsonArray.Children<JObject>()
                    .FirstOrDefault(p => p.Value<string>("Nombre") == nombreACambiar);

                if (elemento != null)
                {
                    elemento["Nombre"] = textBox1.Text;
                    elemento["ID"] = textBox2.Text;
                    elemento["Cupo Max."] = textBox3.Text;
                    elemento["Descripcion"] = textBox4.Text;

                    File.WriteAllText(jsonFilePath, jsonArray.ToString());
                    MessageBox.Show("Datos actualizados correctamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DB.LimpiarTablaSql("Cursos");
                    DB.GuardarJsonCursosSql();
                    Close();
                }
                else
                {
                    MessageBox.Show("Error al cargar datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.textBox1.Clear();
            this.textBox2.Clear();
            this.textBox3.Clear();
            this.textBox4.Clear();
        }
    }
}
