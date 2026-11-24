 
 

using libreria_aplicaciones.Entidades;

namespace presentacion_aplicaciones.Interfaces
{
    public interface IDiagnosticosPresentacion
    {
        Diagnosticos Insertar(Diagnosticos entidad);
        Diagnosticos Actualizar(Diagnosticos entidad);
        List<Diagnosticos> Consultar();
        List<Diagnosticos> Buscar(Diagnosticos entidad);
        Diagnosticos Borrar(Diagnosticos entidad);
    }
}