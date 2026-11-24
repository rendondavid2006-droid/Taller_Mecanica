 
 

using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;

namespace presentacion_aplicaciones.Implementaciones
{
    public class Detalles_ComprasPresentacion : IDetalles_ComprasPresentacion
    {
        private IComunicaciones IComunicaciones;

        public Detalles_ComprasPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

         
        public Detalles_Compras Actualizar(Detalles_Compras entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Detalles_Compras>(
                "Detalles_Compras/Actualizar", 
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public Detalles_Compras Borrar(Detalles_Compras entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Detalles_Compras>(
                "Detalles_Compras/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public List<Detalles_Compras> Buscar(Detalles_Compras entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Detalles_Compras>>(
                "Detalles_Compras/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public List<Detalles_Compras> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Detalles_Compras>>(
                "Detalles_Compras/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

         
        public Detalles_Compras Insertar(Detalles_Compras entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Detalles_Compras>(
                "Detalles_Compras/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        private bool Validar(Detalles_Compras entidad)
        {
            return entidad.Compra <= 0 ||
                entidad.Repuesto <= 0 ||
                entidad.Cantidad <= 0 ||
                entidad.Precio_Unitario <= 0 ||
                entidad.Subtotal <= 0;
        }
    }
}