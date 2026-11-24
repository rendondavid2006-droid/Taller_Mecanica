 
 

using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;
using libreria_aplicaciones.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_aplicaciones.Pages.Ventanas.Inventario
{
    public class RepuestosModel : PageModel
    {
        private IRepuestosPresentacion
            IRepuestosPresentacion;


        public RepuestosModel(
            IRepuestosPresentacion IRepuestosPresentacion)
        {
            this.IRepuestosPresentacion =
                IRepuestosPresentacion;

            this.Filtro =
                new Repuestos();
        }

        [BindProperty] public IFormFile? FormFile { get; set; }

        [BindProperty] public List<Repuestos> Lista { get; set; } = new List<Repuestos>();


        [BindProperty] public Repuestos? Filtro { get; set; }
         

        [BindProperty] public Repuestos? Actual { get; set; }


        [BindProperty] public bool Formulario { get; set; } = false;


        [BindProperty] public bool Borrar { get; set; } = false;




         
        private void ValidarAcceso()
        {
            string? rolId =
                HttpContext.Session.GetString("RolId");


            if (rolId != "1" &&
                rolId != "2")
            {
                throw new Exception(
                    "No tiene permiso para acceder a repuestos");
            }
        }




         
        private void ValidarAdministrador()
        {
            string? rolId =
                HttpContext.Session.GetString("RolId");


            if (rolId != "1")
            {
                throw new Exception(
                    "No tiene permiso para modificar repuestos");
            }
        }



         
        public void OnGet()
        {
            var variable_session =
                HttpContext.Session.GetString("Usuario");


            if (string.IsNullOrEmpty(variable_session))
            {
                HttpContext.Response.Redirect("/");
                return;
            }


            string? rolId =
                HttpContext.Session.GetString("RolId");



            if (rolId != "1" &&
                rolId != "2")
            {
                HttpContext.Response.Redirect("/");
                return;
            }


            OnPostBtBuscar();
        }




         
        public void OnPostBtBuscar()
        {
            try
            {
                ValidarAcceso();


                this.Filtro ??=
                    new Repuestos();


                this.Filtro.Nombre_Repuesto ??=
                    "";


                Lista =
                    this.IRepuestosPresentacion
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
            try
            {
                ValidarAdministrador();


                Formulario =
                    true;


                this.Actual =
                    new Repuestos();
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
            try
            {

                if (FormFile != null)
                {
                    var memoryStream =
                        new MemoryStream();

                    FormFile
                        .CopyToAsync(memoryStream)
                        .Wait();

                    this.Actual.Imagen =
                        Convert.ToBase64String(
                            memoryStream.ToArray());

                    memoryStream.Dispose();
                }

                ValidarAdministrador();


                if (this.Actual == null)
                {
                    throw new Exception(
                        "No existe información del repuesto");
                }


                if (this.Actual.Id == 0)
                {
                    this.Actual =
                        this.IRepuestosPresentacion
                            .Insertar(
                                this.Actual);
                }
                else
                {
                    this.Actual =
                        this.IRepuestosPresentacion
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
            try
            {
                ValidarAdministrador();


                OnPostBtBuscar();


                if (!int.TryParse(
                    data,
                    out int idRepuesto))
                {
                    throw new Exception(
                        "El repuesto seleccionado no es válido");
                }


                Actual =
                    this.Lista
                        .FirstOrDefault(
                            x =>
                                x.Id ==
                                idRepuesto);


                if (Actual == null)
                {
                    throw new Exception(
                        "No se encontró el repuesto");
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
            try
            {
                ValidarAdministrador();


                OnPostBtBuscar();


                if (!int.TryParse(
                    data,
                    out int idRepuesto))
                {
                    throw new Exception(
                        "El repuesto seleccionado no es válido");
                }


                Actual =
                    this.Lista
                        .FirstOrDefault(
                            x =>
                                x.Id ==
                                idRepuesto);


                if (Actual == null)
                {
                    throw new Exception(
                        "No se encontró el repuesto");
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
            try
            {
                ValidarAdministrador();


                if (this.Actual == null ||
                    this.Actual.Id == 0)
                {
                    throw new Exception(
                        "No se encontró el repuesto");
                }


                this.Actual =
                    this.IRepuestosPresentacion
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