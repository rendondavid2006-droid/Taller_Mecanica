 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicio_aplicaciones.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class Ordenes_ServiciosController : ControllerBase
    {
        private IOrdenes_ServiciosAplicacion IOrdenes_ServiciosAplicacion;

        public Ordenes_ServiciosController(IOrdenes_ServiciosAplicacion IOrdenes_ServiciosAplicacion)
        {
            this.IOrdenes_ServiciosAplicacion = IOrdenes_ServiciosAplicacion;
        }

        [HttpPost]
         
        public List<Ordenes_Servicios> Consultar()
        {
            return this.IOrdenes_ServiciosAplicacion.Consultar();
        }

        [HttpPost]
         
        public List<Ordenes_Servicios> Buscar(Ordenes_Servicios entidad)
        {
            return this.IOrdenes_ServiciosAplicacion.Buscar(entidad);
        }

        [HttpPost]
         
        public Ordenes_Servicios Insertar(Ordenes_Servicios entidad)
        {
            return this.IOrdenes_ServiciosAplicacion.Insertar(entidad);
        }

        [HttpPost]
         
        public Ordenes_Servicios Actualizar(Ordenes_Servicios entidad)
        {
            return this.IOrdenes_ServiciosAplicacion.Actualizar(entidad);
        }

        [HttpPost]
         
        public Ordenes_Servicios Borrar(Ordenes_Servicios entidad)
        {
            return this.IOrdenes_ServiciosAplicacion.Borrar(entidad);
        }
    }
}
