using libreriaClases;
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
    public partial class InfoCursos : Form
    {
        public InfoCursos()
        {
            InitializeComponent();
            MostrarEnTabla();
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormRegistrarCurso formRegistrarCurso = new FormRegistrarCurso();
            formRegistrarCurso.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            MostrarEnTabla();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EditarCurso formEditarCurso = new EditarCurso();
            formEditarCurso.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormEliminarCurso formEliminarCurso = new FormEliminarCurso();
            formEliminarCurso.ShowDialog();
        }
    }
}
