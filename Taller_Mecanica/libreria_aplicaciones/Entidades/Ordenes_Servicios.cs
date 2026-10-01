

using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_aplicaciones.Entidades
{
    public class Ordenes_Servicios
    {
        public int Id { get; set; }
        public int Cita { get; set; }
        public int Vehiculo { get; set; }
        public int Empleado { get; set; }
        public int Estado_Orden { get; set; }
        public DateTime Fecha_Ingreso { get; set; }
        public string? Kilometraje_Ingreso { get; set; }
        public string? Observaciones { get; set; }



        [ForeignKey("Cita")] public Citas? _Cita { get; set; }
        [ForeignKey("Vehiculo")] public Vehiculos? _Vehiculo { get; set; }
        [ForeignKey("Empleado")] public Empleados? _Empleado { get; set; }
        [ForeignKey("Estado_Orden")] public Estados_Ordenes? _EstadoOrden { get; set; }

        public List<Diagnosticos>? Diagnosticos { get; set; }
        public List<Detalles_Ordenes_Servicios>? Detalles_Ordenes_Servicios { get; set; }
        public List<Detalles_Repuestos_Ordenes>? Detalles_Repuestos_Ordenes { get; set; }
        public List<Facturas>? Facturas { get; set; }
    }
}
