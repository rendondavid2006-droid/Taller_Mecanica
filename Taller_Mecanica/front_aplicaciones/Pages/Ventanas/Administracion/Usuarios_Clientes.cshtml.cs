 
 

using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;
using libreria_aplicaciones.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_aplicaciones.Pages.Ventanas.Administracion
{
    public class Usuarios_ClientesModel : PageModel
    {
        private IUsuarios_ClientesPresentacion
            IUsuarios_ClientesPresentacion;

        private IClientesPresentacion
            IClientesPresentacion;

        private IRolesPresentacion
            IRolesPresentacion;


        public Usuarios_ClientesModel(
            IUsuarios_ClientesPresentacion IUsuarios_ClientesPresentacion,
            IClientesPresentacion IClientesPresentacion,
            IRolesPresentacion IRolesPresentacion)
        {
            this.IUsuarios_ClientesPresentacion =
                IUsuarios_ClientesPresentacion;

            this.IClientesPresentacion =
                IClientesPresentacion;

            this.IRolesPresentacion =
                IRolesPresentacion;

            this.Filtro =
                new Usuarios_Clientes();
        }


        [BindProperty]
        public List<Usuarios_Clientes> Lista { get; set; }
            = new List<Usuarios_Clientes>();


        [BindProperty]
        public Usuarios_Clientes? Filtro { get; set; }


        [BindProperty]
        public Usuarios_Clientes? Actual { get; set; }


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


         
        private void CargarClientes()
        {
            this.Clientes =
                this.IClientesPresentacion
                    .Consultar();
        }


         
        private int ObtenerRolCliente()
        {
            var rol =
                this.IRolesPresentacion
                    .Consultar()
                    .FirstOrDefault(
                        x =>
                            x.Nombre_Rol == "Cliente" &&
                            x.Estado == true);


            if (rol == null)
            {
                throw new Exception(
                    "No existe el rol Cliente activo");
            }


            return rol.Id;
        }


         
        public void OnPostBtBuscarCliente()
        {
            if (!ValidarAdministrador())
            {
                HttpContext.Response.Redirect("/");
                return;
            }

            Formulario = true;

            try
            {
                CargarClientes();
                this.Actual ??= new Usuarios_Clientes();

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
                this.DocumentoCliente = documento;
                ModelState.Remove("Actual.Cliente");
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }


         
        public void OnGet()
        {
            if (!ValidarAdministrador())
            {
                HttpContext.Response.Redirect("/");
                return;
            }


            CargarClientes();

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
                    new Usuarios_Clientes();


                this.Filtro.Nombre_Usuario ??=
                    "";


                Lista =
                    this.IUsuarios_ClientesPresentacion
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
                CargarClientes();
                this.DocumentoCliente = "";
                this.ClienteEncontrado = null;

                this.Actual =
                    new Usuarios_Clientes
                    {
                        Rol =
                            ObtenerRolCliente(),

                        Estado =
                            true,

                        Fecha_Creacion =
                            DateTime.Now
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
                CargarClientes();


                if (this.Actual == null)
                {
                    throw new Exception(
                        "No existe información del usuario cliente");
                }

                 
                string documento = this.DocumentoCliente?.Trim() ?? "";
                if (string.IsNullOrEmpty(documento))
                    throw new Exception("Busque primero el cliente por su cédula");

                this.ClienteEncontrado = this.Clientes
                    .FirstOrDefault(x => x.Documento == documento);
                if (this.ClienteEncontrado == null)
                    throw new Exception("No se encontró un cliente con esa cédula");

                this.Actual.Cliente = this.ClienteEncontrado.Id;
                ModelState.Remove("Actual.Cliente");

                this.Actual.Rol =
                    ObtenerRolCliente();


                if (this.Actual.Fecha_Creacion ==
                    DateTime.MinValue)
                {
                    this.Actual.Fecha_Creacion =
                        DateTime.Now;
                }


                if (this.Actual.Id == 0)
                {
                    this.Actual =
                        this.IUsuarios_ClientesPresentacion
                            .Insertar(
                                this.Actual);
                }
                else
                {
                    this.Actual =
                        this.IUsuarios_ClientesPresentacion
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
                    CargarClientes();
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
                    out int idUsuarioCliente))
                {
                    throw new Exception(
                        "El usuario cliente seleccionado no es válido");
                }


                Actual =
                    this.Lista
                        .FirstOrDefault(
                            x =>
                                x.Id ==
                                idUsuarioCliente);


                if (Actual == null)
                {
                    throw new Exception(
                        "No se encontró el usuario cliente");
                }


                CargarClientes();
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
                    out int idUsuarioCliente))
                {
                    throw new Exception(
                        "El usuario cliente seleccionado no es válido");
                }


                Actual =
                    this.Lista
                        .FirstOrDefault(
                            x =>
                                x.Id ==
                                idUsuarioCliente);


                if (Actual == null)
                {
                    throw new Exception(
                        "No se encontró el usuario cliente");
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
                        "No se encontró el usuario cliente");
                }


                this.Actual =
                    this.IUsuarios_ClientesPresentacion
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