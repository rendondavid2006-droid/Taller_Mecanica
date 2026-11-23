 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace libreria_aplicaciones.Implementaciones
{
    public class ComprasAplicacion : IComprasAplicacion
    {
        private IConexion IConexion;

        public ComprasAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

         
        public Compras Actualizar(Compras entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion!.Entry<Compras>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Compras", entidad.Id, "UPDATE", "Se actualizó un registro de compra");
            return entidad;
        }
         
        public Compras Borrar(Compras entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Compras!.Remove(entidad!);
            this.IConexion.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Compras", entidad.Id, "DELETE", "Se eliminó un registro de compra");
            return entidad;
        }

         
        public List<Compras> Buscar(Compras entidad)
        {
            return this.IConexion.Compras!
                .Include(x => x._Proveedor)
                .Where(x => x.Numero_Factura_Proveedor!.Contains(entidad.Numero_Factura_Proveedor!))
                .ToList();
        }

         
        public List<Compras> Consultar()
        {
            return this.IConexion.Compras!
                .Include(x => x._Proveedor)
                .ToList();
        }

         
        public Compras Insertar(Compras entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.Compras!.Add(entidad!);
            this.IConexion.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Compras", entidad.Id, "INSERT", "Se creó un registro de compra");
            return entidad;
        }

         
        private bool Validar(Compras entidad)
        {
            return entidad.Proveedor <= 0 ||
                entidad.Fecha_Compra == default ||
                string.IsNullOrEmpty(entidad.Numero_Factura_Proveedor) ||
                entidad.Total_Compra <= 0 ||
                (entidad.Estado != "Pendiente" &&
                 entidad.Estado != "Recibida" &&
                 entidad.Estado != "Pagada" &&
                 entidad.Estado != "Cancelada");
        }
    }
}