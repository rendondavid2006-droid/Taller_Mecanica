
using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_aplicaciones.Entidades
{
    public class Detalles_Compras
    {
        public int Id { get; set; }
        public int Compra { get; set; }
        public int Repuesto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio_Unitario { get; set; }
        public decimal Subtotal { get; set; }



        [ForeignKey("Compra")] public Compras? _Compra { get; set; }
        [ForeignKey("Repuesto")] public Repuestos? _Repuesto { get; set; }
    }
}
