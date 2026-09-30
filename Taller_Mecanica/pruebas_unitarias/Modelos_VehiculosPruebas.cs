using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Implementaciones;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class Modelos_VehiculosPruebas
    {
        private IConexion conexion;
        private Modelos_Vehiculos? entidad;

        public Modelos_VehiculosPruebas()
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
            this.entidad = new Modelos_Vehiculos()
            {
                Marca = 1,
                Nombre_Modelo = "ModeloTest",
                Año_inicio = 2000,
                Tipo_Vehiculo = "TipoTest",
                Cilindraje = "Test",
            };

            this.conexion.Modelos_Vehiculos!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_modelos_vehiculos = this.conexion.Modelos_Vehiculos!.ToList();
            if (lista_modelos_vehiculos.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Cilindraje = "500c";

            var entry = this.conexion!.Entry<Modelos_Vehiculos>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Modelos_Vehiculos!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
