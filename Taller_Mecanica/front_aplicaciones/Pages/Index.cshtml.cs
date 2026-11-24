 
 

using libreria_aplicaciones.Entidades;
using libreria_aplicaciones.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using presentacion_aplicaciones.Interfaces;

namespace front_aplicaciones.Pages
{
    public class IndexModel : PageModel
    {
        private IUsuariosPresentacion IUsuariosPresentacion;

        private IUsuarios_ClientesPresentacion
            IUsuarios_ClientesPresentacion;


        public IndexModel(
            IUsuariosPresentacion IUsuariosPresentacion,
            IUsuarios_ClientesPresentacion IUsuarios_ClientesPresentacion)
        {
            this.IUsuariosPresentacion =
                IUsuariosPresentacion;

            this.IUsuarios_ClientesPresentacion =
                IUsuarios_ClientesPresentacion;
        }


        public bool EstaLogueado { get; set; } = false;


        public string? UsuarioActual { get; set; }


        public string? RolActual { get; set; }


        [BindProperty]
        public string? Nombre_Usuario { get; set; }


        [BindProperty]
        public string? Contraseña { get; set; }


         
        public void OnGet()
        {
            var variable_session =
                HttpContext.Session.GetString("Usuario");


            if (!string.IsNullOrEmpty(variable_session))
            {
                EstaLogueado = true;

                UsuarioActual =
                    variable_session;

                RolActual =
                    HttpContext.Session.GetString("Rol");

                return;
            }


            EstaLogueado = false;
        }


         
        public void OnPostBtClean()
        {
            try
            {
                Nombre_Usuario =
                    string.Empty;

                Contraseña =
                    string.Empty;

                ModelState.Clear();

                EstaLogueado =
                    false;
            }
            catch (Exception ex)
            {
                LogConversor.Log(
                    ex,
                    ViewData!);
            }
        }


         
        public void OnPostBtEnter()
        {
            try
            {
                if (string.IsNullOrEmpty(Nombre_Usuario) ||
                    string.IsNullOrEmpty(Contraseña))
                {
                    throw new Exception(
                        "Debe ingresar usuario y contraseña");
                }



                Usuarios entidad =
                    new Usuarios();


                entidad.Nombre_Usuario =
                    Nombre_Usuario;

                entidad.Contraseña =
                    Contraseña;


                Usuarios usuario =
                    this.IUsuariosPresentacion
                        .Login(entidad);


                if (usuario != null &&
                    usuario.Id != 0)
                {
                    HttpContext.Session.SetString(
                        "Usuario",
                        usuario.Nombre_Usuario!);


                    if (usuario._Empleado != null)
                    {
                        HttpContext.Session.SetString(
                            "Empleado",
                            usuario._Empleado.Nombre!);


                        HttpContext.Session.SetString(
                            "EmpleadoId",
                            usuario.Empleado.ToString());


                        if (usuario._Empleado._Rol != null)
                        {
                            HttpContext.Session.SetString(
                                "Rol",
                                usuario._Empleado._Rol.Nombre_Rol!);


                            HttpContext.Session.SetString(
                                "RolId",
                                usuario._Empleado._Rol.Id.ToString());
                        }
                    }


                    EstaLogueado =
                        true;


                    UsuarioActual =
                        usuario.Nombre_Usuario;


                    RolActual =
                        usuario._Empleado?._Rol?.Nombre_Rol;


                    Nombre_Usuario =
                        string.Empty;

                    Contraseña =
                        string.Empty;


                    return;
                }



                Usuarios_Clientes entidadCliente =
                    new Usuarios_Clientes();


                entidadCliente.Nombre_Usuario =
                    Nombre_Usuario;

                entidadCliente.Contraseña =
                    Contraseña;


                Usuarios_Clientes usuarioCliente =
                    this.IUsuarios_ClientesPresentacion
                        .Login(entidadCliente);


                if (usuarioCliente == null ||
                    usuarioCliente.Id == 0)
                {
                    throw new Exception(
                        "Usuario o contraseña incorrectos");
                }


                HttpContext.Session.SetString(
                    "Usuario",
                    usuarioCliente.Nombre_Usuario!);


                HttpContext.Session.SetString(
                    "ClienteId",
                    usuarioCliente.Cliente.ToString());


                HttpContext.Session.SetString(
                    "RolId",
                    usuarioCliente.Rol.ToString());


                string rolCliente =
                    usuarioCliente._Rol?.Nombre_Rol
                    ?? "Cliente";


                HttpContext.Session.SetString(
                    "Rol",
                    rolCliente);


                EstaLogueado =
                    true;


                UsuarioActual =
                    usuarioCliente.Nombre_Usuario;


                RolActual =
                    rolCliente;


                Nombre_Usuario =
                    string.Empty;

                Contraseña =
                    string.Empty;
            }
            catch (Exception ex)
            {
                EstaLogueado =
                    false;


                LogConversor.Log(
                    ex,
                    ViewData!);
            }
        }


         
        public void OnPostBtClose()
        {
            try
            {
                HttpContext.Session.Clear();


                EstaLogueado =
                    false;


                HttpContext.Response.Redirect("/");
            }
            catch (Exception ex)
            {
                LogConversor.Log(
                    ex,
                    ViewData!);
            }
        }
    }
}