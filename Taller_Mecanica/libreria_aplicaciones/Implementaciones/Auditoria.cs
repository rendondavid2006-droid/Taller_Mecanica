 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;

namespace libreria_aplicaciones.Implementaciones
{
    public static class Auditoria
    {
         
        public static void Registrar(IConexion conexion, string tabla,int registroId, string accion, string descripcion)
        {
            if (descripcion.Length > 200)
                descripcion = descripcion.Substring(0, 200);

            var auditoria = new Auditorias
            {
                Tabla = tabla,
                RegistroId = registroId,
                Accion = accion,
                Fecha = DateTime.Now,
                Descripcion = descripcion,
                Usuario = conexion.UsuarioActual ?? "Sistema"
            };

            conexion.Auditorias!.Add(auditoria);
            conexion.SaveChanges();
        }
    }
}
