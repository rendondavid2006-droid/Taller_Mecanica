 
 

using libreria_aplicaciones.Entidades;

namespace libreria_aplicaciones.Interfaces
{
    public interface IPermisosAplicacion
    {
        Permisos Insertar(Permisos entidad);
        Permisos Actualizar(Permisos entidad);
        List<Permisos> Consultar();
        List<Permisos> Buscar(Permisos entidad);
        Permisos Borrar(Permisos entidad);
        List<Permisos> ConsultarPorRol(int rol);
    }
}