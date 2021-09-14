using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace framework.asp.Models
{
    public class Reporte
    {
        public String NombreUsuario { get; set; }
        public String EmailUsuario { get; set; }
        public DateTime Fecha_Compra { get; set; }
        public int TotalCompra { get; set; }

    }
}