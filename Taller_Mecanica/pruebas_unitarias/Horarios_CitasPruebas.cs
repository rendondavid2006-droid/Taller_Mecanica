 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Implementaciones;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class Horarios_CitasPruebas
    {
        private IConexion conexion;
        private Horarios_Citas? entidad;

        public Horarios_CitasPruebas()
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
            DateTime fechaPrueba = DateTime.Today.AddDays(1);

            this.entidad = new Horarios_Citas()
            {
                Fecha = fechaPrueba,
                Hora_Inicio = fechaPrueba.AddHours(8),
                Hora_Fin = fechaPrueba.AddHours(9),
                Disponible = true
            };

            this.conexion.Horarios_Citas!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_horarios_citas = this.conexion.Horarios_Citas!.ToList();
            if (lista_horarios_citas.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Disponible = false;

            var entry = this.conexion!.Entry<Horarios_Citas>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Horarios_Citas!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
