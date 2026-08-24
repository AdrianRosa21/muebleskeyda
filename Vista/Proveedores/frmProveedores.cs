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
                MessageBox.Show("Ingrese el telÃ©fono del proveedor.");
                txtTelefono.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtUbicacion.Text))
            {
                MessageBox.Show("Ingrese la ubicaciÃ³n del proveedor.");
                txtUbicacion.Focus();
                return;
            }

            DbProveedor proveedor = new DbProveedor();

            // TRIM elimina los espacios de los extremos

            proveedor.Nombre_Proveedor1 = txtNombreProveedor.Text.Trim();
            proveedor.Telefono1 = txtTelefono.Text.Trim();
            proveedor.Correo1 = txtCorreo.Text.Trim();
            proveedor.UbicaciÃ³n1 = txtUbicacion.Text.Trim();

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

        private int idProveedorSeleccionado = 0;
        
        private void dgvProveedor_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvProveedor.Rows[e.RowIndex];
                idProveedorSeleccionado = Convert.ToInt32(fila.Cells["IdProveedor"].Value);
                txtNombreProveedor.Text = fila.Cells["Nombre_Proveedor"].Value.ToString();
                txtTelefono.Text = fila.Cells["Telefono"].Value.ToString();
                txtCorreo.Text = fila.Cells["Correo"].Value.ToString();
                txtUbicacion.Text = fila.Cells["Ubicacion"].Value.ToString();
                
                btnEditar.Visible = true;
                btnGuardar.Visible = false;
            }
        }
        
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (idProveedorSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un proveedor para editar.");
                return;
            }
            
            DbProveedor proveedor = new DbProveedor();
            proveedor.IdProveedor1 = idProveedorSeleccionado;
            proveedor.Nombre_Proveedor1 = txtNombreProveedor.Text.Trim();
            proveedor.Telefono1 = txtTelefono.Text.Trim();
            proveedor.Correo1 = txtCorreo.Text.Trim();
            proveedor.Ubicación1 = txtUbicacion.Text.Trim();
            
            if (proveedor.ActualizarProveedor())
            {
                MessageBox.Show("Proveedor actualizado correctamente.");
                MostrarProveedor();
                Limpiar();
            }
            else
            {
                MessageBox.Show("Error al actualizar el proveedor.");
            }
        }
        
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idProveedorSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un proveedor para eliminar.");
                return;
            }
            
            DialogResult res = MessageBox.Show("¿Está seguro de eliminar este proveedor?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res == DialogResult.Yes)
            {
                DbProveedor proveedor = new DbProveedor();
                proveedor.IdProveedor1 = idProveedorSeleccionado;
                
                if (proveedor.EliminarProveedor())
                {
                    MessageBox.Show("Proveedor eliminado correctamente.");
                    MostrarProveedor();
                    Limpiar();
                }
                else
                {
                    MessageBox.Show("No se puede eliminar el proveedor porque tiene registros asociados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
        private void Limpiar()
        {
            txtNombreProveedor.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            txtUbicacion.Clear();
            idProveedorSeleccionado = 0;
            btnEditar.Visible = false;
            btnGuardar.Visible = true;
        }
    }
}
