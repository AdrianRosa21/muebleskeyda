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
            if (medidaLargo == "0" && medidaAncho == "0" && medidaAlto == "0")
            {
                MessageBox.Show("Por favor ingresa las medidas del producto dando clic en 'Medidas del producto'.");
                return;
            }
            
            DataTable dt = (DataTable)dgvDetallesDePedido.DataSource;
            if (dt != null)
            {
                DataRow newRow = dt.NewRow();
                newRow["Mueble"] = txtMuebleaRealizar.Text;
                newRow["Cantidad"] = numericUpDown1.Value;
                newRow["Medidas"] = medidaLargo + "x" + medidaAncho + "x" + medidaAlto;
                dt.Rows.Add(newRow);
            }
            
            txtMuebleaRealizar.Clear();
            numericUpDown1.Value = 0;
            medidaLargo = "0";
            medidaAncho = "0";
            medidaAlto = "0";
            observaciones = "";
        }

                        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (idPedidoSeleccionado > 0)
            {
                string nuevoEstado = comboBox1.Text;
                DateTime fechaEntrega = dtpFechaEntrega.Value;
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
            numericUpDown1.Value = 0;
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

            comboBox1.Items.Add("En proceso");
            comboBox1.Items.Add("Finalizado");
            comboBox1.SelectedIndex = 0;
        }
    
        int idPedidoSeleccionado = 0;
        private void dgvPedidosRegistrados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || ((DataGridView)sender).Rows[e.RowIndex].IsNewRow) return;
            DataGridViewRow row = dgvPedidosRegistrados.Rows[e.RowIndex];
            idPedidoSeleccionado = Convert.ToInt32(row.Cells["IdPedido"].Value);
            string estado = row.Cells["Estado"].Value?.ToString();
            comboBox1.Text = estado;
            
            // Buscar los nombres exactos de las columnas para las fechas
            string colPedido = "";
            string colEntrega = "";
            foreach (DataGridViewColumn col in dgvPedidosRegistrados.Columns)
            {
                if (col.Name.Contains("Fecha") && col.Name.Contains("Pedido")) colPedido = col.Name;
                if (col.Name.Contains("Fecha") && col.Name.Contains("Entrega")) colEntrega = col.Name;
            }
            
            if (!string.IsNullOrEmpty(colPedido) && row.Cells[colPedido].Value != DBNull.Value && row.Cells[colPedido].Value != null)
                dateTimePicker1.Value = Convert.ToDateTime(row.Cells[colPedido].Value);
                
            if (!string.IsNullOrEmpty(colEntrega) && row.Cells[colEntrega].Value != DBNull.Value && row.Cells[colEntrega].Value != null)
                dtpFechaEntrega.Value = Convert.ToDateTime(row.Cells[colEntrega].Value);
                
            dgvDetallesDePedido.DataSource = null;
            dgvDetallesDePedido.DataSource = DetallePedidos.CargarDetallesPorPedido(idPedidoSeleccionado);
        }
    }
}




