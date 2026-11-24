 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicio_aplicaciones.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class Horarios_CitasController : ControllerBase
    {
        private IHorarios_CitasAplicacion IHorarios_CitasAplicacion;

        public Horarios_CitasController(
            IHorarios_CitasAplicacion IHorarios_CitasAplicacion)
        {
            this.IHorarios_CitasAplicacion =
                IHorarios_CitasAplicacion;
        }

        [HttpPost]
         
        public List<Horarios_Citas> Consultar()
        {
            return this.IHorarios_CitasAplicacion.Consultar();
        }

        [HttpPost]
         
        public List<Horarios_Citas> Buscar(
            Horarios_Citas entidad)
        {
            return this.IHorarios_CitasAplicacion.Buscar(entidad);
        }

        [HttpPost]
         
        public Horarios_Citas Insertar(
            Horarios_Citas entidad)
        {
            return this.IHorarios_CitasAplicacion.Insertar(entidad);
        }

        [HttpPost]
         
        public Horarios_Citas Actualizar(
            Horarios_Citas entidad)
        {
            return this.IHorarios_CitasAplicacion.Actualizar(entidad);
        }

        [HttpPost]
         
        public Horarios_Citas Borrar(
            Horarios_Citas entidad)
        {
            return this.IHorarios_CitasAplicacion.Borrar(entidad);
        }
    }
}