using CapaModelo_prototipoumg2k26.Contratos;
using CapaModelo_prototipoumg2k26.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo_prototipoumg2k26.Repositorios
{
    public class RepositorioEmpleados : RepositorioMaestro, IRepositorioEmpleados
    {
        private string selectAll;
        private string insert;
        private string update;
        private string delete;  
        public RepositorioEmpleados()
        {
            selectAll = "SELECT id_aplicacion, nombre_app, descripcion_app, nombre_formulario, permiso_ver, permiso_crear, permiso_modificar, permiso_eliminar, id_rol FROM tbl_aplicaciones ORDER BY id_aplicacion";
            insert = "INSERT INTO tbl_aplicaciones (nombre_app, descripcion_app, nombre_formulario, permiso_ver, permiso_crear, permiso_modificar, permiso_eliminar, id_rol) VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
            update = "UPDATE tbl_aplicaciones SET nombre_app=?, descripcion_app=?, nombre_formulario=?, permiso_ver=?, permiso_crear=?, permiso_modificar=?, permiso_eliminar=?, id_rol=? WHERE id_aplicacion=?";
            delete = "DELETE FROM tbl_aplicaciones WHERE id_aplicacion=?";
        }
        public int Agregar (Empleados entidad)
        {
            var _parametros = new List<OdbcParameter>();
            _parametros.Add(new OdbcParameter("p_nombre_app", entidad.NombreApp));
            _parametros.Add(new OdbcParameter("p_descripcion_app", entidad.DescripcionApp));
            _parametros.Add(new OdbcParameter("p_nombre_formulario", entidad.NombreFormulario));
            _parametros.Add(new OdbcParameter("p_permiso_ver", entidad.PermisoVer));
            _parametros.Add(new OdbcParameter("p_permiso_crear", entidad.PermisoCrear));
            _parametros.Add(new OdbcParameter("p_permiso_modificar", entidad.PermisoModificar));
            _parametros.Add(new OdbcParameter("p_permiso_eliminar", entidad.PermisoEliminar));
            _parametros.Add(new OdbcParameter("p_id_rol", entidad.IdRol));
            
            return EjecucionNonQuery(insert, _parametros, CommandType.Text);
        }
        public int Editar(Empleados entidad)
        {
            var _parametros = new List<OdbcParameter>();    
            _parametros.Add(new OdbcParameter("p_nombre_app", entidad.NombreApp));
            _parametros.Add(new OdbcParameter("p_descripcion_app", entidad.DescripcionApp));
            _parametros.Add(new OdbcParameter("p_nombre_formulario", entidad.NombreFormulario));
            _parametros.Add(new OdbcParameter("p_permiso_ver", entidad.PermisoVer));
            _parametros.Add(new OdbcParameter("p_permiso_crear", entidad.PermisoCrear));
            _parametros.Add(new OdbcParameter("p_permiso_modificar", entidad.PermisoModificar));
            _parametros.Add(new OdbcParameter("p_permiso_eliminar", entidad.PermisoEliminar));
            _parametros.Add(new OdbcParameter("p_id_rol", entidad.IdRol));
            _parametros.Add(new OdbcParameter("p_id_aplicacion", entidad.IdPK));
            return EjecucionNonQuery(update, _parametros, CommandType.Text);
        }
        public int Remover(Empleados entidad)
        {
            var _parametros = new List<OdbcParameter>();
            _parametros.Add(new OdbcParameter("p_id_aplicacion", entidad.IdPK));
            return EjecucionNonQuery(delete, _parametros, CommandType.Text);
        }
        public IEnumerable<Empleados> GetAll()
        {
            var lstEmpleado = new List<Empleados>();
            var tblTabla = EjecucionConsulta(selectAll, CommandType.Text);
            foreach (DataRow row in tblTabla.Rows)
            {
                var empleado = new Empleados();
                empleado.IdPK = Convert.ToInt32(row[0]);
                empleado.NombreApp = row[1].ToString();
                empleado.DescripcionApp = row[2].ToString();
                empleado.NombreFormulario = row[3].ToString();
                empleado.PermisoVer = Convert.ToInt32(row[4]);
                empleado.PermisoCrear = Convert.ToInt32(row[5]);
                empleado.PermisoModificar = Convert.ToInt32(row[6]);
                empleado.PermisoEliminar = Convert.ToInt32(row[7]);
                empleado.IdRol = Convert.ToInt32(row[8]);
                lstEmpleado.Add(empleado);
            }
            tblTabla.Clear();
            tblTabla = null;
            return lstEmpleado;
        }

    }
}
