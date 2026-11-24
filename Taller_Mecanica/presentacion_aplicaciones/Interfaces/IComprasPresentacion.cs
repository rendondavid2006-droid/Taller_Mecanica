 
 

using libreria_aplicaciones.Entidades;

namespace presentacion_aplicaciones.Interfaces
{
    public interface IComprasPresentacion
    {
        Compras Insertar(Compras entidad);
        Compras Actualizar(Compras entidad);
        List<Compras> Consultar();
        List<Compras> Buscar(Compras entidad);
        Compras Borrar(Compras entidad);
    }
}