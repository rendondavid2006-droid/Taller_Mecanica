using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Implementaciones;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class Detalles_Repuestos_OrdenesPruebas
    {
        private IConexion conexion;
        private Detalles_Repuestos_Ordenes? entidad;

        public Detalles_Repuestos_OrdenesPruebas()
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
            this.entidad = new Detalles_Repuestos_Ordenes()
            {
                Orden = 1,
                Repuesto = 1,
                Cantidad = 1,
                Precio_Unitario = 100.0m,
                Subtotal = 100.0m
            };

            this.conexion.Detalles_Repuestos_Ordenes!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_detalles_repuestos_ordenes = this.conexion.Detalles_Repuestos_Ordenes!.ToList();
            if (lista_detalles_repuestos_ordenes.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Subtotal = 200.0m;

            var entry = this.conexion!.Entry<Detalles_Repuestos_Ordenes>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Detalles_Repuestos_Ordenes!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
