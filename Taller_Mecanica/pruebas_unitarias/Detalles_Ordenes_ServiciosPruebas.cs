using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Implementaciones;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class Detalles_Ordenes_ServiciosPruebas
    {
        private IConexion conexion;
        private Detalles_Ordenes_Servicios? entidad;

        public Detalles_Ordenes_ServiciosPruebas()
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
            this.entidad = new Detalles_Ordenes_Servicios()
            {
                Orden = 1,
                Servicio = 1,
                Cantidad = 1,
                Precio_Unitario = 100.0m,
                Subtotal = 100.0m
            };

            this.conexion.Detalles_Ordenes_Servicios!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_detalles_ordenes_servicios = this.conexion.Detalles_Ordenes_Servicios!.ToList();
            if (lista_detalles_ordenes_servicios.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Subtotal = 200.0m;

            var entry = this.conexion!.Entry<Detalles_Ordenes_Servicios>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Detalles_Ordenes_Servicios!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
