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
    public class DbProveedor
    {
        private int IdProveedor;
        private string Nombre_Proveedor;
        private string Telefono;
        private string Correo;
        private string Ubicacion;

        public DbProveedor(int idProveedor, string nombre_Proveedor, string telefono, string correo, string ubicacion)
        {
            IdProveedor=idProveedor;
            Nombre_Proveedor=nombre_Proveedor;
            Telefono=telefono;
            Correo=correo;
            Ubicacion=ubicacion;
        }
        public DbProveedor() 
        {
        }

        public int IdProveedor1 { get => IdProveedor; set => IdProveedor = value; }
        public string Nombre_Proveedor1 { get => Nombre_Proveedor; set => Nombre_Proveedor = value; }
        public string Telefono1 { get => Telefono; set => Telefono = value; }
        public string Correo1 { get => Correo; set => Correo = value; }
        public string Ubicación1 { get => Ubicacion; set => Ubicacion = value; }

        public static DataTable CargarProveedor()
        {
            SqlConnection conectar = Conexion.Conectar();

            string comando = "SELECT * FROM Proveedor;";
            SqlDataAdapter adapter = new SqlDataAdapter(comando, conectar);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public bool InsertarProveedor()
        {
            string comandoSQL = "INSERT INTO Proveedor(Nombre_Proveedor,Telefono,Correo,Ubicacion)" +
                "VALUES (@Nombre_Proveedor,@Telefono,@Correo,@Ubicacion);";

            using (SqlConnection conexion = Conexion.Conectar())
            {
                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    comandoObjeto.Parameters.AddWithValue("@Nombre_Proveedor", Nombre_Proveedor);
                    comandoObjeto.Parameters.AddWithValue("@Telefono", Telefono);
                    comandoObjeto.Parameters.AddWithValue("@Correo", Correo);
                    comandoObjeto.Parameters.AddWithValue("@Ubicacion", Ubicacion);

                    try
                    {
                        int filaAfectada = comandoObjeto.ExecuteNonQuery();

                        return filaAfectada > 0;
                    }
                    catch (SqlException ex)
                    {
                        switch (ex.Number)
                        {
                            case 2627:
                            case 2601:

                                MessageBox.Show(
                                    "El proveedor ya existe en la base de datos. Por favor verifique los datos.",
                                    "Registro Duplicado",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                                break;

                            default:

                                MessageBox.Show(
                                    "Ocurrió un error inesperado en la base de datos " + ex.Message,
                                    "Error " + ex.Number,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                                break;
                        }

                        return false;
                    }
                }
            }
        }
        public bool ActualizarProveedor()
        {
            string comandoSQL = "UPDATE Proveedor SET Nombre_Proveedor = @Nombre, Telefono = @Telefono, Correo = @Correo WHERE IdProveedor = @IdProveedor";
            using (SqlConnection conexion = Conexion.Conectar())
            {
                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    comandoObjeto.Parameters.AddWithValue("@Nombre", Nombre_Proveedor);
                    comandoObjeto.Parameters.AddWithValue("@Telefono", Telefono);
                    comandoObjeto.Parameters.AddWithValue("@Correo", Correo);
                    comandoObjeto.Parameters.AddWithValue("@IdProveedor", IdProveedor);
                    
                    try { return comandoObjeto.ExecuteNonQuery() > 0; }
                    catch (SqlException) { return false; }
                }
            }
        }

        public static bool EliminarProveedor(int idProveedor)
        {
            using (SqlConnection conexion = Conexion.Conectar())
            {
                string comandoSQL = "DELETE FROM Proveedor WHERE IdProveedor = @IdProveedor";
                using (SqlCommand comandoObjeto = new SqlCommand(comandoSQL, conexion))
                {
                    comandoObjeto.Parameters.AddWithValue("@IdProveedor", idProveedor);
                    try { return comandoObjeto.ExecuteNonQuery() > 0; }
                    catch (SqlException) { return false; }
                }
            }
        }

        public bool ActualizarProveedor()
        {
            string comandoSQL = "UPDATE Proveedor SET Nombre_Proveedor = @Nombre, Telefono = @Telefono, Correo = @Correo, Ubicacion = @Ubicacion WHERE IdProveedor = @IdProveedor;";
            using (SqlConnection conexion = Conexion.Conectar())
            {
                using (SqlCommand cmd = new SqlCommand(comandoSQL, conexion))
                {
                    cmd.Parameters.AddWithValue("@IdProveedor", IdProveedor1);
                    cmd.Parameters.AddWithValue("@Nombre", Nombre_Proveedor1);
                    cmd.Parameters.AddWithValue("@Telefono", Telefono1);
                    cmd.Parameters.AddWithValue("@Correo", Correo1);
                    cmd.Parameters.AddWithValue("@Ubicacion", Ubicación1);
                    try
                    {
                        return cmd.ExecuteNonQuery() > 0;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }
        }
        
        public bool EliminarProveedor()
        {
            string comandoSQL = "DELETE FROM Proveedor WHERE IdProveedor = @IdProveedor;";
            using (SqlConnection conexion = Conexion.Conectar())
            {
                using (SqlCommand cmd = new SqlCommand(comandoSQL, conexion))
                {
                    cmd.Parameters.AddWithValue("@IdProveedor", IdProveedor1);
                    try
                    {
                        return cmd.ExecuteNonQuery() > 0;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }
        }
    }
}
