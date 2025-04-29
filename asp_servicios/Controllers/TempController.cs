using asp_servicios.Nucleo;
using Microsoft.AspNetCore.Mvc;

namespace asp_servicios.Controllers
{
    public class TempController
    {
        [HttpGet("prueba-conexion")]
        public string ProbarConfig()
        {
            try
            {
                string connString = Configuracion.ObtenerValor("ConnectionStrings:DefaultConnection");
                return $"Cadena obtenida: {connString}";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}
