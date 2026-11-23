 
 

using libreria_aplicaciones.Entidades;

namespace libreria_aplicaciones.Interfaces
{
    public interface IVehiculosAplicacion
    {
        Vehiculos Insertar(Vehiculos entidad);
        Vehiculos Actualizar(Vehiculos entidad);
        List<Vehiculos> Consultar();
        List<Vehiculos> Buscar(Vehiculos entidad);
        Vehiculos Borrar(Vehiculos entidad);
    }
}