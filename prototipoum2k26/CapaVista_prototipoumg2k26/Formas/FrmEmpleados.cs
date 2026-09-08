using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaControlador_prototipoumg2k26;

namespace CapaVista_prototipoumg2k26.Formas
{
    public partial class FrmEmpleados : Form
    {
        private ModeloEmpleado empleado = new ModeloEmpleado();
        public FrmEmpleados()
        {
            InitializeComponent();
            panIngresoDatos.Enabled = false;
            CargarDatos();
        }

        private void FrmEmpleados_Load(object sender, EventArgs e)
        {
            listaEmpleados();
        }
        private void listaEmpleados()
        {
            try
            {
                dgvEmpleados.DataSource = empleado.GetAll();
            }
            catch (Exception ex)
            {
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dgvEmpleados.DataSource = empleado.FindbyId(txtSearch.Text);
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dgvEmpleados.DataSource = empleado.FindbyId(txtSearch.Text);
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            empleado.IdNumero = txtNumeroID.Text;
            empleado.Nombre = txtNombre.Text;
            empleado.Correo = txtCorreo.Text;
            empleado.Cumpleaños = txtCumpleaños.Value;
            bool valido = new Ayudas.ValidacionDatos(empleado).Validar();
            if (valido == true)
            {
                string resultado = empleado.GrabarCambios();
                MessageBox.Show(resultado);
                listaEmpleados();
                Reinicio();
            }
        }
        private void Reinicio()
        {
            panIngresoDatos.Enabled = false;
            txtNumeroID.Clear();
            txtNombre.Clear();
            txtCorreo.Clear();
            txtCumpleaños.Value = DateTime.Today;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            panIngresoDatos.Enabled = true;
            empleado.Estado = EstadoEntidad.Added;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvEmpleados.SelectedRows.Count > 0)
            {
                panIngresoDatos.Enabled = true;
                empleado.Estado = EstadoEntidad.Modified;
                empleado.IdPK = Convert.ToInt32(dgvEmpleados.CurrentRow.Cells[0].Value);
                txtNumeroID.Text = dgvEmpleados.CurrentRow.Cells[1].Value.ToString();
                txtNombre.Text = dgvEmpleados.CurrentRow.Cells[2].Value.ToString();
                txtCorreo.Text = dgvEmpleados.CurrentRow.Cells[3].Value.ToString();
                txtCumpleaños.Value = Convert.ToDateTime(dgvEmpleados.CurrentRow.Cells[4].Value);
            }
            else MessageBox.Show("Seleccione una fila");
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (dgvEmpleados.SelectedRows.Count > 0)
            {
                empleado.Estado = EstadoEntidad.Deleted;
                empleado.IdPK = Convert.ToInt32(dgvEmpleados.CurrentRow.Cells[0].Value);
                string resultado = empleado.GrabarCambios();
                MessageBox.Show(resultado);
                listaEmpleados();
            }
            else MessageBox.Show("Seleccione una fila");
        }
        void CargarDatos()
        {
            comboI1.llenarCombo("tbl_empleadospuestos", "codigo_empleado", "puesto");

        }
    }
}