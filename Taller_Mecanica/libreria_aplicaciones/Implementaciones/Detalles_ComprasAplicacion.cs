 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace libreria_aplicaciones.Implementaciones
{
    public class Detalles_ComprasAplicacion : IDetalles_ComprasAplicacion
    {
        private IConexion IConexion;

        public Detalles_ComprasAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

         
        public Detalles_Compras Actualizar(Detalles_Compras entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            entidad.Subtotal =
                entidad.Cantidad * entidad.Precio_Unitario;

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion!.Entry<Detalles_Compras>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            Auditoria.Registrar(
            this.IConexion,"Detalles_Compras",entidad.Id,"UPDATE","Se actualizó un registro de detalle de compra");
            return entidad;
        }
         
        public Detalles_Compras Borrar(Detalles_Compras entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Detalles_Compras!.Remove(entidad!);
            this.IConexion.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Detalles_Compras", entidad.Id, "DELETE", "Se eliminó un registro de detalle de compra");
            return entidad;
        }

         
        public List<Detalles_Compras> Buscar(Detalles_Compras entidad)
        {
            return this.IConexion.Detalles_Compras!
                .Include(x => x._Repuesto)
                .Where(x => x._Repuesto!.Nombre_Repuesto!.Contains(entidad._Repuesto!.Nombre_Repuesto!))
                .ToList();
        }

         
        public List<Detalles_Compras> Consultar()
        {
            return this.IConexion.Detalles_Compras!
                .Include(x => x._Compra)
                .Include(x => x._Repuesto)
                .ToList();
        }

         
        public Detalles_Compras Insertar(Detalles_Compras entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            entidad.Subtotal =
                entidad.Cantidad * entidad.Precio_Unitario;

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.Detalles_Compras!.Add(entidad!);
            this.IConexion.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Detalles_Compras", entidad.Id, "INSERT", "Se creó un registro de detalle de compra");
            return entidad;
        }

         
        private bool Validar(Detalles_Compras entidad)
        {
            return entidad.Compra <= 0 ||
                entidad.Repuesto <= 0 ||
                entidad.Cantidad <= 0 ||
                entidad.Precio_Unitario <= 0 ||
                entidad.Subtotal <= 0 ;
        }
    }
}