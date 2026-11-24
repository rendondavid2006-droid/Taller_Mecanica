 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;

namespace libreria_aplicaciones.Implementaciones
{
    public class AuditoriasAplicacion : IAuditoriasAplicacion
    {
        private IConexion IConexion;

        public AuditoriasAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

         
        public List<Auditorias> Buscar(Auditorias entidad)
        {
            return this.IConexion.Auditorias!
                .Where(x => x.Tabla!.Contains(entidad.Tabla!))
                .OrderByDescending(x => x.Id)
                .Take(50)
                .ToList();
        }

         
        public List<Auditorias> Consultar()
        {
            return this.IConexion.Auditorias!
                .OrderByDescending(x => x.Id)
                .Take(20)
                .ToList();
        }
    }
}
