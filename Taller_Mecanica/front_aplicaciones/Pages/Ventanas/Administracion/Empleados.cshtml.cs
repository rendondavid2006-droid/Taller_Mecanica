 
 

using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;
using libreria_aplicaciones.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_aplicaciones.Pages.Ventanas.Administracion
{
    public class EmpleadosModel : PageModel
    {
        private IEmpleadosPresentacion IEmpleadosPresentacion;
        private IRolesPresentacion IRolesPresentacion;

        public EmpleadosModel(
            IEmpleadosPresentacion IEmpleadosPresentacion,
            IRolesPresentacion IRolesPresentacion)
        {
            this.IEmpleadosPresentacion = IEmpleadosPresentacion;
            this.IRolesPresentacion = IRolesPresentacion;
            this.Filtro = new Empleados();
        }

        [BindProperty]
        public List<Empleados> Lista { get; set; } = new List<Empleados>();

        [BindProperty]
        public Empleados? Filtro { get; set; }

        [BindProperty]
        public Empleados? Actual { get; set; }

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
                this.Filtro ??= new Empleados();
                this.Filtro.Documento ??= "";

                Lista = this.IEmpleadosPresentacion
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
                this.Actual = new Empleados();
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
                    throw new Exception("No existe información del empleado");

                if (this.Actual.Id == 0)
                    this.Actual = this.IEmpleadosPresentacion.Insertar(this.Actual);
                else
                    this.Actual = this.IEmpleadosPresentacion.Actualizar(this.Actual);
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

                if (!int.TryParse(data, out int idEmpleado))
                    throw new Exception("El empleado seleccionado no es válido");

                Actual = this.Lista.FirstOrDefault(x => x.Id == idEmpleado);

                if (Actual == null)
                    throw new Exception("No se encontró el empleado");

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

                if (!int.TryParse(data, out int idEmpleado))
                    throw new Exception("El empleado seleccionado no es válido");

                Actual = this.Lista.FirstOrDefault(x => x.Id == idEmpleado);

                if (Actual == null)
                    throw new Exception("No se encontró el empleado");

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
                    throw new Exception("No se encontró el empleado");

                this.Actual = this.IEmpleadosPresentacion.Borrar(this.Actual);
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
