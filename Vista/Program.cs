using System;
using Modelo.Entidades;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vista.Clientes;
using Vista.Dashboard;
using Vista.DashboardSecretario;
using Vista.Inventario;
using Vista.Login;
using Vista.Pedidos;
using Vista.Producción;
using Vista.Reportes;

namespace Vista
{
    public static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmProduccion());
        }
    }
}
