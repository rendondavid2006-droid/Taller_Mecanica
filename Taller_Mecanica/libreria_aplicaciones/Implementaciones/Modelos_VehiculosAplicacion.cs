 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace libreria_aplicaciones.Implementaciones
{
    public class Modelos_VehiculosAplicacion : IModelos_VehiculosAplicacion
    {
        private IConexion IConexion;

        public Modelos_VehiculosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

         
        public Modelos_Vehiculos Actualizar(Modelos_Vehiculos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion!.Entry<Modelos_Vehiculos>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Modelos_Vehiculos", entidad.Id, "UPDATE", "Se actualizó un registro de modelo de vehículo");
            return entidad;
        }
         
        public Modelos_Vehiculos Borrar(Modelos_Vehiculos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Modelos_Vehiculos!.Remove(entidad!);
            this.IConexion.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Modelos_Vehiculos", entidad.Id, "DELETE", "Se eliminó un registro de modelo de vehículo");
            return entidad;
        }

         
        public List<Modelos_Vehiculos> Buscar(Modelos_Vehiculos entidad)
        {
            return this.IConexion.Modelos_Vehiculos!
                .Where(x => x.Nombre_Modelo!.Contains(entidad.Nombre_Modelo!))
                .ToList();
        }

         
        public List<Modelos_Vehiculos> Consultar()
        {
            return this.IConexion.Modelos_Vehiculos!
                .Include(x => x._Marca)
                .ToList();
        }

         
        public Modelos_Vehiculos Insertar(Modelos_Vehiculos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.Modelos_Vehiculos!.Add(entidad!);
            this.IConexion.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Modelos_Vehiculos", entidad.Id, "INSERT", "Se creó un registro de modelo de vehículo");
            return entidad;
        }

         
        private bool Validar(Modelos_Vehiculos entidad)
        {
            return entidad.Marca <= 0 ||
                string.IsNullOrEmpty(entidad.Nombre_Modelo) ||
                entidad.Año_inicio <= 0 ||
                string.IsNullOrEmpty(entidad.Tipo_Vehiculo) ||
                string.IsNullOrEmpty(entidad.Cilindraje);
        }
    }
}