

using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_aplicaciones.Entidades
{
    public class Citas
    {
        public int Id { get; set; }
        public int Cliente { get; set; }
        public int Vehiculo { get; set; }
        public DateTime Fecha_Cita { get; set; }
        public DateTime Hora_Cita { get; set; }
        public string? Motivo { get; set; }
        public string? Estado { get; set; }



        [ForeignKey("Cliente")] public Clientes? _Cliente { get; set; }
        [ForeignKey("Vehiculo")] public Vehiculos? _Vehiculo { get; set; }

        public List<Ordenes_Servicios>? Ordenes_Servicios { get; set; }
    }
}
