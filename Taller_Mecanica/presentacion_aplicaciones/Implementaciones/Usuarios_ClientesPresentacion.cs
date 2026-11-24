 
 

using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;

namespace presentacion_aplicaciones.Implementaciones
{
    public class Usuarios_ClientesPresentacion :
        IUsuarios_ClientesPresentacion
    {
        private IComunicaciones IComunicaciones;

        public Usuarios_ClientesPresentacion(
            IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

         
        public Usuarios_Clientes Login(
            Usuarios_Clientes entidad)
        {
            if (string.IsNullOrEmpty(entidad.Nombre_Usuario) ||
                string.IsNullOrEmpty(entidad.Contraseña))
            {
                throw new Exception("Falta informacion");
            }

            var task =
                this.IComunicaciones
                    .Ejecutar<Usuarios_Clientes>(
                        "Usuarios_Clientes/Login",
                        entidad);

            task.Wait();

            return task.Result;
        }

         
        public Usuarios_Clientes Actualizar(
            Usuarios_Clientes entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task =
                this.IComunicaciones.Ejecutar<Usuarios_Clientes>(
                    "Usuarios_Clientes/Actualizar",
                    entidad);

            task.Wait();
            return task.Result;
        }

         
        public Usuarios_Clientes Borrar(
            Usuarios_Clientes entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            var task =
                this.IComunicaciones.Ejecutar<Usuarios_Clientes>(
                    "Usuarios_Clientes/Borrar",
                    entidad);

            task.Wait();
            return task.Result;
        }

         
        public List<Usuarios_Clientes> Buscar(
            Usuarios_Clientes entidad)
        {
            var task =
                this.IComunicaciones.Ejecutar<List<Usuarios_Clientes>>(
                    "Usuarios_Clientes/Buscar",
                    entidad);

            task.Wait();
            return task.Result;
        }

         
        public List<Usuarios_Clientes> Consultar()
        {
            var task =
                this.IComunicaciones.Ejecutar<List<Usuarios_Clientes>>(
                    "Usuarios_Clientes/Consultar",
                    null);

            task.Wait();
            return task.Result;
        }

         
        public Usuarios_Clientes Insertar(
            Usuarios_Clientes entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task =
                this.IComunicaciones.Ejecutar<Usuarios_Clientes>(
                    "Usuarios_Clientes/Insertar",
                    entidad);

            task.Wait();
            return task.Result;
        }

         
        private bool Validar(Usuarios_Clientes entidad)
        {
            return entidad.Cliente <= 0 ||
                entidad.Rol <= 0 ||
                string.IsNullOrEmpty(entidad.Nombre_Usuario) ||
                string.IsNullOrEmpty(entidad.Contraseña) ||
                entidad.Fecha_Creacion == DateTime.MinValue;
        }
    }
}