

using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_aplicaciones.Entidades
{
    public class Detalles_Ordenes_Servicios
    {
        public int Id { get; set; }
        public int Orden { get; set; }
        public int Servicio { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio_Unitario { get; set; }
        public decimal Subtotal { get; set; }


        [ForeignKey("Orden")] public Ordenes_Servicios? _Orden { get; set; }
        [ForeignKey("Servicio")] public Servicios? _Servicio { get; set; }
    }
}
