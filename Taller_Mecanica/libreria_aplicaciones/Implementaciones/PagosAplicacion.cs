 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace libreria_aplicaciones.Implementaciones
{
    public class PagosAplicacion : IPagosAplicacion
    {
        private IConexion IConexion;

        public PagosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

         
        public Pagos Actualizar(Pagos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardado");
            if (Validar(entidad))
                throw new Exception("Falta información del pago o estado incorrecto");

            var anterior = this.IConexion.Pagos!.FirstOrDefault(x => x.Id == entidad.Id);
            if (anterior == null)
                throw new Exception("No se encontró el pago");
            if (anterior.Factura != entidad.Factura)
                throw new Exception("No se puede cambiar la factura de un pago registrado");

            bool estabaPagado = EsPagoCompleto(anterior.Estado);
            bool estaPagado = EsPagoCompleto(entidad.Estado);
            bool cambioFactura = false;
            if (estabaPagado || estaPagado)
                cambioFactura = SincronizarFactura(entidad, false);

            this.IConexion.Entry(entidad).State = EntityState.Modified;
            this.IConexion.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Pagos", entidad.Id, "UPDATE", "Se actualizó un registro de pago");
            if (cambioFactura)
                Auditoria.Registrar(this.IConexion, "Facturas", entidad.Factura, "UPDATE", "Se actualizó el estado por el pago");
            return entidad;
        }

        public Pagos Borrar(Pagos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardado");

            var guardado = this.IConexion.Pagos!.FirstOrDefault(x => x.Id == entidad.Id);
            if (guardado == null)
                throw new Exception("No se encontró el pago");

            bool cambioFactura = false;
            if (EsPagoCompleto(guardado.Estado))
                cambioFactura = SincronizarFactura(guardado, true);

            this.IConexion.Pagos!.Remove(guardado);
            this.IConexion.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Pagos", guardado.Id, "DELETE", "Se eliminó un registro de pago");
            if (cambioFactura)
                Auditoria.Registrar(this.IConexion, "Facturas", guardado.Factura, "UPDATE", "Se actualizó el estado por el pago");
            return guardado;
        }

         
        public List<Pagos> Buscar(Pagos entidad)
        {
            entidad._Factura ??= new Facturas();
            entidad._Factura._Cliente ??= new Clientes();
            entidad._Factura._Cliente.Documento ??= "";

            return this.IConexion.Pagos!
                .Include(x => x._Factura)
                    .ThenInclude(c => c!._Cliente)
                .Where(x => x._Factura!._Cliente!.Documento!.Contains(entidad._Factura!._Cliente!.Documento!))
                .ToList();
        }

         
        public List<Pagos> Consultar()
        {
            return this.IConexion.Pagos!
                .Include(x => x._Factura)
                .ToList();
        }

         
         
        public Pagos Insertar(Pagos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            bool cambioFactura = false;
            if (EsPagoCompleto(entidad.Estado))
                cambioFactura = SincronizarFactura(entidad, false);

            this.IConexion.Pagos!.Add(entidad!);
            this.IConexion.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Pagos", entidad.Id, "INSERT", "Se creó un registro de pago");
            if (cambioFactura)
                Auditoria.Registrar(this.IConexion, "Facturas", entidad.Factura, "UPDATE", "Se actualizó el estado por el pago");
            return entidad;
        }

        private static bool EsPagoCompleto(string? estado)
        {
            return estado == "Pagada" || estado == "Completado";
        }

         
        private bool SincronizarFactura(Pagos pago, bool eliminado)
        {
            var factura = this.IConexion.Facturas!.FirstOrDefault(x => x.Id == pago.Factura);
            if (factura == null)
                throw new Exception("No se encontró la factura del pago");

            if (!eliminado && EsPagoCompleto(pago.Estado) && pago.Monto != factura.Total)
                throw new Exception("El monto del pago debe ser igual al total de la factura");

            bool otrosPagos = this.IConexion.Pagos!.Any(x =>
                x.Factura == pago.Factura && x.Id != pago.Id &&
                (x.Estado == "Pagada" || x.Estado == "Completado"));

            string nuevoEstado = (!eliminado && EsPagoCompleto(pago.Estado)) || otrosPagos
                ? "Pagada" : "Pendiente";

            if (factura.Estado == nuevoEstado)
                return false;

            factura.Estado = nuevoEstado;
            this.IConexion.Entry(factura).State = EntityState.Modified;
            return true;
        }

         
        private bool Validar(Pagos entidad)
        {
            return entidad.Factura <= 0 ||
                entidad.Fecha_Pago == default ||
                entidad.Monto <= 0 ||
                string.IsNullOrEmpty(entidad.Metodo_Pago) ||
                string.IsNullOrEmpty(entidad.Referencia_Pago) ||
                (entidad.Estado != "Pendiente" &&
                 entidad.Estado != "Pagada" &&
                 entidad.Estado != "Completado");
        }
    }
}