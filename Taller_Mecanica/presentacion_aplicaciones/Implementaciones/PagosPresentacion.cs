 
 

using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;

namespace presentacion_aplicaciones.Implementaciones
{
    public class PagosPresentacion : IPagosPresentacion
    {
        private IComunicaciones IComunicaciones;

        public PagosPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

         
        public Pagos Actualizar(Pagos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Pagos>(
                "Pagos/Actualizar", 
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public Pagos Borrar(Pagos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Pagos>(
                "Pagos/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public List<Pagos> Buscar(Pagos entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Pagos>>(
                "Pagos/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public List<Pagos> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Pagos>>(
                "Pagos/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

         
        public Pagos Insertar(Pagos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Pagos>(
                "Pagos/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        private bool Validar(Pagos entidad)
        {
            return entidad.Factura <= 0 ||
                entidad.Fecha_Pago == default ||
                entidad.Monto <= 0 ||
                string.IsNullOrEmpty(entidad.Metodo_Pago) ||
                string.IsNullOrEmpty(entidad.Referencia_Pago) ||
                string.IsNullOrEmpty(entidad.Estado);
        }
    }
}