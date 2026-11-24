using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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

        [JsonIgnore] public List<Usuarios>? Usuarios { get; set; }
        [JsonIgnore] public List<Ordenes_Servicios>? Ordenes_Servicios { get; set; }
        [JsonIgnore] public List<Diagnosticos>? Diagnosticos { get; set; }
    }
}
