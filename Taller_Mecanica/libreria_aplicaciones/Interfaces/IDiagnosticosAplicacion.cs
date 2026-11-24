 
 

using libreria_aplicaciones.Entidades;

namespace libreria_aplicaciones.Interfaces
{
    public interface IDiagnosticosAplicacion
    {
        Diagnosticos Insertar(Diagnosticos entidad);
        Diagnosticos Actualizar(Diagnosticos entidad);
        List<Diagnosticos> Consultar();
        List<Diagnosticos> Buscar(Diagnosticos entidad);
        Diagnosticos Borrar(Diagnosticos entidad);
    }
}