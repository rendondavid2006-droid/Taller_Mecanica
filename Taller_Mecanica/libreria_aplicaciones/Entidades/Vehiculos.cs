

using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_aplicaciones.Entidades
{
    public class Vehiculos
    {
        public int Id { get; set; }
        public int Cliente { get; set; }
        public int Modelo { get; set; }
        public string? Placa { get; set; }
        public int Año { get; set; }
        public string? Color { get; set; }
        public string? Kilometraje { get; set; }


        [ForeignKey("Cliente")] public Clientes? _Cliente { get; set; }
        [ForeignKey("Modelo")] public Modelos_Vehiculos? _Modelo { get; set; }

        public List<Citas>? Citas { get; set; }
        public List<Ordenes_Servicios>? Ordenes_Servicios { get; set; }

    }
}
