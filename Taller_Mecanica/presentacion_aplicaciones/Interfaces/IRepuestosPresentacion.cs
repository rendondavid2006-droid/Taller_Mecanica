 
 

using libreria_aplicaciones.Entidades;

namespace presentacion_aplicaciones.Interfaces
{
    public interface IRepuestosPresentacion
    {
        Repuestos Insertar(Repuestos entidad);
        Repuestos Actualizar(Repuestos entidad);
        List<Repuestos> Consultar();
        List<Repuestos> Buscar(Repuestos entidad);
        Repuestos Borrar(Repuestos entidad);
    }
}