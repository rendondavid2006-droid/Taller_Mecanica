 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicio_aplicaciones.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class Usuarios_ClientesController : ControllerBase
    {
        private IUsuarios_ClientesAplicacion IUsuarios_ClientesAplicacion;

        public Usuarios_ClientesController(
            IUsuarios_ClientesAplicacion IUsuarios_ClientesAplicacion)
        {
            this.IUsuarios_ClientesAplicacion =
                IUsuarios_ClientesAplicacion;
        }

        [HttpPost]
        public Usuarios_Clientes Login(Usuarios_Clientes entidad)
        {
            return this.IUsuarios_ClientesAplicacion
                .Login(entidad);
        }

        [HttpPost]
         
        public List<Usuarios_Clientes> Consultar()
        {
            return this.IUsuarios_ClientesAplicacion.Consultar();
        }

        [HttpPost]
         
        public List<Usuarios_Clientes> Buscar(
            Usuarios_Clientes entidad)
        {
            return this.IUsuarios_ClientesAplicacion.Buscar(entidad);
        }

        [HttpPost]
         
        public Usuarios_Clientes Insertar(
            Usuarios_Clientes entidad)
        {
            return this.IUsuarios_ClientesAplicacion.Insertar(entidad);
        }

        [HttpPost]
         
        public Usuarios_Clientes Actualizar(
            Usuarios_Clientes entidad)
        {
            return this.IUsuarios_ClientesAplicacion.Actualizar(entidad);
        }

        [HttpPost]
         
        public Usuarios_Clientes Borrar(
            Usuarios_Clientes entidad)
        {
            return this.IUsuarios_ClientesAplicacion.Borrar(entidad);
        }
    }
}