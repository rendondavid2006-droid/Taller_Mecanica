using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Implementaciones;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class ComprasPruebas
    {
        private IConexion conexion;
        private Compras? entidad;

        public ComprasPruebas()
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
            this.entidad = new Compras()
            {
                Proveedor = 1,
                Fecha_Compra = DateTime.Now,
                Numero_Factura_Proveedor = "TestFactura",
                Total_Compra = 100.0m,
                Estado = "Pendiente"
            };

            this.conexion.Compras!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_compras = this.conexion.Compras!.ToList();
            if (lista_compras.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Estado = "Test.1";

            var entry = this.conexion!.Entry<Compras>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Compras!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
