

namespace libreria_aplicaciones.Entidades
{
    public class Marcas
    {
        public int Id { get; set; }
        public string? Nombre_Marca { get; set; }
        public string? Pais_Origen { get; set; }
        public string? Descripcion { get; set; }
        public bool Estado { get; set; }

        public List<Modelos_Vehiculos>? Modelos_Vehiculos { get; set; }
    }
}
