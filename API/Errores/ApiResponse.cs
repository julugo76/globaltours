using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errores
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string mensaje=null)
        {
            StatusCode = statusCode;
            Mensaje = mensaje ?? MenajeStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Mensaje { get; set; }

        private string MenajeStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Un bad request se ha realizado",
                401 => "No estas autorizado",
                404 => "Recurso no encontrado",
                500 => "Error inyterno comunicarse con el Administrador",
                _ => null
            };
        }
    }
}