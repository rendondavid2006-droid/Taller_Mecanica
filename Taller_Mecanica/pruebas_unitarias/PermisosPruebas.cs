 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Implementaciones;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class PermisosPruebas
    {
        private IConexion conexion;
        private Permisos? entidad;

        public PermisosPruebas()
        {
            this.conexion = new Conexion();
        }

        [TestMethod]
        public void Ejecutar()
        {
            Insertar();
            Consultar();
            Actualizar();
            Borrar();
        }

        private void Insertar()
        {
            this.entidad = new Permisos()
            {
                Rol = 1,
                Nombre_Permiso = "PermisoTest",
                Descripcion = "Permiso de prueba",
                Estado = true
            };

            this.conexion.Permisos!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_permisos = this.conexion.Permisos!.ToList();
            if (lista_permisos.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Estado = false;

            var entry = this.conexion!.Entry<Permisos>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Permisos!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
