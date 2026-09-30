

using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_aplicaciones.Entidades
{
    public class Compras
    {
        public int Id { get; set; }
        public int Proveedor { get; set; }
        public DateTime Fecha_Compra { get; set; }
        public string? Numero_Factura_Proveedor { get; set; }
        public decimal Total_Compra { get; set; }
        public string? Estado { get; set; }



        [ForeignKey("Proveedor")] public Proveedores? _Proveedor { get; set; }

        public List<Detalles_Compras>? Detalles_Compras { get; set; }
    }
}
