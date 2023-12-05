using Generic;
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
    public partial class FormRegistrarAdmin : Form
    {
        public string _nombre { get; set; }
        public string _apellido { get; set; }
        public string _dni { get; set; }
        public string _correo { get; set; }
        public string _telefono { get; set; } 
        public string _direccion { get; set; }
        public string _idAdmin { get; set; }
        public string _clave { get; set; }
        public string _nivel { get; set; }

        public FormRegistrarAdmin()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                Validadores generar = new Validadores();
                _idAdmin = generar.GenerarID(6);
                _clave = generar.GenerarClave();

                _nombre = textBox1.Text;
                _apellido = textBox2.Text;
                _dni = textBox3.Text;
                _correo = textBox4.Text;
                _telefono = textBox5.Text;
                _direccion = textBox6.Text;
                _nivel = comboBox1.Text;

                Archivar();
                DB.LimpiarTablaSql("Administradores");
                DB.GuardarJsonAdminsSql();
                Close();
            }
        }


        public List<Dictionary<string, string>> CrearListDicts()
        {
            List<Dictionary<string, string>> ListdictAdmin = Administrador.AgregarAListaAdmin(_nombre, _apellido, _dni, _correo,_telefono,_direccion,_nivel, _idAdmin, _clave); 
            return ListdictAdmin;
        }

        private void Archivar()
        {
            //DELEGADO
            ManejadorArchivos.GuardarListaDiccionariosDelegado guardarLista = ManejadorArchivos.GuardarListaDiccionariosJSON;
            guardarLista(CrearListDicts(),"Administradores", "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\");
            MessageBox.Show($"Datos cargados, Ahora {_nombre} es administrador\n           ID: {_idAdmin} - Clave: {_clave}", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();

            EnvioEmail envioEmail = new EnvioEmail();
            envioEmail.EnviarCorreo(_correo, "Prueba envio de email", $"Hola {_nombre}!\n Bienvenido a UTN Avellaneda, ahora eres administrador!");
            MessageBox.Show("Email de confirmacion enviado.", "Email", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void button3_Click(object sender, EventArgs e)
        {
            this.textBox1.Clear();
            this.textBox2.Clear();
            this.textBox3.Clear();
            this.textBox4.Clear();
            this.textBox5.Clear();
            this.textBox6.Clear();
            this.comboBox1.Text = "";
        }

        private bool Validar()
        {
            bool valido = true;
            string mensajeError = "";

            if(textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || comboBox1.Text == " ") 
            {
                valido = false;
                mensajeError += "Espacios obligatprios incompletos.\n";
            }
            else
            {
                if (!Validadores.VerificaSoloLetra(textBox1.Text))
                {
                    valido = false;
                    mensajeError += "- Su nombre solo debe contener letras\n";
                }
                if (!Validadores.VerificaSoloLetra(textBox2.Text))
                {
                    valido = false;
                    mensajeError += "- Su apellido solo debe contener letras\n";
                }
                if (!Validadores.VerificaSoloNumero(textBox3.Text))
                {
                    valido = false;
                    mensajeError += "- Su DNI solo debe contener numeros\n";
                }
                if (Validadores.VerificarUnicidad("DNI","C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Administradores.json", textBox3.Text))
                {
                    valido = false;
                    mensajeError += "- Su DNI no es valido (se encontro usuario con el mismo)\n";
                }

                //DELEGADO
                Validadores.ValidarDelegado validar = Validadores.VerificarCorreoElectronico;
                if (!validar(textBox4.Text))
                {
                    valido = false;
                    mensajeError += "- Su correo es incorrecto\n";
                }

                if(Validadores.VerificarUnicidad("Correo", "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Administradores.json", textBox4.Text))
                {
                    valido = false;
                    mensajeError += "- Su correo no es valido (se encontro usuario con el mismo)\n";
                }
                if (!Validadores.VerificaSoloNumero(textBox5.Text))
                {
                    valido = false;
                    mensajeError += "- Su telefono solo debe contener numeros\n";
                }
                if (!Validadores.VerificaSoloAlfanumerico(textBox6.Text))
                {
                    valido = false;
                    mensajeError += "- Su direccion es incorrecta\n";
                }
                if (comboBox1.Text != "Avanzado" && comboBox1.Text != "Intermedio" && comboBox1.Text != "Basico")
                {
                    valido = false;
                    mensajeError += "- Su nivel es incorrecto\n";
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
