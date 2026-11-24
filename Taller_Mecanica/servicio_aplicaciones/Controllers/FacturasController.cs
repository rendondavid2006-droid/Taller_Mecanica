 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicio_aplicaciones.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FacturasController : ControllerBase
    {
        private IFacturasAplicacion IFacturasAplicacion;

        public FacturasController(IFacturasAplicacion IFacturasAplicacion)
        {
            this.IFacturasAplicacion = IFacturasAplicacion;
        }

        [HttpPost]
         
        public List<Facturas> Consultar()
        {
            return this.IFacturasAplicacion.Consultar();
        }

        [HttpPost]
         
        public List<Facturas> Buscar(Facturas entidad)
        {
            return this.IFacturasAplicacion.Buscar(entidad);
        }

        [HttpPost]
         
        public Facturas Insertar(Facturas entidad)
        {
            return this.IFacturasAplicacion.Insertar(entidad);
        }

        [HttpPost]
         
        public Facturas Actualizar(Facturas entidad)
        {
            return this.IFacturasAplicacion.Actualizar(entidad);
        }

        [HttpPost]
         
        public Facturas Borrar(Facturas entidad)
        {
            return this.IFacturasAplicacion.Borrar(entidad);
        }
    }
}
