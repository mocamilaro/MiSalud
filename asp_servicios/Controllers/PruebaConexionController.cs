using lib_repositorios.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace asp_servicios.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PruebaConexionController : ControllerBase
    {
        private readonly IPruebaConexionRepositorio _repositorio;

        public PruebaConexionController(IPruebaConexionRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet]
        public async Task<IActionResult> Probar()
        {
            var resultado = await _repositorio.ProbarConexionAsync();
            return Ok(new { conexionExitosa = resultado });
        }
    }
}