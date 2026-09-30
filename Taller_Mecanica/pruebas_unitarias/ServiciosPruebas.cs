using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Implementaciones;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class ServiciosPruebas
    {
        private IConexion conexion;
        private Servicios? entidad;

        public ServiciosPruebas()
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
            this.entidad = new Servicios()
            {
                Nombre_Servicio = "ServicioTest",
                Descripcion = "Test",
                Precio_Base = 100.0m,
                Tiempo_Estimado = DateTime.Now,
                Estado = true,
            };

            this.conexion.Servicios!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_servicios = this.conexion.Servicios!.ToList();
            if (lista_servicios.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Estado = false;

            var entry = this.conexion!.Entry<Servicios>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Servicios!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
