 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicio_aplicaciones.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ServiciosController : ControllerBase
    {
        private IServiciosAplicacion IServiciosAplicacion;

        public ServiciosController(IServiciosAplicacion IServiciosAplicacion)
        {
            this.IServiciosAplicacion = IServiciosAplicacion;
        }

        [HttpPost]
         
        public List<Servicios> Consultar()
        {
            return this.IServiciosAplicacion.Consultar();
        }

        [HttpPost]
         
        public List<Servicios> Buscar(Servicios entidad)
        {
            return this.IServiciosAplicacion.Buscar(entidad);
        }

        [HttpPost]
         
        public Servicios Insertar(Servicios entidad)
        {
            return this.IServiciosAplicacion.Insertar(entidad);
        }

        [HttpPost]
         
        public Servicios Actualizar(Servicios entidad)
        {
            return this.IServiciosAplicacion.Actualizar(entidad);
        }

        [HttpPost]
         
        public Servicios Borrar(Servicios entidad)
        {
            return this.IServiciosAplicacion.Borrar(entidad);
        }
    }
}
