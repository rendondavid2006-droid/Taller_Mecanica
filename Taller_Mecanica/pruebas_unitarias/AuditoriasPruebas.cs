 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Implementaciones;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class AuditoriasPruebas
    {
        private IConexion conexion;
        private Auditorias? entidad;

        public AuditoriasPruebas()
        {
            this.conexion = new Conexion();
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
            this.entidad = new Auditorias()
            {
                Tabla = "Prueba",
                RegistroId = 1,
                Accion = "INSERT",
                Fecha = DateTime.Now,
                Descripcion = "Registro de auditoria de prueba",
                Usuario = "Test"
            };

            this.conexion.Auditorias!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_auditorias = this.conexion.Auditorias!.ToList();
            if (lista_auditorias.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Descripcion = "Registro de auditoria actualizado";

            var entry = this.conexion!.Entry<Auditorias>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Auditorias!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
