 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace libreria_aplicaciones.Implementaciones
{
    public class CitasAplicacion : ICitasAplicacion
    {
        private IConexion IConexion;

        public CitasAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

         
        public Citas Actualizar(Citas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            Citas? citaOriginal = this.IConexion.Citas!
                .AsTracking()
                .FirstOrDefault(x => x.Id == entidad.Id);

            if (citaOriginal == null)
                throw new Exception("No se encontró la cita");

            ValidarVehiculoCliente(entidad);

            int horarioAnteriorId = citaOriginal.Horario_Cita;

            if (horarioAnteriorId != entidad.Horario_Cita)
            {
                Horarios_Citas? horarioAnterior = this.IConexion.Horarios_Citas!
                    .AsTracking()
                    .FirstOrDefault(x => x.Id == horarioAnteriorId);

                Horarios_Citas? horarioNuevo = this.IConexion.Horarios_Citas!
                    .AsTracking()
                    .FirstOrDefault(x => x.Id == entidad.Horario_Cita);

                if (horarioNuevo == null)
                    throw new Exception("El horario seleccionado no existe");

                if (!horarioNuevo.Disponible)
                    throw new Exception("El horario seleccionado ya no está disponible");

                if (horarioAnterior != null)
                    horarioAnterior.Disponible = true;

                horarioNuevo.Disponible = false;
            }

            citaOriginal.Cliente = entidad.Cliente;
            citaOriginal.Vehiculo = entidad.Vehiculo;
            citaOriginal.Horario_Cita = entidad.Horario_Cita;
            citaOriginal.Motivo = entidad.Motivo;
            citaOriginal.Estado_Cita = entidad.Estado_Cita;

            this.IConexion.SaveChanges();

            Auditoria.Registrar(
                this.IConexion,
                "Citas",
                citaOriginal.Id,
                "UPDATE",
                "Se actualizó un registro de cita");

            if (horarioAnteriorId != entidad.Horario_Cita)
            {
                Auditoria.Registrar(
                    this.IConexion,
                    "Horarios_Citas",
                    horarioAnteriorId,
                    "UPDATE",
                    "Se liberó un horario al cambiar una cita");

                Auditoria.Registrar(
                    this.IConexion,
                    "Horarios_Citas",
                    entidad.Horario_Cita,
                    "UPDATE",
                    "Se ocupó un horario al cambiar una cita");
            }

            return citaOriginal;
        }

         
        public Citas Borrar(Citas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            Citas? citaOriginal = this.IConexion.Citas!
                .AsTracking()
                .FirstOrDefault(x => x.Id == entidad.Id);

            if (citaOriginal == null)
                throw new Exception("No se encontró la cita");

            Horarios_Citas? horario = this.IConexion.Horarios_Citas!
                .AsTracking()
                .FirstOrDefault(x => x.Id == citaOriginal.Horario_Cita);

            if (horario != null)
                horario.Disponible = true;

            this.IConexion.Citas!.Remove(citaOriginal);
            this.IConexion.SaveChanges();

            Auditoria.Registrar(
                this.IConexion,
                "Citas",
                citaOriginal.Id,
                "DELETE",
                "Se eliminó un registro de cita");

            if (horario != null)
            {
                Auditoria.Registrar(
                    this.IConexion,
                    "Horarios_Citas",
                    horario.Id,
                    "UPDATE",
                    "Se liberó un horario al eliminar una cita");
            }

            return citaOriginal;
        }

         
        public List<Citas> Buscar(Citas entidad)
        {
            entidad._Cliente ??= new Clientes();
            entidad._Cliente.Documento ??= "";

            return this.IConexion.Citas!
                .Include(x => x._Cliente)
                .Include(x => x._Vehiculo)
                .Include(x => x._Horario_Cita)
                .Include(x => x._Estado_Cita)
                .Where(x =>
                    (entidad.Cliente == 0 ||
                     x.Cliente == entidad.Cliente) &&
                    x._Cliente!.Documento!.Contains(
                        entidad._Cliente.Documento))
                .ToList();
        }

         
        public List<Citas> Consultar()
        {
            return this.IConexion.Citas!
                .Include(x => x._Cliente)
                .Include(x => x._Vehiculo)
                .Include(x => x._Horario_Cita)
                .Include(x => x._Estado_Cita)
                .ToList();
        }

         
        public Citas Insertar(Citas entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            ValidarVehiculoCliente(entidad);

            Horarios_Citas? horario = this.IConexion.Horarios_Citas!
                .AsTracking()
                .FirstOrDefault(x => x.Id == entidad.Horario_Cita);

            if (horario == null)
                throw new Exception("El horario seleccionado no existe");

            if (!horario.Disponible)
                throw new Exception("El horario seleccionado ya no está disponible");

            horario.Disponible = false;

            this.IConexion.Citas!.Add(entidad);
            this.IConexion.SaveChanges();

            Auditoria.Registrar(
                this.IConexion,
                "Citas",
                entidad.Id,
                "INSERT",
                "Se creó un registro de cita");

            Auditoria.Registrar(
                this.IConexion,
                "Horarios_Citas",
                horario.Id,
                "UPDATE",
                "Se ocupó un horario al crear una cita");

            return entidad;
        }

         
        private void ValidarVehiculoCliente(Citas entidad)
        {
            Vehiculos? vehiculo = this.IConexion.Vehiculos!
                .FirstOrDefault(x => x.Id == entidad.Vehiculo);

            if (vehiculo == null)
                throw new Exception("El vehículo seleccionado no existe");

            if (vehiculo.Cliente != entidad.Cliente)
                throw new Exception("El vehículo seleccionado no pertenece al cliente");
        }

         
        private bool Validar(Citas entidad)
        {
            return entidad.Cliente <= 0 ||
                entidad.Vehiculo <= 0 ||
                entidad.Horario_Cita <= 0 ||
                string.IsNullOrEmpty(entidad.Motivo) ||
                entidad.Estado_Cita <= 0;
        }
    }
}
