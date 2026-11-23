 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace libreria_aplicaciones.Implementaciones
{
    public class Horarios_CitasAplicacion : IHorarios_CitasAplicacion
    {
        private IConexion IConexion;

        public Horarios_CitasAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

         
        public Horarios_Citas Actualizar(Horarios_Citas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion.Entry<Horarios_Citas>(entidad);
            entry.State = EntityState.Modified;

            this.IConexion.SaveChanges();

            Auditoria.Registrar(
                this.IConexion,
                "Horarios_Citas",
                entidad.Id,
                "UPDATE",
                "Se actualizó un horario de cita"
            );

            return entidad;
        }

         
        public Horarios_Citas Borrar(Horarios_Citas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Horarios_Citas!.Remove(entidad);
            this.IConexion.SaveChanges();

            Auditoria.Registrar(
                this.IConexion,
                "Horarios_Citas",
                entidad.Id,
                "DELETE",
                "Se eliminó un horario de cita"
            );

            return entidad;
        }

         
        public List<Horarios_Citas> Buscar(Horarios_Citas entidad)
        {
            if (entidad.Fecha == DateTime.MinValue)
            {
                return this.IConexion.Horarios_Citas!
                    .ToList();
            }

            return this.IConexion.Horarios_Citas!
                .Where(x => x.Fecha.Date == entidad.Fecha.Date)
                .ToList();
        }

         
        public List<Horarios_Citas> Consultar()
        {
            return this.IConexion.Horarios_Citas!
                .ToList();
        }

         
        public Horarios_Citas Insertar(Horarios_Citas entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.Horarios_Citas!.Add(entidad);
            this.IConexion.SaveChanges();

            Auditoria.Registrar(
                this.IConexion,
                "Horarios_Citas",
                entidad.Id,
                "INSERT",
                "Se creó un horario de cita"
            );

            return entidad;
        }

         
        private bool Validar(Horarios_Citas entidad)
        {
            return entidad.Fecha == DateTime.MinValue ||
                entidad.Hora_Inicio == DateTime.MinValue ||
                entidad.Hora_Fin == DateTime.MinValue;
        }
    }
}