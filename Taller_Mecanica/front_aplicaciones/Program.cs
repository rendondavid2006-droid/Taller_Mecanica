


using presentacion_aplicaciones.Implementaciones;
using presentacion_aplicaciones.Interfaces;
using QuestPDF.Infrastructure;

QuestPDF.Settings.License =
    LicenseType.Community;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IEstados_CitasPresentacion, Estados_CitasPresentacion>();
builder.Services.AddScoped<IHorarios_CitasPresentacion, Horarios_CitasPresentacion>();
builder.Services.AddScoped<IUsuarios_ClientesPresentacion, Usuarios_ClientesPresentacion>();
builder.Services.AddScoped<IComunicaciones, Comunicaciones>();
builder.Services.AddScoped<ICitasPresentacion, CitasPresentacion>();
builder.Services.AddScoped<IClientesPresentacion, ClientesPresentacion>();
builder.Services.AddScoped<IComprasPresentacion, ComprasPresentacion>();
builder.Services.AddScoped<IDetalles_ComprasPresentacion, Detalles_ComprasPresentacion>();
builder.Services.AddScoped<IDetalles_Ordenes_ServiciosPresentacion, Detalles_Ordenes_ServiciosPresentacion>();
builder.Services.AddScoped<IDetalles_Repuestos_OrdenesPresentacion, Detalles_Repuestos_OrdenesPresentacion>();
builder.Services.AddScoped<IDiagnosticosPresentacion, DiagnosticosPresentacion>();
builder.Services.AddScoped<IEmpleadosPresentacion, EmpleadosPresentacion>();
builder.Services.AddScoped<IEstados_OrdenesPresentacion, Estados_OrdenesPresentacion>();
builder.Services.AddScoped<IFacturasPresentacion, FacturasPresentacion>();
builder.Services.AddScoped<IMarcasPresentacion, MarcasPresentacion>();
builder.Services.AddScoped<IModelos_VehiculosPresentacion, Modelos_VehiculosPresentacion>();
builder.Services.AddScoped<IOrdenes_ServiciosPresentacion, Ordenes_ServiciosPresentacion>();
builder.Services.AddScoped<IPagosPresentacion, PagosPresentacion>();
builder.Services.AddScoped<IProveedoresPresentacion, ProveedoresPresentacion>();
builder.Services.AddScoped<IRepuestosPresentacion, RepuestosPresentacion>();
builder.Services.AddScoped<IRolesPresentacion, RolesPresentacion>();
builder.Services.AddScoped<IPermisosPresentacion, PermisosPresentacion>();
builder.Services.AddScoped<IServiciosPresentacion, ServiciosPresentacion>();
builder.Services.AddScoped<IUsuariosPresentacion, UsuariosPresentacion>();
builder.Services.AddScoped<IVehiculosPresentacion, VehiculosPresentacion>();
builder.Services.AddScoped<IAuditoriasPresentacion, AuditoriasPresentacion>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();


app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

app.MapRazorPages()
    .WithStaticAssets();

app.Run();