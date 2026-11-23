 
 

using libreria_aplicaciones.Entidades;

namespace libreria_aplicaciones.Interfaces
{
    public interface IUsuariosAplicacion
    {
        Usuarios Insertar(Usuarios entidad);
        Usuarios Actualizar(Usuarios entidad);
        List<Usuarios> Consultar();
        List<Usuarios> Buscar(Usuarios entidad);
        Usuarios Borrar(Usuarios entidad);

        Usuarios Login(Usuarios entidad);
    }
}