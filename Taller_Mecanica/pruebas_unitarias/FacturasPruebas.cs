using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Implementaciones;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class FacturasPruebas
    {
        private IConexion conexion;
        private Facturas? entidad;

        public FacturasPruebas()
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
            this.entidad = new Facturas()
            {
                Orden = 1,
                Cliente = 1,
                Fecha_Factura = DateTime.Now,
                Subtotal = 100.0m,
                Impuesto = 100.0m,
                Estado = "Pendiente",
                Total = 100.0m
            };

            this.conexion.Facturas!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_facturas = this.conexion.Facturas!.ToList();
            if (lista_facturas.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Estado = "Test.1";

            var entry = this.conexion!.Entry<Facturas>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Facturas!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
