using Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vista.Proveedores
{
    public partial class frmProveedores : Form
    {
        public frmProveedores()
        {
            InitializeComponent();
        }

        private void txtBuscar_Enter(object sender, EventArgs e)
        {
            //Cuando el usuario de enter para escribir, se va a borrar el texto de indicacion
            // Y el texto ya no sera opaco, sera color negro
            if (txtBuscar.Text=="Buscar Proveedor...")
            {
                txtBuscar.Text="";
                txtBuscar.ForeColor=Color.Black;
            }
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            txtBuscar.Text="Buscar Proveedor";
            txtBuscar.ForeColor=Color.Gray;
        }

        private void frmProveedores_Load(object sender, EventArgs e)
        {
            MostrarProveedor();
        }
        private void MostrarProveedor()
        {
            dgvProveedor.DataSource = null;
            dgvProveedor.DataSource = DbProveedor.CargarProveedor();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombreProveedor.Text))
            {
                MessageBox.Show("Ingrese el nombre del proveedor.");
                txtNombreProveedor.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                MessageBox.Show("Ingrese el teléfono del proveedor.");
                txtTelefono.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtUbicacion.Text))
            {
                MessageBox.Show("Ingrese la ubicación del proveedor.");
                txtUbicacion.Focus();
                return;
            }

            DbProveedor proveedor = new DbProveedor();

            // TRIM elimina los espacios de los extremos

            proveedor.Nombre_Proveedor1 = txtNombreProveedor.Text.Trim();
            proveedor.Telefono1 = txtTelefono.Text.Trim();
            proveedor.Correo1 = txtCorreo.Text.Trim();
            proveedor.Ubicación1 = txtUbicacion.Text.Trim();

            if (proveedor.InsertarProveedor())
            {
                MessageBox.Show(
                    "Proveedor registrado correctamente.",
                    "Registro exitoso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                txtNombreProveedor.Clear();
                txtTelefono.Clear();
                txtCorreo.Clear();
                txtUbicacion.Clear();

                txtNombreProveedor.Focus();

                // Actualiza la tabla de proveedores

                MostrarProveedor();

            }
        }
    }
}
