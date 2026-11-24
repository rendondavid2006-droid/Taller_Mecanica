 
 

using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;
using libreria_aplicaciones.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_aplicaciones.Pages.Ventanas.VehiculosClientes
{
    public class MarcasModel : PageModel
    {
        private IMarcasPresentacion IMarcasPresentacion;


        public MarcasModel(
            IMarcasPresentacion IMarcasPresentacion)
        {
            this.IMarcasPresentacion =
                IMarcasPresentacion;

            this.Filtro =
                new Marcas();
        }


        [BindProperty]
        public List<Marcas> Lista { get; set; }
            = new List<Marcas>();


        [BindProperty]
        public Marcas? Filtro { get; set; }


        [BindProperty]
        public Marcas? Actual { get; set; }


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
                    new Marcas();


                this.Filtro.Nombre_Marca ??=
                    "";


                Lista =
                    this.IMarcasPresentacion
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
                    new Marcas();


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
                        "No existe información de la marca");
                }


                if (this.Actual.Id == 0)
                {
                    this.Actual =
                        this.IMarcasPresentacion
                            .Insertar(
                                this.Actual);
                }
                else
                {
                    this.Actual =
                        this.IMarcasPresentacion
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
                    out int idMarca))
                {
                    throw new Exception(
                        "La marca seleccionada no es válida");
                }


                Actual =
                    this.Lista
                        .FirstOrDefault(
                            x =>
                                x.Id ==
                                idMarca);


                if (Actual == null)
                {
                    throw new Exception(
                        "No se encontró la marca");
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
                    out int idMarca))
                {
                    throw new Exception(
                        "La marca seleccionada no es válida");
                }


                Actual =
                    this.Lista
                        .FirstOrDefault(
                            x =>
                                x.Id ==
                                idMarca);


                if (Actual == null)
                {
                    throw new Exception(
                        "No se encontró la marca");
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
                        "No se encontró la marca");
                }


                this.Actual =
                    this.IMarcasPresentacion
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