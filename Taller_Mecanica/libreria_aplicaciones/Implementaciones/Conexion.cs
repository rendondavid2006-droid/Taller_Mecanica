using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace libreria_aplicaciones.Implementaciones
{
    public class Conexion : DbContext, IConexion
    {
        public string? StringConexion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.StringConexion!, p => { });
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        public DbSet<Clientes>? Clientes { get; set; }
        public DbSet<Marcas>? Marcas { get; set; }
        public DbSet<Roles>? Roles { get; set; }
        public DbSet<Proveedores>? Proveedores { get; set; }
        public DbSet<Servicios>? Servicios { get; set; }
        public DbSet<Estados_Ordenes>? Estados_Ordenes { get; set; }
        public DbSet<Repuestos>? Repuestos { get; set; }
        public DbSet<Modelos_Vehiculos>? Modelos_Vehiculos { get; set; }
        public DbSet<Vehiculos>? Vehiculos { get; set; }
        public DbSet<Empleados>? Empleados { get; set; }
        public DbSet<Usuarios>? Usuarios { get; set; }
        public DbSet<Citas>? Citas { get; set; }
        public DbSet<Compras>? Compras { get; set; }
        public DbSet<Detalles_Compras>? Detalles_Compras { get; set; }
        public DbSet<Ordenes_Servicios>? Ordenes_Servicios { get; set; }
        public DbSet<Diagnosticos>? Diagnosticos { get; set; }
        public DbSet<Facturas>? Facturas { get; set; }
        public DbSet<Pagos>? Pagos { get; set; }
        public DbSet<Detalles_Ordenes_Servicios>? Detalles_Ordenes_Servicios { get; set; }
        public DbSet<Detalles_Repuestos_Ordenes>? Detalles_Repuestos_Ordenes { get; set; }

    }
}
