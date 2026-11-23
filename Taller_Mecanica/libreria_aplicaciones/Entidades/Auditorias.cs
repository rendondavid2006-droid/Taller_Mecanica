namespace libreria_aplicaciones.Entidades
{
    public class Auditorias
    {
        public int Id { get; set; }
        public string? Tabla { get; set; }
        public int RegistroId { get; set; }
        public string? Accion { get; set; }
        public DateTime Fecha { get; set; }
        public string? Descripcion { get; set; }
        public string? Usuario { get; set; }
    }
}
