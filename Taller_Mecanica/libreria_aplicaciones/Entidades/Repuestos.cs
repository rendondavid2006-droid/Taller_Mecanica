

namespace libreria_aplicaciones.Entidades
{
    public class Repuestos
    {
        public int Id { get; set; }
        public string? Nombre_Repuesto { get; set; }
        public string? Descripcion { get; set; }
        public decimal Precio_Compra { get; set; }
        public decimal Precio_Venta { get; set; }
        public int Stock { get; set; }
        public int Stock_Minimo { get; set; }

        public List<Detalles_Repuestos_Ordenes>? Detalles_Repuestos_Ordenes { get; set; }
        public List<Detalles_Compras>? Detalles_Compras { get; set; }

    }
}
