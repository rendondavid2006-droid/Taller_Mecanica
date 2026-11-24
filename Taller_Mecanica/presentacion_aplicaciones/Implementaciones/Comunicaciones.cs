 
 

using presentacion_aplicaciones.Interfaces;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace presentacion_aplicaciones.Implementaciones
{
    public class Comunicaciones : IComunicaciones
    {
        private readonly IHttpContextAccessor IHttpContextAccessor;
        private readonly IConfiguration IConfiguration;

        public Comunicaciones(
            IHttpContextAccessor IHttpContextAccessor,
            IConfiguration IConfiguration)
        {
            this.IHttpContextAccessor =
                IHttpContextAccessor;

            this.IConfiguration =
                IConfiguration;
        }

        public async Task<T> Ejecutar<T>(
            string url,
            object entidad)
        {
            string? baseUrl =
                this.IConfiguration[
                    "ApiSettings:BaseUrl"];

            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new Exception(
                    "No se encontró la URL de la API");
            }

            baseUrl =
                baseUrl.TrimEnd('/') + "/";

            url =
                baseUrl +
                url.TrimStart('/');

            var body =
                new StringContent(
                    JsonConvert.SerializeObject(
                        entidad),
                    Encoding.UTF8,
                     
                    "application/json");

            var cliente_http =
                new HttpClient();

            cliente_http.Timeout =
                new TimeSpan(
                    0,
                    4,
                    0);

            var usuario =
                this.IHttpContextAccessor
                    .HttpContext?
                    .Session
                    .GetString(
                        "Usuario");

            if (!string.IsNullOrEmpty(usuario))
            {
                cliente_http
                    .DefaultRequestHeaders
                    .Add(
                        "X-Usuario",
                        usuario);
            }

            var message =
                await cliente_http
                    .PostAsync(
                        url,
                        body);

            if (!message.IsSuccessStatusCode)
            {
                throw new Exception(
                    "Error Comunicacion");
            }

            var respuesta =
                await message.Content
                    .ReadAsStringAsync();

            cliente_http.Dispose();

            respuesta =
                Replace(
                    respuesta);

            return JsonConvert
                .DeserializeObject<T>(
                    respuesta)!;
        }

        private string Replace(
            string resp)
        {
            return resp
                .Replace("\\\\r\\\\n", "")
                .Replace("\\r\\n", "")
                .Replace("\\", "")
                .Replace("\\\"", "\"")
                .Replace("\"", "'")
                .Replace("'[", "[")
                .Replace("]'", "]")
                .Replace("'{'", "{'")
                .Replace("\\\\", "\\")
                .Replace("'}'", "'}")
                .Replace("}'", "}")
                .Replace("\\n", "")
                .Replace("\\r", "")
                .Replace("    ", "")
                .Replace("'{", "{")
                .Replace("\"", "")
                .Replace("  ", "");
        }
    }
}