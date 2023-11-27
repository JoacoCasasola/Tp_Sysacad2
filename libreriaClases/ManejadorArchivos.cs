using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace libreriaClases
{
    public class ManejadorArchivos
    {
        public delegate void GuardarListaDiccionariosDelegado(List<Dictionary<string, string>> listaDiccionarios, string nombreArchivo, string rutaArchivo);

        public static void GuardarListaDiccionariosJSON(List<Dictionary<string, string>> listaDiccionarios, string nombreArchivo, string rutaArchivo)
        {
            try
            {
                string rutaArchivoJSON = Path.Combine(rutaArchivo, $"{nombreArchivo}.json");

                if (!File.Exists(rutaArchivoJSON))
                {
                    File.WriteAllText(rutaArchivoJSON, "[]");
                }

                string jsonExistente = File.ReadAllText(rutaArchivoJSON);
                List<Dictionary<string, string>> listaExistente;

                if (!string.IsNullOrEmpty(jsonExistente))
                {
                    listaExistente = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(jsonExistente);
                }
                else
                {
                    listaExistente = new List<Dictionary<string, string>>();
                }

                listaExistente.AddRange(listaDiccionarios);

                string jsonActualizado = JsonConvert.SerializeObject(listaExistente, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(rutaArchivoJSON, jsonActualizado);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al manipular el archivo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
