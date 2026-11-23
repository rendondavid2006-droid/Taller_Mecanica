 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Implementaciones;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class MarcasPruebas
    {
        private IConexion conexion;
        private Marcas? entidad;

        public MarcasPruebas()
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
            this.entidad = new Marcas()
            {
                Nombre_Marca = "MarcaTest",
                Pais_Origen = "PaisTest",
                Descripcion = "DescripcionTest",
                Estado = true
            };

            this.conexion.Marcas!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_marcas = this.conexion.Marcas!.ToList();
            if (lista_marcas.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Estado = false;

            var entry = this.conexion!.Entry<Marcas>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Marcas!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
