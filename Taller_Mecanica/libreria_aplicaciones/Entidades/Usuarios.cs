

using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_aplicaciones.Entidades
{
    public class Usuarios
    {
        public int Id { get; set; }
        public int Empleado { get; set; }
        public string? Nombre_Usuario { get; set; }
        public string? Contraseña { get; set; }
        public bool Estado { get; set; }
        public DateTime Fecha_Creacion { get; set; }


        [ForeignKey("Empleado")] public Empleados? _Empleado { get; set; }
    }
}
