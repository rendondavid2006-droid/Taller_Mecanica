 
 

using libreria_aplicaciones.Implementaciones;
using libreria_aplicaciones.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
 
builder.Services.AddScoped<IEstados_CitasAplicacion,Estados_CitasAplicacion>();
builder.Services.AddScoped<IHorarios_CitasAplicacion,Horarios_CitasAplicacion>();
builder.Services.AddScoped<IUsuarios_ClientesAplicacion,Usuarios_ClientesAplicacion>();
builder.Services.AddScoped<IConexion, Conexion>();
builder.Services.AddScoped<ICitasAplicacion, CitasAplicacion>();
builder.Services.AddScoped<IClientesAplicacion, ClientesAplicacion>();
builder.Services.AddScoped<IComprasAplicacion, ComprasAplicacion>();
builder.Services.AddScoped<IDetalles_ComprasAplicacion, Detalles_ComprasAplicacion>();
builder.Services.AddScoped<IDetalles_Ordenes_ServiciosAplicacion, Detalles_Ordenes_ServiciosAplicacion>();
builder.Services.AddScoped<IDetalles_Repuestos_OrdenesAplicacion, Detalles_Repuestos_OrdenesAplicacion>();
builder.Services.AddScoped<IDiagnosticosAplicacion, DiagnosticosAplicacion>();
builder.Services.AddScoped<IEmpleadosAplicacion, EmpleadosAplicacion>();
builder.Services.AddScoped<IEstados_OrdenesAplicacion, Estados_OrdenesAplicacion>();
builder.Services.AddScoped<IFacturasAplicacion, FacturasAplicacion>();
builder.Services.AddScoped<IMarcasAplicacion, MarcasAplicacion>();
builder.Services.AddScoped<IModelos_VehiculosAplicacion, Modelos_VehiculosAplicacion>();
builder.Services.AddScoped<IOrdenes_ServiciosAplicacion, Ordenes_ServiciosAplicacion>();
builder.Services.AddScoped<IPagosAplicacion, PagosAplicacion>();
builder.Services.AddScoped<IProveedoresAplicacion, ProveedoresAplicacion>();
builder.Services.AddScoped<IRepuestosAplicacion, RepuestosAplicacion>();
builder.Services.AddScoped<IRolesAplicacion, RolesAplicacion>();
builder.Services.AddScoped<IPermisosAplicacion, PermisosAplicacion>();
builder.Services.AddScoped<IServiciosAplicacion, ServiciosAplicacion>();
builder.Services.AddScoped<IUsuariosAplicacion, UsuariosAplicacion>();
builder.Services.AddScoped<IVehiculosAplicacion, VehiculosAplicacion>();
builder.Services.AddScoped<IAuditoriasAplicacion, AuditoriasAplicacion>();


var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
 
app.Use(async (contexto, siguiente) =>
{
    var conexion = contexto.RequestServices.GetRequiredService<IConexion>();
    conexion.UsuarioActual = contexto.Request.Headers["X-Usuario"].FirstOrDefault();
    await siguiente();
});
app.MapControllers();
app.MapGet("/", () => "");
app.Run();
