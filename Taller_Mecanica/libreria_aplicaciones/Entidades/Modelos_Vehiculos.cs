

using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_aplicaciones.Entidades
{
    public class Modelos_Vehiculos
    {
        public int Id { get; set; }
        public int Marca { get; set; }
        public string? Nombre_Modelo { get; set; }
        public int Año_inicio { get; set; }
        public string? Tipo_Vehiculo { get; set; }
        public string? Cilindraje { get; set; }


        [ForeignKey("Marca")] public Marcas? _Marca { get; set; }

        public List<Vehiculos>? Vehiculos { get; set; }
    }
}
