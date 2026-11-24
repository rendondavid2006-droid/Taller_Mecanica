 
 

using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;
using libreria_aplicaciones.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_aplicaciones.Pages.Ventanas.Administracion
{
    public class RolesModel : PageModel
    {
        private IRolesPresentacion IRolesPresentacion;

        public RolesModel(
            IRolesPresentacion IRolesPresentacion)
        {
            this.IRolesPresentacion =
                IRolesPresentacion;

            this.Filtro =
                new Roles();
        }


        [BindProperty]
        public List<Roles> Lista { get; set; }
            = new List<Roles>();


        [BindProperty]
        public Roles? Filtro { get; set; }


        [BindProperty]
        public Roles? Actual { get; set; }


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
                    new Roles();


                this.Filtro.Nombre_Rol ??=
                    "";


                Lista =
                    this.IRolesPresentacion
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
                    new Roles();


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
                        "No existe información del rol");
                }


                if (this.Actual.Id == 0)
                {
                    this.Actual =
                        this.IRolesPresentacion
                            .Insertar(
                                this.Actual);
                }
                else
                {
                    this.Actual =
                        this.IRolesPresentacion
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
                    out int idRol))
                {
                    throw new Exception(
                        "El rol seleccionado no es válido");
                }


                Actual =
                    this.Lista
                        .FirstOrDefault(
                            x =>
                                x.Id ==
                                idRol);


                if (Actual == null)
                {
                    throw new Exception(
                        "No se encontró el rol");
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
                    out int idRol))
                {
                    throw new Exception(
                        "El rol seleccionado no es válido");
                }


                Actual =
                    this.Lista
                        .FirstOrDefault(
                            x =>
                                x.Id ==
                                idRol);


                if (Actual == null)
                {
                    throw new Exception(
                        "No se encontró el rol");
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
                        "No se encontró el rol");
                }


                this.Actual =
                    this.IRolesPresentacion
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