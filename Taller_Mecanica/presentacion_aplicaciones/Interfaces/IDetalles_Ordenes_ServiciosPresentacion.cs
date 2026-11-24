 
 

using libreria_aplicaciones.Entidades;

namespace presentacion_aplicaciones.Interfaces
{
    public interface IDetalles_Ordenes_ServiciosPresentacion
    {
        Detalles_Ordenes_Servicios Insertar(Detalles_Ordenes_Servicios entidad);
        Detalles_Ordenes_Servicios Actualizar(Detalles_Ordenes_Servicios entidad);
        List<Detalles_Ordenes_Servicios> Consultar();
        List<Detalles_Ordenes_Servicios> Buscar(Detalles_Ordenes_Servicios entidad);
        Detalles_Ordenes_Servicios Borrar(Detalles_Ordenes_Servicios entidad);
    }
}