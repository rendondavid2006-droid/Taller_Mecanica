 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicio_aplicaciones.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class Detalles_Repuestos_OrdenesController : ControllerBase
    {
        private IDetalles_Repuestos_OrdenesAplicacion IDetalles_Repuestos_OrdenesAplicacion;

        public Detalles_Repuestos_OrdenesController(IDetalles_Repuestos_OrdenesAplicacion IDetalles_Repuestos_OrdenesAplicacion)
        {
            this.IDetalles_Repuestos_OrdenesAplicacion = IDetalles_Repuestos_OrdenesAplicacion;
        }

        [HttpPost]
         
        public List<Detalles_Repuestos_Ordenes> Consultar()
        {
            return this.IDetalles_Repuestos_OrdenesAplicacion.Consultar();
        }

        [HttpPost]
         
        public List<Detalles_Repuestos_Ordenes> Buscar(Detalles_Repuestos_Ordenes entidad)
        {
            return this.IDetalles_Repuestos_OrdenesAplicacion.Buscar(entidad);
        }

        [HttpPost]
         
        public Detalles_Repuestos_Ordenes Insertar(Detalles_Repuestos_Ordenes entidad)
        {
            return this.IDetalles_Repuestos_OrdenesAplicacion.Insertar(entidad);
        }

        [HttpPost]
         
        public Detalles_Repuestos_Ordenes Actualizar(Detalles_Repuestos_Ordenes entidad)
        {
            return this.IDetalles_Repuestos_OrdenesAplicacion.Actualizar(entidad);
        }

        [HttpPost]
         
        public Detalles_Repuestos_Ordenes Borrar(Detalles_Repuestos_Ordenes entidad)
        {
            return this.IDetalles_Repuestos_OrdenesAplicacion.Borrar(entidad);
        }
    }
}
