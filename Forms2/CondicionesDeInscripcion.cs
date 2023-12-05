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
    public partial class CondicionesDeInscripcion : Form
    {
        List<string> cursosSeleccionados = new List<string>();

        public CondicionesDeInscripcion()
        {
            InitializeComponent();
            MostrarEnTabla();
            label3.Visible = false;
            numericUpDown1.Visible = false;
            label1.Visible = false;
            textBox1.Visible = false;
        }

        private void MostrarEnTabla()
        {
            try
            {
                string jsonFilePath = "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Cursos.json";
                if (File.Exists(jsonFilePath))
                {
                    string jsonData = File.ReadAllText(jsonFilePath);

                    JArray jsonArray = JArray.Parse(jsonData);

                    foreach (JObject curso in jsonArray)
                    {
                        dataGridView1.Rows.Add(
                            curso["Nombre"].ToString(),
                            curso["ID"].ToString(),
                            curso["Descripcion"].ToString(),
                            curso["Cupo Actual"].ToString(),
                            curso["Cupo Max."].ToString(),
                            curso["Horario"].ToString()
                        );
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
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.Name = "seleccionCurso";
            checkBoxColumn.HeaderText = "Seleccion";
            checkBoxColumn.Width = 70;
            dataGridView1.Columns.Add(checkBoxColumn);
        }

        private void GuardarSeleccionados()
        {
            cursosSeleccionados.Clear();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["seleccionCurso"].Value != null && (bool)row.Cells["seleccionCurso"].Value)
                {
                    if (row.Cells[0].Value != null)
                    {
                        cursosSeleccionados.Add(row.Cells[0].Value.ToString());
                    }
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void PermitirCondiciones(bool promedioMinimo, bool inscripcionAnterior)
        {
            try
            {
                string jsonFilePath = "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Cursos.json";
                if (File.Exists(jsonFilePath))
                {
                    string jsonData = File.ReadAllText(jsonFilePath);

                    JArray jsonArray = JArray.Parse(jsonData);

                    foreach (string nombreCurso in cursosSeleccionados)
                    {
                        JObject curso = (JObject)jsonArray.FirstOrDefault(x => x["Nombre"].ToString() == nombreCurso);

                        if (curso != null)
                        {
                            if (promedioMinimo)
                            {
                                curso["Condiciones"]["PromedioMinimo"] = true;
                                curso["Condiciones"]["Promedio"] = (int)numericUpDown1.Value;
                            }
                            else
                            {
                                curso["Condiciones"]["PromedioMinimo"] = false;
                                curso["Condiciones"]["Promedio"] = 0;
                            }

                            if (inscripcionAnterior)
                            {
                                curso["Condiciones"]["InscripcionAnterior"] = true;
                                curso["Condiciones"]["Curso"] = textBox1.Text;
                            }
                            else
                            {
                                curso["Condiciones"]["InscripcionAnterior"] = false;
                                curso["Condiciones"]["Curso"] = "";
                            }
                        }
                    }

                    File.WriteAllText(jsonFilePath, jsonArray.ToString());
                    MessageBox.Show("Condiciones editadas con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            label3.Visible = checkBox1.Checked;
            numericUpDown1.Visible = checkBox1.Checked;

            label1.Visible = checkBox2.Checked;
            textBox1.Visible = checkBox2.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            label3.Visible = checkBox1.Checked;
            numericUpDown1.Visible = checkBox1.Checked;

            label1.Visible = checkBox2.Checked;
            textBox1.Visible = checkBox2.Checked;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            GuardarSeleccionados();
            if (cursosSeleccionados.Count == 0 || cursosSeleccionados.Count > 1)
            {
                MessageBox.Show("Por favor, seleccione exactamente un curso.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            bool promedioMinimo = checkBox1.Checked;
            bool inscripcionAnterior = checkBox2.Checked;

            if (inscripcionAnterior)
            {
                if (!Validadores.VerificarUnicidad("Nombre", "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Cursos.json", textBox1.Text))
                {
                    MessageBox.Show("El curso no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            PermitirCondiciones(promedioMinimo, inscripcionAnterior);

        }
    }
}
