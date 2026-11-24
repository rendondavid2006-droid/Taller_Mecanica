 
 

using libreria_aplicaciones.Entidades;

namespace presentacion_aplicaciones.Interfaces
{
    public interface IOrdenes_ServiciosPresentacion
    {
        Ordenes_Servicios Insertar(Ordenes_Servicios entidad);
        Ordenes_Servicios Actualizar(Ordenes_Servicios entidad);
        List<Ordenes_Servicios> Consultar();
        List<Ordenes_Servicios> Buscar(Ordenes_Servicios entidad);
        Ordenes_Servicios Borrar(Ordenes_Servicios entidad);
    }
}