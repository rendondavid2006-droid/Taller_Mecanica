using System.Text.Json.Serialization;

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

        [JsonIgnore] public List<Vehiculos>? Vehiculos { get; set; }
        [JsonIgnore] public List<Citas>? Citas { get; set; }
        [JsonIgnore] public List<Facturas>? Facturas { get; set; }

        [JsonIgnore] public List<Usuarios_Clientes>? Usuarios_Clientes { get; set; }
    }
}
