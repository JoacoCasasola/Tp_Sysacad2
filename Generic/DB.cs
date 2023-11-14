using Generic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libreriaClases;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;

namespace Generic
{
    public class DB
    {
        private static SqlConnection conexion;
        private static string CadenaConexion;
        private static SqlCommand comando;

        static DB()
        {
            CadenaConexion = @"Server=DESKTOP-6SF8M06\SQLEXPRESS;Database=SqlSysacad;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True;";
            conexion = new SqlConnection(CadenaConexion);

            comando = new SqlCommand();
            comando.CommandType = System.Data.CommandType.Text;
            comando.Connection = conexion;
        }

        public static List<Alumno> Query()
        {
            var alumnos = new List<Alumno>();
            try
            {
                conexion.Open();

                var query = "SELECT * FROM Alumnos";
                comando.CommandText = query;

                using (var reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var nombre = reader["Nombre"].ToString();
                        var apellido = reader["Apellido"].ToString();
                        var dni = reader["DNI"].ToString();
                        var correo = reader["Correo"].ToString();
                        var telefono = reader["Telefono"].ToString();
                        var direccion = reader["Apellido"].ToString();
                        var carrera = reader["Apellido"].ToString();
                        var clave = reader["Clave"].ToString();
                        var legajo = reader["Legajo"].ToString();

                        var alumno = new Alumno(nombre, apellido, dni, telefono, direccion, correo, carrera, clave, legajo);
                        alumnos.Add(alumno);
                    }
                }
                return alumnos;
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
        }


        public static void GuardarJsonAlumnosSql()
        {
            var jsonText = File.ReadAllText("C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Alumnos.json");
            var data = JsonConvert.DeserializeObject<List<Alumno>>(jsonText);


            using (SqlConnection connection = new SqlConnection(CadenaConexion))
            {
                connection.Open();

                var query = "INSERT INTO Alumnos (Nombre, Apellido, Dni, Telefono, Direccion, Correo, Carrera, Clave, Legajo) " +
                            "VALUES (@Nombre, @Apellido, @Dni, @Telefono, @Direccion, @Correo, @Carrera, @Clave, @Legajo)";

                foreach (Alumno item in data)
                {
                    if (item != null)
                    {
                        using (SqlCommand comando = new SqlCommand(query, connection))
                        {
                            comando.Parameters.AddWithValue("@Nombre", item._nombre);
                            comando.Parameters.AddWithValue("@Apellido", item._apellido);
                            comando.Parameters.AddWithValue("@Dni", item._Dni);
                            comando.Parameters.AddWithValue("@Telefono", item._telefono);
                            comando.Parameters.AddWithValue("@Direccion", item._direccion);
                            comando.Parameters.AddWithValue("@Correo", item._correo);
                            comando.Parameters.AddWithValue("@Carrera", item._carrera);
                            comando.Parameters.AddWithValue("@Clave", item._clave);
                            comando.Parameters.AddWithValue("@Legajo", item._legajo);

                            comando.ExecuteNonQuery();
                        }
                    }
                }
                connection.Close();
            }
        }


        public static void GuardarJsonAdminsSql()
        {
            var jsonText = File.ReadAllText("C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Administradores.json");
            var data = JsonConvert.DeserializeObject<List<Administrador>>(jsonText);


            using (SqlConnection connection = new SqlConnection(CadenaConexion))
            {
                connection.Open();

                var query = "INSERT INTO Administradores (Nombre, Apellido, DNI, Telefono, Direccion, Correo, Nivel, Clave, ID) " +
                            "VALUES (@Nombre, @Apellido, @DNI, @Telefono, @Direccion, @Correo, @Nivel, @Clave, @ID)";
                
                foreach (Administrador item in data)
                {
                    if (item != null)
                    {
                        using (SqlCommand comando = new SqlCommand(query, connection))
                        {
                            comando.Parameters.AddWithValue("@Nombre", item._nombre);
                            comando.Parameters.AddWithValue("@Apellido", item._apellido);
                            comando.Parameters.AddWithValue("@DNI", item._dni);
                            comando.Parameters.AddWithValue("@Telefono", item._telefono);
                            comando.Parameters.AddWithValue("@Direccion", item._direccion);
                            comando.Parameters.AddWithValue("@Correo", item._correo);
                            comando.Parameters.AddWithValue("@Nivel", item._nivel);
                            comando.Parameters.AddWithValue("@Clave", item._clave);
                            comando.Parameters.AddWithValue("@ID", item._idAdmin);

                            comando.ExecuteNonQuery();
                        }
                    }
                }
                connection.Close();
            }
        }


        public static void GuardarJsonProfesoresSql()
        {
            var jsonText = File.ReadAllText("C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Profesores.json");
            var data = JsonConvert.DeserializeObject<List<Profesor>>(jsonText);


            using (SqlConnection connection = new SqlConnection(CadenaConexion))
            {
                connection.Open();

                var query = "INSERT INTO Profesores (Nombre, Apellido, DNI, Telefono, Direccion, Correo, Nivel, Clave, ID) " +
                            "VALUES (@Nombre, @Apellido, @DNI, @Telefono, @Direccion, @Correo, @Nivel, @Clave, @ID)";

                foreach (Profesor item in data)
                {
                    if (item != null)
                    {
                        using (SqlCommand comando = new SqlCommand(query, connection))
                        {
                            comando.Parameters.AddWithValue("@Nombre", item._nombre);
                            comando.Parameters.AddWithValue("@Apellido", item._apellido);
                            comando.Parameters.AddWithValue("@Dni", item._dni);
                            comando.Parameters.AddWithValue("@Telefono", item._telefono);
                            comando.Parameters.AddWithValue("@Direccion", item._direccion);
                            comando.Parameters.AddWithValue("@Correo", item._correo);
                            comando.Parameters.AddWithValue("@Nivel", item._nivel);
                            comando.Parameters.AddWithValue("@Clave", item._clave);
                            comando.Parameters.AddWithValue("@ID", item._idProfesor);

                            comando.ExecuteNonQuery();
                        }
                    }
                }
                connection.Close();
            }
        }

        public static void GuardarJsonCursosSql()
        {
            try
            {
                var jsonText = File.ReadAllText("C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Cursos.json");
                var data = JsonConvert.DeserializeObject<List<Curso>>(jsonText);

                using (SqlConnection connection = new SqlConnection(CadenaConexion))
                {
                    connection.Open();

                    var query = "INSERT INTO Cursos (Nombre, Descripcion, Cupo_Max, Cupo_actual, Horario, ID) " +
                                "VALUES (@Nombre, @Descripcion, @Cupo_Max, @Cupo_actual, @Horario, @ID)";

                    foreach (Curso item in data)
                    {
                        if (item != null)
                        {
                            using (SqlCommand comando = new SqlCommand(query, connection))
                            {
                                comando.Parameters.AddWithValue("@Nombre", item._nombre);
                                comando.Parameters.AddWithValue("@Descripcion", item._descripcion);
                                comando.Parameters.AddWithValue("@Cupo_Max", item._cupoMax);
                                comando.Parameters.AddWithValue("@Cupo_actual", item._cupoActual);
                                comando.Parameters.AddWithValue("@Horario", item._horario);
                                comando.Parameters.AddWithValue("@ID", item._idCurso);

                                comando.ExecuteNonQuery();
                            }
                        }
                    }

                    connection.Close();
                    Console.WriteLine("Datos cargados en SQL Server correctamente.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar datos en SQL Server: {ex.Message}");
            }
        }

        public static void LimpiarTablaSql(string nombreTabla)
        {
            {
                using (SqlConnection connection = new SqlConnection(CadenaConexion))
                {
                    connection.Open();

                    string deleteQuery = $"DELETE FROM {nombreTabla}";

                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
        }
    }

}

