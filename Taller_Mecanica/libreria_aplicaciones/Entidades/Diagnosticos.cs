

using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_aplicaciones.Entidades
{
    public class Diagnosticos
    {
        public int Id { get; set; }
        public int Orden { get; set; }
        public int Empleado { get; set; }
        public DateTime Fecha_Diagnostico { get; set; }
        public string? Descripcion_Problema { get; set; }
        public string? Observaciones { get; set; }

        [ForeignKey("Orden")] public Ordenes_Servicios? _Orden { get; set; }
        [ForeignKey("Empleado")] public Empleados? _Empleado { get; set; }

    }
}
