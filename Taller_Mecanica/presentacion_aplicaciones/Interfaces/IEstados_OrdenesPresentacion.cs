 
 

using libreria_aplicaciones.Entidades;

namespace presentacion_aplicaciones.Interfaces
{
    public interface IEstados_OrdenesPresentacion
    {
        Estados_Ordenes Insertar(Estados_Ordenes entidad);
        Estados_Ordenes Actualizar(Estados_Ordenes entidad);
        List<Estados_Ordenes> Consultar();
        List<Estados_Ordenes> Buscar(Estados_Ordenes entidad);
        Estados_Ordenes Borrar(Estados_Ordenes entidad);
    }
}