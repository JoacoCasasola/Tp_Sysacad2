using libreriaClases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Testing
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestVerificaSoloLetra()
        {
            string str = "Ho3la";
            bool valido = Validadores.VerificaSoloLetra(str);

            Assert.IsTrue(valido);
        }

        [TestMethod]
        public void TestVerificaUnicidad()
        {
            string key = "Nombre";
            string path = "C:\\Users\\Admin\\source\\repos\\libreriaClases\\Datos\\Alumnos.json";
            string nombre = "Joaquin";

            bool valido = Validadores.VerificarUnicidad(key, path, nombre);

            Assert.IsTrue(valido);
        }

        [TestMethod]
        public void TestVErificaCorreo()
        {
            string correo = "joaco@gmail.com";
            bool valido = Validadores.VerificarCorreoElectronico(correo);

            Assert.IsTrue(valido);
        }
    }
}
