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

namespace Vista.Produccion_Secretario
{
    public partial class frmProduccionSecretario : Form
    {
        public frmProduccionSecretario()
        {
            InitializeComponent();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            frmEditarProduccionSecretario modal = new frmEditarProduccionSecretario();
            modal.ShowDialog();
        }

        private void txtBuscar_Enter(object sender, EventArgs e)
        {
            //Cuando el usuario de enter para escribir, se va a borrar el texto de indicacion
            // Y el texto ya no sera opaco, sera color negro
            if (txtBuscar.Text=="Buscar por código o nombre de cliente...")
            {
                txtBuscar.Text="";
                txtBuscar.ForeColor=Color.Black;

            }
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            txtBuscar.Text="Buscar por código o nombre de cliente...";
            txtBuscar.ForeColor=Color.Gray;
        }

        public void MostrarProduccion()
        {
            dgvProducción.DataSource = null;
            dgvProducción.DataSource = DbProducción.CargarProducción();
        }

        private void frmProduccionSecretario_Load(object sender, EventArgs e)
        {
            MostrarProduccion();
        }
    }
}
