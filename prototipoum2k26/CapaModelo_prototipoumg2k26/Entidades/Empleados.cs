using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo_prototipoumg2k26.Entidades
{
    public class Empleados
    {
        public int  IdPK { get; set; }
        public string IdNumero { get; set; }
        public string Nombre { get; set; }
        public string Correo {  get; set; }
        public DateTime Cumpleaños { get; set; }
    }
}
