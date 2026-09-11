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
        private TextBox txtNombreApp;
        private TextBox txtDescripcionApp;
        private TextBox txtNombreFormulario;
        private TextBox txtIdRol;
        private CheckBox chkPermisoVer;
        private CheckBox chkPermisoCrear;
        private CheckBox chkPermisoModificar;
        private CheckBox chkPermisoEliminar;
        private Label lblNombreApp;
        private Label lblDescripcionApp;
        private Label lblNombreFormulario;
        private Label lblIdRol;
        public FrmEmpleados()
        {
            InitializeComponent();
            ConfigurarCamposAplicacion();
            this.Text = "Mantenimiento Aplicaciones - Prototipo v 0.5.0";
            dgvEmpleados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEmpleados.MultiSelect = false;
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
                dgvEmpleados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvEmpleados.Columns["IdPK"].HeaderText = "id_aplicacion";
                dgvEmpleados.Columns["NombreApp"].HeaderText = "nombre_app";
                dgvEmpleados.Columns["DescripcionApp"].HeaderText = "descripcion_app";
                dgvEmpleados.Columns["NombreFormulario"].HeaderText = "nombre_formulario";
                dgvEmpleados.Columns["PermisoVer"].HeaderText = "permiso_ver";
                dgvEmpleados.Columns["PermisoCrear"].HeaderText = "permiso_crear";
                dgvEmpleados.Columns["PermisoModificar"].HeaderText = "permiso_modificar";
                dgvEmpleados.Columns["PermisoEliminar"].HeaderText = "permiso_eliminar";
                dgvEmpleados.Columns["IdRol"].HeaderText = "id_rol";
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
            empleado.NombreApp = txtNombreApp.Text.Trim();
            empleado.DescripcionApp = txtDescripcionApp.Text.Trim();
            empleado.NombreFormulario = txtNombreFormulario.Text.Trim();
            empleado.PermisoVer = chkPermisoVer.Checked ? 1 : 0;
            empleado.PermisoCrear = chkPermisoCrear.Checked ? 1 : 0;
            empleado.PermisoModificar = chkPermisoModificar.Checked ? 1 : 0;
            empleado.PermisoEliminar = chkPermisoEliminar.Checked ? 1 : 0;

            int idRol;
            if (!int.TryParse(txtIdRol.Text.Trim(), out idRol))
            {
                MessageBox.Show("El id_rol debe ser numerico");
                return;
            }
            empleado.IdRol = idRol;

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
            txtNombreApp.Clear();
            txtDescripcionApp.Clear();
            txtNombreFormulario.Clear();
            txtIdRol.Clear();
            chkPermisoVer.Checked = true;
            chkPermisoCrear.Checked = true;
            chkPermisoModificar.Checked = true;
            chkPermisoEliminar.Checked = true;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            panIngresoDatos.Enabled = true;
            empleado = new ModeloEmpleado();
            empleado.Estado = EstadoEntidad.Added;
            ReinicioCampos();
            panIngresoDatos.Enabled = true;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvEmpleados.SelectedRows.Count > 0)
            {
                panIngresoDatos.Enabled = true;
                empleado.Estado = EstadoEntidad.Modified;
                empleado.IdPK = Convert.ToInt32(dgvEmpleados.CurrentRow.Cells[0].Value);
                txtNombreApp.Text = dgvEmpleados.CurrentRow.Cells[1].Value.ToString();
                txtDescripcionApp.Text = dgvEmpleados.CurrentRow.Cells[2].Value.ToString();
                txtNombreFormulario.Text = dgvEmpleados.CurrentRow.Cells[3].Value.ToString();
                chkPermisoVer.Checked = Convert.ToInt32(dgvEmpleados.CurrentRow.Cells[4].Value) == 1;
                chkPermisoCrear.Checked = Convert.ToInt32(dgvEmpleados.CurrentRow.Cells[5].Value) == 1;
                chkPermisoModificar.Checked = Convert.ToInt32(dgvEmpleados.CurrentRow.Cells[6].Value) == 1;
                chkPermisoEliminar.Checked = Convert.ToInt32(dgvEmpleados.CurrentRow.Cells[7].Value) == 1;
                txtIdRol.Text = dgvEmpleados.CurrentRow.Cells[8].Value.ToString();
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
            comboI1.Visible = false;
        }

        private void ConfigurarCamposAplicacion()
        {
            txtNumeroID.Visible = false;
            txtNombre.Visible = false;
            txtCorreo.Visible = false;
            txtCumpleaños.Visible = false;
            lblNumeroID.Visible = false;
            lblNombre.Visible = false;
            lblCorreo.Visible = false;
            lblCumpleaños.Visible = false;
            comboI1.Visible = false;

            panIngresoDatos.Size = new Size(305, 376);

            lblNombreApp = CrearLabel("nombre_app", 12, 12);
            txtNombreApp = CrearTextBox(12, 34, 278);
            lblDescripcionApp = CrearLabel("descripcion_app", 12, 62);
            txtDescripcionApp = CrearTextBox(12, 84, 278);
            lblNombreFormulario = CrearLabel("nombre_formulario", 12, 112);
            txtNombreFormulario = CrearTextBox(12, 134, 278);
            lblIdRol = CrearLabel("id_rol", 12, 162);
            txtIdRol = CrearTextBox(12, 184, 278);

            chkPermisoVer = CrearCheckBox("permiso_ver", 12, 214);
            chkPermisoCrear = CrearCheckBox("permiso_crear", 12, 240);
            chkPermisoModificar = CrearCheckBox("permiso_modificar", 12, 266);
            chkPermisoEliminar = CrearCheckBox("permiso_eliminar", 12, 292);

            btnGrabar.Location = new Point(82, 326);
            btnGrabar.Size = new Size(150, 38);
        }

        private Label CrearLabel(string texto, int x, int y)
        {
            var label = new Label();
            label.Text = texto;
            label.AutoSize = true;
            label.Location = new Point(x, y);
            panIngresoDatos.Controls.Add(label);
            return label;
        }

        private TextBox CrearTextBox(int x, int y, int ancho)
        {
            var textBox = new TextBox();
            textBox.Location = new Point(x, y);
            textBox.Size = new Size(ancho, 22);
            panIngresoDatos.Controls.Add(textBox);
            return textBox;
        }

        private CheckBox CrearCheckBox(string texto, int x, int y)
        {
            var checkBox = new CheckBox();
            checkBox.Text = texto;
            checkBox.AutoSize = true;
            checkBox.Checked = true;
            checkBox.Location = new Point(x, y);
            panIngresoDatos.Controls.Add(checkBox);
            return checkBox;
        }

        private void ReinicioCampos()
        {
            txtNombreApp.Clear();
            txtDescripcionApp.Clear();
            txtNombreFormulario.Clear();
            txtIdRol.Clear();
            chkPermisoVer.Checked = true;
            chkPermisoCrear.Checked = true;
            chkPermisoModificar.Checked = true;
            chkPermisoEliminar.Checked = true;
        }
    }
}
