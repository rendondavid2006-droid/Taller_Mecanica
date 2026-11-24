 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Implementaciones;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace libreria_aplicaciones.Implementaciones
{
    public class ClientesAplicacion : IClientesAplicacion
    {
        private IConexion IConexion;

        public ClientesAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

         
        public Clientes Actualizar(Clientes entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion!.Entry<Clientes>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Clientes", entidad.Id, "UPDATE", "Se actualizó un registro de cliente");
            return entidad;
        }

         
        public Clientes Borrar(Clientes entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Clientes!.Remove(entidad!);
            this.IConexion.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Clientes", entidad.Id, "DELETE", "Se eliminó un registro de cliente");
            return entidad;
        }

         
        public List<Clientes> Buscar(Clientes entidad)
        {
            entidad.Nombre ??= "";
            entidad.Documento ??= "";

            if (!string.IsNullOrEmpty(entidad.Documento))
            {
                return this.IConexion.Clientes!
                    .Where(x => x.Documento!.Contains(entidad.Documento))
                    .ToList();
            }

            return this.IConexion.Clientes!
                .Where(x => x.Nombre!.Contains(entidad.Nombre))
                .ToList();
        }

         
        public List<Clientes> Consultar()
        {
            return this.IConexion.Clientes!.ToList();
        }

         
        public Clientes Insertar(Clientes entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.Clientes!.Add(entidad!);
            this.IConexion.SaveChanges();
            Auditoria.Registrar(this.IConexion, "Clientes", entidad.Id, "INSERT", "Se creó un registro de cliente");
            return entidad;
        }

         
        private bool Validar(Clientes entidad)
        {
            return string.IsNullOrEmpty(entidad.Documento) ||
                string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Apellido) ||
                string.IsNullOrEmpty(entidad.Telefono) ||
                string.IsNullOrEmpty(entidad.Correo) ||
                string.IsNullOrEmpty(entidad.Direccion);
        }
    }
}

