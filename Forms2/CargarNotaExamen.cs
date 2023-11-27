using libreriaClases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms2
{
    public partial class CargarNotaExamen : Form
    {
        public CargarNotaExamen()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                Archivar();
                MessageBox.Show("Datos cargados con exito", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            else
            {
                MessageBox.Show("Espacios obligatorios vacios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public List<Dictionary<string, string>> CrearListDicts()
        {
            List<Dictionary<string, string>> ListdictAdmin = Administrador.AgregarListaNotasExamen(textBox1.Text,numericUpDown1.Value.ToString());
            return ListdictAdmin;
        }

        private void Archivar()
        {
            //DELEGADO
            ManejadorArchivos.GuardarListaDiccionariosDelegado guardarLista = ManejadorArchivos.GuardarListaDiccionariosJSON;
            guardarLista(CrearListDicts(), "NotasExamen", "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\");
        }
    }
}
