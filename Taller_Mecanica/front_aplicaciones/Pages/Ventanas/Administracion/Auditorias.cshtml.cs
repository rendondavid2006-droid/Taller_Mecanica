 
 

using ClosedXML.Excel;
using libreria_aplicaciones.Entidades;
using presentacion_aplicaciones.Interfaces;
using libreria_aplicaciones.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace front_aplicaciones.Pages.Ventanas.Administracion
{
    public class AuditoriasModel : PageModel
    {
        private IAuditoriasPresentacion
            IAuditoriasPresentacion;


        public AuditoriasModel(
            IAuditoriasPresentacion IAuditoriasPresentacion)
        {
            this.IAuditoriasPresentacion =
                IAuditoriasPresentacion;

            this.Filtro =
                new Auditorias();
        }


        [BindProperty]
        public List<Auditorias> Lista { get; set; }
            = new List<Auditorias>();


        [BindProperty]
        public Auditorias? Filtro { get; set; }


         
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
                    new Auditorias();


                this.Filtro.Tabla ??=
                    "";


                Lista =
                    this.IAuditoriasPresentacion
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


         
        public IActionResult OnPostBtDescargarJson()
        {
            if (!ValidarAdministrador())
            {
                return new RedirectResult("/");
            }


            try
            {
                this.Filtro ??=
                    new Auditorias();


                this.Filtro.Tabla ??=
                    "";


                Lista =
                    this.IAuditoriasPresentacion
                        .Buscar(
                            this.Filtro);


                var opciones =
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    };


                string json =
                    JsonSerializer.Serialize(
                        Lista,
                        opciones);


                byte[] archivo =
                    Encoding.UTF8.GetBytes(
                        json);


                return File(
                    archivo,
                    "application/json",
                    "Auditorias.json");
            }
            catch (Exception ex)
            {
                LogConversor.Log(
                    ex,
                    ViewData!);


                OnPostBtBuscar();


                return Page();
            }
        }


         
        public IActionResult OnPostBtDescargarExcel()
        {
            if (!ValidarAdministrador())
            {
                return new RedirectResult("/");
            }


            try
            {
                this.Filtro ??=
                    new Auditorias();


                this.Filtro.Tabla ??=
                    "";


                Lista =
                    this.IAuditoriasPresentacion
                        .Buscar(
                            this.Filtro);


                using var workbook =
                    new XLWorkbook();


                var worksheet =
                    workbook.Worksheets
                        .Add(
                            "Auditorias");


                worksheet.Cell("A1")
                    .Value =
                    "Taller Mecánica - Reporte de Auditorías";


                worksheet.Range(
                        "A1:F1")
                    .Merge();


                worksheet.Cell("A1")
                    .Style.Font.Bold =
                    true;


                worksheet.Cell("A1")
                    .Style.Font.FontSize =
                    16;


                worksheet.Cell("A1")
                    .Style.Font.FontColor =
                    XLColor.White;


                worksheet.Cell("A1")
                    .Style.Fill.BackgroundColor =
                    XLColor.FromHtml(
                        "#435EBE");


                worksheet.Cell("A1")
                    .Style.Alignment.Horizontal =
                    XLAlignmentHorizontalValues.Center;


                worksheet.Cell("A2")
                    .Value =
                    "Fecha de generación:";


                worksheet.Cell("B2")
                    .Value =
                    DateTime.Now;


                worksheet.Cell("B2")
                    .Style.DateFormat.Format =
                    "yyyy-MM-dd HH:mm:ss";


                worksheet.Cell("A4")
                    .Value =
                    "Fecha";


                worksheet.Cell("B4")
                    .Value =
                    "Tabla";


                worksheet.Cell("C4")
                    .Value =
                    "Registro";


                worksheet.Cell("D4")
                    .Value =
                    "Acción";


                worksheet.Cell("E4")
                    .Value =
                    "Descripción";


                worksheet.Cell("F4")
                    .Value =
                    "Usuario";


                var encabezados =
                    worksheet.Range(
                        "A4:F4");


                encabezados.Style.Font.Bold =
                    true;


                encabezados.Style.Font.FontColor =
                    XLColor.White;


                encabezados.Style.Fill.BackgroundColor =
                    XLColor.FromHtml(
                        "#6575E8");


                encabezados.Style.Alignment.Horizontal =
                    XLAlignmentHorizontalValues.Center;


                int fila =
                    5;


                foreach (var auditoria in Lista)
                {
                    worksheet.Cell(
                            fila,
                            1)
                        .Value =
                        auditoria.Fecha;


                    worksheet.Cell(
                            fila,
                            1)
                        .Style.DateFormat.Format =
                        "yyyy-MM-dd HH:mm:ss";


                    worksheet.Cell(
                            fila,
                            2)
                        .Value =
                        auditoria.Tabla ?? "";


                    worksheet.Cell(
                            fila,
                            3)
                        .Value =
                        auditoria.RegistroId;


                    worksheet.Cell(
                            fila,
                            4)
                        .Value =
                        auditoria.Accion ?? "";


                    worksheet.Cell(
                            fila,
                            5)
                        .Value =
                        auditoria.Descripcion ?? "";


                    worksheet.Cell(
                            fila,
                            6)
                        .Value =
                        auditoria.Usuario ?? "";


                    fila++;
                }


                if (Lista.Count > 0)
                {
                    var rangoDatos =
                        worksheet.Range(
                            4,
                            1,
                            fila - 1,
                            6);


                    rangoDatos.Style.Border
                        .OutsideBorder =
                        XLBorderStyleValues.Thin;


                    rangoDatos.Style.Border
                        .InsideBorder =
                        XLBorderStyleValues.Thin;


                    rangoDatos.Style.Border
                        .OutsideBorderColor =
                        XLColor.LightGray;


                    rangoDatos.Style.Border
                        .InsideBorderColor =
                        XLColor.LightGray;
                }


                worksheet.Column(1)
                    .Width =
                    21;


                worksheet.Column(2)
                    .Width =
                    25;


                worksheet.Column(3)
                    .Width =
                    12;


                worksheet.Column(4)
                    .Width =
                    15;


                worksheet.Column(5)
                    .Width =
                    55;


                worksheet.Column(6)
                    .Width =
                    20;


                worksheet.Column(5)
                    .Style.Alignment.WrapText =
                    true;


                worksheet.SheetView
                    .FreezeRows(
                        4);


                if (Lista.Count > 0)
                {
                    worksheet.Range(
                            4,
                            1,
                            fila - 1,
                            6)
                        .SetAutoFilter();
                }


                using var stream =
                    new MemoryStream();


                workbook.SaveAs(
                    stream);


                byte[] archivo =
                    stream.ToArray();


                return File(
                    archivo,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    $"Auditorias_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
            }
            catch (Exception ex)
            {
                LogConversor.Log(
                    ex,
                    ViewData!);


                OnPostBtBuscar();


                return Page();
            }
        }
    }
}