 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace libreria_aplicaciones.Implementaciones
{
    public class Usuarios_ClientesAplicacion : IUsuarios_ClientesAplicacion
    {
        private IConexion IConexion;

        public Usuarios_ClientesAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

         
        public Usuarios_Clientes Login(Usuarios_Clientes entidad)
        {
            if (string.IsNullOrEmpty(entidad.Nombre_Usuario) ||
                string.IsNullOrEmpty(entidad.Contraseña))
            {
                return new Usuarios_Clientes();
            }

            string usuarioIngresado =
                entidad.Nombre_Usuario.Trim();

            string contraseñaIngresada =
                entidad.Contraseña.Trim();


            var usuario =
                this.IConexion.Usuarios_Clientes!
                    .Include(x => x._Cliente)
                    .Include(x => x._Rol)
                    .FirstOrDefault(x =>
                        x.Nombre_Usuario == usuarioIngresado &&
                        x.Contraseña == contraseñaIngresada &&
                        x.Estado == true);


            if (usuario == null)
                return new Usuarios_Clientes();


            return usuario;
        }

         
        public Usuarios_Clientes Actualizar(Usuarios_Clientes entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var entry =
                this.IConexion.Entry<Usuarios_Clientes>(entidad);

            entry.State = EntityState.Modified;

            this.IConexion.SaveChanges();

            Auditoria.Registrar(
                this.IConexion,
                "Usuarios_Clientes",
                entidad.Id,
                "UPDATE",
                "Se actualizó un usuario cliente"
            );

            return entidad;
        }

         
        public Usuarios_Clientes Borrar(Usuarios_Clientes entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Usuarios_Clientes!.Remove(entidad);
            this.IConexion.SaveChanges();

            Auditoria.Registrar(
                this.IConexion,
                "Usuarios_Clientes",
                entidad.Id,
                "DELETE",
                "Se eliminó un usuario cliente"
            );

            return entidad;
        }

         
        public List<Usuarios_Clientes> Buscar(
            Usuarios_Clientes entidad)
        {
            return this.IConexion.Usuarios_Clientes!
                .Include(x => x._Cliente)
                .Include(x => x._Rol)
                .Where(x =>
                    x.Nombre_Usuario!.Contains(
                        entidad.Nombre_Usuario ?? ""))
                .ToList();
        }

         
        public List<Usuarios_Clientes> Consultar()
        {
            return this.IConexion.Usuarios_Clientes!
                .Include(x => x._Cliente)
                .Include(x => x._Rol)
                .ToList();
        }

         
        public Usuarios_Clientes Insertar(
            Usuarios_Clientes entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.Usuarios_Clientes!.Add(entidad);
            this.IConexion.SaveChanges();

            Auditoria.Registrar(
                this.IConexion,
                "Usuarios_Clientes",
                entidad.Id,
                "INSERT",
                "Se creó un usuario cliente"
            );

            return entidad;
        }

         
        private bool Validar(Usuarios_Clientes entidad)
        {
            return entidad.Cliente <= 0 ||
                entidad.Rol <= 0 ||
                string.IsNullOrEmpty(entidad.Nombre_Usuario) ||
                string.IsNullOrEmpty(entidad.Contraseña) ||
                entidad.Fecha_Creacion == DateTime.MinValue;
        }
    }
}