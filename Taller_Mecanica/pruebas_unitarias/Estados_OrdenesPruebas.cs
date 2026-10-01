using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Implementaciones;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class Estados_OrdenesPruebas
    {
        private IConexion conexion;
        private Estados_Ordenes? entidad;

        public Estados_OrdenesPruebas()
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
            this.entidad = new Estados_Ordenes()
            {
                Nombre_Estado = "EstadoTest",
                Descripcion = "DescripcionTest",
                Orden_Proceso = 1,
                Estado = true
            };

            this.conexion.Estados_Ordenes!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_estados_ordenes = this.conexion.Estados_Ordenes!.ToList();
            if (lista_estados_ordenes.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Estado = false;

            var entry = this.conexion!.Entry<Estados_Ordenes>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Estados_Ordenes!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
