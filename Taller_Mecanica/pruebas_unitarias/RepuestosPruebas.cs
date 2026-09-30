using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Implementaciones;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class RepuestosPruebas
    {
        private IConexion conexion;
        private Repuestos? entidad;

        public RepuestosPruebas()
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
            this.entidad = new Repuestos()
            {
                Nombre_Repuesto = "RepuestoTest",
                Descripcion = "Test",
                Precio_Compra = 100.0m,
                Precio_Venta = 100.0m,
                Stock = 1,
                Stock_Minimo = 1,
            };

            this.conexion.Repuestos!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_repuestos = this.conexion.Repuestos!.ToList();
            if (lista_repuestos.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Stock = 1000;

            var entry = this.conexion!.Entry<Repuestos>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Repuestos!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
