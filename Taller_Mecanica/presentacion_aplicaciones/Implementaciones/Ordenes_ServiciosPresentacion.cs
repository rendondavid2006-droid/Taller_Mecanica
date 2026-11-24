 
 

using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;

namespace presentacion_aplicaciones.Implementaciones
{
    public class Ordenes_ServiciosPresentacion : IOrdenes_ServiciosPresentacion
    {
        private IComunicaciones IComunicaciones;

        public Ordenes_ServiciosPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

         
        public Ordenes_Servicios Actualizar(Ordenes_Servicios entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Ordenes_Servicios>(
                "Ordenes_Servicios/Actualizar", 
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public Ordenes_Servicios Borrar(Ordenes_Servicios entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Ordenes_Servicios>(
                "Ordenes_Servicios/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public List<Ordenes_Servicios> Buscar(Ordenes_Servicios entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Ordenes_Servicios>>(
                "Ordenes_Servicios/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public List<Ordenes_Servicios> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Ordenes_Servicios>>(
                "Ordenes_Servicios/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

         
        public Ordenes_Servicios Insertar(Ordenes_Servicios entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Ordenes_Servicios>(
                "Ordenes_Servicios/Insertar",
                entidad);
            task.Wait();
            return task.Result;
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