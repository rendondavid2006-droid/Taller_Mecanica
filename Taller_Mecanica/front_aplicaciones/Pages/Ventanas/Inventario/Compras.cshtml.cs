 
 

using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;
using libreria_aplicaciones.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_aplicaciones.Pages.Ventanas.Inventario
{
    public class ComprasModel : PageModel
    {
        private IComprasPresentacion IComprasPresentacion;
        private IProveedoresPresentacion IProveedoresPresentacion;

        public ComprasModel(
            IComprasPresentacion IComprasPresentacion,
            IProveedoresPresentacion IProveedoresPresentacion)
        {
            this.IComprasPresentacion = IComprasPresentacion;
            this.IProveedoresPresentacion = IProveedoresPresentacion;
            this.Filtro = new Compras();
        }

        [BindProperty]
        public List<Compras> Lista { get; set; } = new List<Compras>();

        [BindProperty]
        public Compras? Filtro { get; set; }

        [BindProperty]
        public Compras? Actual { get; set; }

        [BindProperty]
        public bool Formulario { get; set; } = false;

        [BindProperty]
        public bool Borrar { get; set; } = false;

        public List<Proveedores> Proveedores { get; set; } = new List<Proveedores>();

         
        private bool ValidarAdministrador()
        {
            var usuario = HttpContext.Session.GetString("Usuario");
            var rolId = HttpContext.Session.GetString("RolId");

            return !string.IsNullOrEmpty(usuario) && rolId == "1";
        }

         
        private void CargarProveedores()
        {
            Proveedores = this.IProveedoresPresentacion
                .Consultar();
        }

         
        public void OnGet()
        {
            if (!ValidarAdministrador())
            {
                HttpContext.Response.Redirect("/");
                return;
            }

            CargarProveedores();
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
                CargarProveedores();

                this.Filtro ??= new Compras();
                this.Filtro.Numero_Factura_Proveedor ??= "";

                Lista = this.IComprasPresentacion
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
                CargarProveedores();
                this.Actual = new Compras();
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
                    throw new Exception("No existe información de la compra");

                if (this.Actual.Id == 0)
                    this.Actual = this.IComprasPresentacion.Insertar(this.Actual);
                else
                    this.Actual = this.IComprasPresentacion.Actualizar(this.Actual);
            }
            catch (Exception ex)
            {
                CargarProveedores();
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

                if (!int.TryParse(data, out int idCompra))
                    throw new Exception("La compra seleccionada no es válida");

                Actual = this.Lista.FirstOrDefault(x => x.Id == idCompra);

                if (Actual == null)
                    throw new Exception("No se encontró la compra");

                CargarProveedores();
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

                if (!int.TryParse(data, out int idCompra))
                    throw new Exception("La compra seleccionada no es válida");

                Actual = this.Lista.FirstOrDefault(x => x.Id == idCompra);

                if (Actual == null)
                    throw new Exception("No se encontró la compra");

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
                    throw new Exception("No se encontró la compra");

                this.Actual = this.IComprasPresentacion
                    .Borrar(this.Actual);
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
