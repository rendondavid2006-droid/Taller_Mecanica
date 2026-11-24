 
 

using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;
using libreria_aplicaciones.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_aplicaciones.Pages.Ventanas.Facturacion
{
    public class PagosModel : PageModel
    {
        private IPagosPresentacion IPagosPresentacion;

        private IFacturasPresentacion IFacturasPresentacion;


        public PagosModel(
            IPagosPresentacion IPagosPresentacion,
            IFacturasPresentacion IFacturasPresentacion)
        {
            this.IPagosPresentacion =
                IPagosPresentacion;

            this.IFacturasPresentacion =
                IFacturasPresentacion;

            this.Filtro =
                new Pagos
                {
                    _Factura =
                        new Facturas
                        {
                            _Cliente =
                                new Clientes
                                {
                                    Documento = ""
                                }
                        }
                };
        }


        [BindProperty]
        public List<Pagos> Lista { get; set; }
            = new List<Pagos>();


        [BindProperty]
        public Pagos? Filtro { get; set; }


        [BindProperty]
        public Pagos? Actual { get; set; }


        [BindProperty]
        public bool Formulario { get; set; }
            = false;


        [BindProperty]
        public bool Borrar { get; set; }
            = false;


        public List<Facturas> Facturas { get; set; }
            = new List<Facturas>();


         
        private void CargarFacturas()
        {
            this.Facturas =
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
        }


         
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


         
        public void OnGet()
        {
            if (!ValidarAdministrador())
            {
                HttpContext.Response.Redirect("/");
                return;
            }


            CargarFacturas();
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
                this.Filtro ??=
                    new Pagos();


                this.Filtro._Factura ??=
                    new Facturas();


                this.Filtro._Factura._Cliente ??=
                    new Clientes();


                this.Filtro._Factura._Cliente.Documento ??=
                    "";

                CargarFacturas();


                Lista =
                    this.IPagosPresentacion
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
                CargarFacturas();

                this.Actual =
                    new Pagos();


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
                        "No existe información del pago");
                }


                if (this.Actual.Id == 0)
                {
                    this.Actual =
                        this.IPagosPresentacion
                            .Insertar(
                                this.Actual);
                }
                else
                {
                    this.Actual =
                        this.IPagosPresentacion
                            .Actualizar(
                                this.Actual);
                }
            }
            catch (Exception ex)
            {
                CargarFacturas();
                Formulario = true;

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
                    out int idPago))
                {
                    throw new Exception(
                        "El pago seleccionado no es válido");
                }


                Actual =
                    this.Lista
                        .FirstOrDefault(
                            x =>
                                x.Id ==
                                idPago);


                if (Actual == null)
                {
                    throw new Exception(
                        "No se encontró el pago");
                }


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
                    out int idPago))
                {
                    throw new Exception(
                        "El pago seleccionado no es válido");
                }


                Actual =
                    this.Lista
                        .FirstOrDefault(
                            x =>
                                x.Id ==
                                idPago);


                if (Actual == null)
                {
                    throw new Exception(
                        "No se encontró el pago");
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
                        "No se encontró el pago");
                }


                this.Actual =
                    this.IPagosPresentacion
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
    }
}