

namespace libreria_aplicaciones.Entidades
{
    public class Proveedores
    {
        public int Id { get; set; }
        public string? Nit { get; set; }
        public string? Nombre_Proveedor { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public string? Direccion { get; set; }

        public List<Compras>? Compras { get; set; }
    }
}
