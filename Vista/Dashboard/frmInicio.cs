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
using Vista.Inventario;
using Vista.Pedidos;

namespace Vista.Dashboard
{
    public partial class frmInicio : Form
    {
        public frmInicio()
        {
            InitializeComponent();
        }

        private void AbrirFormulario(Form formulario)
        {
            pnlContenedor.Controls.Clear();

            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;

            pnlContenedor.Controls.Add(formulario);
            pnlContenedor.Tag = formulario;

            formulario.Show();
        }

        private void lblVerDetallesClientes_Click_1(object sender, EventArgs e)
        {
            AbrirFormulario(new frmClientes());
        }

        private void lblVerDetallesPedidos_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmPedidos());
        }

        private void lblVerDetallesInventario_Click_1(object sender, EventArgs e)
        {
            AbrirFormulario(new frmInventario());
        }

        private void MostrarPedidosRecientes()
        {
            dgvPedidosRecientes.DataSource = null;
            dgvPedidosRecientes.DataSource = DbPedidos.CargarPedidosRecientes();
        }

        private void frmInicio_Load(object sender, EventArgs e)
        {
            MostrarPedidosRecientes();
        }

        
    }
}

