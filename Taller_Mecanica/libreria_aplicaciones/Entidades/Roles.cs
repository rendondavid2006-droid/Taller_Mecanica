

namespace libreria_aplicaciones.Entidades
{
    public class Roles
    {
        public int Id { get; set; }
        public string? Nombre_Rol { get; set; }
        public string? Descripcion { get; set; }
        public int Nivel_Acceso { get; set; }
        public bool Estado { get; set; }

        public List<Empleados>? Empleados { get; set; }

    }
}
