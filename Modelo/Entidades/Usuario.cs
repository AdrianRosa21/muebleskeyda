using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Entidades
{
    public class Usuario
    {
        private int IdUsuario;
        private string nombre_usuario;
        private string Contrasena;

        public Usuario(int idUsuario, string nombre_usuario, string contrasena)
        {
            IdUsuario1=idUsuario;
            this.Nombre_usuario=nombre_usuario;
            Contrasena1=contrasena;
        }

        public int IdUsuario1 { get => IdUsuario; set => IdUsuario=value; }
        public string Nombre_usuario { get => nombre_usuario; set => nombre_usuario=value; }
        public string Contrasena1 { get => Contrasena; set => Contrasena=value; }
    }
}
