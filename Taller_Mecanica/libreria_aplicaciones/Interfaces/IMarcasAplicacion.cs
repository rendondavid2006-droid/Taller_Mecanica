 
 

using libreria_aplicaciones.Entidades;

namespace libreria_aplicaciones.Interfaces
{
    public interface IMarcasAplicacion
    {
        Marcas Insertar(Marcas entidad);
        Marcas Actualizar(Marcas entidad);
        List<Marcas> Consultar();
        List<Marcas> Buscar(Marcas entidad);
        Marcas Borrar(Marcas entidad);
    }
}