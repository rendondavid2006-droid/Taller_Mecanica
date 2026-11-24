 
 

using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;
using libreria_aplicaciones.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_aplicaciones.Pages.Ventanas.Taller
{
    public class Ordenes_ServiciosModel : PageModel
    {
        private IOrdenes_ServiciosPresentacion
            IOrdenes_ServiciosPresentacion;

        private ICitasPresentacion
            ICitasPresentacion;

        private IVehiculosPresentacion
            IVehiculosPresentacion;

        private IEmpleadosPresentacion
            IEmpleadosPresentacion;

        private IEstados_OrdenesPresentacion
            IEstados_OrdenesPresentacion;


        public Ordenes_ServiciosModel(
            IOrdenes_ServiciosPresentacion IOrdenes_ServiciosPresentacion,
            ICitasPresentacion ICitasPresentacion,
            IVehiculosPresentacion IVehiculosPresentacion,
            IEmpleadosPresentacion IEmpleadosPresentacion,
            IEstados_OrdenesPresentacion IEstados_OrdenesPresentacion)
        {
            this.IOrdenes_ServiciosPresentacion =
                IOrdenes_ServiciosPresentacion;

            this.ICitasPresentacion =
                ICitasPresentacion;

            this.IVehiculosPresentacion =
                IVehiculosPresentacion;

            this.IEmpleadosPresentacion =
                IEmpleadosPresentacion;

            this.IEstados_OrdenesPresentacion =
                IEstados_OrdenesPresentacion;


            this.Filtro =
                new Ordenes_Servicios
                {
                    _Cita =
                        new Citas
                        {
                            _Cliente =
                                new Clientes()
                        }
                };
        }


        [BindProperty]
        public List<Ordenes_Servicios> Lista { get; set; }
            = new List<Ordenes_Servicios>();


        [BindProperty]
        public Ordenes_Servicios? Filtro { get; set; }


        [BindProperty]
        public Ordenes_Servicios? Actual { get; set; }


        [BindProperty]
        public bool Formulario { get; set; } = false;


        [BindProperty]
        public bool Borrar { get; set; } = false;


        public List<Citas> Citas { get; set; }
            = new List<Citas>();


        public List<Vehiculos> Vehiculos { get; set; }
            = new List<Vehiculos>();


        public List<Empleados> Empleados { get; set; }
            = new List<Empleados>();


        public List<Estados_Ordenes> Estados_Ordenes { get; set; }
            = new List<Estados_Ordenes>();




         
        private void CargarListas()
        {
            Citas =
                this.ICitasPresentacion
                    .Buscar(
                        new Citas
                        {
                            _Cliente =
                                new Clientes
                                {
                                    Documento = ""
                                }
                        });


            Vehiculos =
                this.IVehiculosPresentacion
                    .Buscar(
                        new Vehiculos
                        {
                            Placa = ""
                        });


            Empleados =
                this.IEmpleadosPresentacion
                    .Consultar();


            Estados_Ordenes =
                this.IEstados_OrdenesPresentacion
                    .Consultar();


            string? rolId =
                HttpContext.Session.GetString("RolId");


            if (rolId == "2")
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


                Empleados =
                    Empleados
                        .Where(
                            x => x.Id == idEmpleado)
                        .ToList();
            }
        }




         
        private int ObtenerEstadoRecibida()
        {
            var estadoRecibido =
                this.IEstados_OrdenesPresentacion
                    .Consultar()
                    .FirstOrDefault(
                        x =>
                            x.Nombre_Estado == "Recibido" &&
                            x.Estado == true);

            if (estadoRecibido == null)
            {
                throw new Exception(
                    "No se encontró el estado Recibido");
            }

            return estadoRecibido.Id;
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
                this.Filtro ??=
                    new Ordenes_Servicios();


                this.Filtro._Cita ??=
                    new Citas();


                this.Filtro._Cita._Cliente ??=
                    new Clientes();


                this.Filtro._Cita._Cliente.Documento ??=
                    "";


                Lista =
                    this.IOrdenes_ServiciosPresentacion
                        .Buscar(this.Filtro);
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
                string? rolId =
                    HttpContext.Session.GetString("RolId");


                if (rolId != "1" &&
                    rolId != "2")
                {
                    throw new Exception(
                        "No tiene permiso para crear órdenes de servicio");
                }


                this.Actual =
                    new Ordenes_Servicios();



                this.Actual.Fecha_Ingreso =
                    DateTime.Now;



                this.Actual.Estado_Orden =
                    ObtenerEstadoRecibida();


 
                if (rolId == "2")
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


                    this.Actual.Empleado =
                        idEmpleado;
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
                if (this.Actual == null)
                {
                    throw new Exception(
                        "No existe información de la orden");
                }


                string? rolId =
                    HttpContext.Session.GetString("RolId");


                if (rolId != "1" &&
                    rolId != "2")
                {
                    throw new Exception(
                        "No tiene permiso para modificar órdenes de servicio");
                }



                if (rolId == "2")
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



                    if (this.Actual.Id == 0)
                    {
                        this.Actual.Empleado =
                            idEmpleado;



                        this.Actual.Estado_Orden =
                            ObtenerEstadoRecibida();
                    }
                    else
                    {


                        OnPostBtBuscar();


                        Ordenes_Servicios? ordenOriginal =
                            Lista
                                .FirstOrDefault(
                                    x =>
                                        x.Id ==
                                        this.Actual.Id);


                        if (ordenOriginal == null)
                        {
                            throw new Exception(
                                "No se encontró la orden");
                        }


                        this.Actual.Empleado =
                            ordenOriginal.Empleado;
                    }
                }


                if (this.Actual.Id == 0)
                {
                    this.Actual =
                        this.IOrdenes_ServiciosPresentacion
                            .Insertar(this.Actual);
                }
                else
                {
                    this.Actual =
                        this.IOrdenes_ServiciosPresentacion
                            .Actualizar(this.Actual);
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
                string? rolId =
                    HttpContext.Session.GetString("RolId");


                if (rolId != "1" &&
                    rolId != "2")
                {
                    throw new Exception(
                        "No tiene permiso para modificar órdenes de servicio");
                }


                OnPostBtBuscar();


                if (!int.TryParse(
                    data,
                    out int idOrden))
                {
                    throw new Exception(
                        "La orden seleccionada no es válida");
                }


                Actual =
                    this.Lista
                        .FirstOrDefault(
                            x =>
                                x.Id ==
                                idOrden);


                if (Actual == null)
                {
                    throw new Exception(
                        "No se encontró la orden");
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
                        "No tiene permiso para eliminar órdenes de servicio");
                }


                OnPostBtBuscar();


                if (!int.TryParse(
                    data,
                    out int idOrden))
                {
                    throw new Exception(
                        "La orden seleccionada no es válida");
                }


                Actual =
                    this.Lista
                        .FirstOrDefault(
                            x =>
                                x.Id ==
                                idOrden);


                if (Actual == null)
                {
                    throw new Exception(
                        "No se encontró la orden");
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
                        "No tiene permiso para eliminar órdenes de servicio");
                }


                if (this.Actual == null ||
                    this.Actual.Id == 0)
                {
                    throw new Exception(
                        "No se encontró la orden");
                }


                this.Actual =
                    this.IOrdenes_ServiciosPresentacion
                        .Borrar(this.Actual);
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