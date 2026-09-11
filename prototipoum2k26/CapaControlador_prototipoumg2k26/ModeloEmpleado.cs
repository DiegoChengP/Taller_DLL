using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaModelo_prototipoumg2k26.Contratos;
using CapaModelo_prototipoumg2k26.Entidades;
using CapaModelo_prototipoumg2k26.Repositorios;
using System.ComponentModel.DataAnnotations;


namespace CapaControlador_prototipoumg2k26
{
    public class ModeloEmpleado
    {
        private int _idPK;
        private string _nombreApp;
        private string _descripcionApp;
        private string _nombreFormulario;
        private int _permisoVer;
        private int _permisoCrear;
        private int _permisoModificar;
        private int _permisoEliminar;
        private int _idRol;
        private IRepositorioEmpleados RepositorioEmpleados;

        public EstadoEntidad Estado {private get; set;}
        private List<ModeloEmpleado> ListaEmpleados;

        public int IdPK { get => _idPK; set => _idPK = value; }

        [Required(ErrorMessage = "El campo nombre de aplicacion es requerido")]
        [StringLength(maximumLength: 100, MinimumLength = 1, ErrorMessage = "El nombre de aplicacion debe tener entre 1 y 100 caracteres")]
        public string NombreApp { get => _nombreApp; set => _nombreApp = value; }

        [Required(ErrorMessage = "El campo descripcion de aplicacion es requerido")]
        [StringLength(maximumLength: 255, MinimumLength = 1, ErrorMessage = "La descripcion debe tener entre 1 y 255 caracteres")]
        public string DescripcionApp { get => _descripcionApp; set => _descripcionApp = value; }

        [Required(ErrorMessage = "El campo nombre de formulario es requerido")]
        [StringLength(maximumLength: 150, MinimumLength = 1, ErrorMessage = "El nombre del formulario debe tener entre 1 y 150 caracteres")]
        public string NombreFormulario { get => _nombreFormulario; set => _nombreFormulario = value; }

        [Range(0, 1, ErrorMessage = "El permiso ver debe ser 0 o 1")]
        public int PermisoVer { get => _permisoVer; set => _permisoVer = value; }

        [Range(0, 1, ErrorMessage = "El permiso crear debe ser 0 o 1")]
        public int PermisoCrear { get => _permisoCrear; set => _permisoCrear = value; }

        [Range(0, 1, ErrorMessage = "El permiso modificar debe ser 0 o 1")]
        public int PermisoModificar { get => _permisoModificar; set => _permisoModificar = value; }

        [Range(0, 1, ErrorMessage = "El permiso eliminar debe ser 0 o 1")]
        public int PermisoEliminar { get => _permisoEliminar; set => _permisoEliminar = value; }

        [Range(1, int.MaxValue, ErrorMessage = "El id del rol debe ser mayor que 0")]
        public int IdRol { get => _idRol; set => _idRol = value; }

        public ModeloEmpleado()
        {
            RepositorioEmpleados = new RepositorioEmpleados();

        }
        public string GrabarCambios()
        {
            string mensaje = null;
            try
            {
                var modeloDatosEmpleados = new Empleados();
                modeloDatosEmpleados.IdPK = _idPK;
                modeloDatosEmpleados.NombreApp = _nombreApp;
                modeloDatosEmpleados.DescripcionApp = _descripcionApp;
                modeloDatosEmpleados.NombreFormulario = _nombreFormulario;
                modeloDatosEmpleados.PermisoVer = _permisoVer;
                modeloDatosEmpleados.PermisoCrear = _permisoCrear;
                modeloDatosEmpleados.PermisoModificar = _permisoModificar;
                modeloDatosEmpleados.PermisoEliminar = _permisoEliminar;
                modeloDatosEmpleados.IdRol = _idRol;
                switch (Estado)
                {
                    case EstadoEntidad.Added:
                        RepositorioEmpleados.Agregar(modeloDatosEmpleados);
                        mensaje = "Grabacion exitosa";
                        break;
                    case EstadoEntidad.Modified:
                        RepositorioEmpleados.Editar(modeloDatosEmpleados);
                        mensaje = "Actualizacion exitosa";
                        break;
                    case EstadoEntidad.Deleted:
                        RepositorioEmpleados.Remover(modeloDatosEmpleados);
                        mensaje = "Eliminacion exitosa";
                        break;
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.ToString();
            }
            return mensaje;
        }
        public List<ModeloEmpleado> GetAll()
        {
            var modeloDatosEmpleados = RepositorioEmpleados.GetAll();
            ListaEmpleados = new List<ModeloEmpleado>();
            foreach (Empleados item in modeloDatosEmpleados)
            {
                ListaEmpleados.Add(new ModeloEmpleado
                {
                    _idPK = item.IdPK,
                    _nombreApp = item.NombreApp,
                    _descripcionApp = item.DescripcionApp,
                    _nombreFormulario = item.NombreFormulario,
                    _permisoVer = item.PermisoVer,
                    _permisoCrear = item.PermisoCrear,
                    _permisoModificar = item.PermisoModificar,
                    _permisoEliminar = item.PermisoEliminar,
                    _idRol = item.IdRol
                });
            }
            return ListaEmpleados;
        }
        public IEnumerable<ModeloEmpleado> FindbyId (string filter)
        {
            if (ListaEmpleados == null)
                ListaEmpleados = GetAll();

            filter = filter ?? string.Empty;
            return ListaEmpleados.FindAll(e =>
                (e.NombreApp ?? string.Empty).IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0 ||
                (e.DescripcionApp ?? string.Empty).IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0 ||
                (e.NombreFormulario ?? string.Empty).IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0 ||
                e.IdPK.ToString().Contains(filter) ||
                e.IdRol.ToString().Contains(filter));
        }

    }
}
