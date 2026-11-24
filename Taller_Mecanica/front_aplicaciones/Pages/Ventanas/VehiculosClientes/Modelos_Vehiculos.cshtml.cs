 
 

using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;
using libreria_aplicaciones.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_aplicaciones.Pages.Ventanas.VehiculosClientes
{
    public class Modelos_VehiculosModel : PageModel
    {
        private IModelos_VehiculosPresentacion
            IModelos_VehiculosPresentacion;

        private IMarcasPresentacion
            IMarcasPresentacion;


        public Modelos_VehiculosModel(
            IModelos_VehiculosPresentacion IModelos_VehiculosPresentacion,
            IMarcasPresentacion IMarcasPresentacion)
        {
            this.IModelos_VehiculosPresentacion =
                IModelos_VehiculosPresentacion;

            this.IMarcasPresentacion =
                IMarcasPresentacion;

            this.Filtro =
                new Modelos_Vehiculos();
        }


        [BindProperty]
        public List<Modelos_Vehiculos> Lista { get; set; }
            = new List<Modelos_Vehiculos>();


        [BindProperty]
        public Modelos_Vehiculos? Filtro { get; set; }


        [BindProperty]
        public Modelos_Vehiculos? Actual { get; set; }


        [BindProperty]
        public bool Formulario { get; set; }
            = false;


        [BindProperty]
        public bool Borrar { get; set; }
            = false;



        public List<Marcas> Marcas { get; set; }
            = new List<Marcas>();



         
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



         
        private void CargarMarcas()
        {
            this.Marcas =
                this.IMarcasPresentacion
                    .Consultar();
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

                CargarMarcas();

                this.Filtro ??=
                    new Modelos_Vehiculos();

                this.Filtro.Nombre_Modelo ??=
                    "";

                Lista =
                    this.IModelos_VehiculosPresentacion
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

                CargarMarcas();


                this.Actual =
                    new Modelos_Vehiculos();


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
                        "No existe información del modelo de vehículo");
                }


                if (this.Actual.Id == 0)
                {
                    this.Actual =
                        this.IModelos_VehiculosPresentacion
                            .Insertar(
                                this.Actual);
                }
                else
                {
                    this.Actual =
                        this.IModelos_VehiculosPresentacion
                            .Actualizar(
                                this.Actual);
                }
            }
            catch (Exception ex)
            {
 
                CargarMarcas();

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
                    out int idModelo))
                {
                    throw new Exception(
                        "El modelo seleccionado no es válido");
                }


                Actual =
                    this.Lista
                        .FirstOrDefault(
                            x =>
                                x.Id ==
                                idModelo);


                if (Actual == null)
                {
                    throw new Exception(
                        "No se encontró el modelo de vehículo");
                }



                CargarMarcas();


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
                    out int idModelo))
                {
                    throw new Exception(
                        "El modelo seleccionado no es válido");
                }


                Actual =
                    this.Lista
                        .FirstOrDefault(
                            x =>
                                x.Id ==
                                idModelo);


                if (Actual == null)
                {
                    throw new Exception(
                        "No se encontró el modelo de vehículo");
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
                        "No se encontró el modelo de vehículo");
                }


                this.Actual =
                    this.IModelos_VehiculosPresentacion
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