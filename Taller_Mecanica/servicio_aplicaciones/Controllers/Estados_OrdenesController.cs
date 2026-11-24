 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicio_aplicaciones.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class Estados_OrdenesController : ControllerBase
    {
        private IEstados_OrdenesAplicacion IEstados_OrdenesAplicacion;

        public Estados_OrdenesController(IEstados_OrdenesAplicacion IEstados_OrdenesAplicacion)
        {
            this.IEstados_OrdenesAplicacion = IEstados_OrdenesAplicacion;
        }

        [HttpPost]
         
        public List<Estados_Ordenes> Consultar()
        {
            return this.IEstados_OrdenesAplicacion.Consultar();
        }

        [HttpPost]
         
        public List<Estados_Ordenes> Buscar(Estados_Ordenes entidad)
        {
            return this.IEstados_OrdenesAplicacion.Buscar(entidad);
        }

        [HttpPost]
         
        public Estados_Ordenes Insertar(Estados_Ordenes entidad)
        {
            return this.IEstados_OrdenesAplicacion.Insertar(entidad);
        }

        [HttpPost]
         
        public Estados_Ordenes Actualizar(Estados_Ordenes entidad)
        {
            return this.IEstados_OrdenesAplicacion.Actualizar(entidad);
        }

        [HttpPost]
         
        public Estados_Ordenes Borrar(Estados_Ordenes entidad)
        {
            return this.IEstados_OrdenesAplicacion.Borrar(entidad);
        }
    }
}
