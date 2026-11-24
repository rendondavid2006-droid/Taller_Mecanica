 
 

using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;

namespace presentacion_aplicaciones.Implementaciones
{
    public class RepuestosPresentacion : IRepuestosPresentacion
    {
        private IComunicaciones IComunicaciones;

        public RepuestosPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

         
        public Repuestos Actualizar(Repuestos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Repuestos>(
                "Repuestos/Actualizar", 
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public Repuestos Borrar(Repuestos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Repuestos>(
                "Repuestos/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public List<Repuestos> Buscar(Repuestos entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Repuestos>>(
                "Repuestos/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public List<Repuestos> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Repuestos>>(
                "Repuestos/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

         
        public Repuestos Insertar(Repuestos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Repuestos>(
                "Repuestos/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        private bool Validar(Repuestos entidad)
        {
            return string.IsNullOrEmpty(entidad.Nombre_Repuesto) ||
                string.IsNullOrEmpty(entidad.Descripcion) ||
                entidad.Precio_Compra <= 0 ||
                entidad.Precio_Venta <= 0 ||
                entidad.Stock < 0 ||
                entidad.Stock_Minimo < 0;
        }
    }
}