 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicio_aplicaciones.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class Modelos_VehiculosController : ControllerBase
    {
        private IModelos_VehiculosAplicacion IModelos_VehiculosAplicacion;

        public Modelos_VehiculosController(IModelos_VehiculosAplicacion IModelos_VehiculosAplicacion)
        {
            this.IModelos_VehiculosAplicacion = IModelos_VehiculosAplicacion;
        }

        [HttpPost]
         
        public List<Modelos_Vehiculos> Consultar()
        {
            return this.IModelos_VehiculosAplicacion.Consultar();
        }

        [HttpPost]
         
        public List<Modelos_Vehiculos> Buscar(Modelos_Vehiculos entidad)
        {
            return this.IModelos_VehiculosAplicacion.Buscar(entidad);
        }

        [HttpPost]
         
        public Modelos_Vehiculos Insertar(Modelos_Vehiculos entidad)
        {
            return this.IModelos_VehiculosAplicacion.Insertar(entidad);
        }

        [HttpPost]
         
        public Modelos_Vehiculos Actualizar(Modelos_Vehiculos entidad)
        {
            return this.IModelos_VehiculosAplicacion.Actualizar(entidad);
        }

        [HttpPost]
         
        public Modelos_Vehiculos Borrar(Modelos_Vehiculos entidad)
        {
            return this.IModelos_VehiculosAplicacion.Borrar(entidad);
        }
    }
}
