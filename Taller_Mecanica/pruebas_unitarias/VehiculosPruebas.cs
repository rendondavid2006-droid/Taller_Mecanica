using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Implementaciones;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class VehiculosPruebas
    {
        private IConexion conexion;
        private Vehiculos? entidad;

        public VehiculosPruebas()
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
            this.entidad = new Vehiculos()
            {
                Cliente = 1,
                Modelo = 1,
                Placa = "ABC123",
                Año = 2024,
                Color = "AzulTest",
                Kilometraje = "1000",
            };

            this.conexion.Vehiculos!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_vehiculos = this.conexion.Vehiculos!.ToList();
            if (lista_vehiculos.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Placa = "Test";

            var entry = this.conexion!.Entry<Vehiculos>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Vehiculos!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
