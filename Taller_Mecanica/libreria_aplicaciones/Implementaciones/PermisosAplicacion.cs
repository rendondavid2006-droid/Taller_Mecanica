 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace libreria_aplicaciones.Implementaciones
{
    public class PermisosAplicacion : IPermisosAplicacion
    {
        private IConexion IConexion;

        public PermisosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

         
        public Permisos Actualizar(Permisos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion!.Entry<Permisos>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();

            Auditoria.Registrar(
                this.IConexion,
                "Permisos",
                entidad.Id,
                "UPDATE",
                "Se actualizó un registro de permiso"
            );

            return entidad;
        }

         
        public Permisos Borrar(Permisos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Permisos!.Remove(entidad);
            this.IConexion.SaveChanges();

            Auditoria.Registrar(
                this.IConexion,
                "Permisos",
                entidad.Id,
                "DELETE",
                "Se eliminó un registro de permiso"
            );

            return entidad;
        }

         
        public List<Permisos> Buscar(Permisos entidad)
        {
            return this.IConexion.Permisos!
                .Where(x => x.Nombre_Permiso!.Contains(entidad.Nombre_Permiso!))
                .ToList();
        }

         
        public List<Permisos> Consultar()
        {
            return this.IConexion.Permisos!
                .ToList();
        }

        public List<Permisos> ConsultarPorRol(int rol)
        {
            return this.IConexion.Permisos!
                .Where(x =>
                    x.Rol == rol &&
                    x.Estado == true)
                .ToList();
        }

         
        public Permisos Insertar(Permisos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.Permisos!.Add(entidad);
            this.IConexion.SaveChanges();

            Auditoria.Registrar(
                this.IConexion,
                "Permisos",
                entidad.Id,
                "INSERT",
                "Se creó un registro de permiso"
            );

            return entidad;
        }

         
        private bool Validar(Permisos entidad)
        {
            return entidad.Rol <= 0 ||
                string.IsNullOrEmpty(entidad.Nombre_Permiso) ||
                string.IsNullOrEmpty(entidad.Descripcion);
        }
    }
}
