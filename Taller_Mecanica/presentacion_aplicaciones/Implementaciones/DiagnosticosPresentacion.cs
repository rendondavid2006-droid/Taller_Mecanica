 
 

using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;

namespace presentacion_aplicaciones.Implementaciones
{
    public class DiagnosticosPresentacion : IDiagnosticosPresentacion
    {
        private IComunicaciones IComunicaciones;

        public DiagnosticosPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

         
        public Diagnosticos Actualizar(Diagnosticos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Diagnosticos>(
                "Diagnosticos/Actualizar", 
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public Diagnosticos Borrar(Diagnosticos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Diagnosticos>(
                "Diagnosticos/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public List<Diagnosticos> Buscar(Diagnosticos entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Diagnosticos>>(
                "Diagnosticos/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public List<Diagnosticos> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Diagnosticos>>(
                "Diagnosticos/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

         
        public Diagnosticos Insertar(Diagnosticos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Diagnosticos>(
                "Diagnosticos/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        private bool Validar(Diagnosticos entidad)
        {
            return entidad.Orden <= 0 ||
                entidad.Empleado <= 0 ||
                entidad.Fecha_Diagnostico == default ||
                string.IsNullOrEmpty(entidad.Descripcion_Problema) ||
                string.IsNullOrEmpty(entidad.Observaciones);
        }
    }
}