 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace libreria_aplicaciones.Implementaciones
{
    public class Detalles_Ordenes_ServiciosAplicacion : IDetalles_Ordenes_ServiciosAplicacion
    {
        private IConexion IConexion;

        public Detalles_Ordenes_ServiciosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

         
        public Detalles_Ordenes_Servicios Actualizar(Detalles_Ordenes_Servicios entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            entidad.Subtotal =
                entidad.Cantidad * entidad.Precio_Unitario;

            if (Validar(entidad))
                throw new Exception("Falta informacion");

             
            var anterior = this.IConexion.Detalles_Ordenes_Servicios!.FirstOrDefault(x => x.Id == entidad.Id);
            if (anterior == null)
                throw new Exception("No se encontro el servicio de la orden");
            VerificarSinFactura(anterior.Orden);
            VerificarSinFactura(entidad.Orden);

            var entry = this.IConexion!.Entry<Detalles_Ordenes_Servicios>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Detalles_Ordenes_Servicios", entidad.Id, "UPDATE", "Se actualizó un registro de detalle de orden de servicio");
            return entidad;
        }
         
        public Detalles_Ordenes_Servicios Borrar(Detalles_Ordenes_Servicios entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

             
            var anterior = this.IConexion.Detalles_Ordenes_Servicios!.FirstOrDefault(x => x.Id == entidad.Id);
            if (anterior == null)
                throw new Exception("No se encontro el servicio de la orden");
            VerificarSinFactura(anterior.Orden);
            this.IConexion.Detalles_Ordenes_Servicios!.Remove(anterior);
            this.IConexion.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Detalles_Ordenes_Servicios", entidad.Id, "DELETE", "Se eliminó un registro de detalle de orden de servicio");
            return entidad;
        }

         
        public List<Detalles_Ordenes_Servicios> Buscar(Detalles_Ordenes_Servicios entidad)
        {
            return this.IConexion.Detalles_Ordenes_Servicios!
                .Include(x => x._Orden)
                    .ThenInclude(o => o!._Empleado)
                .Where(x => x._Orden!._Empleado!.Nombre!.Contains(entidad._Orden!._Empleado!.Nombre!))
                .ToList();
        }

         
        public List<Detalles_Ordenes_Servicios> Consultar()
        {
            return this.IConexion.Detalles_Ordenes_Servicios!
                .Include(x => x._Orden)
                .Include(x => x._Servicio)
                .ToList();
        }

         
        public Detalles_Ordenes_Servicios Insertar(Detalles_Ordenes_Servicios entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            entidad.Subtotal =
                entidad.Cantidad * entidad.Precio_Unitario;

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            VerificarSinFactura(entidad.Orden);
            this.IConexion.Detalles_Ordenes_Servicios!.Add(entidad!);
            this.IConexion.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Detalles_Ordenes_Servicios", entidad.Id, "INSERT", "Se creó un registro de detalle de orden de servicio");
            return entidad;
        }

         
        private void VerificarSinFactura(int idOrden)
        {
            if (this.IConexion.Facturas!.Any(x => x.Orden == idOrden))
                throw new Exception("Esta orden ya tiene factura. No se pueden cambiar sus servicios");
        }

         
        private bool Validar(Detalles_Ordenes_Servicios entidad)
        {
            return entidad.Orden <= 0 ||
                entidad.Servicio <= 0 ||
                entidad.Cantidad <= 0 ||
                entidad.Precio_Unitario <= 0 ||
                entidad.Subtotal <= 0;
        }
    }
}