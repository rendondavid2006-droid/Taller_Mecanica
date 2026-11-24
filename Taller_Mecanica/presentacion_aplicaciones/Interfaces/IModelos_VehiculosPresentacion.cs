 
 

using libreria_aplicaciones.Entidades;

namespace presentacion_aplicaciones.Interfaces
{
    public interface IModelos_VehiculosPresentacion
    {
        Modelos_Vehiculos Insertar(Modelos_Vehiculos entidad);
        Modelos_Vehiculos Actualizar(Modelos_Vehiculos entidad);
        List<Modelos_Vehiculos> Consultar();
        List<Modelos_Vehiculos> Buscar(Modelos_Vehiculos entidad);
        Modelos_Vehiculos Borrar(Modelos_Vehiculos entidad);
    }
}