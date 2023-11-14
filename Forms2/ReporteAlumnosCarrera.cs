using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml;
using libreriaClases;
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
using Newtonsoft.Json.Linq;

namespace Forms2
{
    public partial class ReporteAlumnosCarrera : Form
    {
        public ReporteAlumnosCarrera()
        {
            InitializeComponent();
            btnGenerarPdf.Visible = false;
            dataGridView1.Visible = false;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool CursoEncontrado = Validadores.VerificarUnicidad("Carrera", "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Alumnos.json", textBox2.Text);

            bool idEncontrado = Validadores.VerificarUnicidad("ID", "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Administradores.json", textBox1.Text);

            if (idEncontrado)
            {
                if (CursoEncontrado)
                {
                    btnGenerarPdf.Visible = true;
                    dataGridView1.Visible = true;
                    panel1.Visible = false;
                    label1.Visible = false;
                    label2.Visible = false;
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    button1.Visible = false;

                    MostrarEnTabla(textBox2.Text);
                }
                else
                {
                    MessageBox.Show("El Curso es incorrecto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("El ID es incorrecto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnGenerarPdf_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardarArchivo = new SaveFileDialog();
            guardarArchivo.FileName = DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf";

            string html = Properties.Resources.plantilla.ToString();
            html = html.Replace("@Admin", GetNombreAdmin(textBox1.Text, "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Administradores.json"));
            html = html.Replace("@ID", textBox1.Text);
            html = html.Replace("@Fecha", DateTime.Now.ToString("dd/MM/yyyy"));
            html = html.Replace("Curso:", "Carrera:");
            html = html.Replace("@Curso", textBox2.Text);

            string filas = string.Empty;
            int indice = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                indice++;
                filas += "<tr>";
                filas += "<td>" + indice.ToString() + "</td>";
                filas += "<td>" + row.Cells["Nombre"].Value + "</td>";
                filas += "<td>" + row.Cells["Apellido"].Value + "</td>";
                filas += "<td>" + row.Cells["DNI"].Value + "</td>";
                filas += "<td>" + row.Cells["Legajo"].Value + "</td>";
                filas += "</tr>";
            }
            html = html.Replace("@Filas", filas);


            if (guardarArchivo.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(guardarArchivo.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);

                    PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, stream);

                    pdfDoc.Open();

                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Properties.Resources.Logo_UTN_Avellaneda_1, System.Drawing.Imaging.ImageFormat.Png);
                    img.ScaleToFit(80, 60);
                    img.Alignment = iTextSharp.text.Image.UNDERLYING;
                    img.SetAbsolutePosition(pdfDoc.LeftMargin, pdfDoc.Top - 40);
                    pdfDoc.Add(img);

                    pdfDoc.Add(new Paragraph(""));

                    using (StringReader stringReader = new StringReader(html))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(pdfWriter, pdfDoc, stringReader);
                    }

                    pdfDoc.Close();

                    stream.Close();
                }
            }
        }

        private void MostrarEnTabla(string carrera)
        {
            try
            {
                string jsonFilePath = "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Alumnos.json";
                if (File.Exists(jsonFilePath))
                {
                    string jsonData = File.ReadAllText(jsonFilePath);

                    JArray jsonArray = JArray.Parse(jsonData);

                    var alumnosEnCarrera = jsonArray
                        .Where(alumno => alumno.Value<string>("Carrera") == carrera)
                        .ToList();

                    if (alumnosEnCarrera.Count > 0)
                    {
                        dataGridView1.Columns.Add("Nombre", "Nombre");
                        dataGridView1.Columns.Add("Apellido", "Apellido");
                        dataGridView1.Columns.Add("DNI", "DNI");
                        dataGridView1.Columns.Add("Legajo", "Legajo");

                        foreach (JObject alumno in alumnosEnCarrera)
                        {
                            dataGridView1.Rows.Add(
                                alumno.Value<string>("Nombre"),
                                alumno.Value<string>("Apellido"),
                                alumno.Value<string>("DNI"),
                                alumno.Value<string>("Legajo")
                            );
                        }
                    }
                    else
                    {
                        MessageBox.Show("No hay alumnos en la carrera especificada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("El archivo JSON de alumnos no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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


        public string GetNombreAdmin(string id, string pathJson)
        {
            string nombre = "";
            try
            {
                string json = File.ReadAllText(pathJson);

                JArray jsonArray = JArray.Parse(json);

                JObject persona = jsonArray.Children<JObject>()
                    .FirstOrDefault(p => p.Value<string>("ID") == id);

                if (persona != null)
                {
                    nombre += persona.Value<string>("Nombre");
                    nombre += " ";
                    nombre += persona.Value<string>("Apellido");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al leer o buscar en el archivo JSON: " + ex.Message);
            }
            return nombre;
        }
    }
}
