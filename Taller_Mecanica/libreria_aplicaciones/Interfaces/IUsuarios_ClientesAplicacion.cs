 
 

using libreria_aplicaciones.Entidades;

namespace libreria_aplicaciones.Interfaces
{
    public interface IUsuarios_ClientesAplicacion
    {
        Usuarios_Clientes Insertar(Usuarios_Clientes entidad);
        Usuarios_Clientes Actualizar(Usuarios_Clientes entidad);
        List<Usuarios_Clientes> Consultar();
        List<Usuarios_Clientes> Buscar(Usuarios_Clientes entidad);
        Usuarios_Clientes Borrar(Usuarios_Clientes entidad);
        Usuarios_Clientes Login(Usuarios_Clientes entidad);
    }
}