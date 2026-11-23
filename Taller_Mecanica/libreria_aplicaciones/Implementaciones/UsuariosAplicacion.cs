 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace libreria_aplicaciones.Implementaciones
{
    public class UsuariosAplicacion : IUsuariosAplicacion
    {
        private IConexion IConexion;

        public UsuariosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

         
        public Usuarios Actualizar(Usuarios entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion!.Entry<Usuarios>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Usuarios", entidad.Id, "UPDATE", "Se actualizó un registro de usuario");
            return entidad;
        }
         
        public Usuarios Borrar(Usuarios entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Usuarios!.Remove(entidad!);
            this.IConexion.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Usuarios", entidad.Id, "DELETE", "Se eliminó un registro de usuario");
            return entidad;
        }

         
        public List<Usuarios> Buscar(Usuarios entidad)
        {
            return this.IConexion.Usuarios!
                .Where(x => x.Nombre_Usuario!.Contains(entidad.Nombre_Usuario!))
                .ToList();
        }

         
        public List<Usuarios> Consultar()
        {
            return this.IConexion.Usuarios!
                .Include(x => x._Empleado)
                .ToList();
        }

         
        public Usuarios Insertar(Usuarios entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.Usuarios!.Add(entidad!);
            this.IConexion.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Usuarios", entidad.Id, "INSERT", "Se creó un registro de usuario");
            return entidad;
        }

         
        private bool Validar(Usuarios entidad)
        {
            return entidad.Empleado <= 0 ||
                string.IsNullOrEmpty(entidad.Nombre_Usuario) ||
                string.IsNullOrEmpty(entidad.Contraseña) ||
                entidad.Fecha_Creacion == default;
        }

         
        public Usuarios Login(Usuarios entidad)
        {
            if (string.IsNullOrEmpty(entidad.Nombre_Usuario) ||
                string.IsNullOrEmpty(entidad.Contraseña))
            {
                return new Usuarios();
            }

            var usuario = this.IConexion.Usuarios!
                .Include(x => x._Empleado)
                .ThenInclude(x => x!._Rol)
                .FirstOrDefault(x =>
                    x.Nombre_Usuario == entidad.Nombre_Usuario &&
                    x.Contraseña == entidad.Contraseña &&
                    x.Estado == true);

            return usuario ?? new Usuarios();
        }
    }
}