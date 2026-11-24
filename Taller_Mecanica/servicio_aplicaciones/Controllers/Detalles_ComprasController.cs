 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicio_aplicaciones.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class Detalles_ComprasController : ControllerBase
    {
        private IDetalles_ComprasAplicacion IDetalles_ComprasAplicacion;

        public Detalles_ComprasController(IDetalles_ComprasAplicacion IDetalles_ComprasAplicacion)
        {
            this.IDetalles_ComprasAplicacion = IDetalles_ComprasAplicacion;
        }

        [HttpPost]
         
        public List<Detalles_Compras> Consultar()
        {
            return this.IDetalles_ComprasAplicacion.Consultar();
        }

        [HttpPost]
         
        public List<Detalles_Compras> Buscar(Detalles_Compras entidad)
        {
            return this.IDetalles_ComprasAplicacion.Buscar(entidad);
        }

        [HttpPost]
         
        public Detalles_Compras Insertar(Detalles_Compras entidad)
        {
            return this.IDetalles_ComprasAplicacion.Insertar(entidad);
        }

        [HttpPost]
         
        public Detalles_Compras Actualizar(Detalles_Compras entidad)
        {
            return this.IDetalles_ComprasAplicacion.Actualizar(entidad);
        }

        [HttpPost]
         
        public Detalles_Compras Borrar(Detalles_Compras entidad)
        {
            return this.IDetalles_ComprasAplicacion.Borrar(entidad);
        }
    }
}
