

using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_aplicaciones.Entidades
{
    public class Pagos
    {
        public int Id { get; set; }
        public int Factura { get; set; }
        public DateTime Fecha_Pago { get; set; }
        public decimal Monto { get; set; }
        public string? Metodo_Pago { get; set; }
        public string? Referencia_Pago { get; set; }
        public string? Estado { get; set; }

        [ForeignKey("Factura")] public Facturas? _Factura { get; set; }
    }
}
