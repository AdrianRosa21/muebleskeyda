using Modelo.Conexión_DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modelo.Entidades
{
    public class ComprasDb
    {
        private int IdCompra;
        private DateTime FechaCompra;
        private decimal TotalCompra;
        private int IdProveedor;

        public ComprasDb(int idCompra, DateTime fechaCompra, decimal totalCompra, int idProveedor)
        {
            IdCompra1=idCompra;
            FechaCompra1=fechaCompra;
            TotalCompra1=totalCompra;
            IdProveedor1=idProveedor;
        }

        public int IdCompra1 { get => IdCompra; set => IdCompra=value; }
        public DateTime FechaCompra1 { get => FechaCompra; set => FechaCompra=value; }
        public decimal TotalCompra1 { get => TotalCompra; set => TotalCompra=value; }
        public int IdProveedor1 { get => IdProveedor; set => IdProveedor=value; }

        public static DataTable CargarComprasRegistradas()
        {
            SqlConnection conectar = Conexion.Conectar();

            string comando = "SELECT * FROM VerCompras;";
            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public int InsertarCompra()
        {
            string comandoSQL = @"INSERT INTO Compras (FechaCompra,TotalCompra,IdProveedor)VALUES
            (@FechaCompra,@TotalCompra,@IdProveedor); SELECT CAST(SCOPE_IDENTITY() AS INT);"; //SELECT CAST(SCOPE_IDENTITY() AS INT); SIRVE PARA OBTENER EL ID QUE SLQ RECIENTEMENTE GUARDO

            using (SqlConnection conexion = Conexion.Conectar())
            {
                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    comandoObjeto.Parameters.AddWithValue("@FechaCompra", FechaCompra);

                    comandoObjeto.Parameters.AddWithValue("@IdProveedor", IdProveedor);

                    SqlParameter parametro = comandoObjeto.Parameters.Add( "@TotalCompra", SqlDbType.Decimal);
                    parametro.Precision = 10;
                    parametro.Scale = 2;
                    parametro.Value = TotalCompra;

                try
                {
                    int idCompra =
                        Convert.ToInt32( comandoObjeto.ExecuteScalar());

                    return idCompra;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show( "Ocurrió un error al guardar la compra.\n\n"+ ex.Message,"Error " + ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return 0;
                }
                }

            }
        }
    }
}
