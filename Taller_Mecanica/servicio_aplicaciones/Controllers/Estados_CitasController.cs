 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicio_aplicaciones.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class Estados_CitasController : ControllerBase
    {
        private IEstados_CitasAplicacion IEstados_CitasAplicacion;

        public Estados_CitasController(
            IEstados_CitasAplicacion IEstados_CitasAplicacion)
        {
            this.IEstados_CitasAplicacion =
                IEstados_CitasAplicacion;
        }

        [HttpPost]
         
        public List<Estados_Citas> Consultar()
        {
            return this.IEstados_CitasAplicacion.Consultar();
        }

        [HttpPost]
         
        public List<Estados_Citas> Buscar(
            Estados_Citas entidad)
        {
            return this.IEstados_CitasAplicacion.Buscar(entidad);
        }

        [HttpPost]
         
        public Estados_Citas Insertar(
            Estados_Citas entidad)
        {
            return this.IEstados_CitasAplicacion.Insertar(entidad);
        }

        [HttpPost]
         
        public Estados_Citas Actualizar(
            Estados_Citas entidad)
        {
            return this.IEstados_CitasAplicacion.Actualizar(entidad);
        }

        [HttpPost]
         
        public Estados_Citas Borrar(
            Estados_Citas entidad)
        {
            return this.IEstados_CitasAplicacion.Borrar(entidad);
        }
    }
}