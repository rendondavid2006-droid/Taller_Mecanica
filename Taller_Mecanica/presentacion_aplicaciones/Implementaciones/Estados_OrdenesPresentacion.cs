 
 

using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;

namespace presentacion_aplicaciones.Implementaciones
{
    public class Estados_OrdenesPresentacion : IEstados_OrdenesPresentacion
    {
        private IComunicaciones IComunicaciones;

        public Estados_OrdenesPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

         
        public Estados_Ordenes Actualizar(Estados_Ordenes entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Estados_Ordenes>(
                "Estados_Ordenes/Actualizar", 
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public Estados_Ordenes Borrar(Estados_Ordenes entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Estados_Ordenes>(
                "Estados_Ordenes/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public List<Estados_Ordenes> Buscar(Estados_Ordenes entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Estados_Ordenes>>(
                "Estados_Ordenes/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public List<Estados_Ordenes> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Estados_Ordenes>>(
                "Estados_Ordenes/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

         
        public Estados_Ordenes Insertar(Estados_Ordenes entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Estados_Ordenes>(
                "Estados_Ordenes/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        private bool Validar(Estados_Ordenes entidad)
        {
            return string.IsNullOrEmpty(entidad.Nombre_Estado) ||
                string.IsNullOrEmpty(entidad.Descripcion) ||
                entidad.Orden_Proceso <= 0;
        }
    }
}