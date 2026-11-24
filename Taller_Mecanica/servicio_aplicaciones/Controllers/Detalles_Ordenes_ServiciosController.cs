 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicio_aplicaciones.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class Detalles_Ordenes_ServiciosController : ControllerBase
    {
        private IDetalles_Ordenes_ServiciosAplicacion IDetalles_Ordenes_ServiciosAplicacion;

        public Detalles_Ordenes_ServiciosController(IDetalles_Ordenes_ServiciosAplicacion IDetalles_Ordenes_ServiciosAplicacion)
        {
            this.IDetalles_Ordenes_ServiciosAplicacion = IDetalles_Ordenes_ServiciosAplicacion;
        }

        [HttpPost]
         
        public List<Detalles_Ordenes_Servicios> Consultar()
        {
            return this.IDetalles_Ordenes_ServiciosAplicacion.Consultar();
        }

        [HttpPost]
         
        public List<Detalles_Ordenes_Servicios> Buscar(Detalles_Ordenes_Servicios entidad)
        {
            return this.IDetalles_Ordenes_ServiciosAplicacion.Buscar(entidad);
        }

        [HttpPost]
         
        public Detalles_Ordenes_Servicios Insertar(Detalles_Ordenes_Servicios entidad)
        {
            return this.IDetalles_Ordenes_ServiciosAplicacion.Insertar(entidad);
        }

        [HttpPost]
         
        public Detalles_Ordenes_Servicios Actualizar(Detalles_Ordenes_Servicios entidad)
        {
            return this.IDetalles_Ordenes_ServiciosAplicacion.Actualizar(entidad);
        }

        [HttpPost]
         
        public Detalles_Ordenes_Servicios Borrar(Detalles_Ordenes_Servicios entidad)
        {
            return this.IDetalles_Ordenes_ServiciosAplicacion.Borrar(entidad);
        }
    }
}
