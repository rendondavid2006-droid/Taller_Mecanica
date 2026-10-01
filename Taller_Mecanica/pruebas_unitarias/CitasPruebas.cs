using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Implementaciones;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class CitasPruebas
    {
        private IConexion conexion;
        private Citas? entidad;

        public CitasPruebas()
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
            this.entidad = new Citas()
            {
                Cliente = 1,
                Vehiculo = 1,
                Fecha_Cita = DateTime.Now,
                Hora_Cita = DateTime.Now,
                Motivo = "Test",
                Estado = "Pendiente",
            };

            this.conexion.Citas!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_citas = this.conexion.Citas!.ToList();
            if (lista_citas.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Motivo = "Test.1";

            var entry = this.conexion!.Entry<Citas>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Citas!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
