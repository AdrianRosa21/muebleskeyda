using Modelo.Conexión_DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Entidades
{
    public class DetallePedidos
    {
        private int IdDetallePedido;
        private int Cantidad;
        private double Largo;
        private double Ancho;
        private double Alto;
        private string MuebleARealizar;
        private string Descripcion;

        public DetallePedidos(int idDetallePedido, int cantidad, double largo, double ancho, double alto, string muebleARealizar, string descripcion)
        {
            IdDetallePedido=idDetallePedido;
            Cantidad=cantidad;
            Largo=largo;
            Ancho=ancho;
            Alto=alto;
            MuebleARealizar=muebleARealizar;
            Descripcion=descripcion;
        }

        public int IdDetallePedido1 { get => IdDetallePedido; set => IdDetallePedido=value; }
        public int Cantidad1 { get => Cantidad; set => Cantidad=value; }
        public double Largo1 { get => Largo; set => Largo=value; }
        public double Ancho1 { get => Ancho; set => Ancho=value; }
        public double Alto1 { get => Alto; set => Alto=value; }
        public string MuebleARealizar1 { get => MuebleARealizar; set => MuebleARealizar=value; }
        public string Descripcion1 { get => Descripcion; set => Descripcion=value; }

        public static DataTable CargarDetallesPedidos()
        {
            SqlConnection conectar = Conexion.Conectar();

            string comando = "SELECT * FROM DetallePedido;";
            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
    }
}
