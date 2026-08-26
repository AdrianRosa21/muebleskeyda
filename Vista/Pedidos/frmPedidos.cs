using Modelo.Conexión_DB;
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
using Vista.Clientes;

namespace Vista.Pedidos
{
    public partial class frmPedidos : Form
    {
        public frmPedidos()
        {
            InitializeComponent();
        }

       


        private int idClienteSeleccionado = 0;

        private void btnSeleccionarCliente_Click(object sender, EventArgs e)
        {
            frmBuscarCliente modal = new frmBuscarCliente();
            if (modal.ShowDialog() == DialogResult.OK)
            {
                idClienteSeleccionado = modal.IdClienteSeleccionado;
                btnSeleccionarCliente.Text = modal.NombreClienteSeleccionado;
            }
        }

        string medidaLargo = "0";
        string medidaAncho = "0";
        string medidaAlto = "0";
        string observaciones = "";

        private void btnDeatllePedido_Click(object sender, EventArgs e)
        {
            frmDetallePedido modal = new frmDetallePedido();
            if (modal.ShowDialog() == DialogResult.OK)
            {
                medidaLargo = modal.Largo;
                medidaAncho = modal.Ancho;
                medidaAlto = modal.Alto;
                observaciones = modal.Observaciones;
            }
        }

       private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMuebleaRealizar.Text))
            {
                MessageBox.Show("Ingresa el nombre del mueble.");
                return;
            }

            if (string.IsNullOrWhiteSpace(nudCantidad.Text))
            {
                MessageBox.Show("Ingrese la cantidad de productos que desea agregar.");
                return;
            }
            if (medidaLargo == "0" && medidaAncho == "0" && medidaAlto == "0")
            {
                MessageBox.Show("Por favor ingresa las medidas del producto dando clic en 'Medidas del producto'.");
                return;
            }
            
            if (idPedidoSeleccionado <= 0)
            {
                MessageBox.Show("Seleccione un pedido primero.");
                return;
            }
            
            string medidas = medidaLargo + "x" + medidaAncho + "x" + medidaAlto;
            if (DetallePedidos.InsertarDetalle(idPedidoSeleccionado, txtMuebleaRealizar.Text, Convert.ToInt32(nudCantidad.Value), medidas))
            {
                MessageBox.Show("Detalle agregado correctamente.");
                dgvDetallesDePedido.DataSource = null;
                dgvDetallesDePedido.DataSource = DetallePedidos.CargarDetallesPorPedido(idPedidoSeleccionado);
            }
            else
            {
                MessageBox.Show("Error al agregar el detalle.");
            }
            
            txtMuebleaRealizar.Clear();
            nudCantidad.Value = 0;
            medidaLargo = "0";
            medidaAncho = "0";
            medidaAlto = "0";
            observaciones = "";
        }

       private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (dtpFechaDeEntrega.Value.Date < dtpFechaPedido.Value.Date)
            {
                MessageBox.Show("La fecha de entrega no puede ser anterior a la fecha del pedido.");
                return;
            }

            if (idPedidoSeleccionado > 0)
            {
                string nuevoEstado = cbEstado.Text;
                DateTime fechaEntrega = dtpFechaDeEntrega.Value;
                if (DbPedidos.ActualizarPedido(idPedidoSeleccionado, nuevoEstado, fechaEntrega))
                {
                    MessageBox.Show("El estado del pedido se actualizó correctamente a: " + nuevoEstado);
                    MostrarPedidos();
                }
                else
                {
                    MessageBox.Show("Error al actualizar el pedido.");
                }
            }
            else
            {
                MessageBox.Show("Para actualizar el estado, selecciona un pedido de la lista, elige 'En proceso' o 'Finalizado' en la lista desplegable y presiona Guardar.");
            }
       }

        private void btnCamcelar_Click(object sender, EventArgs e)
        {
            ((DataTable)dgvDetallesDePedido.DataSource)?.RejectChanges();
            btnSeleccionarCliente.Text = "Seleccionar Cliente";
            txtMuebleaRealizar.Clear();
            nudCantidad.Value = 0;
        }

        private void txtBuscar_Enter(object sender, EventArgs e)
        {
            //Cuando el usuario de enter para escribir, se va a borrar el texto de indicacion
            // Y el texto ya no sera opaco, sera color negro
            if (txtBuscar.Text=="Buscar Pedido...")
            {
                txtBuscar.Text="";
                txtBuscar.ForeColor=Color.Black;
            }
        }


        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            txtBuscar.Text="Buscar Pedido...";
            txtBuscar.ForeColor=Color.Gray;
        }

       
        private void MostrarDetallesPedido()
        {
            dgvDetallesDePedido.DataSource = null;
            dgvDetallesDePedido.DataSource = DetallePedidos.CargarDetallesPedidos();
        }


        private void MostrarPedidos()
        {
            dgvPedidosRegistrados.DataSource = null;
            dgvPedidosRegistrados.DataSource = DbPedidos.CargarRegistroPedidos();
        }

        private void frmPedidos_Load(object sender, EventArgs e)
        {
            MostrarPedidos();
            MostrarDetallesPedido();

            cbEstado.Items.Add("En proceso");
            cbEstado.Items.Add("Finalizado");
            cbEstado.SelectedIndex = 0;

            //Maximo de caracteres admitidos
            txtMuebleaRealizar.MaxLength = 150;


            //Navegar con la tecla TAB
            btnSeleccionarCliente.TabIndex = 1;
            dtpFechaPedido.TabIndex= 2;
            dtpFechaDeEntrega.TabIndex= 3;
            cbEstado.TabIndex= 4;
            txtMuebleaRealizar.TabIndex= 5;
            nudCantidad.TabIndex= 6;
            btnDetallePedido.TabIndex = 7;
            btnAgregar.TabIndex= 8;
            btnGuardar.TabIndex= 9;
            btnCamcelar.TabIndex= 10;
        }


        int idPedidoSeleccionado = 0;
        private void dgvPedidosRegistrados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || ((DataGridView)sender).Rows[e.RowIndex].IsNewRow) return;
            DataGridViewRow row = dgvPedidosRegistrados.Rows[e.RowIndex];
            idPedidoSeleccionado = Convert.ToInt32(row.Cells["IdPedido"].Value);
            string estado = row.Cells["Estado"].Value?.ToString();
            cbEstado.Text = estado;
            
            // Buscar los nombres exactos de las columnas para las fechas
            string colPedido = "";
            string colEntrega = "";
            foreach (DataGridViewColumn col in dgvPedidosRegistrados.Columns)
            {
                if (col.Name.Contains("Fecha") && col.Name.Contains("Pedido")) colPedido = col.Name;
                if (col.Name.Contains("Fecha") && col.Name.Contains("Entrega")) colEntrega = col.Name;
            }
            
            if (!string.IsNullOrEmpty(colPedido) && row.Cells[colPedido].Value != DBNull.Value && row.Cells[colPedido].Value != null)
                dtpFechaPedido.Value = Convert.ToDateTime(row.Cells[colPedido].Value);
                
            if (!string.IsNullOrEmpty(colEntrega) && row.Cells[colEntrega].Value != DBNull.Value && row.Cells[colEntrega].Value != null)
                dtpFechaDeEntrega.Value = Convert.ToDateTime(row.Cells[colEntrega].Value);
                
            dgvDetallesDePedido.DataSource = null;
            dgvDetallesDePedido.DataSource = DetallePedidos.CargarDetallesPorPedido(idPedidoSeleccionado);
        }

        private void DesactivarCopiarPegar(Control control)
        {
            foreach (Control elemento in control.Controls)
            {
                if (elemento is TextBox)
                {
                    ((TextBox)elemento).ShortcutsEnabled = false;
                }

                if (elemento.HasChildren)
                {
                    DesactivarCopiarPegar(elemento);
                }
            }
        }
        // Caracteres admitidos para el campo del mueble del pedido
        private void txtMuebleaRealizar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }
    }
}




