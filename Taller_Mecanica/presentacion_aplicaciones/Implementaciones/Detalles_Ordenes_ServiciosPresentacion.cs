 
 

using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;

namespace presentacion_aplicaciones.Implementaciones
{
    public class Detalles_Ordenes_ServiciosPresentacion : IDetalles_Ordenes_ServiciosPresentacion
    {
        private IComunicaciones IComunicaciones;

        public Detalles_Ordenes_ServiciosPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

         
        public Detalles_Ordenes_Servicios Actualizar(Detalles_Ordenes_Servicios entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Detalles_Ordenes_Servicios>(
                "Detalles_Ordenes_Servicios/Actualizar", 
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public Detalles_Ordenes_Servicios Borrar(Detalles_Ordenes_Servicios entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Detalles_Ordenes_Servicios>(
                "Detalles_Ordenes_Servicios/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public List<Detalles_Ordenes_Servicios> Buscar(Detalles_Ordenes_Servicios entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Detalles_Ordenes_Servicios>>(
                "Detalles_Ordenes_Servicios/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public List<Detalles_Ordenes_Servicios> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Detalles_Ordenes_Servicios>>(
                "Detalles_Ordenes_Servicios/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

         
        public Detalles_Ordenes_Servicios Insertar(Detalles_Ordenes_Servicios entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Detalles_Ordenes_Servicios>(
                "Detalles_Ordenes_Servicios/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        private bool Validar(Detalles_Ordenes_Servicios entidad)
        {
            return entidad.Orden <= 0 ||
                entidad.Servicio <= 0 ||
                entidad.Cantidad <= 0 ||
                entidad.Precio_Unitario <= 0 ||
                entidad.Subtotal <= 0;
        }
    }
}