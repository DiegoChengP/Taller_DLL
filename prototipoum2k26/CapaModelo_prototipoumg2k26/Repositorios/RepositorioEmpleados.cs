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
            selectAll = "SELECT * FROM empleados";
            insert = "INSERT INTO empleados value (NULL, ?, ?, ?, ?)";
            update = "UPDATE empleados SET idNumero=?, Nombre=?, Correo=?, Cumpleaños=? WHERE IdPK=?";
            delete = "DELETE FROM empleados WHERE IdPK=?";
        }
        public int Agregar (Empleados entidad)
        {
            var _parametros = new List<OdbcParameter>();
            _parametros.Add(new OdbcParameter("p_IdNumero", entidad.IdNumero));
            _parametros.Add(new OdbcParameter("p_Nombre", entidad.Nombre));
            _parametros.Add(new OdbcParameter("p_Correo", entidad.Correo));
            _parametros.Add(new OdbcParameter("p_Cumpleaños", entidad.Cumpleaños));
            
            return EjecucionNonQuery(insert, _parametros, CommandType.Text);
        }
        public int Editar(Empleados entidad)
        {
            var _parametros = new List<OdbcParameter>();    
            _parametros.Add(new OdbcParameter("p_IdNumero", entidad.IdNumero));
            _parametros.Add(new OdbcParameter("p_Nombre", entidad.Nombre));
            _parametros.Add(new OdbcParameter("p_Correo", entidad.Correo));
            _parametros.Add(new OdbcParameter("p_Cumpleaños", entidad.Cumpleaños));
            _parametros.Add(new OdbcParameter("p_IdPK", entidad.IdPK));
            return EjecucionNonQuery(update, _parametros, CommandType.Text);
        }
        public int Remover(Empleados entidad)
        {
            var _parametros = new List<OdbcParameter>();
            _parametros.Add(new OdbcParameter("p_IdPK", entidad.IdPK));
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
                empleado.IdNumero = row[1].ToString();
                empleado.Nombre = row[2].ToString();
                empleado.Correo = row[3].ToString();
                empleado.Cumpleaños = Convert.ToDateTime(row[4]);
                lstEmpleado.Add(empleado);
            }
            tblTabla.Clear();
            tblTabla = null;
            return lstEmpleado;
        }

    }
}
