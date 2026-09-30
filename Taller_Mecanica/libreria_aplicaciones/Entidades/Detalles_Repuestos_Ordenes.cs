

using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_aplicaciones.Entidades
{
    public class Detalles_Repuestos_Ordenes
    {
        public int Id { get; set; }
        public int Orden { get; set; }
        public int Repuesto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio_Unitario { get; set; }
        public decimal Subtotal { get; set; }



        [ForeignKey("Orden")] public Ordenes_Servicios? _Orden { get; set; }
        [ForeignKey("Repuesto")] public Repuestos? _Repuesto { get; set; }
    }
}
