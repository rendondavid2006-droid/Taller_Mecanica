using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Implementaciones;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class EmpleadosPruebas
    {
        private IConexion conexion;
        private Empleados? entidad;

        public EmpleadosPruebas()
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
            this.entidad = new Empleados()
            {
                Rol = 1,
                Documento = "123",
                Nombre = "Test",
                Apellido = "TestApellido",
                Telefono = "TestTelefono",
                Correo = "TestCorreo",
            };

            this.conexion.Empleados!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_empleados = this.conexion.Empleados!.ToList();
            if (lista_empleados.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Documento = "0";

            var entry = this.conexion!.Entry<Empleados>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Empleados!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
