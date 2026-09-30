using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Implementaciones;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class RolesPruebas
    {
        private IConexion conexion;
        private Roles? entidad;

        public RolesPruebas()
        {
            this.conexion = new Conexion();
            conexion.StringConexion = "server=localhost;database=Taller_Mecanica;Integrated Security=True;TrustServerCertificate=true;";
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
            this.entidad = new Roles()
            {
                Nombre_Rol = "RolTest",
                Descripcion = "Test",
                Nivel_Acceso = 1,
                Estado = true
            };

            this.conexion.Roles!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_roles = this.conexion.Roles!.ToList();
            if (lista_roles.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Estado = false;

            var entry = this.conexion!.Entry<Roles>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Roles!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
