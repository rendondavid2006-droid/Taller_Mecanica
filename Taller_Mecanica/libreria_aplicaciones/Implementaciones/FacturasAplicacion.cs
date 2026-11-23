 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace libreria_aplicaciones.Implementaciones
{
    public class FacturasAplicacion : IFacturasAplicacion
    {
        private IConexion IConexion;
        private const decimal IVA = 0.19m;  

        public FacturasAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

         
        public Facturas Actualizar(Facturas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardado");

            var guardada = this.IConexion.Facturas!.FirstOrDefault(x => x.Id == entidad.Id);
            if (guardada == null)
                throw new Exception("No se encontro la factura");
            if (guardada.Orden != entidad.Orden)
                throw new Exception("No se puede cambiar la orden de una factura existente");

             
            entidad.Cliente = guardada.Cliente;

            bool tieneServicios = this.IConexion.Detalles_Ordenes_Servicios!.Any(x => x.Orden == entidad.Orden);
            bool tieneRepuestos = this.IConexion.Detalles_Repuestos_Ordenes!.Any(x => x.Orden == entidad.Orden);
            if (tieneServicios || tieneRepuestos)
                CalcularValores(entidad);
            else
            {
                 
                entidad.Subtotal = guardada.Subtotal;
                entidad.Impuesto = guardada.Impuesto;
                entidad.Total = guardada.Total;
            }

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.Entry(entidad).State = EntityState.Modified;
            this.IConexion.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Facturas", entidad.Id, "UPDATE", "Se actualizo un registro de factura");
            return entidad;
        }

        public Facturas Borrar(Facturas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Facturas!.Remove(entidad!);
            this.IConexion.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Facturas", entidad.Id, "DELETE", "Se elimino un registro de factura");
            return entidad;
        }

        public List<Facturas> Buscar(Facturas entidad)
        {
            entidad._Cliente ??= new Clientes();
            entidad._Cliente.Documento ??= "";
            return this.IConexion.Facturas!
                .Include(x => x._Cliente)
                .Where(x => x._Cliente!.Documento!.Contains(entidad._Cliente!.Documento!))
                .ToList();
        }

        public List<Facturas> Consultar()
        {
            return this.IConexion.Facturas!
                .Include(x => x._Orden)
                .Include(x => x._Cliente)
                .ToList();
        }

         
        public Facturas Insertar(Facturas entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");
            if (this.IConexion.Facturas!.Any(x => x.Orden == entidad.Orden))
                throw new Exception("Esta orden ya tiene una factura registrada");

            CalcularValores(entidad);
            if (Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.Facturas!.Add(entidad);
            this.IConexion.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Facturas", entidad.Id, "INSERT", "Se creo una factura con el total de servicios y repuestos");
            return entidad;
        }

         
        private void CalcularValores(Facturas entidad)
        {
            if (!this.IConexion.Ordenes_Servicios!.Any(x => x.Id == entidad.Orden))
                throw new Exception("La orden seleccionada no existe");

            decimal servicios = this.IConexion.Detalles_Ordenes_Servicios!
                .Where(x => x.Orden == entidad.Orden)
                .Sum(x => (decimal?)x.Subtotal) ?? 0;
            decimal repuestos = this.IConexion.Detalles_Repuestos_Ordenes!
                .Where(x => x.Orden == entidad.Orden)
                .Sum(x => (decimal?)x.Subtotal) ?? 0;

            entidad.Subtotal = servicios + repuestos;
            if (entidad.Subtotal <= 0)
                throw new Exception("Primero registre los servicios o repuestos de la orden");

            entidad.Impuesto = Math.Round(entidad.Subtotal * IVA, 2);
            entidad.Total = entidad.Subtotal + entidad.Impuesto;
        }

        private bool Validar(Facturas entidad)
        {
            return entidad.Orden <= 0 ||
                entidad.Cliente <= 0 ||
                entidad.Fecha_Factura == default ||
                entidad.Subtotal <= 0 ||
                entidad.Impuesto < 0 ||
                (entidad.Estado != "Pendiente" &&
                 entidad.Estado != "Pagada" &&
                 entidad.Estado != "Anulada") ||
                entidad.Total <= 0;
        }
    }
}
