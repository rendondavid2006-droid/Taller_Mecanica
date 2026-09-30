

namespace libreria_aplicaciones.Entidades
{
    public class Clientes
    {
        public int Id { get; set; }
        public string? Documento { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public string? Direccion { get; set; }

        public List<Vehiculos>? Vehiculos { get; set; }
        public List<Citas>? Citas { get; set; }
        public List<Facturas>? Facturas { get; set; }
    }
}
