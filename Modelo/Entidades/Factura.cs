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
     public class DbFactura
    {
        private int IdFactura;
        private DateTime FechaEmision;
        private DateTime FechaVencimiento;
        private int IdVenta;
        private string Observaciones;

        public DbFactura(int idFactura, DateTime fechaEmisión, DateTime fechaVencimiento, int venta, string observaciones)
        {
            IdFactura=idFactura;
            FechaEmision=fechaEmisión;
            FechaVencimiento=fechaVencimiento;
            IdVenta=venta;
            Observaciones=observaciones;
        }

        public int IdFactura1 { get => IdFactura; set => IdFactura = value; }
        public DateTime FechaEmisión1 { get => FechaEmision; set => FechaEmision = value; }
        public DateTime FechaVencimiento1 { get => FechaVencimiento; set => FechaVencimiento = value; }
        public int Venta1 { get => IdVenta; set => IdVenta = value; }
        public string Observaciones1 { get => Observaciones; set => Observaciones = value; }


        public static DataTable CargarVentasParaFactura()
        {
            SqlConnection conectar = Conexion.Conectar();
            string comando = "SELECT v.IdVenta, CAST(v.IdVenta AS VARCHAR) + ' - ' + c.Identificador1 + ' ' + ISNULL(c.Identificador2, '') AS Display FROM Venta v INNER JOIN Cliente c ON v.IdCliente = c.IdCliente LEFT JOIN Factura f ON v.IdVenta = f.IdVenta WHERE f.IdFactura IS NULL;";
            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public static DataTable CargarRegistrosFacturas()
        {
            SqlConnection conectar = Conexion.Conectar();

            string comando = "SELECT * FROM VerFacturas;";
            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public static DataTable CargarDetalleFacturas(int idVenta)
        {
            SqlConnection conectar = Conexion.Conectar();

            string comando = "SELECT * FROM DetalleDeFactura\r\nWHERE IdVenta=@IdVenta;";
            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);
            adapter.SelectCommand.Parameters.AddWithValue("@IdVenta", idVenta);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public void InsertarFactura()
        {
            string sql = @"INSERT INTO Factura
                       (
                           FechaEmision,
                           FechaVencimiento,
                           IdVenta,
                           Observaciones
                       )
                       VALUES
                       (
                           @FechaEmision,
                           @FechaVencimiento,
                           @IdVenta,
                           @Observaciones
                       )";

            using (SqlConnection cn = Conexion.Conectar())
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@FechaEmision", FechaEmision);
                    cmd.Parameters.AddWithValue("@FechaVencimiento", FechaVencimiento);
                    cmd.Parameters.AddWithValue("@IdVenta", IdVenta);
                    cmd.Parameters.AddWithValue("@Observaciones", Observaciones);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

}
