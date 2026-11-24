 
 

using libreria_aplicaciones.Entidades;

namespace presentacion_aplicaciones.Interfaces
{
    public interface IPagosPresentacion
    {
        Pagos Insertar(Pagos entidad);
        Pagos Actualizar(Pagos entidad);
        List<Pagos> Consultar();
        List<Pagos> Buscar(Pagos entidad);
        Pagos Borrar(Pagos entidad);
    }
}