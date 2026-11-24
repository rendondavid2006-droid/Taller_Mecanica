 
 

using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;

namespace presentacion_aplicaciones.Implementaciones
{
    public class MarcasPresentacion : IMarcasPresentacion
    {
        private IComunicaciones IComunicaciones;

        public MarcasPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

         
        public Marcas Actualizar(Marcas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Marcas>(
                "Marcas/Actualizar", 
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public Marcas Borrar(Marcas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Marcas>(
                "Marcas/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public List<Marcas> Buscar(Marcas entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Marcas>>(
                "Marcas/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public List<Marcas> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Marcas>>(
                "Marcas/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

         
        public Marcas Insertar(Marcas entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Marcas>(
                "Marcas/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        private bool Validar(Marcas entidad)
        {
            return string.IsNullOrEmpty(entidad.Nombre_Marca) ||
                string.IsNullOrEmpty(entidad.Pais_Origen) ||
                string.IsNullOrEmpty(entidad.Descripcion);
        }
    }
}