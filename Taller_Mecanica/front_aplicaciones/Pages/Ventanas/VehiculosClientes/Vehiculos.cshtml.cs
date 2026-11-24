 
 

using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;
using libreria_aplicaciones.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_aplicaciones.Pages.Ventanas.VehiculosClientes
{
    public class VehiculosModel : PageModel
    {
        private IVehiculosPresentacion
            IVehiculosPresentacion;

        private IClientesPresentacion
            IClientesPresentacion;

        private IModelos_VehiculosPresentacion
            IModelos_VehiculosPresentacion;


        public VehiculosModel(
            IVehiculosPresentacion IVehiculosPresentacion,
            IClientesPresentacion IClientesPresentacion,
            IModelos_VehiculosPresentacion IModelos_VehiculosPresentacion)
        {
            this.IVehiculosPresentacion =
                IVehiculosPresentacion;

            this.IClientesPresentacion =
                IClientesPresentacion;

            this.IModelos_VehiculosPresentacion =
                IModelos_VehiculosPresentacion;

            this.Filtro =
                new Vehiculos();
        }


        [BindProperty]
        public List<Vehiculos> Lista { get; set; }
            = new List<Vehiculos>();


        [BindProperty]
        public Vehiculos? Filtro { get; set; }


        [BindProperty]
        public Vehiculos? Actual { get; set; }


        [BindProperty]
        public bool Formulario { get; set; }
            = false;


        [BindProperty]
        public bool Borrar { get; set; }
            = false;


        public List<Clientes> Clientes { get; set; }
            = new List<Clientes>();

         
        [BindProperty]
        public string? DocumentoCliente { get; set; }

        public Clientes? ClienteEncontrado { get; set; }


        public List<Modelos_Vehiculos> Modelos { get; set; }
            = new List<Modelos_Vehiculos>();


 
         
        private void CargarClientes()
        {
            this.Clientes =
                this.IClientesPresentacion
                    .Consultar();
        }



         
        private void CargarModelos()
        {
            this.Modelos =
                this.IModelos_VehiculosPresentacion
                    .Consultar();
        }


         
        private void CargarListas()
        {
            CargarClientes();
            CargarModelos();
        }



         
        public void OnPostBtBuscarCliente()
        {
            if (HttpContext.Session.GetString("RolId") != "1")
            {
                HttpContext.Response.Redirect("/");
                return;
            }

            Formulario = true;
            try
            {
                CargarListas();
                this.Actual ??= new Vehiculos();
                string documento = this.DocumentoCliente?.Trim() ?? "";
                if (string.IsNullOrEmpty(documento))
                    throw new Exception("Escriba la cédula del cliente");

                this.ClienteEncontrado = this.Clientes
                    .FirstOrDefault(x => x.Documento == documento);

                if (this.ClienteEncontrado == null)
                {
                    this.Actual.Cliente = 0;
                    throw new Exception("No se encontró un cliente con esa cédula");
                }

                this.Actual.Cliente = this.ClienteEncontrado.Id;
                ModelState.Remove("Actual.Cliente");
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
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


            OnPostBtBuscar();
        }


 
         
        public void OnPostBtBuscar()
        {
            try
            {

                CargarListas();


                this.Filtro ??=
                    new Vehiculos();


                this.Filtro.Placa ??=
                    "";


                string? rolId =
                    HttpContext.Session.GetString("RolId");


                string? clienteId =
                    HttpContext.Session.GetString("ClienteId");

                if (rolId == "3")
                {
                    if (string.IsNullOrEmpty(clienteId))
                    {
                        throw new Exception(
                            "No se encontró el cliente asociado al usuario");
                    }


                    if (!int.TryParse(
                        clienteId,
                        out int idCliente))
                    {
                        throw new Exception(
                            "El cliente asociado al usuario no es válido");
                    }


                    this.Filtro.Cliente =
                        idCliente;
                }
                else
                {

                    this.Filtro.Cliente =
                        0;
                }


                Lista =
                    this.IVehiculosPresentacion
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
                if (HttpContext.Session.GetString("RolId") != "1")
                {
                    throw new Exception(
                        "No tiene permiso para crear vehículos");
                }


                CargarListas();


                this.Actual =
                    new Vehiculos();
                this.DocumentoCliente = "";
                this.ClienteEncontrado = null;

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
                if (HttpContext.Session.GetString("RolId") != "1")
                {
                    throw new Exception(
                        "No tiene permiso para modificar vehículos");
                }


                if (this.Actual == null)
                {
                    throw new Exception(
                        "No existe información del vehículo");
                }


                if (this.Actual.Id == 0)
                {
                    this.Actual =
                        this.IVehiculosPresentacion
                            .Insertar(
                                this.Actual);
                }
                else
                {
                    this.Actual =
                        this.IVehiculosPresentacion
                            .Actualizar(
                                this.Actual);
                }
            }
            catch (Exception ex)
            {

                CargarListas();
                this.ClienteEncontrado = this.Clientes
                    .FirstOrDefault(x => x.Id == this.Actual?.Cliente);
                this.DocumentoCliente = this.ClienteEncontrado?.Documento ?? this.DocumentoCliente;

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
            try
            {
                if (HttpContext.Session.GetString("RolId") != "1")
                {
                    throw new Exception(
                        "No tiene permiso para modificar vehículos");
                }


                OnPostBtBuscar();


                if (!int.TryParse(
                    data,
                    out int idVehiculo))
                {
                    throw new Exception(
                        "El vehículo seleccionado no es válido");
                }


                Actual =
                    this.Lista
                        .FirstOrDefault(
                            x =>
                                x.Id ==
                                idVehiculo);


                if (Actual == null)
                {
                    throw new Exception(
                        "No se encontró el vehículo");
                }



                CargarListas();
                this.ClienteEncontrado = this.Clientes
                    .FirstOrDefault(x => x.Id == this.Actual.Cliente);
                this.DocumentoCliente = this.ClienteEncontrado?.Documento;

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
                        "No tiene permiso para eliminar vehículos");
                }


                OnPostBtBuscar();


                if (!int.TryParse(
                    data,
                    out int idVehiculo))
                {
                    throw new Exception(
                        "El vehículo seleccionado no es válido");
                }


                Actual =
                    this.Lista
                        .FirstOrDefault(
                            x =>
                                x.Id ==
                                idVehiculo);


                if (Actual == null)
                {
                    throw new Exception(
                        "No se encontró el vehículo");
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
                        "No tiene permiso para eliminar vehículos");
                }


                if (this.Actual == null ||
                    this.Actual.Id == 0)
                {
                    throw new Exception(
                        "No se encontró el vehículo");
                }


                this.Actual =
                    this.IVehiculosPresentacion
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