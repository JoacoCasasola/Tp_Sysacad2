﻿using libreriaClases;
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
    public partial class RegistraNotas : Form
    {
        List<string> alumnosSeleccionados = new List<string>();

        public RegistraNotas()
        {
            InitializeComponent();
            btnExamenes.Visible = false;
            btnTps.Visible = false;
            btnTClase.Visible = false;
            btnSeleccionar.Visible = false;
            label1.Visible = false;
            dataGridViewExam.Visible = false;
            dataGridViewTp.Visible = false;
            dataGridViewTclase.Visible = false;
            textBox2.Visible = false;
            dateTimePicker1.Visible = false;
            numericUpDown1.Visible = false;
            label3.Visible = false;
            label5.Visible = false;
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


        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (VerificarCurso(textBox1.Text))
            {
                btnRegistrar.Visible = false;
                panel1.Visible = false;
                label2.Visible = false;
                textBox1.Visible = false;
                btnExamenes.Visible = true;
                btnTps.Visible = true;
                btnTClase.Visible = true;
                label1.Text = $"Elija tipo de trabajo del curso '{textBox1.Text}'";
                label1.Visible = true;
            }
        }

        private void btnExamenes_Click(object sender, EventArgs e)
        {
            dataGridViewExam.Visible = true;
            btnSeleccionar.Visible = true;
            btnExamenes.Visible = false;
            btnTps.Visible = false;
            btnTClase.Visible = false;
            label1.Visible = false;
            label3.Visible = true;
            label5.Visible = true;
            textBox2.Visible = true;
            numericUpDown1.Visible = true;

            label5.Text = "Examen";
            
            MostrarEnTabla(textBox1.Text, dataGridViewExam);
        }

        private void btnTClase_Click(object sender, EventArgs e)
        {
            dataGridViewTclase.Visible = true;
            btnSeleccionar.Visible = true;
            btnExamenes.Visible = false;
            btnTps.Visible = false;
            btnTClase.Visible = false;
            label1.Visible = false;
            label3.Visible = true;
            label5.Visible = true;
            dateTimePicker1.Visible = true;
            numericUpDown1.Visible = true;

            label5.Text = "Clase";

            MostrarEnTabla(textBox1.Text, dataGridViewTclase);

        }

        private void btnTps_Click(object sender, EventArgs e)
        {
            dataGridViewTp.Visible = true;
            btnSeleccionar.Visible = true;
            btnExamenes.Visible = false;
            btnTps.Visible = false;
            btnTClase.Visible = false;
            label1.Visible = false;
            label3.Visible = true;
            label5.Visible = true;
            textBox2.Visible = true;
            numericUpDown1.Visible = true;

            label5.Text = "Trabajo practico";

            MostrarEnTabla(textBox1.Text, dataGridViewTp);
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (dataGridViewTp.Visible)
            {
                GuardarSeleccionados(dataGridViewTp);
                if (alumnosSeleccionados.Count == 1)
                {
                    if (ValidarNota())
                    {
                        List<Dictionary<string, string>> ListDictTp = Administrador.AgregarListaNotasTp(textBox1.Text, alumnosSeleccionados[0], textBox2.Text, numericUpDown1.Value.ToString(), false);
                        Archivar("NotasTps", ListDictTp);
                        MessageBox.Show("Nota cargada con exito.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione un solo alumno para cargar la nota.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (dataGridViewExam.Visible)
            {
                GuardarSeleccionados(dataGridViewExam);
                if (alumnosSeleccionados.Count == 1)
                {
                    if(ValidarNota())
                    {
                        List<Dictionary<string, string>> ListDictExam = Administrador.AgregarListaNotasExamen(textBox1.Text, alumnosSeleccionados[0], textBox2.Text, numericUpDown1.Value.ToString(), false);
                        Archivar("NotasExamenes", ListDictExam);
                        MessageBox.Show("Nota cargada con exito.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione un solo alumno para cargar la nota.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (dataGridViewTclase.Visible)
            {
                GuardarSeleccionados(dataGridViewTclase);
                if (alumnosSeleccionados.Count == 1)
                {
                    List<Dictionary<string, string>> ListDictTclase = Administrador.AgregarListaNotasTclase(textBox1.Text, alumnosSeleccionados[0], dateTimePicker1.Text, numericUpDown1.Value.ToString(), false);
                    Archivar("NotasTrabajoClase", ListDictTclase);
                    MessageBox.Show("Nota cargada con exito.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    
                }
                else
                {
                    MessageBox.Show("Seleccione un solo alumno para cargar la nota.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Archivar(string nombreArchivo, List<Dictionary<string, string>> listaDict)
        {
            //DELEGADO
            ManejadorArchivos.GuardarListaDiccionariosDelegado guardarLista = ManejadorArchivos.GuardarListaDiccionariosJSON;
            guardarLista(listaDict, nombreArchivo, "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\");
        }

        private bool ValidarNota()
        {
            bool valido = true;
            if(textBox2.Text == "") 
            {
                valido = false;
                MessageBox.Show("Areas obligatorias incompletas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return valido;
        }
    }
}
