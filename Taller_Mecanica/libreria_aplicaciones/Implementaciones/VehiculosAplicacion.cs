 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace libreria_aplicaciones.Implementaciones
{
    public class VehiculosAplicacion : IVehiculosAplicacion
    {
        private IConexion IConexion;

        public VehiculosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

         
        public Vehiculos Actualizar(Vehiculos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion.Entry<Vehiculos>(entidad);
            entry.State = EntityState.Modified;

            this.IConexion.SaveChanges();

            Auditoria.Registrar(
                this.IConexion,
                "Vehiculos",
                entidad.Id,
                "UPDATE",
                "Se actualizó un registro de vehículo"
            );

            return entidad;
        }

         
        public Vehiculos Borrar(Vehiculos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Vehiculos!.Remove(entidad);

            this.IConexion.SaveChanges();

            Auditoria.Registrar(
                this.IConexion,
                "Vehiculos",
                entidad.Id,
                "DELETE",
                "Se eliminó un registro de vehículo"
            );

            return entidad;
        }

         
        public List<Vehiculos> Buscar(Vehiculos entidad)
        {
            entidad.Placa ??= "";

            return this.IConexion.Vehiculos!
                .Include(x => x._Cliente)
                .Include(x => x._Modelo)
                .Where(x =>
                    (entidad.Cliente == 0 ||
                     x.Cliente == entidad.Cliente) &&
                    x.Placa!.Contains(
                        entidad.Placa))
                .ToList();
        }

         
        public List<Vehiculos> Consultar()
        {
            return this.IConexion.Vehiculos!
                .Include(x => x._Cliente)
                .Include(x => x._Modelo)
                .ToList();
        }

         
        public Vehiculos Insertar(Vehiculos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.Vehiculos!.Add(entidad);

            this.IConexion.SaveChanges();

            Auditoria.Registrar(
                this.IConexion,
                "Vehiculos",
                entidad.Id,
                "INSERT",
                "Se creó un registro de vehículo"
            );

            return entidad;
        }

         
        private bool Validar(Vehiculos entidad)
        {
            return entidad.Cliente <= 0 ||
                   entidad.Modelo <= 0 ||
                   string.IsNullOrEmpty(entidad.Placa) ||
                   entidad.Año <= 0 ||
                   string.IsNullOrEmpty(entidad.Color) ||
                   string.IsNullOrEmpty(entidad.Kilometraje);
        }
    }
}