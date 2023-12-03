using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libreriaClases
{
    public class Pago
    {
        public string lagajo {  get; set; }
        public DateTime fecha { get; set; }
        public string monto { get; set; }
        public string carrito { get; set; }

        public Pago(string legajoAlumno, DateTime fechaPago, string montoPago, string carroDeCompra) 
        {
            lagajo = legajoAlumno;
            fecha = fechaPago;
            monto = montoPago;
            carrito = carroDeCompra;
        }
    }
}
