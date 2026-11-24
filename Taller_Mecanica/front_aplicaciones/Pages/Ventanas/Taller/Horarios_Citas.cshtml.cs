 
 

using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;
using libreria_aplicaciones.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_aplicaciones.Pages.Ventanas.Taller
{
    public class Horarios_CitasModel : PageModel
    {
        private IHorarios_CitasPresentacion
            IHorarios_CitasPresentacion;


        public Horarios_CitasModel(
            IHorarios_CitasPresentacion IHorarios_CitasPresentacion)
        {
            this.IHorarios_CitasPresentacion =
                IHorarios_CitasPresentacion;

            this.Filtro =
                new Horarios_Citas();
        }


        [BindProperty]
        public List<Horarios_Citas> Lista { get; set; }
            = new List<Horarios_Citas>();


        [BindProperty]
        public Horarios_Citas? Filtro { get; set; }


        [BindProperty]
        public Horarios_Citas? Actual { get; set; }


        [BindProperty]
        public string? Hora_Inicio_Texto { get; set; }


        [BindProperty]
        public string? Hora_Fin_Texto { get; set; }


        [BindProperty]
        public bool Formulario { get; set; }
            = false;


        [BindProperty]
        public bool Borrar { get; set; }
            = false;



         
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



         
        public void OnGet()
        {
            if (!ValidarAdministrador())
            {
                HttpContext.Response.Redirect("/");
                return;
            }


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
                    new Horarios_Citas();


                Lista =
                    this.IHorarios_CitasPresentacion
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
                this.Actual =
                    new Horarios_Citas
                    {
                        Fecha =
                            DateTime.Today,

                        Disponible =
                            true
                    };


                Hora_Inicio_Texto =
                    "";


                Hora_Fin_Texto =
                    "";


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
                if (this.Actual == null)
                {
                    throw new Exception(
                        "No existe información del horario");
                }


                if (!TimeSpan.TryParse(
                    Hora_Inicio_Texto,
                    out TimeSpan horaInicio))
                {
                    throw new Exception(
                        "Hora de inicio invalida");
                }


                if (!TimeSpan.TryParse(
                    Hora_Fin_Texto,
                    out TimeSpan horaFin))
                {
                    throw new Exception(
                        "Hora de fin invalida");
                }


                if (horaFin <= horaInicio)
                {
                    throw new Exception(
                        "La hora final debe ser mayor a la hora inicial");
                }


                DateTime fechaBase =
                    new DateTime(
                        1900,
                        1,
                        1);


                this.Actual.Hora_Inicio =
                    fechaBase.Add(
                        horaInicio);


                this.Actual.Hora_Fin =
                    fechaBase.Add(
                        horaFin);


                if (this.Actual.Id == 0)
                {
                    this.Actual =
                        this.IHorarios_CitasPresentacion
                            .Insertar(
                                this.Actual);
                }
                else
                {
                    this.Actual =
                        this.IHorarios_CitasPresentacion
                            .Actualizar(
                                this.Actual);
                }
            }
            catch (Exception ex)
            {
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
                    out int idHorario))
                {
                    throw new Exception(
                        "El horario seleccionado no es válido");
                }


                Actual =
                    this.Lista
                        .FirstOrDefault(
                            x =>
                                x.Id ==
                                idHorario);


                if (Actual == null)
                {
                    throw new Exception(
                        "No se encontró el horario");
                }


                Hora_Inicio_Texto =
                    Actual.Hora_Inicio
                        .ToString("HH:mm");


                Hora_Fin_Texto =
                    Actual.Hora_Fin
                        .ToString("HH:mm");


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
                    out int idHorario))
                {
                    throw new Exception(
                        "El horario seleccionado no es válido");
                }


                Actual =
                    this.Lista
                        .FirstOrDefault(
                            x =>
                                x.Id ==
                                idHorario);


                if (Actual == null)
                {
                    throw new Exception(
                        "No se encontró el horario");
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
                        "No se encontró el horario");
                }


                this.Actual =
                    this.IHorarios_CitasPresentacion
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