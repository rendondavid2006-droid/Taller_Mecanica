 
 

using libreria_aplicaciones.Entidades;

namespace libreria_aplicaciones.Interfaces
{
    public interface IDetalles_Repuestos_OrdenesAplicacion
    {
        Detalles_Repuestos_Ordenes Insertar(Detalles_Repuestos_Ordenes entidad);
        Detalles_Repuestos_Ordenes Actualizar(Detalles_Repuestos_Ordenes entidad);
        List<Detalles_Repuestos_Ordenes> Consultar();
        List<Detalles_Repuestos_Ordenes> Buscar(Detalles_Repuestos_Ordenes entidad);
        Detalles_Repuestos_Ordenes Borrar(Detalles_Repuestos_Ordenes entidad);
    }
}