 
 

using libreria_aplicaciones.Entidades;

namespace presentacion_aplicaciones.Interfaces
{
    public interface IPermisosPresentacion
    {
        Permisos Insertar(Permisos entidad);
        Permisos Actualizar(Permisos entidad);
        List<Permisos> Consultar();
        List<Permisos> Buscar(Permisos entidad);
        Permisos Borrar(Permisos entidad);
        List<Permisos> ConsultarPorRol(int rol);
    }
}