 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace libreria_aplicaciones.Implementaciones
{
    public class DiagnosticosAplicacion : IDiagnosticosAplicacion
    {
        private IConexion IConexion;

        public DiagnosticosAplicacion(
            IConexion IConexion)
        {
            this.IConexion = IConexion;
        }


         
        public Diagnosticos Actualizar(
            Diagnosticos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception(
                    "No se ha guardo");

            if (Validar(entidad))
                throw new Exception(
                    "Falta informacion");


            var entry =
                this.IConexion!
                    .Entry<Diagnosticos>(entidad);


            entry.State =
                EntityState.Modified;


            this.IConexion!
                .SaveChanges();


            Auditoria.Registrar(
                this.IConexion,
                "Diagnosticos",
                entidad.Id,
                "UPDATE",
                "Se actualizó un registro de diagnóstico");


            return entidad;
        }


         
        public Diagnosticos Borrar(
            Diagnosticos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception(
                    "No se ha guardo");


            this.IConexion
                .Diagnosticos!
                .Remove(entidad);


            this.IConexion
                .SaveChanges();


            Auditoria.Registrar(
                this.IConexion,
                "Diagnosticos",
                entidad.Id,
                "DELETE",
                "Se eliminó un registro de diagnóstico");


            return entidad;
        }


         
        public List<Diagnosticos> Buscar(
            Diagnosticos entidad)
        {
            entidad.Descripcion_Problema ??=
                "";


            return this.IConexion
                .Diagnosticos!

 
                .Include(
                    x => x._Orden)


                .ThenInclude(
                    x => x!._Vehiculo)


                .Include(
                    x => x._Empleado)

                .Where(
                    x =>
                        x.Descripcion_Problema!
                            .Contains(
                                entidad.Descripcion_Problema))

                .ToList();
        }


         
        public List<Diagnosticos> Consultar()
        {
            return this.IConexion
                .Diagnosticos!

                .Include(
                    x => x._Orden)

                .ThenInclude(
                    x => x!._Vehiculo)

                .Include(
                    x => x._Empleado)

                .ToList();
        }


         
        public Diagnosticos Insertar(
            Diagnosticos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception(
                    "Ya se guardo");

            if (Validar(entidad))
                throw new Exception(
                    "Falta informacion");


            this.IConexion
                .Diagnosticos!
                .Add(entidad);


            this.IConexion
                .SaveChanges();


            Auditoria.Registrar(
                this.IConexion,
                "Diagnosticos",
                entidad.Id,
                "INSERT",
                "Se creó un registro de diagnóstico");


            return entidad;
        }


         
        private bool Validar(
            Diagnosticos entidad)
        {
            return
                entidad.Orden <= 0 ||
                entidad.Empleado <= 0 ||
                entidad.Fecha_Diagnostico == default ||
                string.IsNullOrEmpty(
                    entidad.Descripcion_Problema) ||
                string.IsNullOrEmpty(
                    entidad.Observaciones);
        }
    }
}