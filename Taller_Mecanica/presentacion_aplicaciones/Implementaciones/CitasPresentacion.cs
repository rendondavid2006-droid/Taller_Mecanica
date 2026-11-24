 
 

using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;

namespace presentacion_aplicaciones.Implementaciones
{
    public class CitasPresentacion : ICitasPresentacion
    {
        private IComunicaciones IComunicaciones;

        public CitasPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

         
        public Citas Actualizar(Citas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Citas>(
                "Citas/Actualizar", 
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public Citas Borrar(Citas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Citas>(
                "Citas/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public List<Citas> Buscar(Citas entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Citas>>(
                "Citas/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public List<Citas> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Citas>>(
                "Citas/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

         
        public Citas Insertar(Citas entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Citas>(
                "Citas/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        private bool Validar(Citas entidad)
        {
            return entidad.Cliente <= 0 ||
                entidad.Vehiculo <= 0 ||
                entidad.Horario_Cita <= 0 ||
                string.IsNullOrEmpty(entidad.Motivo) ||
                entidad.Estado_Cita <= 0;
        }
    }
}