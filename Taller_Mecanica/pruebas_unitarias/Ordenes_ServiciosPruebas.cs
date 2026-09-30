using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Implementaciones;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class Ordenes_ServiciosPruebas
    {
        private IConexion conexion;
        private Ordenes_Servicios? entidad;

        public Ordenes_ServiciosPruebas()
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
            this.entidad = new Ordenes_Servicios()
            {
                Cita = 1,
                Vehiculo = 1,
                Empleado = 1,
                Estado_Orden = 1,
                Fecha_Ingreso = DateTime.Now,
                Kilometraje_Ingreso = "500",
                Observaciones = "ObservacionesTest"
            };

            this.conexion.Ordenes_Servicios!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_ordenes_Servicios = this.conexion.Ordenes_Servicios!.ToList();
            if (lista_ordenes_Servicios.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Kilometraje_Ingreso = "100";

            var entry = this.conexion!.Entry<Ordenes_Servicios>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Ordenes_Servicios!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
