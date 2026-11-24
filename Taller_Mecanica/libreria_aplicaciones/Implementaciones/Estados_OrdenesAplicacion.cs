 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace libreria_aplicaciones.Implementaciones
{
    public class Estados_OrdenesAplicacion : IEstados_OrdenesAplicacion
    {
        private IConexion IConexion;

        public Estados_OrdenesAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

         
        public Estados_Ordenes Actualizar(Estados_Ordenes entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion!.Entry<Estados_Ordenes>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Estados_Ordenes", entidad.Id, "UPDATE", "Se actualizó un registro de estado de orden");
            return entidad;
        }
         
        public Estados_Ordenes Borrar(Estados_Ordenes entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Estados_Ordenes!.Remove(entidad!);
            this.IConexion.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Estados_Ordenes", entidad.Id, "DELETE", "Se eliminó un registro de estado de orden");
            return entidad;
        }

         
        public List<Estados_Ordenes> Buscar(Estados_Ordenes entidad)
        {
            return this.IConexion.Estados_Ordenes!
                .Where(x => x.Nombre_Estado!.Contains(entidad.Nombre_Estado!))
                .ToList();
        }

         
        public List<Estados_Ordenes> Consultar()
        {
            return this.IConexion.Estados_Ordenes!.ToList();
        }

         
        public Estados_Ordenes Insertar(Estados_Ordenes entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.Estados_Ordenes!.Add(entidad!);
            this.IConexion.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Estados_Ordenes", entidad.Id, "INSERT", "Se creó un registro de estado de orden");
            return entidad;
        }

         
        private bool Validar(Estados_Ordenes entidad)
        {
            return string.IsNullOrEmpty(entidad.Nombre_Estado) ||
                string.IsNullOrEmpty(entidad.Descripcion) ||
                entidad.Orden_Proceso <= 0;
        }
    }
}