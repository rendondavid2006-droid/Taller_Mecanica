 
 

using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;
using libreria_aplicaciones.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_aplicaciones.Pages.Ventanas.Taller
{
    public class CitasModel : PageModel
    {
        private ICitasPresentacion ICitasPresentacion;
        private IClientesPresentacion IClientesPresentacion;
        private IVehiculosPresentacion IVehiculosPresentacion;
        private IHorarios_CitasPresentacion IHorarios_CitasPresentacion;
        private IEstados_CitasPresentacion IEstados_CitasPresentacion;

        public CitasModel(
            ICitasPresentacion ICitasPresentacion,
            IClientesPresentacion IClientesPresentacion,
            IVehiculosPresentacion IVehiculosPresentacion,
            IHorarios_CitasPresentacion IHorarios_CitasPresentacion,
            IEstados_CitasPresentacion IEstados_CitasPresentacion)
        {
            this.ICitasPresentacion = ICitasPresentacion;
            this.IClientesPresentacion = IClientesPresentacion;
            this.IVehiculosPresentacion = IVehiculosPresentacion;
            this.IHorarios_CitasPresentacion = IHorarios_CitasPresentacion;
            this.IEstados_CitasPresentacion = IEstados_CitasPresentacion;

            this.Filtro = new Citas
            {
                _Cliente = new Clientes()
            };
        }

        [BindProperty]
        public List<Citas> Lista { get; set; } = new List<Citas>();

        [BindProperty]
        public Citas? Filtro { get; set; }

        [BindProperty]
        public Citas? Actual { get; set; }

        [BindProperty]
        public bool Formulario { get; set; } = false;

        [BindProperty]
        public bool Borrar { get; set; } = false;

        [BindProperty]
        public string? DocumentoCliente { get; set; }

        public List<Clientes> Clientes { get; set; } = new List<Clientes>();
        public List<Vehiculos> Vehiculos { get; set; } = new List<Vehiculos>();
        public List<Horarios_Citas> Horarios { get; set; } = new List<Horarios_Citas>();
        public List<Estados_Citas> Estados_Citas { get; set; } = new List<Estados_Citas>();

         
        private void CargarListas(
            int? horarioActual = null,
            int? clienteActual = null)
        {
            string? rolId =
                HttpContext.Session.GetString("RolId");

            string? clienteId =
                HttpContext.Session.GetString("ClienteId");

            Clientes = this.IClientesPresentacion
                .Buscar(new Clientes
                {
                    Nombre = "",
                    Documento = ""
                });

            int idCliente =
                clienteActual ?? 0;

            if (rolId == "3")
            {
                if (string.IsNullOrEmpty(clienteId))
                {
                    throw new Exception(
                        "No se encontro el cliente asociado al usuario");
                }

                if (!int.TryParse(clienteId, out idCliente))
                {
                    throw new Exception(
                        "El cliente asociado al usuario no es valido");
                }

                Clientes = Clientes
                    .Where(x => x.Id == idCliente)
                    .ToList();

                DocumentoCliente = Clientes
                    .FirstOrDefault()
                    ?.Documento;
            }

            if (idCliente > 0)
            {
                Vehiculos = this.IVehiculosPresentacion
                    .Buscar(new Vehiculos
                    {
                        Cliente = idCliente,
                        Placa = ""
                    });
            }
            else
            {
                Vehiculos =
                    new List<Vehiculos>();
            }

            Horarios = this.IHorarios_CitasPresentacion
                .Consultar()
                .Where(x =>
                    x.Disponible ||
                    x.Id == horarioActual)
                .ToList();

            Estados_Citas = this.IEstados_CitasPresentacion
                .Consultar();
        }

         
        private int ObtenerEstadoPendiente()
        {
            var estadoPendiente = this.IEstados_CitasPresentacion
                .Consultar()
                .FirstOrDefault(x =>
                    x.Nombre_Estado == "Pendiente" &&
                    x.Estado == true);

            if (estadoPendiente == null)
            {
                throw new Exception(
                    "No se encontró el estado Pendiente");
            }

            return estadoPendiente.Id;
        }

         
        private int ObtenerEstadoCancelado()
        {
            var estadoCancelado = this.IEstados_CitasPresentacion
                .Consultar()
                .FirstOrDefault(x =>
                    x.Nombre_Estado == "Cancelada" &&
                    x.Estado == true);

            if (estadoCancelado == null)
            {
                throw new Exception(
                    "No se encontró el estado Cancelada");
            }

            return estadoCancelado.Id;
        }

         
        private void ValidarVehiculoDelCliente(Citas cita)
        {
            Vehiculos? vehiculo = this.IVehiculosPresentacion
                .Buscar(new Vehiculos
                {
                    Cliente = cita.Cliente,
                    Placa = ""
                })
                .FirstOrDefault(x => x.Id == cita.Vehiculo);

            if (vehiculo == null)
            {
                throw new Exception(
                    "El vehículo seleccionado no existe");
            }

            if (vehiculo.Cliente != cita.Cliente)
            {
                throw new Exception(
                    "El vehículo seleccionado no pertenece al cliente");
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
                this.Filtro ??= new Citas();
                this.Filtro._Cliente ??= new Clientes();
                this.Filtro._Cliente.Documento ??= "";

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

                    if (!int.TryParse(clienteId, out int idCliente))
                    {
                        throw new Exception(
                            "El cliente asociado al usuario no es válido");
                    }

                    this.Filtro.Cliente = idCliente;
                }
                else
                {
                    this.Filtro.Cliente = 0;
                }

                Lista = this.ICitasPresentacion
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

                if (rolId == "2")
                {
                    throw new Exception(
                        "El mecánico no tiene permiso para crear citas");
                }

                this.Actual = new Citas();

                if (rolId == "1")
                {
                    this.Actual.Estado_Cita =
                        ObtenerEstadoPendiente();
                }

                if (rolId == "3")
                {
                    string? clienteId =
                        HttpContext.Session.GetString("ClienteId");

                    if (string.IsNullOrEmpty(clienteId))
                    {
                        throw new Exception(
                            "No se encontró el cliente asociado al usuario");
                    }

                    if (!int.TryParse(clienteId, out int idCliente))
                    {
                        throw new Exception(
                            "El cliente asociado al usuario no es válido");
                    }

                    this.Actual.Cliente = idCliente;
                    this.Actual.Estado_Cita = ObtenerEstadoPendiente();
                }

                if (rolId == "3")
                {
                    CargarListas(
                        null,
                        this.Actual.Cliente);
                }
                else
                {
                    DocumentoCliente = "";
                    CargarListas();
                }

                Formulario = true;
            }
            catch (Exception ex)
            {
                LogConversor.Log(
                    ex,
                    ViewData!);
            }
        }

         
        public void OnPostBtCargarCliente()
        {
            try
            {
                if (HttpContext.Session.GetString("RolId") != "1")
                {
                    throw new Exception(
                        "Esta accion corresponde al administrador");
                }

                this.Actual ??=
                    new Citas();

                DocumentoCliente =
                    DocumentoCliente?.Trim();

                if (string.IsNullOrEmpty(DocumentoCliente))
                {
                    throw new Exception(
                        "Ingrese el documento del cliente");
                }

                this.Actual.Cliente = 0;
                this.Actual.Vehiculo = 0;

                Clientes clienteResultado =
                    this.IClientesPresentacion
                        .Buscar(new Clientes
                        {
                            Documento = DocumentoCliente,
                            Nombre = ""
                        })
                        .FirstOrDefault(x =>
                            x.Documento == DocumentoCliente)
                    ?? throw new Exception(
                        "No se encontro un cliente con ese documento");

                this.Actual.Cliente =
                    clienteResultado.Id;


                ModelState.Remove("Actual.Cliente");
                ModelState.Remove("Actual.Vehiculo");

                CargarListas(
                    this.Actual.Horario_Cita > 0
                        ? this.Actual.Horario_Cita
                        : null,
                    clienteResultado.Id);

                if (!Vehiculos.Any())
                {
                    throw new Exception(
                        "El cliente no tiene vehiculos registrados");
                }

                if (!Vehiculos.Any(x =>
                    x.Id == this.Actual.Vehiculo))
                {
                    this.Actual.Vehiculo = 0;
                }

                Formulario = true;
            }
            catch (Exception ex)
            {
                Formulario = true;

                try
                {
                    CargarListas(
                        this.Actual?.Horario_Cita > 0
                            ? this.Actual.Horario_Cita
                            : null,
                        this.Actual?.Cliente > 0
                            ? this.Actual.Cliente
                            : null);
                }
                catch
                {
                }

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
                        "No existe información de la cita");
                }

                string? rolId =
                    HttpContext.Session.GetString("RolId");

                if (rolId != "1" &&
                    rolId != "2" &&
                    rolId != "3")
                {
                    throw new Exception(
                        "No tiene permiso para modificar citas");
                }

                bool esCitaNueva =
                    this.Actual.Id == 0;

                if (rolId == "1")
                {
   
                    DocumentoCliente =
                        DocumentoCliente?.Trim();

                    if (string.IsNullOrEmpty(DocumentoCliente))
                    {
                        throw new Exception(
                            "Primero busque el cliente por documento");
                    }

                    Clientes clienteResultado =
                        this.IClientesPresentacion
                            .Buscar(new Clientes
                            {
                                Documento = DocumentoCliente,
                                Nombre = ""
                            })
                            .FirstOrDefault(x =>
                                x.Documento == DocumentoCliente)
                        ?? throw new Exception(
                            "No se encontro un cliente con ese documento");

                    this.Actual.Cliente =
                        clienteResultado.Id;
                }


                if (rolId == "2")
                {
                    if (esCitaNueva)
                    {
                        throw new Exception(
                            "El mecánico no tiene permiso para crear citas");
                    }

                    Citas filtro = new Citas
                    {
                        _Cliente = new Clientes
                        {
                            Nombre = ""
                        }
                    };

                    List<Citas> citas =
                        this.ICitasPresentacion.Buscar(filtro);

                    Citas? citaOriginal = citas
                        .FirstOrDefault(x => x.Id == this.Actual.Id);

                    if (citaOriginal == null)
                    {
                        throw new Exception(
                            "No se encontró la cita");
                    }

                    int nuevoEstado =
                        this.Actual.Estado_Cita;

                    this.Actual = citaOriginal;
                    this.Actual.Estado_Cita = nuevoEstado;
                }


                if (rolId == "3")
                {
                    if (!esCitaNueva)
                    {
                        throw new Exception(
                            "El cliente no tiene permiso para editar citas");
                    }

                    string? clienteId =
                        HttpContext.Session.GetString("ClienteId");

                    if (string.IsNullOrEmpty(clienteId))
                    {
                        throw new Exception(
                            "No se encontró el cliente asociado al usuario");
                    }

                    if (!int.TryParse(clienteId, out int idCliente))
                    {
                        throw new Exception(
                            "El cliente asociado al usuario no es válido");
                    }

                    this.Actual.Cliente = idCliente;
                    this.Actual.Estado_Cita = ObtenerEstadoPendiente();
                }

   
                ValidarVehiculoDelCliente(this.Actual);


                if (esCitaNueva)
                {
                    Horarios_Citas? horarioSeleccionado =
                        this.IHorarios_CitasPresentacion
                            .Consultar()
                            .FirstOrDefault(x =>
                                x.Id == this.Actual.Horario_Cita);

                    if (horarioSeleccionado == null)
                    {
                        throw new Exception(
                            "El horario seleccionado no existe");
                    }

                    if (!horarioSeleccionado.Disponible)
                    {
                        throw new Exception(
                            "El horario seleccionado ya no está disponible");
                    }
                }

    
                if (esCitaNueva)
                {
                    this.Actual = this.ICitasPresentacion
                        .Insertar(this.Actual);
                }
                else
                {
                    this.Actual = this.ICitasPresentacion
                        .Actualizar(this.Actual);
                }
            }
            catch (Exception ex)
            {
                Formulario = true;

                try
                {
                    CargarListas(
                        this.Actual?.Horario_Cita,
                        this.Actual?.Cliente);
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

         
        public void OnPostBtEditar(string data)
        {
            try
            {
                if (HttpContext.Session.GetString("RolId") == "3")
                {
                    throw new Exception(
                        "El cliente no tiene permiso para editar citas");
                }

                OnPostBtBuscar();

                if (!int.TryParse(data, out int idCita))
                {
                    throw new Exception(
                        "La cita seleccionada no es válida");
                }

                Actual = this.Lista
                    .FirstOrDefault(x => x.Id == idCita);

                if (Actual == null)
                {
                    throw new Exception(
                        "No se encontró la cita");
                }

                DocumentoCliente =
                    Actual._Cliente?.Documento;

                CargarListas(
                    Actual.Horario_Cita,
                    Actual.Cliente);

                if (string.IsNullOrEmpty(DocumentoCliente))
                {
                    DocumentoCliente = Clientes
                        .FirstOrDefault(x =>
                            x.Id == Actual.Cliente)
                        ?.Documento;
                }

                Formulario = true;
            }
            catch (Exception ex)
            {
                LogConversor.Log(
                    ex,
                    ViewData!);
            }
        }

         
        public void OnPostBtPregBorrar(string data)
        {
            try
            {
                string? rolId =
                    HttpContext.Session.GetString("RolId");

                if (rolId != "1")
                {
                    throw new Exception(
                        "No tiene permiso para eliminar citas");
                }

                OnPostBtBuscar();

                if (!int.TryParse(data, out int idCita))
                {
                    throw new Exception(
                        "La cita seleccionada no es válida");
                }

                Actual = this.Lista
                    .FirstOrDefault(x => x.Id == idCita);

                if (Actual == null)
                {
                    throw new Exception(
                        "No se encontró la cita");
                }

                Borrar = true;
            }
            catch (Exception ex)
            {
                LogConversor.Log(
                    ex,
                    ViewData!);
            }
        }

         
        public void OnPostBtCancelar(string data)
        {
            try
            {
                string? rolId =
                    HttpContext.Session.GetString("RolId");

                if (rolId != "3")
                {
                    throw new Exception(
                        "Esta acción corresponde al cliente");
                }

                string? clienteId =
                    HttpContext.Session.GetString("ClienteId");

                if (string.IsNullOrEmpty(clienteId))
                {
                    throw new Exception(
                        "No se encontró el cliente asociado al usuario");
                }

                if (!int.TryParse(clienteId, out int idCliente))
                {
                    throw new Exception(
                        "El cliente asociado al usuario no es válido");
                }

                if (!int.TryParse(data, out int idCita))
                {
                    throw new Exception(
                        "La cita seleccionada no es válida");
                }

                Citas filtroSeguridad = new Citas
                {
                    Cliente = idCliente,
                    _Cliente = new Clientes
                    {
                        Nombre = ""
                    }
                };

                List<Citas> citasCliente =
                    this.ICitasPresentacion
                        .Buscar(filtroSeguridad);

                Citas? cita = citasCliente
                    .FirstOrDefault(x => x.Id == idCita);

                if (cita == null)
                {
                    throw new Exception(
                        "La cita no pertenece al cliente");
                }

                if (cita._Estado_Cita?.Nombre_Estado == "Finalizada")
                {
                    throw new Exception(
                        "Una cita finalizada no puede cancelarse");
                }

                if (cita._Estado_Cita?.Nombre_Estado == "Cancelada")
                {
                    throw new Exception(
                        "La cita ya se encuentra cancelada");
                }

                cita.Estado_Cita =
                    ObtenerEstadoCancelado();

                this.ICitasPresentacion
                    .Actualizar(cita);


                Horarios_Citas? horario =
                    this.IHorarios_CitasPresentacion
                        .Consultar()
                        .FirstOrDefault(x =>
                            x.Id == cita.Horario_Cita);

                if (horario != null)
                {
                    horario.Disponible = true;

                    this.IHorarios_CitasPresentacion
                        .Actualizar(horario);
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

         
        public void OnPostBtBorrar()
        {
            try
            {
                if (this.Actual == null ||
                    this.Actual.Id == 0)
                {
                    throw new Exception(
                        "No se encontró la cita");
                }

                string? rolId =
                    HttpContext.Session.GetString("RolId");

                if (rolId != "1")
                {
                    throw new Exception(
                        "No tiene permiso para eliminar citas");
                }

                this.Actual = this.ICitasPresentacion
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
