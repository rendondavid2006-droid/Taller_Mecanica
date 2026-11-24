 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace libreria_aplicaciones.Implementaciones
{
    public class Detalles_Repuestos_OrdenesAplicacion : IDetalles_Repuestos_OrdenesAplicacion
    {
        private IConexion IConexion;

        public Detalles_Repuestos_OrdenesAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

         
        public Detalles_Repuestos_Ordenes Actualizar(Detalles_Repuestos_Ordenes entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardado");

            entidad.Subtotal = entidad.Cantidad * entidad.Precio_Unitario;
            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var anterior = this.IConexion.Detalles_Repuestos_Ordenes!
                .FirstOrDefault(x => x.Id == entidad.Id);
            if (anterior == null)
                throw new Exception("No se encontro el detalle de repuesto");

            VerificarSinFactura(anterior.Orden);
            VerificarSinFactura(entidad.Orden);

            var repuestoAnterior = ObtenerRepuesto(anterior.Repuesto);
            if (anterior.Repuesto == entidad.Repuesto)
            {
                 
                int stockNuevo = repuestoAnterior.Stock + anterior.Cantidad - entidad.Cantidad;
                if (stockNuevo < 0)
                    throw new Exception("No hay suficientes unidades de este repuesto en el inventario");

                repuestoAnterior.Stock = stockNuevo;
                this.IConexion.Entry(repuestoAnterior).State = EntityState.Modified;
            }
            else
            {
                 
                var repuestoNuevo = ObtenerRepuesto(entidad.Repuesto);
                if (repuestoNuevo.Stock < entidad.Cantidad)
                    throw new Exception("No hay suficientes unidades del nuevo repuesto en el inventario");

                repuestoAnterior.Stock += anterior.Cantidad;
                repuestoNuevo.Stock -= entidad.Cantidad;
                this.IConexion.Entry(repuestoAnterior).State = EntityState.Modified;
                this.IConexion.Entry(repuestoNuevo).State = EntityState.Modified;
            }

            this.IConexion.Entry(entidad).State = EntityState.Modified;
             
            this.IConexion.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Detalles_Repuestos_Ordenes", entidad.Id, "UPDATE", "Se actualizo un detalle de repuestos y su inventario");
            Auditoria.Registrar(this.IConexion, "Repuestos", entidad.Repuesto, "UPDATE", "Stock ajustado por edicion de un detalle de orden");
            if (anterior.Repuesto != entidad.Repuesto)
                Auditoria.Registrar(this.IConexion, "Repuestos", anterior.Repuesto, "UPDATE", "Stock devuelto por cambio de repuesto en una orden");
            return entidad;
        }

         
        public Detalles_Repuestos_Ordenes Borrar(Detalles_Repuestos_Ordenes entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardado");

            var anterior = this.IConexion.Detalles_Repuestos_Ordenes!
                .FirstOrDefault(x => x.Id == entidad.Id);
            if (anterior == null)
                throw new Exception("No se encontro el detalle de repuesto");

            VerificarSinFactura(anterior.Orden);
            var repuesto = ObtenerRepuesto(anterior.Repuesto);
            repuesto.Stock += anterior.Cantidad;
            this.IConexion.Entry(repuesto).State = EntityState.Modified;

            this.IConexion.Detalles_Repuestos_Ordenes!.Remove(anterior);
            this.IConexion.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Detalles_Repuestos_Ordenes", anterior.Id, "DELETE", "Se elimino un detalle de repuestos y se devolvio el stock");
            Auditoria.Registrar(this.IConexion, "Repuestos", anterior.Repuesto, "UPDATE", "Stock devuelto por eliminacion de detalle de orden");
            return anterior;
        }

        public List<Detalles_Repuestos_Ordenes> Buscar(Detalles_Repuestos_Ordenes entidad)
        {
            return this.IConexion.Detalles_Repuestos_Ordenes!
                .Include(x => x._Repuesto)
                .Where(x => x._Repuesto!.Nombre_Repuesto!.Contains(entidad._Repuesto!.Nombre_Repuesto!))
                .ToList();
        }

        public List<Detalles_Repuestos_Ordenes> Consultar()
        {
            return this.IConexion.Detalles_Repuestos_Ordenes!
                .Include(x => x._Orden)
                .Include(x => x._Repuesto)
                .ToList();
        }

         
        public Detalles_Repuestos_Ordenes Insertar(Detalles_Repuestos_Ordenes entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            entidad.Subtotal = entidad.Cantidad * entidad.Precio_Unitario;
            if (Validar(entidad))
                throw new Exception("Falta informacion");

            VerificarSinFactura(entidad.Orden);
            var repuesto = ObtenerRepuesto(entidad.Repuesto);
            if (repuesto.Stock < entidad.Cantidad)
                throw new Exception("No hay suficientes unidades de este repuesto en el inventario");

            repuesto.Stock -= entidad.Cantidad;
            this.IConexion.Entry(repuesto).State = EntityState.Modified;
            this.IConexion.Detalles_Repuestos_Ordenes!.Add(entidad);
            this.IConexion.SaveChanges();

            Auditoria.Registrar(this.IConexion, "Detalles_Repuestos_Ordenes", entidad.Id, "INSERT", "Se registro un repuesto usado en una orden");
            Auditoria.Registrar(this.IConexion, "Repuestos", entidad.Repuesto, "UPDATE", "Stock descontado por uso en una orden de servicio");
            return entidad;
        }

         
        private Repuestos ObtenerRepuesto(int id)
        {
            var repuesto = this.IConexion.Repuestos!.FirstOrDefault(x => x.Id == id);
            if (repuesto == null)
                throw new Exception("El repuesto seleccionado no existe");
            return repuesto;
        }

         
        private void VerificarSinFactura(int idOrden)
        {
            if (this.IConexion.Facturas!.Any(x => x.Orden == idOrden))
                throw new Exception("Esta orden ya tiene factura. No se pueden cambiar sus repuestos");
        }

        private bool Validar(Detalles_Repuestos_Ordenes entidad)
        {
            return entidad.Orden <= 0 ||
                entidad.Repuesto <= 0 ||
                entidad.Cantidad <= 0 ||
                entidad.Precio_Unitario <= 0 ||
                entidad.Subtotal <= 0;
        }
    }
}
