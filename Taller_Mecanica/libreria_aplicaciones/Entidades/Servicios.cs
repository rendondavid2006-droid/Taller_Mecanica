

namespace libreria_aplicaciones.Entidades
{
    public class Servicios
    {
        public int Id { get; set; }
        public string? Nombre_Servicio { get; set; }
        public string? Descripcion { get; set; }
        public decimal Precio_Base { get; set; }
        public DateTime Tiempo_Estimado { get; set; }
        public bool Estado { get; set; }

        public List<Detalles_Ordenes_Servicios>? Detalles_Ordenes_Servicios { get; set; }
    }
}
