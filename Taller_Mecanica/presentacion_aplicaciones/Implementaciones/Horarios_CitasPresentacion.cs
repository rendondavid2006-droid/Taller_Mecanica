 
 

using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;

namespace presentacion_aplicaciones.Implementaciones
{
    public class Horarios_CitasPresentacion : IHorarios_CitasPresentacion
    {
        private IComunicaciones IComunicaciones;

        public Horarios_CitasPresentacion(
            IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

         
        public Horarios_Citas Actualizar(
            Horarios_Citas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task =
                this.IComunicaciones.Ejecutar<Horarios_Citas>(
                    "Horarios_Citas/Actualizar",
                    entidad);

            task.Wait();
            return task.Result;
        }

         
        public Horarios_Citas Borrar(
            Horarios_Citas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            var task =
                this.IComunicaciones.Ejecutar<Horarios_Citas>(
                    "Horarios_Citas/Borrar",
                    entidad);

            task.Wait();
            return task.Result;
        }

         
        public List<Horarios_Citas> Buscar(
            Horarios_Citas entidad)
        {
            var task =
                this.IComunicaciones.Ejecutar<List<Horarios_Citas>>(
                    "Horarios_Citas/Buscar",
                    entidad);

            task.Wait();
            return task.Result;
        }

         
        public List<Horarios_Citas> Consultar()
        {
            var task =
                this.IComunicaciones.Ejecutar<List<Horarios_Citas>>(
                    "Horarios_Citas/Consultar",
                    null);

            task.Wait();
            return task.Result;
        }

         
        public Horarios_Citas Insertar(
            Horarios_Citas entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task =
                this.IComunicaciones.Ejecutar<Horarios_Citas>(
                    "Horarios_Citas/Insertar",
                    entidad);

            task.Wait();
            return task.Result;
        }

         
        private bool Validar(Horarios_Citas entidad)
        {
            return entidad.Fecha == DateTime.MinValue ||
                entidad.Hora_Inicio == DateTime.MinValue ||
                entidad.Hora_Fin == DateTime.MinValue;
        }
    }
}