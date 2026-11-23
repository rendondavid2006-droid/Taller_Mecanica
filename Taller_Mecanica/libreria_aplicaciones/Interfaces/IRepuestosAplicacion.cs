 
 

using libreria_aplicaciones.Entidades;

namespace libreria_aplicaciones.Interfaces
{
    public interface IRepuestosAplicacion
    {
        Repuestos Insertar(Repuestos entidad);
        Repuestos Actualizar(Repuestos entidad);
        List<Repuestos> Consultar();
        List<Repuestos> Buscar(Repuestos entidad);
        Repuestos Borrar(Repuestos entidad);
    }
}