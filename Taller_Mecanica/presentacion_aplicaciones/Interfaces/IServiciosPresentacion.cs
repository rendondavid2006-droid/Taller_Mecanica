 
 

using libreria_aplicaciones.Entidades;

namespace presentacion_aplicaciones.Interfaces
{
    public interface IServiciosPresentacion
    {
        Servicios Insertar(Servicios entidad);
        Servicios Actualizar(Servicios entidad);
        List<Servicios> Consultar();
        List<Servicios> Buscar(Servicios entidad);
        Servicios Borrar(Servicios entidad);
    }
}