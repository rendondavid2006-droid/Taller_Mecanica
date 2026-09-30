using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Implementaciones;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class UsuariosPruebas
    {
        private IConexion conexion;
        private Usuarios? entidad;

        public UsuariosPruebas()
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
            this.entidad = new Usuarios()
            {
                Empleado = 1,
                Nombre_Usuario = "TestUser",
                Contraseña = "TestPassword",
                Estado = true,
                Fecha_Creacion = DateTime.Now,
            };

            this.conexion.Usuarios!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_usuarios = this.conexion.Usuarios!.ToList();
            if (lista_usuarios.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Estado = false;

            var entry = this.conexion!.Entry<Usuarios>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Usuarios!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
