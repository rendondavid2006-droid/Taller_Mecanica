 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace libreria_aplicaciones.Implementaciones
{
    public class Estados_CitasAplicacion : IEstados_CitasAplicacion
    {
        private IConexion IConexion;

        public Estados_CitasAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

         
        public Estados_Citas Actualizar(Estados_Citas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion.Entry<Estados_Citas>(entidad);
            entry.State = EntityState.Modified;

            this.IConexion.SaveChanges();

            Auditoria.Registrar(
                this.IConexion,
                "Estados_Citas",
                entidad.Id,
                "UPDATE",
                "Se actualizó un estado de cita"
            );

            return entidad;
        }

         
        public Estados_Citas Borrar(Estados_Citas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Estados_Citas!.Remove(entidad);
            this.IConexion.SaveChanges();

            Auditoria.Registrar(
                this.IConexion,
                "Estados_Citas",
                entidad.Id,
                "DELETE",
                "Se eliminó un estado de cita"
            );

            return entidad;
        }

         
        public List<Estados_Citas> Buscar(Estados_Citas entidad)
        {
            return this.IConexion.Estados_Citas!
                .Where(x =>
                    x.Nombre_Estado!.Contains(
                        entidad.Nombre_Estado ?? ""))
                .ToList();
        }

         
        public List<Estados_Citas> Consultar()
        {
            return this.IConexion.Estados_Citas!
                .ToList();
        }

         
        public Estados_Citas Insertar(Estados_Citas entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.Estados_Citas!.Add(entidad);
            this.IConexion.SaveChanges();

            Auditoria.Registrar(
                this.IConexion,
                "Estados_Citas",
                entidad.Id,
                "INSERT",
                "Se creó un estado de cita"
            );

            return entidad;
        }

         
        private bool Validar(Estados_Citas entidad)
        {
            return string.IsNullOrEmpty(entidad.Nombre_Estado) ||
                string.IsNullOrEmpty(entidad.Descripcion);
        }
    }
}