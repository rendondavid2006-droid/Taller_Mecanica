using System.Text.Json.Serialization;

namespace libreria_aplicaciones.Entidades
{
    public class Estados_Citas
    {
        public int Id { get; set; }

        public string? Nombre_Estado { get; set; }

        public string? Descripcion { get; set; }

        public bool Estado { get; set; }


        [JsonIgnore]
        public List<Citas>? Citas { get; set; }
    }
}