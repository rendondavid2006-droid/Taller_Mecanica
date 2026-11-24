 
 

using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;
using libreria_aplicaciones.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_aplicaciones.Pages.Ventanas.Administracion
{
    public class UsuariosModel : PageModel
    {
        private IUsuariosPresentacion IUsuariosPresentacion;
        private IEmpleadosPresentacion IEmpleadosPresentacion;

        public UsuariosModel(
            IUsuariosPresentacion IUsuariosPresentacion,
            IEmpleadosPresentacion IEmpleadosPresentacion)
        {
            this.IUsuariosPresentacion = IUsuariosPresentacion;
            this.IEmpleadosPresentacion = IEmpleadosPresentacion;
            this.Filtro = new Usuarios();
        }

        [BindProperty]
        public List<Usuarios> Lista { get; set; } = new List<Usuarios>();

        [BindProperty]
        public Usuarios? Filtro { get; set; }

        [BindProperty]
        public Usuarios? Actual { get; set; }

        [BindProperty]
        public bool Formulario { get; set; } = false;

        [BindProperty]
        public bool Borrar { get; set; } = false;

        public List<Empleados> Empleados { get; set; } = new List<Empleados>();

         
        private bool ValidarAdministrador()
        {
            var usuario = HttpContext.Session.GetString("Usuario");
            var rolId = HttpContext.Session.GetString("RolId");

            return !string.IsNullOrEmpty(usuario) && rolId == "1";
        }

         
        private void CargarEmpleados()
        {
            Empleados = this.IEmpleadosPresentacion
                .Buscar(new Empleados { Documento = "" });
        }

         
        public void OnGet()
        {
            if (!ValidarAdministrador())
            {
                HttpContext.Response.Redirect("/");
                return;
            }

            CargarEmpleados();
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
                CargarEmpleados();
                this.Filtro ??= new Usuarios();
                this.Filtro.Nombre_Usuario ??= "";

                Lista = this.IUsuariosPresentacion
                    .Buscar(this.Filtro);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
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
                CargarEmpleados();
                this.Actual = new Usuarios();
                Formulario = true;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
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
                    throw new Exception("No existe información del usuario");

                if (this.Actual.Id == 0)
                    this.Actual = this.IUsuariosPresentacion.Insertar(this.Actual);
                else
                    this.Actual = this.IUsuariosPresentacion.Actualizar(this.Actual);
            }
            catch (Exception ex)
            {
                CargarEmpleados();
                Formulario = true;
                LogConversor.Log(ex, ViewData!);
            }
            finally
            {
                OnPostBtBuscar();
            }
        }

         
        public void OnPostBtEditar(string data)
        {
            if (!ValidarAdministrador())
            {
                HttpContext.Response.Redirect("/");
                return;
            }

            try
            {
                OnPostBtBuscar();

                if (!int.TryParse(data, out int idUsuario))
                    throw new Exception("El usuario seleccionado no es válido");

                Actual = this.Lista.FirstOrDefault(x => x.Id == idUsuario);

                if (Actual == null)
                    throw new Exception("No se encontró el usuario");

                CargarEmpleados();
                Formulario = true;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

         
        public void OnPostBtPregBorrar(string data)
        {
            if (!ValidarAdministrador())
            {
                HttpContext.Response.Redirect("/");
                return;
            }

            try
            {
                OnPostBtBuscar();

                if (!int.TryParse(data, out int idUsuario))
                    throw new Exception("El usuario seleccionado no es válido");

                Actual = this.Lista.FirstOrDefault(x => x.Id == idUsuario);

                if (Actual == null)
                    throw new Exception("No se encontró el usuario");

                Borrar = true;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
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
                if (this.Actual == null || this.Actual.Id == 0)
                    throw new Exception("No se encontró el usuario");

                this.Actual = this.IUsuariosPresentacion.Borrar(this.Actual);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
            finally
            {
                OnPostBtBuscar();
            }
        }
    }
}
