using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Implementaciones;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class DiagnosticosPruebas
    {
        private IConexion conexion;
        private Diagnosticos? entidad;

        public DiagnosticosPruebas()
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
            this.entidad = new Diagnosticos()
            {
                Orden = 1,
                Empleado = 1,
                Fecha_Diagnostico = DateTime.Now,
                Descripcion_Problema = "Test",
                Observaciones = "Pendiente"
            };

            this.conexion.Diagnosticos!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_diagnosticos = this.conexion.Diagnosticos!.ToList();
            if (lista_diagnosticos.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Observaciones = "Test.1";

            var entry = this.conexion!.Entry<Diagnosticos>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Diagnosticos!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
