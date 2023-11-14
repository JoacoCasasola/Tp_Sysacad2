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
    public partial class FormRegistrarCurso : Form
    {
        public string _nombre { get; set; }
        public string _id { get; set; }
        public int _cupoMax { get; set; }
        public string _descripcion { get; set; }
        public int _cupoActual { get; set; }
        public string _horario { get; set; }

        public List<string> _ids { get; set; }

        public List<string> _listaEspera { get; set; }

        public FormRegistrarCurso()
        {
            Validadores generar = new Validadores();
            string idDefecto = generar.GenerarID(6);
            InitializeComponent();
            textBox2.Text = idDefecto;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                _nombre = textBox1.Text;
                _id = textBox2.Text;
                _descripcion = textBox4.Text;
                _cupoActual = 0;
                _ids = new List<string>();
                _listaEspera = new List<string>();

                string strCupoMax = textBox3.Text;
                if (int.TryParse(strCupoMax, out int numeroEntero))
                {
                    _cupoMax = numeroEntero;
                }
                else
                {
                    Console.WriteLine("La cadena no es un número entero válido.");
                }
                _horario = textBox5.Text;
                
                Archivar();
                DB.LimpiarTablaSql("Cursos");
                DB.GuardarJsonCursosSql();
                Close();
            }
        }


        public List<Dictionary<string, string>> CrearListDicts()
        {
            
            List<Dictionary<string, string>> ListdictAdmin = Administrador.AgregarAListaCursos(_nombre, _id, _cupoMax, _cupoActual, _descripcion, _ids, _listaEspera, _horario);
            return ListdictAdmin;
        }

        private void Archivar()
        {
            ManejadorArchivos.GuardarListaDiccionariosJSON(CrearListDicts(), "Cursos", "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\");
            MessageBox.Show($"Nuevo curso '{_nombre}' agregado correctamente.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        public bool Validar()
        {
            bool valido = true;
            string mensajeError = "";

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
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
                if(Validadores.VerificarUnicidad("Nombre", "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Cursos.json", textBox1.Text))
                {
                    valido = false;
                    mensajeError += "- Este nombre ya fue usado\n";
                }
                if (!Validadores.VerificaSoloNumero(textBox2.Text))
                {
                    valido = false;
                    mensajeError += "- El ID solo debe contener numeros\n";
                }
                if (Validadores.VerificarUnicidad("ID", "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Cursos.json",textBox2.Text))
                {
                    valido = false;
                    mensajeError += "- El ID ya existe\n";
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
                if (!Validadores.VerificarDiaHorario(textBox5.Text))
                {
                    valido = false;
                    mensajeError += "- El horario debe tener esta estructura: 'Lunes 18:30 - 22:00'\n";
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
