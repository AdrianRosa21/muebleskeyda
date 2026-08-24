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

namespace Vista.Inventario
{
    public partial class frmInventario : Form
    {
        public frmInventario()
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

        private void MostrarInventario()
        {
            dgvMateriales.DataSource = null;
            dgvMateriales.DataSource = Material.CargarMateriales();
        }

        private void frmInventario_Load(object sender, EventArgs e)
        {
            MostrarInventario();
            CargarComboBoxCategorias();
            btnGuardarCambios.Visible = false;
        }

        private void CargarComboBoxCategorias()
        {
            //Llamar al metodo de las categorias
            DataTable dtCategoria = Categorias.CargarCategorias();
            cbCategorias.DataSource = dtCategoria;
            cbCategorias.DisplayMember = "Nombre_Categoria";
            cbCategorias.ValueMember = "IdCategoria";
            cbCategorias.SelectedIndex = -1;
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idMaterialSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un material primero.");
                return;
            }
            DialogResult resultado = MessageBox.Show("¿Está seguro de eliminar este material?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (resultado == DialogResult.Yes)
            {
                Material material = new Material();
                material.idMaterial1 = idMaterialSeleccionado;
                if (material.EliminarMaterial())
                {
                    MessageBox.Show("Material eliminado correctamente.");
                    MostrarInventario();
                    
                    idMaterialSeleccionado = 0;
                    txtMaterial.Clear();
                    txtCantidad.Clear();
                    cbUnidadMedida.SelectedIndex = -1;
                    cbCategorias.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("No se puede eliminar el material porque está siendo utilizado en producciones.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //INSERT
            Material material = new Material();

            material.idMaterial1 = 0 ;
            material.NombreDelMaterial1 = txtMaterial.Text;
            material.UnidadDeMedida1 = cbUnidadMedida.Text;
            material.Stock1 = 0;
            material.Categoria1 = cbCategorias.Text;

            bool resultado = material.InsertarMateriales();

            if (resultado)
            {
                MessageBox.Show("Material registrado correctamente.", "Registro exitoso",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            MostrarInventario();


        }
        private int idMaterialSeleccionado = 0;
        private void dgvInventario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (idMaterialSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un material primero.");
                return;
            }
            HabilitarCampos();



        }
        private void BloquearCampos()
        {
            txtMaterial.ReadOnly = true;
            cbUnidadMedida.Enabled = false;
            cbCategorias.Enabled = false;
            txtCantidad.ReadOnly = true;

            btnEditar.Visible = true;
            btnGuardar.Visible = false;

            btnGuardarCambios.Visible=true;
        }

        private void HabilitarCampos()
        {
            txtMaterial.ReadOnly = false;
            cbUnidadMedida.Enabled = true;
            cbCategorias.Enabled = true;
            txtCantidad.ReadOnly = false;

            btnEditar.Visible = true;
            btnGuardar.Visible = false;

            btnGuardarCambios.Visible=true;
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            if (idMaterialSeleccionado == 0)
            {
                MessageBox.Show("No hay ningun material seleccionado.");
                return;
            }

            Material material = new Material();

            material.idMaterial1 = idMaterialSeleccionado;
            material.NombreDelMaterial1 = txtMaterial.Text;
            material.UnidadDeMedida1 = cbUnidadMedida.Text;
            material.Stock1 = Convert.ToInt32(txtCantidad.Text);
            material.Categoria1 = cbCategorias.Text;

            if (material.ActualizarMaterial())
            {
                MessageBox.Show("Material actualizado correctamente.");

                MostrarInventario();

                btnGuardarCambios.Visible = false;
                btnEditar.Visible = true;
                btnGuardar.Visible = true;

                txtMaterial.ReadOnly = true;
                cbUnidadMedida.Enabled = false;
                txtCantidad.ReadOnly = true;
                cbCategorias.Enabled = false;

                idMaterialSeleccionado = 0;
            }
        }

        private void dgvMateriales_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvMateriales.Rows[e.RowIndex].IsNewRow)
                return;

            DataGridViewRow fila = dgvMateriales.Rows[e.RowIndex];

            idMaterialSeleccionado = Convert.ToInt32(fila.Cells["IdMaterial"].Value);

            txtMaterial.Text =fila.Cells["Material"].Value?.ToString() ?? "";

            string idUnidadStr = "";
            if (dgvMateriales.Columns.Contains("IdUnidadDeMedida")) idUnidadStr = fila.Cells["IdUnidadDeMedida"].Value?.ToString();
            else if (dgvMateriales.Columns.Contains("UnidadDeMedida")) idUnidadStr = fila.Cells["UnidadDeMedida"].Value?.ToString();
            else if (dgvMateriales.Columns.Contains("Unidad_De_Medida")) idUnidadStr = fila.Cells["Unidad_De_Medida"].Value?.ToString();
            string unidadTexto = "";
            if (idUnidadStr == "1") unidadTexto = "Centimetros";
            else if (idUnidadStr == "2") unidadTexto = "Metros";
            else if (idUnidadStr == "3") unidadTexto = "Piezas";
            else if (idUnidadStr == "4") unidadTexto = "Pulgadas";
            else if (idUnidadStr == "5") unidadTexto = "Litros";
            else if (idUnidadStr == "6") unidadTexto = "Galones";
            else if (idUnidadStr == "7") unidadTexto = "Libras";
            else unidadTexto = idUnidadStr;
            cbUnidadMedida.Text = unidadTexto;

            txtCantidad.Text =fila.Cells["Stock"].Value?.ToString() ?? "";

            cbCategorias.Text =fila.Cells["Categoria"].Value?.ToString() ?? "";

            btnEditar.Visible = true;
            btnGuardar.Visible = true;

            BloquearCampos();
        }
    }
}




