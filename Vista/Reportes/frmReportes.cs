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



namespace Vista.Reportes
{
    public partial class frmReportes : Form
    {
        public frmReportes()
        {
            InitializeComponent();
            btnVentas.Cursor= Cursors.Default;
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            
             pnlReporteDeClientes.Visible = true;
            pnlReportesVentas.Visible = false;
            pnlBarraCambio.Visible = true;
            pnlBarraCambioVentas.Visible = false;


        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            pnlReporteDeClientes.Visible = false;
            pnlReportesVentas.Visible = true;
            pnlBarraCambioVentas.Visible = true;
            pnlBarraCambio.Visible = false;
        }

        public void CargarReporteClientes()
        {
            dgvReporteClientes.DataSource = null;
            dgvReporteClientes.DataSource = ReportesClientes.CargarReporteClientes();
        }

        public void CargarReporteVentas()
        {
            dgvReporteVentas.DataSource = null;
            dgvReporteVentas.DataSource = ReportesVentas.CargarReporteVentas();
        }

        private void frmReportes_Load(object sender, EventArgs e)
        {
            CargarReporteClientes();
            CargarReporteVentas();

            dtFechaFin.MaxDate = DateTime.Today;
            dtFechaInicio.MaxDate = DateTime.Today;
        }

      
    }
}

