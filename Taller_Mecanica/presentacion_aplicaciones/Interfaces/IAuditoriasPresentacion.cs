 
 

using libreria_aplicaciones.Entidades;

namespace presentacion_aplicaciones.Interfaces
{
    public interface IAuditoriasPresentacion
    {
        List<Auditorias> Consultar();
        List<Auditorias> Buscar(Auditorias entidad);
    }
}
