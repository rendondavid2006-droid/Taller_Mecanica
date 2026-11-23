 
 

using libreria_aplicaciones.Entidades;

namespace libreria_aplicaciones.Interfaces
{
    public interface IEstados_OrdenesAplicacion
    {
        Estados_Ordenes Insertar(Estados_Ordenes entidad);
        Estados_Ordenes Actualizar(Estados_Ordenes entidad);
        List<Estados_Ordenes> Consultar();
        List<Estados_Ordenes> Buscar(Estados_Ordenes entidad);
        Estados_Ordenes Borrar(Estados_Ordenes entidad);
    }
}