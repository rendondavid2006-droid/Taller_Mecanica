using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_aplicaciones.Entidades
{
    public class Usuarios_Clientes
    {
        public int Id { get; set; }

        public int Cliente { get; set; }

        public int Rol { get; set; }

        public string? Nombre_Usuario { get; set; }

        public string? Contraseña { get; set; }

        public bool Estado { get; set; }

        public DateTime Fecha_Creacion { get; set; }


        [ForeignKey("Cliente")]
        public Clientes? _Cliente { get; set; }


        [ForeignKey("Rol")]
        public Roles? _Rol { get; set; }
    }
}