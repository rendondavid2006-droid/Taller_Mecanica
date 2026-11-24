 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace libreria_aplicaciones.Implementaciones
{
    public class ServiciosAplicacion : IServiciosAplicacion
    {
        private IConexion IConexion;

        public ServiciosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }


         
        public Servicios Actualizar(Servicios entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");


            if (entidad.Tiempo_Estimado.Year < 1900)
            {
                entidad.Tiempo_Estimado =
                    new DateTime(
                        1900,
                        1,
                        1,
                        entidad.Tiempo_Estimado.Hour,
                        entidad.Tiempo_Estimado.Minute,
                        0);
            }


            var entry =
                this.IConexion!
                    .Entry<Servicios>(entidad);

            entry.State =
                EntityState.Modified;


            this.IConexion!
                .SaveChanges();


            Auditoria.Registrar(
                this.IConexion,
                "Servicios",
                entidad.Id,
                "UPDATE",
                "Se actualizó un registro de servicio");


            return entidad;
        }


         
        public Servicios Borrar(Servicios entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");



            var servicio =
                this.IConexion
                    .Servicios!
                    .IgnoreQueryFilters()
                    .FirstOrDefault(
                        x => x.Id == entidad.Id);


            if (servicio == null)
                throw new Exception(
                    "No se encontró el servicio");

            servicio.Estado = false;



            var entry =
                this.IConexion
                    .Entry<Servicios>(servicio);

            entry.State =
                EntityState.Modified;


            this.IConexion
                .SaveChanges();


            Auditoria.Registrar(
                this.IConexion,
                "Servicios",
                servicio.Id,
                "DELETE",
                "Se desactivó un registro de servicio");


            return servicio;
        }

         
        public List<Servicios> Buscar(
            Servicios entidad)
        {
            entidad.Nombre_Servicio ??= "";

            return this.IConexion
                .Servicios!
                .IgnoreQueryFilters()
                .Where(
                    x =>
                        x.Nombre_Servicio!
                            .Contains(
                                entidad.Nombre_Servicio))
                .ToList();
        }


         
        public List<Servicios> Consultar()
        {

            return this.IConexion
                .Servicios!
                .IgnoreQueryFilters()
                .ToList();
        }


         
        public Servicios Insertar(
            Servicios entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");



            if (entidad.Tiempo_Estimado.Year < 1900)
            {
                entidad.Tiempo_Estimado =
                    new DateTime(
                        1900,
                        1,
                        1,
                        entidad.Tiempo_Estimado.Hour,
                        entidad.Tiempo_Estimado.Minute,
                        0);
            }


            this.IConexion
                .Servicios!
                .Add(entidad);


            this.IConexion
                .SaveChanges();


            Auditoria.Registrar(
                this.IConexion,
                "Servicios",
                entidad.Id,
                "INSERT",
                "Se creó un registro de servicio");


            return entidad;
        }


         
        private bool Validar(
            Servicios entidad)
        {
            return
                string.IsNullOrEmpty(
                    entidad.Nombre_Servicio) ||

                string.IsNullOrEmpty(
                    entidad.Descripcion) ||

                entidad.Precio_Base <= 0 ||

                entidad.Tiempo_Estimado == default;
        }
    }
}