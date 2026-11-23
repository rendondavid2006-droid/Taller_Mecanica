 
 

using libreria_aplicaciones.Entidades;

namespace libreria_aplicaciones.Interfaces
{
    public interface IHorarios_CitasAplicacion
    {
        Horarios_Citas Insertar(Horarios_Citas entidad);
        Horarios_Citas Actualizar(Horarios_Citas entidad);
        List<Horarios_Citas> Consultar();
        List<Horarios_Citas> Buscar(Horarios_Citas entidad);
        Horarios_Citas Borrar(Horarios_Citas entidad);
    }
}