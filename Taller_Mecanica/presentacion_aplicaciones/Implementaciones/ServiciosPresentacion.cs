 
 

using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;

namespace presentacion_aplicaciones.Implementaciones
{
    public class ServiciosPresentacion : IServiciosPresentacion
    {
        private IComunicaciones IComunicaciones;

        public ServiciosPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

         
        public Servicios Actualizar(Servicios entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Servicios>(
                "Servicios/Actualizar", 
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public Servicios Borrar(Servicios entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Servicios>(
                "Servicios/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public List<Servicios> Buscar(Servicios entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Servicios>>(
                "Servicios/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public List<Servicios> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Servicios>>(
                "Servicios/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

         
        public Servicios Insertar(Servicios entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Servicios>(
                "Servicios/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        private bool Validar(Servicios entidad)
        {
            return string.IsNullOrEmpty(entidad.Nombre_Servicio) ||
                string.IsNullOrEmpty(entidad.Descripcion) ||
                entidad.Precio_Base <= 0 ||
                entidad.Tiempo_Estimado == default;
        }
    }
}