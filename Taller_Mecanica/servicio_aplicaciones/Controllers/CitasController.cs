 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicio_aplicaciones.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CitasController : ControllerBase
    {
        private ICitasAplicacion ICitasAplicacion;

        public CitasController(ICitasAplicacion ICitasAplicacion)
        {
            this.ICitasAplicacion = ICitasAplicacion;
        }

        [HttpPost]
         
        public List<Citas> Consultar()
        {
            return this.ICitasAplicacion.Consultar();
        }

        [HttpPost]
         
        public List<Citas> Buscar(Citas entidad)
        {
            return this.ICitasAplicacion.Buscar(entidad);
        }

        [HttpPost]
         
        public Citas Insertar(Citas entidad)
        {
            return this.ICitasAplicacion.Insertar(entidad);
        }

        [HttpPost]
         
        public Citas Actualizar(Citas entidad)
        {
            return this.ICitasAplicacion.Actualizar(entidad);
        }

        [HttpPost]
         
        public Citas Borrar(Citas entidad)
        {
            return this.ICitasAplicacion.Borrar(entidad);
        }
    }
}
