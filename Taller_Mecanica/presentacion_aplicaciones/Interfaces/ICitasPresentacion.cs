 
 

using libreria_aplicaciones.Entidades;

namespace presentacion_aplicaciones.Interfaces
{
    public interface ICitasPresentacion
    {
        Citas Insertar(Citas entidad);
        Citas Actualizar(Citas entidad);
        List<Citas> Consultar();
        List<Citas> Buscar(Citas entidad);
        Citas Borrar(Citas entidad);
    }
}