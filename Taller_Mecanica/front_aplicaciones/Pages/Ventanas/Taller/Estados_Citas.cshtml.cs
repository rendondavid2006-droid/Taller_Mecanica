 
 

using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;
using libreria_aplicaciones.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_aplicaciones.Pages.Ventanas.Taller
{
    public class Estados_CitasModel : PageModel
    {
        private IEstados_CitasPresentacion IEstados_CitasPresentacion;


        public Estados_CitasModel(
            IEstados_CitasPresentacion IEstados_CitasPresentacion)
        {
            this.IEstados_CitasPresentacion =
                IEstados_CitasPresentacion;

            this.Filtro =
                new Estados_Citas();
        }


        [BindProperty]
        public List<Estados_Citas> Lista { get; set; }
            = new List<Estados_Citas>();


        [BindProperty]
        public Estados_Citas? Filtro { get; set; }


        [BindProperty]
        public Estados_Citas? Actual { get; set; }


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
                this.Filtro ??=
                    new Estados_Citas();


                this.Filtro.Nombre_Estado ??=
                    "";


                Lista =
                    this.IEstados_CitasPresentacion
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
                this.Actual =
                    new Estados_Citas
                    {
                        Estado = true
                    };


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
                        "No existe información del estado de cita");
                }


                if (this.Actual.Id == 0)
                {
                    this.Actual =
                        this.IEstados_CitasPresentacion
                            .Insertar(
                                this.Actual);
                }
                else
                {
                    this.Actual =
                        this.IEstados_CitasPresentacion
                            .Actualizar(
                                this.Actual);
                }
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
                    out int idEstadoCita))
                {
                    throw new Exception(
                        "El estado de cita seleccionado no es válido");
                }


                Actual =
                    this.Lista
                        .FirstOrDefault(
                            x =>
                                x.Id ==
                                idEstadoCita);


                if (Actual == null)
                {
                    throw new Exception(
                        "No se encontró el estado de cita");
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
                    out int idEstadoCita))
                {
                    throw new Exception(
                        "El estado de cita seleccionado no es válido");
                }


                Actual =
                    this.Lista
                        .FirstOrDefault(
                            x =>
                                x.Id ==
                                idEstadoCita);


                if (Actual == null)
                {
                    throw new Exception(
                        "No se encontró el estado de cita");
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
                        "No se encontró el estado de cita");
                }


                this.Actual =
                    this.IEstados_CitasPresentacion
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