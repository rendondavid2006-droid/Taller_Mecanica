 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace libreria_aplicaciones.Implementaciones
{
    public class EmpleadosAplicacion : IEmpleadosAplicacion
    {
        private IConexion IConexion;

        public EmpleadosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

         
        public Empleados Actualizar(Empleados entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion!.Entry<Empleados>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Empleados", entidad.Id, "UPDATE", "Se actualizó un registro de empleado");
            return entidad;
        }

         
        public Empleados Borrar(Empleados entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Empleados!.Remove(entidad!);
            this.IConexion.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Empleados", entidad.Id, "DELETE", "Se eliminó un registro de empleado");
            return entidad;
        }

         
        public List<Empleados> Buscar(Empleados entidad)
        {
            entidad.Documento ??= "";

            return this.IConexion.Empleados!
                .Include(x => x._Rol)
                .Where(x => x.Documento!.Contains(entidad.Documento))
                .ToList();
        }

         
        public List<Empleados> Consultar()
        {
            return this.IConexion.Empleados!
                .Include(x => x._Rol)
                .ToList();
        }

         
        public Empleados Insertar(Empleados entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.Empleados!.Add(entidad!);
            this.IConexion.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Empleados", entidad.Id, "INSERT", "Se creó un registro de empleado");
            return entidad;
        }

         
        private bool Validar(Empleados entidad)
        {
            return entidad.Rol <= 0 ||
                string.IsNullOrEmpty(entidad.Documento) ||
                string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Apellido) ||
                string.IsNullOrEmpty(entidad.Telefono) ||
                string.IsNullOrEmpty(entidad.Correo);
        }
    }
}
