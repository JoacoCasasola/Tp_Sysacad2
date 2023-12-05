using Generic;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Forms2
{
    public partial class InscripcionACurso : Form
    {
        List<string> cursosSeleccionados = new List<string>();

        public InscripcionACurso()
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
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.Name = "seleccionCurso";
            checkBoxColumn.HeaderText = "Seleccion";
            checkBoxColumn.Width = 70;
            dataGridView1.Columns.Add(checkBoxColumn);
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GuardarSeleccionados();

            foreach (string nombreCurso in cursosSeleccionados)
            {
                if (ComprobarCondicionPromedio(nombreCurso, textBox1.Text))
                {
                    VerificarCupo(nombreCurso);
                }
            }
        }


        private bool ComprobarCondicionPromedio(string nombreCurso, string legajoAlumno)
        {
            string jsonCursos = "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Cursos.json";
            string jsonAlumnos = "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Alumnos.json";

            if (File.Exists(jsonCursos))
            {
                string jsonDataCursos = File.ReadAllText(jsonCursos);
                string jsonDataAlumnos = File.ReadAllText(jsonAlumnos);

                JArray jsonArrayCursos = JArray.Parse(jsonDataCursos);
                JArray jsonArrayAlumnos = JArray.Parse(jsonDataAlumnos);

                JObject curso = (JObject)jsonArrayCursos.FirstOrDefault(x => x["Nombre"].ToString() == nombreCurso);
                JObject alumno = (JObject)jsonArrayAlumnos.FirstOrDefault(x => x["Legajo"].ToString() == legajoAlumno);

                if (curso != null)
                {
                    JToken condicionesToken = curso["Condiciones"];
                    if (condicionesToken != null && condicionesToken.HasValues)
                    {
                        bool condicion = (bool)condicionesToken["PromedioMinimo"];
                        int promedio = (int)condicionesToken["Promedio"];

                        if (alumno != null)
                        {
                            var notasAlumno = jsonArrayAlumnos
                                .Where(x =>
                                    x["Curso"] != null &&
                                    x["Legajo Alumno"] != null &&
                                    x["Curso"].ToString() == nombreCurso &&
                                    x["Legajo Alumno"].ToString() == legajoAlumno)
                                .Select(x => Convert.ToInt32(x["Nota"].ToString()));

                            double promedioAlumno = notasAlumno.Any() ? notasAlumno.Average() : 0;

                            if (condicion && promedioAlumno >= promedio)
                            {
                                return true;
                            }
                            else
                            {
                                MessageBox.Show($"No cumples con los requisitos de promedio minimo de {promedio}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }

                return false;
            }

            return false;
        }


        //private bool ComprobarCondicionInscripcion(string nombreCurso,string cursoEspecifico ,string legajoAlumno)
        //{
        //    try
        //    {
        //        string jsonAlumnos = "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Alumnos.json";
        //        string jsonCursos = "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Cursos.json";

        //        if (File.Exists(jsonAlumnos))
        //        {
        //            string jsonDataCursos = File.ReadAllText(jsonCursos);
        //            string jsonDataAlumnos = File.ReadAllText(jsonAlumnos);

        //            JArray jsonArrayCursos = JArray.Parse(jsonDataCursos);
        //            JArray jsonArrayAlumnos = JArray.Parse(jsonDataAlumnos);

        //            JObject curso = (JObject)jsonArrayCursos.FirstOrDefault(x => x["Nombre"].ToString() == nombreCurso);
        //            JObject alumno = (JObject)jsonArrayAlumnos.FirstOrDefault(x => x["Legajo"].ToString() == legajoAlumno);

        //            JToken condicionesToken = alumno["Condiciones"];
        //            if (condicionesToken != null && condicionesToken.HasValues)
        //            {
        //                bool condicion = (bool)condicionesToken["InscripcionAnterior"];
        //                string promedio = (string)condicionesToken["Curso"];

        //                bool inscripcionExistente = jsonArrayAlumnos.Any(x =>
        //                x["Curso"].ToString() == cursoEspecifico &&
        //                x["Legajo"].ToString() == legajoAlumno);

        //                if (inscripcionExistente && nombreCurso != cursoEspecifico)
        //                {
        //                    return true;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error al cargar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //    return false;
        //}




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

       

        private void VerificarCupo(string nombreCurso)
        {
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
                        int cupoDisponible = Convert.ToInt32(curso["Cupo Actual"]);
                        int cupoMax = Convert.ToInt32(curso["Cupo Max."]);

                        if (cupoDisponible < cupoMax)
                        {
                            AgregarLegajoAlCurso(nombreCurso);
                        }
                        else
                        {
                            MessageBox.Show($"El curso '{nombreCurso}' no tiene cupo disponible.", "Cupo Agotado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            DialogResult result = MessageBox.Show($"Decea entrar a la lista de espera del curso '{nombreCurso}'?", "Lista de Espera", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (!VerificarLegajoEnCurso(nombreCurso, textBox1.Text, "Lista de espera"))
                            {
                                if (result == DialogResult.Yes)
                                {
                                    AgregarAListaEspera(nombreCurso);
                                }
                            }
                            else
                            {
                                MessageBox.Show($"Ya estas en la lista de espera de inscripcion a '{nombreCurso}'.", "Lista de Espera", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
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

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
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

        private void AgregarLegajoAlCurso(string nombreCurso)
        {
            string jsonFilePath = "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Cursos.json";

            if (!VerificarLegajoEnCurso(nombreCurso, textBox1.Text, "ID Inscriptos"))
            {
                try
                {
                    if (File.Exists(jsonFilePath))
                    {
                        string jsonData = File.ReadAllText(jsonFilePath);

                        JArray jsonArray = JArray.Parse(jsonData);

                        JObject curso = (JObject)jsonArray.FirstOrDefault(x => x["Nombre"].ToString() == nombreCurso);

                        if (curso != null)
                        {
                            var contenido = curso["ID Inscriptos"];
                            if (string.IsNullOrEmpty(contenido.ToString()))
                            {
                                curso["ID Inscriptos"] += textBox1.Text;
                            }
                            else
                            {
                                curso["ID Inscriptos"] += $",{textBox1.Text}";
                            }
                            int cupoDisponible = Convert.ToInt32(curso["Cupo Actual"]);
                            curso["Cupo Actual"] = (cupoDisponible + 1).ToString();
                            File.WriteAllText(jsonFilePath, jsonArray.ToString());

                            DB.LimpiarTablaSql("Cursos");
                            DB.GuardarJsonCursosSql();

                            MessageBox.Show($"Inscripción realizada a '{nombreCurso}' correctamente.", "Inscripto", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            else
            {
                MessageBox.Show($"Ya estás inscripto en el curso '{nombreCurso}'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AgregarAListaEspera(string nombreCurso)
        {
            string jsonFilePath = "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Cursos.json";

            if (!VerificarLegajoEnCurso(nombreCurso, textBox1.Text, "ID Inscriptos"))
            {
                try
                {
                    if (File.Exists(jsonFilePath))
                    {
                        string jsonData = File.ReadAllText(jsonFilePath);

                        JArray jsonArray = JArray.Parse(jsonData);

                        JObject curso = (JObject)jsonArray.FirstOrDefault(x => x["Nombre"].ToString() == nombreCurso);

                        if (curso != null)
                        {
                            var contenido = curso["Lista de espera"];
                            if (string.IsNullOrEmpty(contenido.ToString()))
                            {
                                curso["Lista de espera"] += textBox1.Text;
                            }
                            else
                            {
                                curso["Lista de espera"] += $",{textBox1.Text}";
                            }
                            File.WriteAllText(jsonFilePath, jsonArray.ToString());

                            DB.LimpiarTablaSql("Cursos");
                            DB.GuardarJsonCursosSql();

                            MessageBox.Show($"Agregado a lista de espera para inscripcion a '{nombreCurso}'.", "Lista de Espera", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("El archivo JSON no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los datos:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show($"Ya estás inscripto en el curso '{nombreCurso}'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private bool VerificarLegajoEnCurso(string nombreCurso, string legajo, string key)
        {
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
                        string idInscriptos = curso.Value<string>(key);
                        string[] inscriptos = idInscriptos.Split(',');

                        if (inscriptos.Contains(legajo))
                        {
                            return true; 
                        }
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

            return false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            int columnIndex = dataGridView1.Columns["seleccionCurso"].Index;
            dataGridView1.Columns.RemoveAt(columnIndex);
            MostrarEnTabla();
        }
    }
}
