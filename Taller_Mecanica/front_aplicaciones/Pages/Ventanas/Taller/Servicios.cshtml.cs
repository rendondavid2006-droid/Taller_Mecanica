 
 

using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;
using libreria_aplicaciones.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace front_aplicaciones.Pages.Ventanas.Taller
{
    public class ServiciosModel : PageModel
    {
        private IServiciosPresentacion IServiciosPresentacion;


        public ServiciosModel(
            IServiciosPresentacion IServiciosPresentacion)
        {
            this.IServiciosPresentacion =
                IServiciosPresentacion;

            this.Filtro =
                new Servicios();
        }



        [BindProperty]
        public IFormFile? FormFile { get; set; }


        [BindProperty]
        public List<Servicios> Lista { get; set; }
            = new List<Servicios>();


        [BindProperty]
        public Servicios? Filtro { get; set; }


        [BindProperty]
        public Servicios? Actual { get; set; }


        [BindProperty]
        public bool Formulario { get; set; }
            = false;


        [BindProperty]
        public bool Borrar { get; set; }
            = false;



         
        private void ValidarAcceso()
        {
            string? rolId =
                HttpContext.Session.GetString("RolId");


            if (rolId != "1" &&
                rolId != "2" &&
                rolId != "3")
            {
                throw new Exception(
                    "No tiene permiso para acceder a servicios");
            }
        }



         
        private void ValidarAdministrador()
        {
            string? rolId =
                HttpContext.Session.GetString("RolId");


            if (rolId != "1")
            {
                throw new Exception(
                    "No tiene permiso para modificar servicios");
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
                rolId != "2" &&
                rolId != "3")
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
                    new Servicios();


                this.Filtro.Nombre_Servicio ??=
                    "";


                Lista =
                    this.IServiciosPresentacion
                        .Buscar(
                            this.Filtro);

                if (HttpContext.Session.GetString("RolId") == "3")
                {
                    Lista = Lista
                        .Where(x => x.Estado)
                        .ToList();
                }
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


                this.Actual =
                    new Servicios();


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
            try
            {
                ValidarAdministrador();


                if (this.Actual == null)
                {
                    throw new Exception(
                        "No existe información del servicio");
                }



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


                if (this.Actual.Id == 0)
                {
                    this.Actual =
                        this.IServiciosPresentacion
                            .Insertar(
                                this.Actual);
                }
                else
                {
                    this.Actual =
                        this.IServiciosPresentacion
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
                    out int idServicio))
                {
                    throw new Exception(
                        "El servicio seleccionado no es válido");
                }


                Actual =
                    this.Lista
                        .FirstOrDefault(
                            x =>
                                x.Id ==
                                idServicio);


                if (Actual == null)
                {
                    throw new Exception(
                        "No se encontró el servicio");
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
                    out int idServicio))
                {
                    throw new Exception(
                        "El servicio seleccionado no es válido");
                }


                Actual =
                    this.Lista
                        .FirstOrDefault(
                            x =>
                                x.Id ==
                                idServicio);


                if (Actual == null)
                {
                    throw new Exception(
                        "No se encontró el servicio");
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
                        "No se encontró el servicio");
                }


                this.Actual =
                    this.IServiciosPresentacion
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