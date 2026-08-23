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

namespace Vista.Categorías
{
    public partial class frmCategorias : Form
    {
        public frmCategorias()
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
            if (txtBuscarCategoria.Text== "")
            {
                txtBuscarCategoria.Text="Buscar Categoría...";
                txtBuscarCategoria.ForeColor=Color.Gray;
            }
        }

        private void frmCategorias_Load(object sender, EventArgs e)
        {
            MostrarCategorias();
            btnGuardarCambios.Visible = false;

        }

        public void MostrarCategorias()
        {
            dgvCategorias.DataSource = null;
            dgvCategorias.DataSource = Categorias.CargarCategorias();
        }
     


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Categorias categoria = new Categorias();

            categoria.Nombre_Categoria1 = txtCategoria.Text;
            categoria.Descripción1 = txtDescripcion.Text;
            categoria.Estado1 = cbEstado.Text;
            if (categoria.InsertarCategoria())
            {
                MessageBox.Show(
                    "Categoría registrada correctamente.",
                    "Registro exitoso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            MostrarCategorias();
        }
        private int idCategoriaSeleccionada = 0;
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idCategoriaSeleccionada == 0)
            {
                MessageBox.Show("No hay ninguna categoría seleccionada.");
                return;
            }

            DialogResult resultado = MessageBox.Show(
                "¿Está seguro de eliminar esta categoría?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (resultado == DialogResult.Yes)
            {
                Categorias categoria = new Categorias();

                categoria.IdCategoria1 = idCategoriaSeleccionada;

                if (categoria.EliminarCategoria())
                {
                    MessageBox.Show("Categoría eliminada correctamente.");

                    MostrarCategorias();

                    idCategoriaSeleccionada = 0;
                }
            }
            MostrarCategorias();
        
    }

       

        private void dgvCategorias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                idCategoriaSeleccionada = Convert.ToInt32(
                    dgvCategorias.Rows[e.RowIndex].Cells["IdCategoria"].Value
                );
            }
        }

        private void BloquearCampos()
        {
            txtCategoria.ReadOnly = true;
            txtDescripcion.ReadOnly = true;
            cbEstado.Enabled = false;
        }

        private void DesbloquearCampos()
        {
            txtCategoria.ReadOnly = false;
            txtDescripcion.ReadOnly = false;
            cbEstado.Enabled = true;
        }

        private void dgvCategorias_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                idCategoriaSeleccionada = Convert.ToInt32(
                    dgvCategorias.Rows[e.RowIndex].Cells["IdCategoria"].Value
                );

                txtCategoria.Text = dgvCategorias.Rows[e.RowIndex]
                    .Cells["Nombre_Categoria"].Value.ToString();

                txtDescripcion.Text = dgvCategorias.Rows[e.RowIndex]
                    .Cells["Descripcion"].Value.ToString();

                cbEstado.Text = dgvCategorias.Rows[e.RowIndex]
                    .Cells["Estado"].Value.ToString();

                // Mantener campos bloqueados
                BloquearCampos();

                // Mostrar Editar
                btnEditar.Visible = true;
                btnGuardarCambios.Visible = false;
            }
            
            }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (idCategoriaSeleccionada == 0)
            {
                MessageBox.Show("No hay ninguna categoría seleccionada.");
                return;
            }

            DesbloquearCampos();

            btnEditar.Visible = true;
            btnGuardarCambios.Visible = true;
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            if (idCategoriaSeleccionada == 0)
            {
                MessageBox.Show("No hay ninguna categoría seleccionada.");
                return;
            }

            Categorias categoria = new Categorias();

            categoria.IdCategoria1 = idCategoriaSeleccionada;
            categoria.Nombre_Categoria1 = txtCategoria.Text;
            categoria.Descripción1 = txtDescripcion.Text;
            categoria.Estado1 = cbEstado.Text;

            categoria.ActualizarCategoria();

            MessageBox.Show("Categoría actualizada correctamente.");

            MostrarCategorias();

            txtCategoria.Clear();
            txtDescripcion.Clear();
            cbEstado.SelectedIndex = -1;

            BloquearCampos();

            btnGuardarCambios.Visible = false;
            btnEditar.Visible = false;

            idCategoriaSeleccionada = 0;


        }
    }
    }

