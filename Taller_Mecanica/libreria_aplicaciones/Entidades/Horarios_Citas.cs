using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
namespace libreria_aplicaciones.Entidades
{
    public class Horarios_Citas
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public DateTime Hora_Inicio { get; set; }

        public DateTime Hora_Fin { get; set; }

        public bool Disponible { get; set; }

        [NotMapped]
        public string Horario_Descripcion
        {
            get
            {
                return Fecha.ToString("dd/MM/yyyy")
                    + " "
                    + Hora_Inicio.ToString("HH:mm")
                    + " - "
                    + Hora_Fin.ToString("HH:mm");
            }
        }

        [JsonIgnore]
        public List<Citas>? Citas { get; set; }
    }
}