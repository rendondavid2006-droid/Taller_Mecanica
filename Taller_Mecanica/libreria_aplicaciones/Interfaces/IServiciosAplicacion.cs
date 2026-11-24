 
 

using libreria_aplicaciones.Entidades;

namespace libreria_aplicaciones.Interfaces
{
    public interface IServiciosAplicacion
    {
        Servicios Insertar(Servicios entidad);
        Servicios Actualizar(Servicios entidad);
        List<Servicios> Consultar();
        List<Servicios> Buscar(Servicios entidad);
        Servicios Borrar(Servicios entidad);
    }
}