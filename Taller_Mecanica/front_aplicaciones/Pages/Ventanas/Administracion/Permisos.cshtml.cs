 
 

using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;
using libreria_aplicaciones.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_aplicaciones.Pages.Ventanas.Administracion
{
    public class PermisosModel : PageModel
    {
        private IPermisosPresentacion IPermisosPresentacion;
        private IRolesPresentacion IRolesPresentacion;

        public PermisosModel(
            IPermisosPresentacion IPermisosPresentacion,
            IRolesPresentacion IRolesPresentacion)
        {
            this.IPermisosPresentacion = IPermisosPresentacion;
            this.IRolesPresentacion = IRolesPresentacion;
            this.Filtro = new Permisos();
        }

        [BindProperty]
        public List<Permisos> Lista { get; set; } = new List<Permisos>();

        [BindProperty]
        public Permisos? Filtro { get; set; }

        [BindProperty]
        public Permisos? Actual { get; set; }

        [BindProperty]
        public bool Formulario { get; set; } = false;

        [BindProperty]
        public bool Borrar { get; set; } = false;

        public List<Roles> Roles { get; set; } = new List<Roles>();

         
        private bool ValidarAdministrador()
        {
            var usuario = HttpContext.Session.GetString("Usuario");
            var rolId = HttpContext.Session.GetString("RolId");

            return !string.IsNullOrEmpty(usuario) && rolId == "1";
        }

         
        private void CargarRoles()
        {
            Roles = this.IRolesPresentacion
                .Consultar()
                .Where(x => x.Estado)
                .ToList();
        }

         
        public void OnGet()
        {
            if (!ValidarAdministrador())
            {
                HttpContext.Response.Redirect("/");
                return;
            }

            CargarRoles();
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
                CargarRoles();
                this.Filtro ??= new Permisos();
                this.Filtro.Nombre_Permiso ??= "";

                Lista = this.IPermisosPresentacion
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
                CargarRoles();
                this.Actual = new Permisos();
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
                    throw new Exception("No existe información del permiso");

                if (this.Actual.Id == 0)
                    this.Actual = this.IPermisosPresentacion.Insertar(this.Actual);
                else
                    this.Actual = this.IPermisosPresentacion.Actualizar(this.Actual);
            }
            catch (Exception ex)
            {
                CargarRoles();
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

                if (!int.TryParse(data, out int idPermiso))
                    throw new Exception("El permiso seleccionado no es válido");

                Actual = this.Lista.FirstOrDefault(x => x.Id == idPermiso);

                if (Actual == null)
                    throw new Exception("No se encontró el permiso");

                CargarRoles();
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

                if (!int.TryParse(data, out int idPermiso))
                    throw new Exception("El permiso seleccionado no es válido");

                Actual = this.Lista.FirstOrDefault(x => x.Id == idPermiso);

                if (Actual == null)
                    throw new Exception("No se encontró el permiso");

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
                    throw new Exception("No se encontró el permiso");

                this.Actual = this.IPermisosPresentacion.Borrar(this.Actual);
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
