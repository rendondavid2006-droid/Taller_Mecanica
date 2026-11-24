 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Implementaciones;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class Estados_CitasPruebas
    {
        private IConexion conexion;
        private Estados_Citas? entidad;

        public Estados_CitasPruebas()
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
            this.entidad = new Estados_Citas()
            {
                Nombre_Estado = "EstadoTest",
                Descripcion = "Estado de cita de prueba",
                Estado = true
            };

            this.conexion.Estados_Citas!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_estados_citas = this.conexion.Estados_Citas!.ToList();
            if (lista_estados_citas.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Estado = false;

            var entry = this.conexion!.Entry<Estados_Citas>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Estados_Citas!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
