using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_aplicaciones.Entidades
{
    public class Citas
    {
        public int Id { get; set; }

        public int Cliente { get; set; }

        public int Vehiculo { get; set; }

        public int Horario_Cita { get; set; }

        public string? Motivo { get; set; }

        public int Estado_Cita { get; set; }


        [ForeignKey("Cliente")]
        public Clientes? _Cliente { get; set; }


        [ForeignKey("Vehiculo")]
        public Vehiculos? _Vehiculo { get; set; }


        [ForeignKey("Horario_Cita")]
        public Horarios_Citas? _Horario_Cita { get; set; }


        [ForeignKey("Estado_Cita")]
        public Estados_Citas? _Estado_Cita { get; set; }
    }
}