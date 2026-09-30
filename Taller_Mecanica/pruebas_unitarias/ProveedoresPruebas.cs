using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Implementaciones;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class ProveedoresPruebas
    {
        private IConexion conexion;
        private Proveedores? entidad;

        public ProveedoresPruebas()
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
            this.entidad = new Proveedores()
            {
                Nit = "1",
                Nombre_Proveedor = "Test",
                Telefono = "0000",
                Correo = "admin@test",
                Direccion = "DireccionTest"
            };

            this.conexion.Proveedores!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_proveedores = this.conexion.Proveedores!.ToList();
            if (lista_proveedores.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Nit = "1000";

            var entry = this.conexion!.Entry<Proveedores>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Proveedores!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
