 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicio_aplicaciones.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PagosController : ControllerBase
    {
        private IPagosAplicacion IPagosAplicacion;

        public PagosController(IPagosAplicacion IPagosAplicacion)
        {
            this.IPagosAplicacion = IPagosAplicacion;
        }

        [HttpPost]
         
        public List<Pagos> Consultar()
        {
            return this.IPagosAplicacion.Consultar();
        }

        [HttpPost]
         
        public List<Pagos> Buscar(Pagos entidad)
        {
            return this.IPagosAplicacion.Buscar(entidad);
        }

        [HttpPost]
         
        public Pagos Insertar(Pagos entidad)
        {
            return this.IPagosAplicacion.Insertar(entidad);
        }

        [HttpPost]
         
        public Pagos Actualizar(Pagos entidad)
        {
            return this.IPagosAplicacion.Actualizar(entidad);
        }

        [HttpPost]
         
        public Pagos Borrar(Pagos entidad)
        {
            return this.IPagosAplicacion.Borrar(entidad);
        }
    }
}
