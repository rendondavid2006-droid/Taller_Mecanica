using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Implementaciones;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class PagosPruebas
    {
        private IConexion conexion;
        private Pagos? entidad;

        public PagosPruebas()
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
            this.entidad = new Pagos()
            {
                Factura = 1,
                Fecha_Pago = DateTime.Now,
                Monto = 100.0m,
                Metodo_Pago = "Tarjeta",
                Referencia_Pago = "TestReferencia",
                Estado = "Pendiente",
            };

            this.conexion.Pagos!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_pagos = this.conexion.Pagos!.ToList();
            if (lista_pagos.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Estado = "Aprobado";

            var entry = this.conexion!.Entry<Pagos>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Pagos!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
