using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_aplicaciones.Entidades
{
    public class Permisos
    {
        public int Id { get; set; }

        public int Rol { get; set; }

        public string Nombre_Permiso { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public bool Estado { get; set; }

        [ForeignKey("Rol")] public Roles? _Rol { get; set; }
    }
}