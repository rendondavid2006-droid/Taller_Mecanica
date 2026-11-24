 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicio_aplicaciones.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DiagnosticosController : ControllerBase
    {
        private IDiagnosticosAplicacion IDiagnosticosAplicacion;

        public DiagnosticosController(IDiagnosticosAplicacion IDiagnosticosAplicacion)
        {
            this.IDiagnosticosAplicacion = IDiagnosticosAplicacion;
        }

        [HttpPost]
         
        public List<Diagnosticos> Consultar()
        {
            return this.IDiagnosticosAplicacion.Consultar();
        }

        [HttpPost]
         
        public List<Diagnosticos> Buscar(Diagnosticos entidad)
        {
            return this.IDiagnosticosAplicacion.Buscar(entidad);
        }

        [HttpPost]
         
        public Diagnosticos Insertar(Diagnosticos entidad)
        {
            return this.IDiagnosticosAplicacion.Insertar(entidad);
        }

        [HttpPost]
         
        public Diagnosticos Actualizar(Diagnosticos entidad)
        {
            return this.IDiagnosticosAplicacion.Actualizar(entidad);
        }

        [HttpPost]
         
        public Diagnosticos Borrar(Diagnosticos entidad)
        {
            return this.IDiagnosticosAplicacion.Borrar(entidad);
        }
    }
}
