using libreriaClases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms2
{
    public partial class CargarNotaTClase : Form
    {
        public string legajoalumno { get; set; }
        public string nombreCurso { get; set; }
        public CargarNotaTClase()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Archivar();
            MessageBox.Show("Datos cargados con exito", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        public List<Dictionary<string, string>> CrearListDicts()
        {
            List<Dictionary<string, string>> ListdictAdmin = Administrador.AgregarListaNotasTclase(nombreCurso, legajoalumno, dateTimePicker1.Text, numericUpDown1.Value.ToString());
            return ListdictAdmin;
        }

        public void Archivar()
        {
            //DELEGADO
            ManejadorArchivos.GuardarListaDiccionariosDelegado guardarLista = ManejadorArchivos.GuardarListaDiccionariosJSON;
            guardarLista(CrearListDicts(), "NotasTrabajoClase", "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\");
        }
    }
}
