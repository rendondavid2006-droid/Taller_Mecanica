 
 

using libreria_aplicaciones.Entidades;

namespace presentacion_aplicaciones.Interfaces
{
    public interface IEstados_CitasPresentacion
    {
        Estados_Citas Insertar(Estados_Citas entidad);
        Estados_Citas Actualizar(Estados_Citas entidad);
        List<Estados_Citas> Consultar();
        List<Estados_Citas> Buscar(Estados_Citas entidad);
        Estados_Citas Borrar(Estados_Citas entidad);
    }
}