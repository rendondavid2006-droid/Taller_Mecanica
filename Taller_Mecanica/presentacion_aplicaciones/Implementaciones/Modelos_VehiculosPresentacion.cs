 
 

using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;

namespace presentacion_aplicaciones.Implementaciones
{
    public class Modelos_VehiculosPresentacion : IModelos_VehiculosPresentacion
    {
        private IComunicaciones IComunicaciones;

        public Modelos_VehiculosPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

         
        public Modelos_Vehiculos Actualizar(Modelos_Vehiculos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Modelos_Vehiculos>(
                "Modelos_Vehiculos/Actualizar", 
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public Modelos_Vehiculos Borrar(Modelos_Vehiculos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Modelos_Vehiculos>(
                "Modelos_Vehiculos/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public List<Modelos_Vehiculos> Buscar(Modelos_Vehiculos entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Modelos_Vehiculos>>(
                "Modelos_Vehiculos/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public List<Modelos_Vehiculos> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Modelos_Vehiculos>>(
                "Modelos_Vehiculos/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

         
        public Modelos_Vehiculos Insertar(Modelos_Vehiculos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Modelos_Vehiculos>(
                "Modelos_Vehiculos/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        private bool Validar(Modelos_Vehiculos entidad)
        {
            return entidad.Marca <= 0 ||
                string.IsNullOrEmpty(entidad.Nombre_Modelo) ||
                entidad.Año_inicio <= 0 ||
                string.IsNullOrEmpty(entidad.Tipo_Vehiculo) ||
                string.IsNullOrEmpty(entidad.Cilindraje);
        }
    }
}