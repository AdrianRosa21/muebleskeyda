using Modelo.Conexión_DB;
using Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vista.Pedidos;

namespace Vista.Ventas
{
    public partial class frmVentas : Form
    {
        // Lista temporal donde se guardarán los productos
        // antes de guardar la venta en la base de datos
        private List<DetalleVenta> detallesVenta = new List<DetalleVenta>();

        public frmVentas()
        {
            InitializeComponent();
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtBuscar_Enter(object sender, EventArgs e)
        {
            //Cuando el usuario de enter para escribir, se va a borrar el texto de indicacion
            // Y el texto ya no sera opaco, sera color negro
            if (txtBuscar.Text=="Buscar Venta...")
            {
                txtBuscar.Text="";
                txtBuscar.ForeColor=Color.Black;

            }
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            txtBuscar.Text="Buscar Venta...";
            txtBuscar.ForeColor=Color.Gray;
        }
        public void MostrarVentas()
        {
            dgvVentas.DataSource = null;
            dgvVentas.DataSource = DbVentas.CargarVentas();
        }

        private void frmVentas_Load(object sender, EventArgs e)
        {
            // Mostrar las ventas existentes
            MostrarVentas();

            // Cargar clientes
            CargarComboBoxClientes();

            // Cargar mÃ©todos de pago
            CargarComboBoxMetodosDePago();
        }
        private void CalcularTotal()
        {
            decimal subtotal = 0;

            decimal.TryParse(
                txtSubTotal.Text,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out subtotal);

            decimal iva = subtotal * 0.13m;

            decimal total = subtotal + iva;

            txtIVA.Text = iva.ToString("0.00", CultureInfo.InvariantCulture);
            txtTotalPagar.Text = total.ToString("0.00", CultureInfo.InvariantCulture);
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {

         
            // VALIDAR CLIENTE
      
            if (cbCliente.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Seleccione un cliente.",
                    "Campo requerido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // VALIDAR MÃ‰TODO DE PAGO

            if (cbMetodoPago.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Seleccione un mÃ©todo de pago.",
                    "Campo requerido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

       
            // VALIDAR QUE  SI HAYA PRODUCTOS
      
            if (detallesVenta.Count == 0)
            {
                MessageBox.Show(
                    "Agregue al menos un producto a la venta.",
                    "Venta sin productos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }



            // OBTENER DATOS DE LA VENTA
 
            DateTime fechaVenta = dtFechaVenta.Value;

            int cliente =Convert.ToInt32(cbCliente.SelectedValue);

            int metodoPago =Convert.ToInt32(cbMetodoPago.SelectedValue);


            // OBTENER SUBTOTAL

            decimal subtotal;

            if (!decimal.TryParse( txtSubTotal.Text,NumberStyles.Any, CultureInfo.InvariantCulture,out subtotal))
            {
                MessageBox.Show("El subtotal debe ser un valor numÃ©rico.","Dato incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            // CREAR OBJETO VENTA

            DbVentas venta = new DbVentas( 0, fechaVenta, cliente, metodoPago, subtotal);

            // GUARDAR VENTA
       
            int idVenta = venta.InsertarVenta();

            // Si devuelve 0 es porque ocurrió un error
            if (idVenta <= 0)
            {
                MessageBox.Show(
                    "No se pudo guardar la venta.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            // GUARDAR DETALLES DE PRODUCTOS
            foreach (DetalleVenta detalle in detallesVenta)
            {
                // Asignar el IdVenta generado
                detalle.IdVenta1 = idVenta;

                // Guardar detalle
                if (!detalle.InsertarDetalleVenta())
                {
                    MessageBox.Show(
                        "La venta fue creada, pero ocurrió un error "
                        + "al guardar uno de los detalles.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }
            }

            //  VALIDAR QUE TODO SE GUARDÃ“ CORRECTAMENTE
           
            MessageBox.Show(
                "Venta registrada correctamente.",
                "Registro exitoso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            // LIMPIAR FORMULARIO

            detallesVenta.Clear();

            cbCliente.SelectedIndex = -1;
            cbMetodoPago.SelectedIndex = -1;

            txtSubTotal.Clear();
            txtIVA.Clear();
            txtTotalPagar.Clear();

            dgvVentas.DataSource = null;

            // Actualizar el listado de ventas

            MostrarVentas();

        }

       





        private void txtIVA_TextChanged(object sender, EventArgs e)
        {
            CalcularTotal();
        }

        private void txtSubTotal_TextChanged(object sender, EventArgs e)
        {
            CalcularTotal();
        }

        private void CargarComboBoxClientes()
        {
            DataTable dtCliente = DbCliente.CargarTodosLosClientes();

            cbCliente.DataSource = dtCliente;
            cbCliente.DisplayMember = "NombreCliente";
            cbCliente.ValueMember = "IdCliente";
            cbCliente.SelectedIndex = -1;
        }

        private void CargarComboBoxMetodosDePago()
        {
            DataTable dtMetodoPago = MetodoPago.CargarMetodosDePago();

            cbMetodoPago.DataSource = dtMetodoPago;
            cbMetodoPago.DisplayMember = "MetodoPago";
            cbMetodoPago.ValueMember = "IdMetodoPago";
            cbMetodoPago.SelectedIndex = -1;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnAgregarProductos_Click(object sender, EventArgs e)
        {
           
            // ABRIR FORMULARIO DE DETALLE DE VENTA
           
            using (FrmDetalleVenta formulario =
                new FrmDetalleVenta())
            {
                // Abrir FrmDetalleVenta y esperar
                // a que el usuario agregue el producto
                if (formulario.ShowDialog() == DialogResult.OK)
                {
                    // RECIBIR EL PRODUCTO

                    // Obtener el detalle que se guardo en el 
                    // FrmDetalleVenta
                    DetalleVenta detalle =
                        formulario.DetalleSeleccionado;


                    // AGREGAR EL PRODUCTO A LA LISTA

                    // Guardamos temporalmente el producto
                    // en la lista de detalles
                    detallesVenta.Add(detalle);

                    // MOSTRAR LOS PRODUCTOS

                    // Actualizar el DataGridView
                    MostrarDetalles();
                }
            }
        }

       
            private void MostrarDetalles()
            {
            // MOSTRAR LISTA DE DETALLES
            
            // Quitar temporalmente el origen de datos
            dgvDetalleDeVenta.DataSource = null;


            // Asignar nuevamente la lista actualizada
            dgvDetalleDeVenta.DataSource = detallesVenta;

            // CALCULAR SUBTOTAL

            CalcularSubtotalVenta();
        }
        

        private void CalcularSubtotalVenta()
        {
            // CALCULAR SUBTOTAL
           
            decimal subtotal = 0;

            // Recorrer todos los productos agregados
            foreach (DetalleVenta detalle in detallesVenta)
            {
                // Calcular subtotal del producto
                decimal subtotalProducto =
                    detalle.Cantidad1 *
                    detalle.PrecioUnitario1;

                // Sumarlo al subtotal general
                subtotal += subtotalProducto;
            }

            // Mostrar subtotal
            txtSubTotal.Text =
                subtotal.ToString("0.00");

            // Calcular IVA y total
            CalcularTotal();
        }


        private void dgvVentas_SelectionChanged(object sender, EventArgs e)
        {

            if (dgvVentas.CurrentRow == null)
                return;

            int idVenta = Convert.ToInt32(
                dgvVentas.CurrentRow.Cells["IdVenta"].Value
            );

            dgvDetalleDeVenta.DataSource =DetalleVenta.CargarDetalleVenta(idVenta);
        }
       
    
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvVentas.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una venta de la tabla.");
                return;
            }
            
            int id = Convert.ToInt32(dgvVentas.CurrentRow.Cells["IdVenta"].Value);
            
            DialogResult res = MessageBox.Show("¿Está seguro de eliminar esta venta? Se eliminarán todos los detalles y facturas asociados de forma permanente.", "Confirmar Eliminación (Cascada)", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res == DialogResult.Yes)
            {
                Modelo.Entidades.DbVentas venta = new Modelo.Entidades.DbVentas();
                venta.IdVenta = id;
                if (venta.EliminarVenta())
                {
                    MessageBox.Show("Venta eliminada correctamente.");
                    MostrarVentas();
                }
                else
                {
                    MessageBox.Show("Error al eliminar la venta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
