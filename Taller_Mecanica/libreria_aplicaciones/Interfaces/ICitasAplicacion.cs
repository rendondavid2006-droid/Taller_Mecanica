 
 

using libreria_aplicaciones.Entidades;

namespace libreria_aplicaciones.Interfaces
{
    public interface ICitasAplicacion
    {
        Citas Insertar(Citas entidad);
        Citas Actualizar(Citas entidad);
        List<Citas> Consultar();
        List<Citas> Buscar(Citas entidad);
        Citas Borrar(Citas entidad);
    }
}