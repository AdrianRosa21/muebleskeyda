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

namespace Vista.Iventario_Secretario
{
    public partial class frmInventarioSecretario : Form
    {
        public frmInventarioSecretario()
        {
            InitializeComponent();
        }

        private void txtBuscar_Enter(object sender, EventArgs e)
        {
            //Cuando el usuario de enter para escribir, se va a borrar el texto de indicacion
            // Y el texto ya no sera opaco, sera color negro
            if (txtBuscar.Text=="Buscar Material...")
            {
                txtBuscar.Text="";
                txtBuscar.ForeColor=Color.Black;

            }
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            txtBuscar.Text="Buscar Material...";
            txtBuscar.ForeColor=Color.Gray;
        }

        public void MostrarInventario()
        {
            dgvInventario.DataSource = null;
            dgvInventario.DataSource = Material.CargarMateriales();
        }

        private void frmInventarioSecretario_Load(object sender, EventArgs e)
        {
            MostrarInventario();
        }

       
    }
}
