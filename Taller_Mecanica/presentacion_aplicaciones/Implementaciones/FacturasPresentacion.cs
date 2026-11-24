 
 

using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;

namespace presentacion_aplicaciones.Implementaciones
{
    public class FacturasPresentacion : IFacturasPresentacion
    {
        private IComunicaciones IComunicaciones;

        public FacturasPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

         
        public Facturas Actualizar(Facturas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Facturas>(
                "Facturas/Actualizar", 
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public Facturas Borrar(Facturas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Facturas>(
                "Facturas/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public List<Facturas> Buscar(Facturas entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Facturas>>(
                "Facturas/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public List<Facturas> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Facturas>>(
                "Facturas/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

         
        public Facturas Insertar(Facturas entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Facturas>(
                "Facturas/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        private bool Validar(Facturas entidad)
        {
            return entidad.Orden <= 0 ||
                entidad.Cliente <= 0 ||
                entidad.Fecha_Factura == default ||
                entidad.Subtotal <= 0 ||
                entidad.Impuesto < 0 ||
                string.IsNullOrEmpty(entidad.Estado) ||
                entidad.Total <= 0;
        }
    }
}