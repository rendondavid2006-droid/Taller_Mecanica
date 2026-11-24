 
 

using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;
using libreria_aplicaciones.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_aplicaciones.Pages.Ventanas.Taller
{
    public class DiagnosticosModel : PageModel
    {
        private IDiagnosticosPresentacion
            IDiagnosticosPresentacion;

        private IOrdenes_ServiciosPresentacion
            IOrdenes_ServiciosPresentacion;

        private IEmpleadosPresentacion
            IEmpleadosPresentacion;


        public DiagnosticosModel(
            IDiagnosticosPresentacion IDiagnosticosPresentacion,
            IOrdenes_ServiciosPresentacion IOrdenes_ServiciosPresentacion,
            IEmpleadosPresentacion IEmpleadosPresentacion)
        {
            this.IDiagnosticosPresentacion =
                IDiagnosticosPresentacion;

            this.IOrdenes_ServiciosPresentacion =
                IOrdenes_ServiciosPresentacion;

            this.IEmpleadosPresentacion =
                IEmpleadosPresentacion;

            this.Filtro =
                new Diagnosticos();
        }


        [BindProperty]
        public List<Diagnosticos> Lista { get; set; }
            = new List<Diagnosticos>();


        [BindProperty]
        public Diagnosticos? Filtro { get; set; }

        [BindProperty]
        public string? PlacaBusqueda { get; set; }


        [BindProperty]
        public Diagnosticos? Actual { get; set; }


        [BindProperty]
        public bool Formulario { get; set; }
            = false;


        [BindProperty]
        public bool Borrar { get; set; }
            = false;



        public List<Ordenes_Servicios> Ordenes { get; set; }
            = new List<Ordenes_Servicios>();


        public List<Empleados> Empleados { get; set; }
            = new List<Empleados>();



         
        private void ValidarAcceso()
        {
            string? rolId =
                HttpContext.Session.GetString("RolId");


            if (rolId != "1" &&
                rolId != "2")
            {
                throw new Exception(
                    "No tiene permiso para acceder a diagnósticos");
            }
        }




         
        private int ObtenerEmpleadoSesion()
        {
            string? empleadoId =
                HttpContext.Session.GetString("EmpleadoId");


            if (string.IsNullOrEmpty(empleadoId))
            {
                throw new Exception(
                    "No se encontró el empleado asociado al usuario");
            }


            if (!int.TryParse(
                empleadoId,
                out int idEmpleado))
            {
                throw new Exception(
                    "El empleado asociado al usuario no es válido");
            }


            return idEmpleado;
        }


  
         
        private void CargarListas()
        {
            Ordenes =
                this.IOrdenes_ServiciosPresentacion
                    .Consultar();


            Empleados =
                this.IEmpleadosPresentacion
                    .Consultar();


            string? rolId =
                HttpContext.Session.GetString("RolId");


        
            if (rolId == "2")
            {
                int idEmpleado =
                    ObtenerEmpleadoSesion();


                Empleados =
                    Empleados
                        .Where(
                            x =>
                                x.Id ==
                                idEmpleado)
                        .ToList();
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


            try
            {
                CargarListas();

                OnPostBtBuscar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(
                    ex,
                    ViewData!);
            }
        }




         
        public void OnPostBtBuscar()
        {
            try
            {
                ValidarAcceso();

                CargarListas();


                this.Filtro ??=
                    new Diagnosticos();


                string? rolId =
                    HttpContext.Session.GetString("RolId");

                if (rolId == "2")
                {
                    this.Filtro.Descripcion_Problema = "";
                }
                else
                {
                    this.Filtro.Descripcion_Problema ??= "";
                }

                Lista =
                    this.IDiagnosticosPresentacion
                        .Buscar(this.Filtro);

                if (rolId == "2" &&
                    !string.IsNullOrWhiteSpace(PlacaBusqueda))
                {
                    Lista = Lista
                        .Where(x => x._Orden?._Vehiculo?.Placa?.Contains(
                            PlacaBusqueda.Trim(),
                            StringComparison.OrdinalIgnoreCase) == true)
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
                ValidarAcceso();


                string? rolId =
                    HttpContext.Session.GetString("RolId");


                this.Actual =
                    new Diagnosticos();


                this.Actual.Fecha_Diagnostico =
                    DateTime.Now;


                if (rolId == "2")
                {
                    this.Actual.Empleado =
                        ObtenerEmpleadoSesion();
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




         
        public void OnPostBtGuardar()
        {
            try
            {
                ValidarAcceso();


                if (this.Actual == null)
                {
                    throw new Exception(
                        "No existe información del diagnóstico");
                }


                string? rolId =
                    HttpContext.Session.GetString("RolId");



                if (rolId == "2")
                {
                    int idEmpleado =
                        ObtenerEmpleadoSesion();


   
                    if (this.Actual.Id == 0)
                    {
                        this.Actual.Empleado =
                            idEmpleado;


                        this.Actual.Fecha_Diagnostico =
                            DateTime.Now;
                    }
                    else
                    {


                        OnPostBtBuscar();


                        Diagnosticos? diagnosticoOriginal =
                            Lista
                                .FirstOrDefault(
                                    x =>
                                        x.Id ==
                                        this.Actual.Id);


                        if (diagnosticoOriginal == null)
                        {
                            throw new Exception(
                                "No se encontró el diagnóstico");
                        }


                        this.Actual.Orden =
                            diagnosticoOriginal.Orden;


                        this.Actual.Empleado =
                            diagnosticoOriginal.Empleado;


                        this.Actual.Fecha_Diagnostico =
                            diagnosticoOriginal.Fecha_Diagnostico;
                    }
                }



                if (this.Actual.Id == 0)
                {
                    this.Actual =
                        this.IDiagnosticosPresentacion
                            .Insertar(
                                this.Actual);
                }
                else
                {
                    this.Actual =
                        this.IDiagnosticosPresentacion
                            .Actualizar(
                                this.Actual);
                }
            }
            catch (Exception ex)
            {
                Formulario =
                    true;


                try
                {
                    CargarListas();
                }
                catch
                {
                }


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
                ValidarAcceso();


                OnPostBtBuscar();


                if (!int.TryParse(
                    data,
                    out int idDiagnostico))
                {
                    throw new Exception(
                        "El diagnóstico seleccionado no es válido");
                }


                Actual =
                    this.Lista
                        .FirstOrDefault(
                            x =>
                                x.Id ==
                                idDiagnostico);


                if (Actual == null)
                {
                    throw new Exception(
                        "No se encontró el diagnóstico");
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
            try
            {
                if (HttpContext.Session.GetString("RolId") != "1")
                {
                    throw new Exception(
                        "El mecánico no tiene permiso para eliminar diagnósticos");
                }


                OnPostBtBuscar();


                if (!int.TryParse(
                    data,
                    out int idDiagnostico))
                {
                    throw new Exception(
                        "El diagnóstico seleccionado no es válido");
                }


                Actual =
                    this.Lista
                        .FirstOrDefault(
                            x =>
                                x.Id ==
                                idDiagnostico);


                if (Actual == null)
                {
                    throw new Exception(
                        "No se encontró el diagnóstico");
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
                if (HttpContext.Session.GetString("RolId") != "1")
                {
                    throw new Exception(
                        "El mecánico no tiene permiso para eliminar diagnósticos");
                }


                if (this.Actual == null ||
                    this.Actual.Id == 0)
                {
                    throw new Exception(
                        "No se encontró el diagnóstico");
                }


                this.Actual =
                    this.IDiagnosticosPresentacion
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