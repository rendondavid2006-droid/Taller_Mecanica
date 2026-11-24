 
 

using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;
using libreria_aplicaciones.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_aplicaciones.Pages.Ventanas.Inventario
{
    public class Detalles_ComprasModel : PageModel
    {
        private IDetalles_ComprasPresentacion
            IDetalles_ComprasPresentacion;

        private IComprasPresentacion
            IComprasPresentacion;

        private IRepuestosPresentacion
            IRepuestosPresentacion;


        public Detalles_ComprasModel(
            IDetalles_ComprasPresentacion IDetalles_ComprasPresentacion,
            IComprasPresentacion IComprasPresentacion,
            IRepuestosPresentacion IRepuestosPresentacion)
        {
            this.IDetalles_ComprasPresentacion =
                IDetalles_ComprasPresentacion;

            this.IComprasPresentacion =
                IComprasPresentacion;

            this.IRepuestosPresentacion =
                IRepuestosPresentacion;

            this.Filtro =
                new Detalles_Compras
                {
                    _Repuesto =
                        new Repuestos()
                };
        }


        [BindProperty]
        public List<Detalles_Compras> Lista { get; set; }
            = new List<Detalles_Compras>();


        public List<Compras> Compras { get; set; }
            = new List<Compras>();


        public List<Repuestos> Repuestos { get; set; }
            = new List<Repuestos>();


        [BindProperty]
        public Detalles_Compras? Filtro { get; set; }


        [BindProperty]
        public Detalles_Compras? Actual { get; set; }


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
            this.Compras =
                this.IComprasPresentacion
                    .Buscar(
                        new Compras
                        {
                            Numero_Factura_Proveedor = ""
                        });

            this.Repuestos =
                this.IRepuestosPresentacion
                    .Buscar(
                        new Repuestos
                        {
                            Nombre_Repuesto = ""
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
                    new Detalles_Compras();


                this.Filtro._Repuesto ??=
                    new Repuestos();


                this.Filtro._Repuesto.Nombre_Repuesto ??=
                    "";


                Lista =
                    this.IDetalles_ComprasPresentacion
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
                    new Detalles_Compras();


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
                        "No existe información del detalle de compra");
                }


                if (this.Actual.Id == 0)
                {
                    this.Actual =
                        this.IDetalles_ComprasPresentacion
                            .Insertar(
                                this.Actual);
                }
                else
                {
                    this.Actual =
                        this.IDetalles_ComprasPresentacion
                            .Actualizar(
                                this.Actual);
                }
            }
            catch (Exception ex)
            {
                CargarListas();
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
                    out int idDetalleCompra))
                {
                    throw new Exception(
                        "El detalle de compra seleccionado no es válido");
                }


                Actual =
                    this.Lista
                        .FirstOrDefault(
                            x =>
                                x.Id ==
                                idDetalleCompra);


                if (Actual == null)
                {
                    throw new Exception(
                        "No se encontró el detalle de compra");
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
                    out int idDetalleCompra))
                {
                    throw new Exception(
                        "El detalle de compra seleccionado no es válido");
                }


                Actual =
                    this.Lista
                        .FirstOrDefault(
                            x =>
                                x.Id ==
                                idDetalleCompra);


                if (Actual == null)
                {
                    throw new Exception(
                        "No se encontró el detalle de compra");
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
                        "No se encontró el detalle de compra");
                }


                this.Actual =
                    this.IDetalles_ComprasPresentacion
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
