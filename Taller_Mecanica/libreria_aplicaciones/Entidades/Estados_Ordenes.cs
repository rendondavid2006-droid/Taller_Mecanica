

namespace libreria_aplicaciones.Entidades
{
    public class Estados_Ordenes
    {
        public int Id { get; set; }
        public string? Nombre_Estado { get; set; }
        public string? Descripcion { get; set; }
        public int Orden_Proceso { get; set; }
        public bool Estado { get; set; }

        public List<Ordenes_Servicios>? Ordenes_Servicios { get; set; }
    }
}
