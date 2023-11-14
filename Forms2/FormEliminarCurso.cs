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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Forms2
{
    public partial class FormEliminarCurso : Form
    {
        public FormEliminarCurso()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show($"Esta seguro que desea eliminar el curso '{textBox1.Text}'?", "Atencion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(result == DialogResult.Yes)
            {
                if (Validadores.VerificarUnicidad("Nombre", "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Cursos.json", textBox1.Text))
                {
                    try
                    {
                        string json = File.ReadAllText("C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Cursos.json");

                        JArray jsonArray = JArray.Parse(json);

                        JObject CursoAEliminar = jsonArray.Children<JObject>()
                            .FirstOrDefault(p => p.Value<string>("Nombre") == textBox1.Text);

                        if (CursoAEliminar != null)
                        {
                            jsonArray.Remove(CursoAEliminar);

                            File.WriteAllText("C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Cursos.json", jsonArray.ToString());
                            MessageBox.Show("Curso eliminado con exito.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DB.LimpiarTablaSql("Cursos");
                            DB.GuardarJsonCursosSql();
                            Close();
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
        }
    }
}
