using System.Text.Json.Serialization;

namespace libreria_aplicaciones.Entidades
{
    public class Roles
    {
        public int Id { get; set; }
        public string? Nombre_Rol { get; set; }
        public string? Descripcion { get; set; }
        public int Nivel_Acceso { get; set; }
        public bool Estado { get; set; }

        [JsonIgnore] public List<Permisos>? Permisos { get; set; }
        [JsonIgnore] public List<Empleados>? Empleados { get; set; }
        [JsonIgnore] public List<Usuarios_Clientes>? Usuarios_Clientes { get; set; }

    }
}
