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

namespace Vista.Clientes
{
    public partial class frmBuscarCliente : Form
    {
        public frmBuscarCliente()
        {
            InitializeComponent();
        }


        public int IdClienteSeleccionado { get; private set; }
        public string NombreClienteSeleccionado { get; private set; }
        public string TelefonoClienteSeleccionado { get; private set; }

        public string CorreoClienteSeleccionado { get; private set; }

        public string DireccionClienteSeleccionado { get; private set; }


        private void MostrarClientes()
        {

            dgvClientes.DataSource = DbCliente.CargarClientesParaSeleccionar();

            dgvClientes.Columns["IdCliente"].Visible = false;

            dgvClientes.Columns["Cliente"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }



        private void frmBuscarCliente_Load(object sender, EventArgs e)
        {
            MostrarClientes();
        }

        private void btnSeleccionarCliente_Click(object sender, EventArgs e)
        {
            if (dgvClientes.CurrentRow == null)
            {
                MessageBox.Show("Selecciona un cliente.");
                return;
            }

            DataGridViewRow fila = dgvClientes.CurrentRow;

            IdClienteSeleccionado =
                Convert.ToInt32(fila.Cells["IdCliente"].Value);

            NombreClienteSeleccionado =
                fila.Cells["Cliente"].Value.ToString();

            TelefonoClienteSeleccionado =
                fila.Cells["Telefono"].Value.ToString();

            CorreoClienteSeleccionado =
                fila.Cells["Correo"].Value.ToString();

            DireccionClienteSeleccionado =
                fila.Cells["Direccion"].Value.ToString();

            DialogResult = DialogResult.OK;

            Close();
        }
    }
}

