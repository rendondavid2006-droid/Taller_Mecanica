 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace libreria_aplicaciones.Implementaciones
{
    public class RolesAplicacion : IRolesAplicacion
    {
        private IConexion IConexion;

        public RolesAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

         
        public Roles Actualizar(Roles entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion!.Entry<Roles>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Roles", entidad.Id, "UPDATE", "Se actualizó un registro de rol");
            return entidad;
        }
        

         
        public Roles Borrar(Roles entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Roles!.Remove(entidad!);
            this.IConexion.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Roles", entidad.Id, "DELETE", "Se eliminó un registro de rol");
            return entidad;
        }

         
        public List<Roles> Buscar(Roles entidad)
        {
            return this.IConexion.Roles!
                .Where(x => x.Nombre_Rol!.Contains(entidad.Nombre_Rol!))
                .ToList();
        }

         
        public List<Roles> Consultar()
        {
            return this.IConexion.Roles!
                .ToList();
        }

         
        public Roles Insertar(Roles entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.Roles!.Add(entidad!);
            this.IConexion.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Roles", entidad.Id, "INSERT", "Se creó un registro de rol");
            return entidad;
        }

         
        private bool Validar(Roles entidad)
        {
            return string.IsNullOrEmpty(entidad.Nombre_Rol) ||
                string.IsNullOrEmpty(entidad.Descripcion) ||
                entidad.Nivel_Acceso <= 0;
        }
    }
}