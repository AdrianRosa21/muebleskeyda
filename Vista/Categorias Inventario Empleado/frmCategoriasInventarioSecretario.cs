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

namespace Vista.Categorias_Inventario_Empleado
{
    public partial class frmCategoriasInventarioSecretario : Form
    {
        public frmCategoriasInventarioSecretario()
        {
            InitializeComponent();
        }

       

        private void txtBuscarCategoria_Enter(object sender, EventArgs e)
        {
            //Cuando el usuario de enter para escribir, se va a borrar el texto de indicacion
            // Y el texto ya no sera opaco, sera color negro
            if (txtBuscarCategoria.Text=="Buscar Categoría...")
            {
                txtBuscarCategoria.Text="";
                txtBuscarCategoria.ForeColor=Color.Black;

            }


        }

        private void txtBuscarCategoria_Leave(object sender, EventArgs e)
        {
            txtBuscarCategoria.Text="Buscar Categoría...";
            txtBuscarCategoria.ForeColor=Color.Gray;
        }

        public void MostrarCategorias()
        {
            dgvCategorias.DataSource = null;
            dgvCategorias.DataSource = Categorias.CargarCategorias();
        }

        private void frmCategoriasInventarioSecretario_Load(object sender, EventArgs e)
        {
            MostrarCategorias();
        }
    }
}
