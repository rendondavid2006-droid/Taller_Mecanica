 
 

using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;

namespace presentacion_aplicaciones.Implementaciones
{
    public class Estados_CitasPresentacion : IEstados_CitasPresentacion
    {
        private IComunicaciones IComunicaciones;

        public Estados_CitasPresentacion(
            IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

         
        public Estados_Citas Actualizar(
            Estados_Citas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task =
                this.IComunicaciones.Ejecutar<Estados_Citas>(
                    "Estados_Citas/Actualizar",
                    entidad);

            task.Wait();
            return task.Result;
        }

         
        public Estados_Citas Borrar(
            Estados_Citas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            var task =
                this.IComunicaciones.Ejecutar<Estados_Citas>(
                    "Estados_Citas/Borrar",
                    entidad);

            task.Wait();
            return task.Result;
        }

         
        public List<Estados_Citas> Buscar(
            Estados_Citas entidad)
        {
            var task =
                this.IComunicaciones.Ejecutar<List<Estados_Citas>>(
                    "Estados_Citas/Buscar",
                    entidad);

            task.Wait();
            return task.Result;
        }

         
        public List<Estados_Citas> Consultar()
        {
            var task =
                this.IComunicaciones.Ejecutar<List<Estados_Citas>>(
                    "Estados_Citas/Consultar",
                    null);

            task.Wait();
            return task.Result;
        }

         
        public Estados_Citas Insertar(
            Estados_Citas entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task =
                this.IComunicaciones.Ejecutar<Estados_Citas>(
                    "Estados_Citas/Insertar",
                    entidad);

            task.Wait();
            return task.Result;
        }

         
        private bool Validar(Estados_Citas entidad)
        {
            return string.IsNullOrEmpty(entidad.Nombre_Estado) ||
                string.IsNullOrEmpty(entidad.Descripcion);
        }
    }
}