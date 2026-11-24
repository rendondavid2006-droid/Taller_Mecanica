 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace libreria_aplicaciones.Implementaciones
{
    public class MarcasAplicacion : IMarcasAplicacion
    {
        private IConexion IConexion;

        public MarcasAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }
        

         
        public Marcas Actualizar(Marcas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion!.Entry<Marcas>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Marcas", entidad.Id, "UPDATE", "Se actualizó un registro de marca");
            return entidad;
        }

         
        public Marcas Borrar(Marcas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Marcas!.Remove(entidad!);
            this.IConexion.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Marcas", entidad.Id, "DELETE", "Se eliminó un registro de marca");
            return entidad;
        }

         
        public List<Marcas> Buscar(Marcas entidad)
        {
            return this.IConexion.Marcas!
                .Where(x => x.Nombre_Marca!.Contains(entidad.Nombre_Marca!))
                .ToList();
        }

         
        public List<Marcas> Consultar()
        {
            return this.IConexion.Marcas!
                .ToList();
        }

         
        public Marcas Insertar(Marcas entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.Marcas!.Add(entidad!);
            this.IConexion.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Marcas", entidad.Id, "INSERT", "Se creó un registro de marca");
            return entidad;
        }

         
        private bool Validar(Marcas entidad)
        {
            return string.IsNullOrEmpty(entidad.Nombre_Marca) ||
                string.IsNullOrEmpty(entidad.Pais_Origen) ||
                string.IsNullOrEmpty(entidad.Descripcion);
        }
    }
}