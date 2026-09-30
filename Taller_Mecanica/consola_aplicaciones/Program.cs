
using libreria_aplicaciones.Interfaces;
using libreria_aplicaciones.Implementaciones;
using Microsoft.EntityFrameworkCore;


try
{
    IConexion conexion = new Conexion();
    conexion.StringConexion = "server=localhost;database=Taller_Mecanica;Integrated Security=True;TrustServerCertificate=true;";
    var lista_clientes = conexion.Clientes!.ToList();
    var lista_marcas = conexion.Marcas!.ToList();
    var lista_roles = conexion.Roles!.ToList();
    var lista_proveedores = conexion.Proveedores!.ToList();
    var lista_servicios = conexion.Servicios!.ToList();
    var lista_estados_ordenes = conexion.Estados_Ordenes!.ToList();
    var lista_repuestos = conexion.Repuestos!.ToList();
    var lista_modelos_vehiculos = conexion.Modelos_Vehiculos!.ToList();
    var lista_vehiculos = conexion.Vehiculos!.Include(x => x._Cliente).Include(x => x._Modelo).ToList();
    var lista_empleados = conexion.Empleados!.Include(x => x._Rol).ToList();
    var lista_usuarios = conexion.Usuarios!.Include(x => x._Empleado).ToList();
    var lista_citas = conexion.Citas!.Include(x => x._Cliente).Include(x => x._Vehiculo).ToList();
    var lista_compras = conexion.Compras!.Include(x => x._Proveedor).ToList();
    var lista_detalles_compras = conexion.Detalles_Compras!.Include(x => x._Compra).Include(x => x._Repuesto).ToList();
    var lista_ordenes_servicios = conexion.Ordenes_Servicios!.Include(x => x._Cita).Include(x => x._Vehiculo).Include(x => x._Empleado).ToList();
    var lista_diagnosticos = conexion.Diagnosticos!.Include(x => x._Orden).Include(x => x._Empleado).ToList();
    var lista_facturas = conexion.Facturas!.Include(x => x._Orden).Include(x => x._Cliente).ToList();
    var lista_pagos = conexion.Pagos!.Include(x => x._Factura).ToList();
    var lista_detalles_ordenes_servicios = conexion.Detalles_Ordenes_Servicios!.Include(x => x._Orden).Include(x => x._Servicio).ToList();
    var lista_detalles_repuestos_ordenes = conexion.Detalles_Repuestos_Ordenes!.Include(x => x._Orden).Include(x => x._Repuesto).ToList();
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}

Console.WriteLine("Ejecutado Correctamente");
