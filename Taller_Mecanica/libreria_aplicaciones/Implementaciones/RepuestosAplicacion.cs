 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace libreria_aplicaciones.Implementaciones
{
    public class RepuestosAplicacion : IRepuestosAplicacion
    {
        private IConexion IConexion;

        public RepuestosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

         
        public Repuestos Actualizar(Repuestos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion!.Entry<Repuestos>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Repuestos", entidad.Id, "UPDATE", "Se actualizó un registro de repuesto");
            return entidad;
        }
         
        public Repuestos Borrar(Repuestos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Repuestos!.Remove(entidad!);
            this.IConexion.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Repuestos", entidad.Id, "DELETE", "Se eliminó un registro de repuesto");
            return entidad;
        }

         
        public List<Repuestos> Buscar(Repuestos entidad)
        {
            return this.IConexion.Repuestos!
                .Where(x => x.Nombre_Repuesto!.Contains(entidad.Nombre_Repuesto!))
                .ToList();
        }

         
        public List<Repuestos> Consultar()
        {
            return this.IConexion.Repuestos!
                .ToList();
        }

         
        public Repuestos Insertar(Repuestos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.Repuestos!.Add(entidad!);
            this.IConexion.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Repuestos", entidad.Id, "INSERT", "Se creó un registro de repuesto");
            return entidad;
        }

         
        private bool Validar(Repuestos entidad)
        {
            return string.IsNullOrEmpty(entidad.Nombre_Repuesto) ||
                string.IsNullOrEmpty(entidad.Descripcion) ||
                entidad.Precio_Compra <= 0 ||
                entidad.Precio_Venta <= 0 ||
                entidad.Stock < 0 ||
                entidad.Stock_Minimo < 0;
        }
    }
}