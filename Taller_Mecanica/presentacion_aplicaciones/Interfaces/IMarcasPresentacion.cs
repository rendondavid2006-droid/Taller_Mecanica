 
 

using libreria_aplicaciones.Entidades;

namespace presentacion_aplicaciones.Interfaces
{
    public interface IMarcasPresentacion
    {
        Marcas Insertar(Marcas entidad);
        Marcas Actualizar(Marcas entidad);
        List<Marcas> Consultar();
        List<Marcas> Buscar(Marcas entidad);
        Marcas Borrar(Marcas entidad);
    }
}