 
 

using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;
using libreria_aplicaciones.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_aplicaciones.Pages.Ventanas.Facturacion
{
    public class FacturasModel : PageModel
    {
        private IFacturasPresentacion
            IFacturasPresentacion;

        private IOrdenes_ServiciosPresentacion
            IOrdenes_ServiciosPresentacion;

        private IClientesPresentacion
            IClientesPresentacion;

         
        private IDetalles_Ordenes_ServiciosPresentacion IDetallesServiciosPresentacion;
        private IDetalles_Repuestos_OrdenesPresentacion IDetallesRepuestosPresentacion;


        public FacturasModel(
            IFacturasPresentacion IFacturasPresentacion,
            IOrdenes_ServiciosPresentacion IOrdenes_ServiciosPresentacion,
            IClientesPresentacion IClientesPresentacion,
            IDetalles_Ordenes_ServiciosPresentacion IDetallesServiciosPresentacion,
            IDetalles_Repuestos_OrdenesPresentacion IDetallesRepuestosPresentacion)
        {
            this.IFacturasPresentacion =
                IFacturasPresentacion;

            this.IOrdenes_ServiciosPresentacion =
                IOrdenes_ServiciosPresentacion;

            this.IClientesPresentacion =
                IClientesPresentacion;
            this.IDetallesServiciosPresentacion = IDetallesServiciosPresentacion;
            this.IDetallesRepuestosPresentacion = IDetallesRepuestosPresentacion;


            this.Filtro =
                new Facturas
                {
                    _Cliente =
                        new Clientes()
                };
        }


        [BindProperty]
        public List<Facturas> Lista { get; set; }
            = new List<Facturas>();


        public List<Ordenes_Servicios> Ordenes { get; set; }
            = new List<Ordenes_Servicios>();


        public List<Clientes> Clientes { get; set; }
            = new List<Clientes>();


         
        public List<Detalles_Ordenes_Servicios> DetallesServicios { get; set; } = new();
        public List<Detalles_Repuestos_Ordenes> DetallesRepuestos { get; set; } = new();

        [BindProperty]
        public Facturas? Filtro { get; set; }


        [BindProperty]
        public Facturas? Actual { get; set; }


        [BindProperty]
        public bool Formulario { get; set; }
            = false;


        [BindProperty]
        public bool Borrar { get; set; }
            = false;


         
        private bool ValidarAdministrador()
        {
            var usuario =
                HttpContext.Session.GetString("Usuario");

            var rolId =
                HttpContext.Session.GetString("RolId");


            return
                !string.IsNullOrEmpty(usuario) &&
                rolId == "1";
        }


         
        private void CargarListas()
        {
            this.Ordenes =
                this.IOrdenes_ServiciosPresentacion
                    .Consultar();


            this.Clientes =
                this.IClientesPresentacion
                    .Buscar(
                        new Clientes
                        {
                            Documento = ""
                        });

             
            this.DetallesServicios = this.IDetallesServiciosPresentacion.Buscar(
                new Detalles_Ordenes_Servicios
                {
                    _Orden = new Ordenes_Servicios { _Empleado = new Empleados { Nombre = "" } }
                });
            this.DetallesRepuestos = this.IDetallesRepuestosPresentacion.Buscar(
                new Detalles_Repuestos_Ordenes
                {
                    _Repuesto = new Repuestos { Nombre_Repuesto = "" }
                });
        }


         
        public void OnGet()
        {
            if (!ValidarAdministrador())
            {
                HttpContext.Response.Redirect("/");
                return;
            }


            OnPostBtBuscar();
        }


         
        public void OnPostBtBuscar()
        {
            if (!ValidarAdministrador())
            {
                HttpContext.Response.Redirect("/");
                return;
            }


            try
            {
                CargarListas();


                this.Filtro ??=
                    new Facturas();


                this.Filtro._Cliente ??=
                    new Clientes();


                this.Filtro._Cliente.Documento ??=
                    "";


                Lista =
                    this.IFacturasPresentacion
                        .Buscar(
                            this.Filtro);
            }
            catch (Exception ex)
            {
                LogConversor.Log(
                    ex,
                    ViewData!);
            }
        }


         
        public void OnPostBtNuevo()
        {
            if (!ValidarAdministrador())
            {
                HttpContext.Response.Redirect("/");
                return;
            }


            try
            {
                CargarListas();


                this.Actual =
                    new Facturas();


                Formulario =
                    true;
            }
            catch (Exception ex)
            {
                LogConversor.Log(
                    ex,
                    ViewData!);
            }
        }


         
        public void OnPostBtGuardar()
        {
            if (!ValidarAdministrador())
            {
                HttpContext.Response.Redirect("/");
                return;
            }


            try
            {
                if (this.Actual == null)
                {
                    throw new Exception(
                        "No existe información de la factura");
                }


                if (this.Actual.Id == 0)
                {
                    this.Actual =
                        this.IFacturasPresentacion
                            .Insertar(
                                this.Actual);
                }
                else
                {
                    this.Actual =
                        this.IFacturasPresentacion
                            .Actualizar(
                                this.Actual);
                }
            }
            catch (Exception ex)
            {
                CargarListas();


                Formulario =
                    true;


                LogConversor.Log(
                    ex,
                    ViewData!);
            }
            finally
            {
                OnPostBtBuscar();
            }
        }


         
        public void OnPostBtEditar(
            string data)
        {
            if (!ValidarAdministrador())
            {
                HttpContext.Response.Redirect("/");
                return;
            }


            try
            {
                OnPostBtBuscar();


                if (!int.TryParse(
                    data,
                    out int idFactura))
                {
                    throw new Exception(
                        "La factura seleccionada no es válida");
                }


                Actual =
                    this.Lista
                        .FirstOrDefault(
                            x =>
                                x.Id ==
                                idFactura);


                if (Actual == null)
                {
                    throw new Exception(
                        "No se encontró la factura");
                }


                CargarListas();


                Formulario =
                    true;
            }
            catch (Exception ex)
            {
                LogConversor.Log(
                    ex,
                    ViewData!);
            }
        }


         
        public void OnPostBtPregBorrar(
            string data)
        {
            if (!ValidarAdministrador())
            {
                HttpContext.Response.Redirect("/");
                return;
            }


            try
            {
                OnPostBtBuscar();


                if (!int.TryParse(
                    data,
                    out int idFactura))
                {
                    throw new Exception(
                        "La factura seleccionada no es válida");
                }


                Actual =
                    this.Lista
                        .FirstOrDefault(
                            x =>
                                x.Id ==
                                idFactura);


                if (Actual == null)
                {
                    throw new Exception(
                        "No se encontró la factura");
                }


                Borrar =
                    true;
            }
            catch (Exception ex)
            {
                LogConversor.Log(
                    ex,
                    ViewData!);
            }
        }


         
        public void OnPostBtBorrar()
        {
            if (!ValidarAdministrador())
            {
                HttpContext.Response.Redirect("/");
                return;
            }


            try
            {
                if (this.Actual == null ||
                    this.Actual.Id == 0)
                {
                    throw new Exception(
                        "No se encontró la factura");
                }


                this.Actual =
                    this.IFacturasPresentacion
                        .Borrar(
                            this.Actual);
            }
            catch (Exception ex)
            {
                LogConversor.Log(
                    ex,
                    ViewData!);
            }
            finally
            {
                OnPostBtBuscar();
            }
        }


         
        public IActionResult OnPostBtDescargarPdf(
            string data)
        {
            if (!ValidarAdministrador())
            {
                return new RedirectResult("/");
            }


            try
            {
                if (!int.TryParse(
                    data,
                    out int idFactura))
                {
                    throw new Exception(
                        "La factura seleccionada no es válida");
                }


                CargarListas();


                var facturas =
                    this.IFacturasPresentacion
                        .Buscar(
                            new Facturas
                            {
                                _Cliente =
                                    new Clientes
                                    {
                                        Documento = ""
                                    }
                            });


                var factura =
                    facturas
                        .FirstOrDefault(
                            x =>
                                x.Id ==
                                idFactura);


                if (factura == null)
                {
                    throw new Exception(
                        "No se encontró la factura");
                }


                var cliente =
                    this.Clientes
                        .FirstOrDefault(
                            x =>
                                x.Id ==
                                factura.Cliente);


                var orden =
                    this.Ordenes
                        .FirstOrDefault(
                            x =>
                                x.Id ==
                                factura.Orden);


                string nombreCliente =
                    cliente != null
                        ? $"{cliente.Nombre} {cliente.Apellido}".Trim()
                        : $"Cliente #{factura.Cliente}";


                string ordenTexto;


                if (orden != null &&
                    orden._Vehiculo != null)
                {
                    ordenTexto =
                        $"Orden #{orden.Id} - Placa {orden._Vehiculo.Placa}";
                }
                else
                {
                    ordenTexto =
                        $"Orden #{factura.Orden}";
                }


                static IContainer CeldaTitulo(
                    IContainer container)
                {
                    return container
                        .Background(
                            Colors.Grey.Lighten3)
                        .BorderBottom(1)
                        .BorderColor(
                            Colors.Grey.Lighten2)
                        .Padding(7);
                }


                static IContainer CeldaDato(
                    IContainer container)
                {
                    return container
                        .BorderBottom(1)
                        .BorderColor(
                            Colors.Grey.Lighten2)
                        .Padding(7);
                }


                var documento =
                    Document.Create(
                        document =>
                        {
                            document.Page(
                                page =>
                                {
                                    page.Size(
                                        PageSizes.A4);


                                    page.Margin(
                                        40);


                                    page.DefaultTextStyle(
                                        x =>
                                            x.FontSize(11));


                                    page.Header()
                                        .Row(
                                            row =>
                                            {
                                                row.RelativeItem()
                                                    .Column(
                                                        column =>
                                                        {
                                                            column.Item()
                                                                .Text(
                                                                    "Taller Mecánica")
                                                                .FontSize(20)
                                                                .Bold();


                                                            column.Item()
                                                                .Text(
                                                                    "Factura de servicio")
                                                                .FontSize(11);
                                                        });


                                                row.ConstantItem(180)
                                                    .AlignRight()
                                                    .Column(
                                                        column =>
                                                        {
                                                            column.Item()
                                                                .AlignRight()
                                                                .Text(
                                                                    $"FACTURA #{factura.Id}")
                                                                .FontSize(18)
                                                                .Bold();


                                                            column.Item()
                                                                .AlignRight()
                                                                .Text(
                                                                    factura.Fecha_Factura
                                                                        .ToString(
                                                                            "dd/MM/yyyy"));
                                                        });
                                            });


                                    page.Content()
                                        .PaddingVertical(20)
                                        .Column(
                                            column =>
                                            {
                                                column.Spacing(15);


                                                column.Item()
                                                    .Text(
                                                        "Información de la factura")
                                                    .FontSize(14)
                                                    .Bold();


                                                column.Item()
                                                    .Table(
                                                        table =>
                                                        {
                                                            table.ColumnsDefinition(
                                                                columns =>
                                                                {
                                                                    columns.RelativeColumn();
                                                                    columns.RelativeColumn(2);
                                                                });


                                                            table.Cell()
                                                                .Element(CeldaTitulo)
                                                                .Text(
                                                                    "Cliente")
                                                                .Bold();


                                                            table.Cell()
                                                                .Element(CeldaDato)
                                                                .Text(
                                                                    nombreCliente);


                                                            table.Cell()
                                                                .Element(CeldaTitulo)
                                                                .Text(
                                                                    "Orden")
                                                                .Bold();


                                                            table.Cell()
                                                                .Element(CeldaDato)
                                                                .Text(
                                                                    ordenTexto);


                                                            table.Cell()
                                                                .Element(CeldaTitulo)
                                                                .Text(
                                                                    "Fecha")
                                                                .Bold();


                                                            table.Cell()
                                                                .Element(CeldaDato)
                                                                .Text(
                                                                    factura.Fecha_Factura
                                                                        .ToString(
                                                                            "dd/MM/yyyy HH:mm"));


                                                            table.Cell()
                                                                .Element(CeldaTitulo)
                                                                .Text(
                                                                    "Estado")
                                                                .Bold();


                                                            table.Cell()
                                                                .Element(CeldaDato)
                                                                .Text(
                                                                    factura.Estado ??
                                                                    "");
                                                        });


                                                column.Item()
                                                    .PaddingTop(10)
                                                    .Text(
                                                        "Resumen de valores")
                                                    .FontSize(14)
                                                    .Bold();


                                                column.Item()
                                                    .Table(
                                                        table =>
                                                        {
                                                            table.ColumnsDefinition(
                                                                columns =>
                                                                {
                                                                    columns.RelativeColumn();
                                                                    columns.RelativeColumn();
                                                                });


                                                            table.Cell()
                                                                .Element(CeldaTitulo)
                                                                .Text(
                                                                    "Subtotal")
                                                                .Bold();


                                                            table.Cell()
                                                                .Element(CeldaDato)
                                                                .AlignRight()
                                                                .Text(
                                                                    $"$ {factura.Subtotal:N2}");


                                                            table.Cell()
                                                                .Element(CeldaTitulo)
                                                                .Text(
                                                                    "Impuesto")
                                                                .Bold();


                                                            table.Cell()
                                                                .Element(CeldaDato)
                                                                .AlignRight()
                                                                .Text(
                                                                    $"$ {factura.Impuesto:N2}");


                                                            table.Cell()
                                                                .Element(CeldaTitulo)
                                                                .Text(
                                                                    "TOTAL")
                                                                .Bold();


                                                            table.Cell()
                                                                .Element(CeldaDato)
                                                                .AlignRight()
                                                                .Text(
                                                                    $"$ {factura.Total:N2}")
                                                                .FontSize(14)
                                                                .Bold();
                                                        });
                                            });


                                    page.Footer()
                                        .AlignCenter()
                                        .Text(
                                            "Documento generado por Taller Mecánica");
                                });
                        });


                byte[] archivoPdf =
                    documento.GeneratePdf();


                return File(
                    archivoPdf,
                    "application/pdf",
                    $"Factura_{factura.Id}.pdf");
            }
            catch (Exception ex)
            {
                LogConversor.Log(
                    ex,
                    ViewData!);


                OnPostBtBuscar();


                return Page();
            }
        }
    }
}