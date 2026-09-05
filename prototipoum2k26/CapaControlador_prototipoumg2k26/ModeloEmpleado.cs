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
        private string _idNumero;
        private string _nombre;
        private string _correo;
        private DateTime _cumpleaños;
        private int _edad;
        private IRepositorioEmpleados RepositorioEmpleados;

        public EstadoEntidad Estado {private get; set;}
        private List<ModeloEmpleado> ListaEmpleados;

        public int IdPK { get => _idPK; set => _idPK = value; }

        [Required(ErrorMessage = "El campo numero de identificacion es requerido")]
        [RegularExpression("([0-9]+)", ErrorMessage ="Numero de identificacion debe ser numerico")]
        [StringLength(maximumLength:10, MinimumLength =10, ErrorMessage ="Numero de identificacion debe tener 10 digitos")]
        public string IdNumero { get => _idNumero; set => _idNumero = value; }

        [Required]
        [RegularExpression("^[a-zA-Zá-ú ]+$", ErrorMessage = "El campo Nombre debe ser solo letras")]
        [StringLength(maximumLength: 100, MinimumLength = 3)]
        public string Nombre { get => _nombre; set => _nombre = value; }

        [Required]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Debe ingresar una direccion de correo valida")]
        public string Correo { get => _correo; set => _correo = value; }

        public DateTime Cumpleaños { get => _cumpleaños; set => _cumpleaños = value; }

        public int Edad { get => _edad; private set => _edad = value; }

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
                modeloDatosEmpleados.IdNumero = _idNumero;
                modeloDatosEmpleados.Nombre = _nombre;   
                modeloDatosEmpleados.Correo = _correo;
                modeloDatosEmpleados.Cumpleaños = _cumpleaños;
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
                var fechaCumpleaños = item.Cumpleaños;
                ListaEmpleados.Add(new ModeloEmpleado
                {
                    _idPK = item.IdPK,
                    _idNumero = item.IdNumero,
                    _nombre = item.Nombre,
                    _correo = item.Correo,
                    _cumpleaños = item.Cumpleaños,
                    _edad=CalcularEdad(fechaCumpleaños)
                });
            }
            return ListaEmpleados;
        }
        public IEnumerable<ModeloEmpleado> FindbyId (string filter)
        {
            return ListaEmpleados.FindAll(e=> e.IdNumero.Contains(filter) || e._nombre.Contains(filter));
        }
        private int CalcularEdad(DateTime date)
        {
            DateTime fechaActual = DateTime.Now;
            return fechaActual.Year-date.Year;
        }

    }
}
