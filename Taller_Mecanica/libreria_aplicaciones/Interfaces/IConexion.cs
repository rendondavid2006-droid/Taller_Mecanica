
using libreria_aplicaciones.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace libreria_aplicaciones.Interfaces
{
    public interface IConexion
    {
        string? StringConexion { get; set; }

        DbSet<Clientes>? Clientes { get; set; }
        DbSet<Marcas>? Marcas { get; set; }
        DbSet<Roles>? Roles { get; set; }
        DbSet<Proveedores>? Proveedores { get; set; }
        DbSet<Servicios>? Servicios { get; set; }
        DbSet<Estados_Ordenes>? Estados_Ordenes { get; set; }
        DbSet<Repuestos>? Repuestos { get; set; }
        DbSet<Modelos_Vehiculos>? Modelos_Vehiculos { get; set; }
        DbSet<Vehiculos>? Vehiculos { get; set; }
        DbSet<Empleados>? Empleados { get; set; }
        DbSet<Usuarios>? Usuarios { get; set; }
        DbSet<Citas>? Citas { get; set; }
        DbSet<Compras>? Compras { get; set; }
        DbSet<Detalles_Compras>? Detalles_Compras { get; set; }
        DbSet<Ordenes_Servicios>? Ordenes_Servicios { get; set; }
        DbSet<Diagnosticos>? Diagnosticos { get; set; }
        DbSet<Facturas>? Facturas { get; set; }
        DbSet<Pagos>? Pagos { get; set; }
        DbSet<Detalles_Ordenes_Servicios>? Detalles_Ordenes_Servicios { get; set; }
        DbSet<Detalles_Repuestos_Ordenes>? Detalles_Repuestos_Ordenes { get; set; }

        EntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}
