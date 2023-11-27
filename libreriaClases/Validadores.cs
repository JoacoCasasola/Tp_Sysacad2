using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace libreriaClases
{
    public class Validadores
    {
        public delegate bool ValidarDelegado(string input);

        public delegate void UnicidadVerificadaEventHandler(string mensaje);
        public static event UnicidadVerificadaEventHandler UnicidadVerificada;

        protected static void OnUnicidadVerificada(string mensaje)
        {
            UnicidadVerificada.Invoke(mensaje);
        }

        public static bool VerificarUnicidad(string key, string jsonFilePath, string claveBuscada)
        {
            bool verifica = false;
            try
            {
                string json = File.ReadAllText(jsonFilePath);
                JArray jsonArray = JArray.Parse(json);
                Console.WriteLine(jsonArray);

                foreach (JObject jsonObject in jsonArray)
                {
                    if (jsonObject.ContainsKey(key))
                    {
                        string value = jsonObject[key].ToString();
                        if (value == claveBuscada)
                        {
                            verifica = true;
                            OnUnicidadVerificada($"El valor '{claveBuscada}' es único para la clave '{key}'.");
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al leer o buscar en el archivo JSON: " + ex.Message);
            }
            return verifica;
        }


        public static bool VerificaSoloNumero(string input)
        {
            string patron = @"^[0-9-]+$";
            return Regex.IsMatch(input, patron);
        }

        public static bool VerificaSoloLetra(string input)
        {
            string patron = @"^[ña-zA-Z ]+$";
            return Regex.IsMatch(input, patron);
        }

        public static bool VerificaSoloAlfanumerico(string input)
        {
            string patron = @"^[ña-zA-Z0-9 ]+$";
            return Regex.IsMatch(input, patron);
        }

        public static bool VerificarCorreoElectronico(string email)
        {
            string patron = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, patron);
        }

        public static bool VerificarDiaHorario(string diaHorario)
        {
            string patron = @"^[A-Za-zÁáÉéÍíÓóÚúÑñ]+ [0-9]{2}:[0-9]{2} [-aA] [0-9]{2}:[0-9]{2}$";
            return Regex.IsMatch(diaHorario, patron);
        }



        public string GenerarID(int longitud)
        {
            Random random = new Random();

            const string caracteres = "0123456789";
            var id = new char[longitud];

            for (int i = 0; i < longitud; i++)
            {
                id[i] = caracteres[random.Next(caracteres.Length)];
            }
            string newId = new string(id);

            return newId;
        }

        public string GenerarClave()
        {
            string clave = string.Empty;
            int longitudCodigo = 8;
            Random random = new Random();

            string caracteresPermitidos = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            char[] codigo = new char[longitudCodigo];

            for (int i = 0; i < longitudCodigo; i++)
            {
                int indiceCaracter = random.Next(caracteresPermitidos.Length);
                codigo[i] = caracteresPermitidos[indiceCaracter];
            }

            clave = new string(codigo);
            return clave;
        }
    }
}
