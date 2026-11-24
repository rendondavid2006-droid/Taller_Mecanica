 
 

using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;
using libreria_aplicaciones.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_aplicaciones.Pages.Ventanas.Inventario
{
    public class Detalles_Repuestos_OrdenesModel : PageModel
    {
        private IDetalles_Repuestos_OrdenesPresentacion
            IDetalles_Repuestos_OrdenesPresentacion;

        private IOrdenes_ServiciosPresentacion
            IOrdenes_ServiciosPresentacion;

        private IRepuestosPresentacion
            IRepuestosPresentacion;


        public Detalles_Repuestos_OrdenesModel(
            IDetalles_Repuestos_OrdenesPresentacion IDetalles_Repuestos_OrdenesPresentacion,
            IOrdenes_ServiciosPresentacion IOrdenes_ServiciosPresentacion,
            IRepuestosPresentacion IRepuestosPresentacion)
        {
            this.IDetalles_Repuestos_OrdenesPresentacion =
                IDetalles_Repuestos_OrdenesPresentacion;

            this.IOrdenes_ServiciosPresentacion =
                IOrdenes_ServiciosPresentacion;

            this.IRepuestosPresentacion =
                IRepuestosPresentacion;

            this.Filtro =
                new Detalles_Repuestos_Ordenes
                {
                    _Repuesto =
                        new Repuestos()
                };
        }


        [BindProperty]
        public List<Detalles_Repuestos_Ordenes> Lista { get; set; }
            = new List<Detalles_Repuestos_Ordenes>();


        public List<Ordenes_Servicios> Ordenes { get; set; }
            = new List<Ordenes_Servicios>();


        public List<Repuestos> Repuestos { get; set; }
            = new List<Repuestos>();


        [BindProperty]
        public Detalles_Repuestos_Ordenes? Filtro { get; set; }


        [BindProperty]
        public Detalles_Repuestos_Ordenes? Actual { get; set; }


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
                    new Detalles_Repuestos_Ordenes();


                this.Filtro._Repuesto ??=
                    new Repuestos();


                this.Filtro._Repuesto.Nombre_Repuesto ??=
                    "";


                Lista =
                    this.IDetalles_Repuestos_OrdenesPresentacion
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
                    new Detalles_Repuestos_Ordenes();


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
                        "No existe información del detalle de repuesto de la orden");
                }


                if (this.Actual.Id == 0)
                {
                    this.Actual =
                        this.IDetalles_Repuestos_OrdenesPresentacion
                            .Insertar(
                                this.Actual);
                }
                else
                {
                    this.Actual =
                        this.IDetalles_Repuestos_OrdenesPresentacion
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
                    out int idDetalle))
                {
                    throw new Exception(
                        "El detalle seleccionado no es válido");
                }


                Actual =
                    this.Lista
                        .FirstOrDefault(
                            x =>
                                x.Id ==
                                idDetalle);


                if (Actual == null)
                {
                    throw new Exception(
                        "No se encontró el detalle de repuesto de la orden");
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
                    out int idDetalle))
                {
                    throw new Exception(
                        "El detalle seleccionado no es válido");
                }


                Actual =
                    this.Lista
                        .FirstOrDefault(
                            x =>
                                x.Id ==
                                idDetalle);


                if (Actual == null)
                {
                    throw new Exception(
                        "No se encontró el detalle de repuesto de la orden");
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
                        "No se encontró el detalle de repuesto de la orden");
                }


                this.Actual =
                    this.IDetalles_Repuestos_OrdenesPresentacion
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
