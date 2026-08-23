using System;
using Modelo.Entidades;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vista.Pedidos
{
    public partial class frmDetallePedido : Form
    {
        public frmDetallePedido()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        public string Largo { get; private set; }
        public string Ancho { get; private set; }
        public string Alto { get; private set; }
        public string Observaciones { get; private set; }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Largo = txtLargo.Text;
            Ancho = textBox3.Text;
            Alto = txtAlto.Text;
            Observaciones = txtObservaciones.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}


