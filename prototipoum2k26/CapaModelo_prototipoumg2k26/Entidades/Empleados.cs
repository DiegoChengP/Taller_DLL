using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo_prototipoumg2k26.Entidades
{
    public class Empleados
    {
        public int IdPK { get; set; }
        public string NombreApp { get; set; }
        public string DescripcionApp { get; set; }
        public string NombreFormulario { get; set; }
        public int PermisoVer { get; set; }
        public int PermisoCrear { get; set; }
        public int PermisoModificar { get; set; }
        public int PermisoEliminar { get; set; }
        public int IdRol { get; set; }
    }
}
