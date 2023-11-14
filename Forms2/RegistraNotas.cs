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
        public RegistraNotas()
        {
            InitializeComponent();
            dataGridView1.Visible = false;
        }

        private void MostrarEnTabla(string nombreCurso)
        {
            
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

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (VerificarCurso(textBox1.Text))
            {
                MostrarEnTabla(textBox1.Text);
                btnRegistrar.Visible = false;
                panel1.Visible = false;
                label2.Visible = false;
                textBox1.Visible = false;
                dataGridView1.Visible = true;
            }
        }
    }
}
