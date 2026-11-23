 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace libreria_aplicaciones.Implementaciones
{
    public class ProveedoresAplicacion : IProveedoresAplicacion
    {
        private IConexion IConexion;

        public ProveedoresAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

         
        public Proveedores Actualizar(Proveedores entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion!.Entry<Proveedores>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Proveedores", entidad.Id, "UPDATE", "Se actualizó un registro de proveedor");
            return entidad;
        }

         
        public Proveedores Borrar(Proveedores entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Proveedores!.Remove(entidad!);
            this.IConexion.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Proveedores", entidad.Id, "DELETE", "Se eliminó un registro de proveedor");
            return entidad;
        }

         
        public List<Proveedores> Buscar(Proveedores entidad)
        {
            return this.IConexion.Proveedores!
                .Where(x => x.Nombre_Proveedor!.Contains(entidad.Nombre_Proveedor!))
                .ToList();
        }

         
        public List<Proveedores> Consultar()
        {
            return this.IConexion.Proveedores!
                .ToList();
        }

         
        public Proveedores Insertar(Proveedores entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.Proveedores!.Add(entidad!);
            this.IConexion.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Proveedores", entidad.Id, "INSERT", "Se creó un registro de proveedor");
            return entidad;
        }

         
        private bool Validar(Proveedores entidad)
        {
            return string.IsNullOrEmpty(entidad.Nit) ||
                string.IsNullOrEmpty(entidad.Nombre_Proveedor) ||
                string.IsNullOrEmpty(entidad.Telefono) ||
                string.IsNullOrEmpty(entidad.Correo) ||
                string.IsNullOrEmpty(entidad.Direccion);
        }
    }
}