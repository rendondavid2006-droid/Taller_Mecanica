 
 

using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;

namespace presentacion_aplicaciones.Implementaciones
{
    public class UsuariosPresentacion : IUsuariosPresentacion
    {
        private IComunicaciones IComunicaciones;

        public UsuariosPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

         
        public Usuarios Actualizar(Usuarios entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Usuarios>(
                "Usuarios/Actualizar", 
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public Usuarios Borrar(Usuarios entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Usuarios>(
                "Usuarios/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public List<Usuarios> Buscar(Usuarios entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Usuarios>>(
                "Usuarios/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        public List<Usuarios> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Usuarios>>(
                "Usuarios/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

         
        public Usuarios Insertar(Usuarios entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Usuarios>(
                "Usuarios/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

         
        private bool Validar(Usuarios entidad)
        {
            return entidad.Empleado <= 0 ||
                string.IsNullOrEmpty(entidad.Nombre_Usuario) ||
                string.IsNullOrEmpty(entidad.Contraseña) ||
                entidad.Fecha_Creacion == default;
        }

         
        public Usuarios Login(Usuarios entidad)
        {
            if (string.IsNullOrEmpty(entidad.Nombre_Usuario) ||
                string.IsNullOrEmpty(entidad.Contraseña))
            {
                throw new Exception("Falta informacion");
            }

            var task = this.IComunicaciones.Ejecutar<Usuarios>(
                "Usuarios/Login",
                entidad);

            task.Wait();

            return task.Result;
        }
    }
}