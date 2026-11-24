 
 

using libreria_aplicaciones.Entidades;

namespace presentacion_aplicaciones.Interfaces
{
    public interface IDetalles_ComprasPresentacion
    {
        Detalles_Compras Insertar(Detalles_Compras entidad);
        Detalles_Compras Actualizar(Detalles_Compras entidad);
        List<Detalles_Compras> Consultar();
        List<Detalles_Compras> Buscar(Detalles_Compras entidad);
        Detalles_Compras Borrar(Detalles_Compras entidad);
    }
}