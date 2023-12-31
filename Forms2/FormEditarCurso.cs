﻿using libreriaClases;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Forms2
{
    public partial class FormEditarCurso : Form
    {
        bool infoVisible = false;
        public FormEditarCurso()
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

        private void button4_Click(object sender, EventArgs e)
        {
            EncontrarCurso(textBox5.Text);
        }

        private void EncontrarCurso(string nombre)
        {
            if (Validadores.VerificarUnicidad("Nombre", "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Cursos.json", nombre))
            {
                try
                {
                    string json = File.ReadAllText("C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Cursos.json");

                    JArray jsonArray = JArray.Parse(json);

                    JObject persona = jsonArray.Children<JObject>()
                        .FirstOrDefault(p => p.Value<string>("Nombre") == nombre);

                    if (persona != null)
                    {
                        textBox1.Text = persona.Value<string>("Nombre");
                        textBox2.Text = persona.Value<string>("ID");
                        textBox3.Text = persona.Value<string>("Cupo");
                        textBox4.Text = persona.Value<string>("Descripcion");


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

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                
                string jsonFilePath = "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Cursos.json";

                string json = File.ReadAllText(jsonFilePath);
                JArray jsonArray = JArray.Parse(json);

                string nombreACambiar = textBox1.Text;
                JObject elemento = jsonArray.Children<JObject>()
                    .FirstOrDefault(p => p.Value<string>("Nombre") == nombreACambiar);

                if (elemento != null)
                {
                    elemento["Nombre"] = textBox1.Text;
                    elemento["ID"] = textBox2.Text;
                    elemento["Cupo"] = textBox3.Text;
                    elemento["Descripcion"] = textBox4.Text;

                    File.WriteAllText(jsonFilePath, jsonArray.ToString());
                    MessageBox.Show("Nueva informacion cargada con exito", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    Console.WriteLine("Elemento no encontrado");
                }
            }
        }

        public bool Validar()
        {
            bool valido = true;
            string mensajeError = "";

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
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
            }
            if (!valido)
            {
                MessageBox.Show(mensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return valido;

        }
    }
}
