

using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_aplicaciones.Entidades
{
    public class Facturas
    {
        public int Id { get; set; }
        public int Orden { get; set; }
        public int Cliente { get; set; }
        public DateTime Fecha_Factura { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Impuesto { get; set; }
        public string? Estado { get; set; }
        public decimal Total { get; set; }


        [ForeignKey("Orden")] public Ordenes_Servicios? _Orden { get; set; }
        [ForeignKey("Cliente")] public Clientes? _Cliente { get; set; }

        public List<Pagos>? Pagos { get; set; }
    }
}
