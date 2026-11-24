 
 

using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;

namespace presentacion_aplicaciones.Implementaciones
{
    public class Detalles_Repuestos_OrdenesPresentacion : IDetalles_Repuestos_OrdenesPresentacion
    {
        private IComunicaciones IComunicaciones;

        public Detalles_Repuestos_OrdenesPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

         
        public Detalles_Repuestos_Ordenes Actualizar(Detalles_Repuestos_Ordenes entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Detalles_Repuestos_Ordenes>(
                "Detalles_Repuestos_Ordenes/Actualizar", 
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public Detalles_Repuestos_Ordenes Borrar(Detalles_Repuestos_Ordenes entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Detalles_Repuestos_Ordenes>(
                "Detalles_Repuestos_Ordenes/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public List<Detalles_Repuestos_Ordenes> Buscar(Detalles_Repuestos_Ordenes entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Detalles_Repuestos_Ordenes>>(
                "Detalles_Repuestos_Ordenes/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public List<Detalles_Repuestos_Ordenes> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Detalles_Repuestos_Ordenes>>(
                "Detalles_Repuestos_Ordenes/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

         
        public Detalles_Repuestos_Ordenes Insertar(Detalles_Repuestos_Ordenes entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Detalles_Repuestos_Ordenes>(
                "Detalles_Repuestos_Ordenes/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        private bool Validar(Detalles_Repuestos_Ordenes entidad)
        {
            return entidad.Orden <= 0 ||
                entidad.Repuesto <= 0 ||
                entidad.Cantidad <= 0 ||
                entidad.Precio_Unitario <= 0 ||
                entidad.Subtotal <= 0;
        }
    }
}