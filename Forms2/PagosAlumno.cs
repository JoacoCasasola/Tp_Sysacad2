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
    public partial class PagosAlumno : Form
    {
        List<string> cursosSeleccionados = new List<string>();
        string tarifaDePago;

        public PagosAlumno()
        {
            InitializeComponent();
            MostrarEnTabla();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MostrarEnTabla()
        {
            dataGridView1.Rows.Add("Matricula", 15000);
            dataGridView1.Rows.Add("Cargo Administrativo", 5000);
            dataGridView1.Rows.Add("Libros/Materiales", 1200);

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

        private void button2_Click(object sender, EventArgs e)
        {
            GuardarSeleccionados();

            if (cursosSeleccionados.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione al menos un item a pagar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (string item in cursosSeleccionados)
            {
                if (AlumnoYaRealizoPago(textBox1.Text, item))
                {
                    MessageBox.Show($"Ya has abonado {item}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            string mensaje = "";
            foreach (string curso in cursosSeleccionados)
            {
                mensaje += $"\n- {curso}";
            }

            DialogResult result = MessageBox.Show($"Seguro que decea realizar los pagos de: {mensaje}", "Pago", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                decimal suma = 0;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    try
                    {
                        bool isSelected = false;
                        if (row.Cells["seleccionCurso"].Value != null && (bool)row.Cells["seleccionCurso"].Value)
                        {
                            isSelected = true;
                        }

                        if (isSelected)
                        {
                            object cellValue = row.Cells["precio"].Value;
                            Console.WriteLine($"Valor de la celda 'precio': {cellValue}");

                            if (cellValue != null && decimal.TryParse(cellValue.ToString(), out decimal valorCelda))
                            {
                                suma += valorCelda;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al procesar la fila: {ex.Message}");
                    }
                }

                tarifaDePago = suma.ToString();
                FormasDePago formasDePago = new FormasDePago();
                formasDePago.valorAPagar = tarifaDePago;
                formasDePago.ShowDialog();

                GuardarInformacionPago();
                
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (VerificarLegajo(textBox1.Text))
            {
                panel1.Visible = false;
                panel2.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
                textBox1.Visible = false;
                label2.Visible = false;
            }
        }

        private bool AlumnoYaRealizoPago(string legajoAlumno, string item)
        {
            List<Pago> pagos = ObtenerPagosDesdeArchivo();
            return pagos.Any(p => p.lagajo == legajoAlumno && p.carrito.Contains(item));
        }
    


        private void GuardarInformacionPago()
        {
            try
            {
                string carro = "";
                foreach (string producto in cursosSeleccionados)
                {
                    if (carro == "")
                    {
                        carro += $"{producto}";
                    }
                    else
                    {
                        carro += $", {producto}";
                    }
                }

                string legajoAlumno = textBox1.Text; 

                Pago nuevoPago = new Pago(legajoAlumno, DateTime.Now, tarifaDePago, carro);

                List<Pago> pagos = ObtenerPagosDesdeArchivo();

                pagos.Add(nuevoPago);

                GuardarPagosEnArchivo(pagos);

                MostrarComprobantePago(nuevoPago);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar la información del pago: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public List<Pago> ObtenerPagosDesdeArchivo()
        {
            string jsonFilePath = "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Pagos.json";

            if (File.Exists(jsonFilePath))
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                List<Pago> pagos = JsonConvert.DeserializeObject<List<Pago>>(jsonData);
                return pagos ?? new List<Pago>();
            }
            else
            {
                return new List<Pago>();
            }
        }

        private void GuardarPagosEnArchivo(List<Pago> pagos)
        {
            string jsonFilePath = "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Pagos.json";
            string jsonData = JsonConvert.SerializeObject(pagos, Formatting.Indented);
            File.WriteAllText(jsonFilePath, jsonData);
        }

        public bool VerificarLegajo(string legajo)
        {
            bool verifica = false;

            string jsonFilePath = "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Alumnos.json";
            if (File.Exists(jsonFilePath))
            {

                string jsonData = File.ReadAllText(jsonFilePath);

                JArray jsonArray = JArray.Parse(jsonData);

                JObject curso = (JObject)jsonArray.FirstOrDefault(x => x["Legajo"].ToString() == legajo);

                if (curso != null)
                {
                    verifica = true;
                }
                else
                {
                    MessageBox.Show("El legajo no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("El archivo JSON no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return verifica;
        }

        private void MostrarComprobantePago(Pago pago)
        {
            string mensajeComprobante = $"- Fecha: {pago.fecha}\n";
            mensajeComprobante += $"- Monto: ${pago.monto}\n";
            mensajeComprobante += $"- Conceptos de Pago: {pago.carrito}\n";

            MessageBox.Show($"Comprobante de Pago \n\n{mensajeComprobante}", "Comprobante de Pago", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
