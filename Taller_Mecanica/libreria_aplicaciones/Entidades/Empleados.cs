

using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_aplicaciones.Entidades
{
    public class Empleados
    {
        public int Id { get; set; }
        public int Rol { get; set; }
        public string? Documento { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }


        [ForeignKey("Rol")] public Roles? _Rol { get; set; }

        public List<Usuarios>? Usuarios { get; set; }
        public List<Ordenes_Servicios>? Ordenes_Servicios { get; set; }
        public List<Diagnosticos>? Diagnosticos { get; set; }
    }
}
