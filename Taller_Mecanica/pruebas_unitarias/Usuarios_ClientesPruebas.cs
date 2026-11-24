 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Implementaciones;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class Usuarios_ClientesPruebas
    {
        private IConexion conexion;
        private Usuarios_Clientes? entidad;

        public Usuarios_ClientesPruebas()
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
            this.entidad = new Usuarios_Clientes()
            {
                Cliente = 1,
                Rol = 3,
                Nombre_Usuario = "ClienteTest",
                Contraseña = "Test123",
                Estado = true,
                Fecha_Creacion = DateTime.Now
            };

            this.conexion.Usuarios_Clientes!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_usuarios_clientes = this.conexion.Usuarios_Clientes!.ToList();
            if (lista_usuarios_clientes.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Estado = false;

            var entry = this.conexion!.Entry<Usuarios_Clientes>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Usuarios_Clientes!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
