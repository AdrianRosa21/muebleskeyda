using System;
using Modelo.Entidades;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vista.Dashboard;
using Vista.DashboardSecretario;

namespace Vista.Login
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
        }

        private void btnIngresar_Click_1(object sender, EventArgs e)
        {
        
            string usuario = txtUsuario.Text;
            string contraseña = txtContraseña.Text;


            // Usuario administrador
            if (usuario == "José.Avila" && contraseña == "MueblesKeyda")
            {
                frmDashboard form_principal = new frmDashboard();
                this.Hide();
                form_principal.Show();
            }

            // Usuario secretario
            else if (usuario == "Secretario" && contraseña == "Muebles")
            {
                frmDashboardSecretariocs fe = new frmDashboardSecretariocs();
                this.Hide();
                fe.Show();
            }

            // Datos incorrectos
            else
            {
                MessageBox.Show("Usuario o contraseña incorrecta");
            }
        }

        private void btnCerrarClientes_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

