 
 

using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;
using libreria_aplicaciones.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_aplicaciones.Pages.Ventanas.Taller
{
    public class Detalles_Ordenes_ServiciosModel : PageModel
    {
        private IDetalles_Ordenes_ServiciosPresentacion
            IDetalles_Ordenes_ServiciosPresentacion;

        private IOrdenes_ServiciosPresentacion
            IOrdenes_ServiciosPresentacion;

        private IServiciosPresentacion
            IServiciosPresentacion;


        public Detalles_Ordenes_ServiciosModel(
            IDetalles_Ordenes_ServiciosPresentacion
                IDetalles_Ordenes_ServiciosPresentacion,
            IOrdenes_ServiciosPresentacion
                IOrdenes_ServiciosPresentacion,
            IServiciosPresentacion
                IServiciosPresentacion)
        {
            this.IDetalles_Ordenes_ServiciosPresentacion =
                IDetalles_Ordenes_ServiciosPresentacion;

            this.IOrdenes_ServiciosPresentacion =
                IOrdenes_ServiciosPresentacion;

            this.IServiciosPresentacion =
                IServiciosPresentacion;


            this.Filtro =
                new Detalles_Ordenes_Servicios
                {
                    _Orden =
                        new Ordenes_Servicios
                        {
                            _Empleado =
                                new Empleados()
                        }
                };
        }


        [BindProperty]
        public List<Detalles_Ordenes_Servicios> Lista { get; set; }
            = new List<Detalles_Ordenes_Servicios>();


        [BindProperty]
        public Detalles_Ordenes_Servicios? Filtro { get; set; }


        [BindProperty]
        public Detalles_Ordenes_Servicios? Actual { get; set; }


        [BindProperty]
        public bool Formulario { get; set; }
            = false;


        [BindProperty]
        public bool Borrar { get; set; }
            = false;


   

        public List<Ordenes_Servicios> Ordenes { get; set; }
            = new List<Ordenes_Servicios>();


        public List<Servicios> Servicios { get; set; }
            = new List<Servicios>();



         
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



         
        private void CargarOrdenes()
        {
            this.Ordenes =
                this.IOrdenes_ServiciosPresentacion
                    .Consultar();
        }



         
        private void CargarServicios()
        {
            this.Servicios =
                this.IServiciosPresentacion
                    .Consultar();
        }



         
        private void CargarListas()
        {
            CargarOrdenes();
            CargarServicios();
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
                    new Detalles_Ordenes_Servicios();


                this.Filtro._Orden ??=
                    new Ordenes_Servicios();


                this.Filtro._Orden._Empleado ??=
                    new Empleados();


                this.Filtro._Orden._Empleado.Nombre ??=
                    "";


                Lista =
                    this.IDetalles_Ordenes_ServiciosPresentacion
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
                    new Detalles_Ordenes_Servicios();


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
                        "No existe información del detalle de servicio de la orden");
                }


                if (this.Actual.Id == 0)
                {
                    this.Actual =
                        this.IDetalles_Ordenes_ServiciosPresentacion
                            .Insertar(
                                this.Actual);
                }
                else
                {
                    this.Actual =
                        this.IDetalles_Ordenes_ServiciosPresentacion
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
                        "No se encontró el detalle de servicio de la orden");
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
                        "No se encontró el detalle de servicio de la orden");
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
                        "No se encontró el detalle de servicio de la orden");
                }


                this.Actual =
                    this.IDetalles_Ordenes_ServiciosPresentacion
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