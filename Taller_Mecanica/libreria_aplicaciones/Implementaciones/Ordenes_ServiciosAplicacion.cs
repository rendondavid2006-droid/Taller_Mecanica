 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace libreria_aplicaciones.Implementaciones
{
    public class Ordenes_ServiciosAplicacion : IOrdenes_ServiciosAplicacion
    {
        private IConexion IConexion;

        public Ordenes_ServiciosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

         
        public Ordenes_Servicios Actualizar(Ordenes_Servicios entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            ValidarCitaVehiculo(entidad);

            var entry = this.IConexion!.Entry<Ordenes_Servicios>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Ordenes_Servicios", entidad.Id, "UPDATE", "Se actualizó un registro de orden de servicio");
            return entidad;
        }
         
        public Ordenes_Servicios Borrar(Ordenes_Servicios entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Ordenes_Servicios!.Remove(entidad!);
            this.IConexion.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Ordenes_Servicios", entidad.Id, "DELETE", "Se eliminó un registro de orden de servicio");
            return entidad;
        }

         
        public List<Ordenes_Servicios> Buscar(
            Ordenes_Servicios entidad)
        {
            entidad._Cita ??=
                new Citas();

            entidad._Cita._Cliente ??=
                new Clientes();

            entidad._Cita._Cliente.Documento ??=
                "";

            return this.IConexion.Ordenes_Servicios!
                .Include(x => x._Cita)
                    .ThenInclude(x => x!._Cliente)
                .Include(x => x._Cita)
                    .ThenInclude(x => x!._Vehiculo)
                .Include(x => x._Vehiculo)
                .Include(x => x._Empleado)
                .Include(x => x._EstadoOrden)
                .Where(
                    x =>
                        x._Cita == null ||
                        x._Cita._Cliente == null ||
                        x._Cita._Cliente.Documento!
                            .Contains(
                                entidad._Cita._Cliente.Documento))
                .ToList();
        }

         
        public List<Ordenes_Servicios> Consultar()
        {
            return this.IConexion.Ordenes_Servicios!
                .Include(x => x._Cita)
                    .ThenInclude(x => x!._Cliente)

                .Include(x => x._Cita)
                    .ThenInclude(x => x!._Vehiculo)
                .Include(x => x._Vehiculo)
                .Include(x => x._Empleado)
                .Include(x => x._EstadoOrden)
                .ToList();
        }

         
        public Ordenes_Servicios Insertar(Ordenes_Servicios entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            ValidarCitaVehiculo(entidad);

            this.IConexion.Ordenes_Servicios!.Add(entidad!);
            this.IConexion.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Ordenes_Servicios", entidad.Id, "INSERT", "Se creó un registro de orden de servicio");
            return entidad;
        }

         
        private void ValidarCitaVehiculo(Ordenes_Servicios entidad)
        {
            Citas? cita =
                this.IConexion.Citas!
                    .FirstOrDefault(
                        x => x.Id == entidad.Cita);

            if (cita == null)
                throw new Exception("La cita seleccionada no existe");

            if (cita.Vehiculo != entidad.Vehiculo)
                throw new Exception("El vehículo seleccionado no corresponde a la cita");
        }

         
        private bool Validar(Ordenes_Servicios entidad)
        {
            return entidad.Cita <= 0 ||
                entidad.Vehiculo <= 0 ||
                entidad.Empleado <= 0 ||
                entidad.Estado_Orden <= 0 ||
                entidad.Fecha_Ingreso == default ||
                string.IsNullOrEmpty(entidad.Kilometraje_Ingreso) ||
                string.IsNullOrEmpty(entidad.Observaciones);
        }
    }
}