using asp_servicios.Nucleo;
using lib_aplicaciones.Interfaces;
using lib_entidades.Modelos;
using lib_repositorios;
using lib_utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace asp_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HistoriaClinicaController : ControllerBase
    {
        private IHistoriaClinicaAplicacion? iAplicacion = null;
        private TokenController? tokenController = null;

        public HistoriaClinicaController(IHistoriaClinicaAplicacion? iAplicacion,
            TokenController tokenController)
        {
            this.iAplicacion = iAplicacion;
            this.tokenController = tokenController;
        }

        private Dictionary<string, object> ObtenerDatos()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                var datos = new StreamReader(Request.Body).ReadToEnd().ToString();
                if (string.IsNullOrEmpty(datos))
                    datos = "{}";
                return JsonConversor.ConvertirAObjeto(datos);
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.Message.ToString();
                return respuesta;
            }
        }

        [HttpPost]
        public string Listar()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                var datos = ObtenerDatos();
                /*if (!tokenController!.Validate(datos))
                {
                    respuesta["Error"] = "lbNoAutenticacion";
                    return JsonConversor.ConvertirAStringHC(respuesta);
                }*/

                this.iAplicacion!.Configurar(Configuracion.ObtenerValor("ConnectionStrings:DefaultConnection"));
                respuesta["Entidades"] = this.iAplicacion!.Listar();

                respuesta["Respuesta"] = "OK";
                respuesta["Fecha"] = DateTime.Now.ToString();
                return JsonConversor.ConvertirAStringHC(respuesta);
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.Message.ToString();
                return JsonConversor.ConvertirAStringHC(respuesta);
            }
        }

        [HttpPost]
        public string Buscar()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                var datos = ObtenerDatos();
                /*if (!tokenController!.Validate(datos))
                {
                    respuesta["Error"] = "lbNoAutenticacion";
                    return JsonConversor.ConvertirAStringHC(respuesta);
                }*/

                // Convertir la entidad a objeto de HistoriaClinica
                var entidad = JsonConversor.ConvertirAObjeto<HistoriaClinica>(
                    JsonConversor.ConvertirAStringHC(datos["Entidad"]));

                // Verificar si FechaCreacion es de tipo DateTimeOffset y si no está en UTC
                if (entidad.FechaCreacion.Offset != TimeSpan.Zero)
                {
                    // Convertir la fecha a UTC si no está en UTC
                    entidad.FechaCreacion = entidad.FechaCreacion.ToUniversalTime();
                }

                var tipo = datos["Tipo"].ToString();

                this.iAplicacion!.Configurar(Configuracion.ObtenerValor("ConnectionStrings:DefaultConnection"));
                respuesta["Entidades"] = this.iAplicacion!.Buscar(entidad, tipo);

                respuesta["Respuesta"] = "OK";
                respuesta["Fecha"] = DateTime.Now.ToString();
                return JsonConversor.ConvertirAStringHC(respuesta);
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.Message.ToString();
                return JsonConversor.ConvertirAStringHC(respuesta);
            }
        }

        [HttpPost]
        public string Guardar()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                var datos = ObtenerDatos();
                if (!tokenController!.Validate(datos))
                {
                    respuesta["Error"] = "lbNoAutenticacion";
                    return JsonConversor.ConvertirAStringHC(respuesta);
                }

                var entidad = JsonConversor.ConvertirAObjeto<HistoriaClinica>(
                    JsonConversor.ConvertirAStringHC(datos["Entidad"]));

                this.iAplicacion!.Configurar(Configuracion.ObtenerValor("ConnectionStrings:DefaultConnection"));
                entidad = this.iAplicacion!.Guardar(entidad);

                respuesta["Entidad"] = entidad;
                respuesta["Respuesta"] = "OK";
                respuesta["Fecha"] = DateTime.Now.ToString();
                return JsonConversor.ConvertirAStringHC(respuesta);
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.Message.ToString();
                return JsonConversor.ConvertirAStringHC(respuesta);
            }
        }

        [HttpPost]
        public string Modificar()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                var datos = ObtenerDatos();
                if (!tokenController!.Validate(datos))
                {
                    respuesta["Error"] = "lbNoAutenticacion";
                    return JsonConversor.ConvertirAStringHC(respuesta);
                }

                var entidad = JsonConversor.ConvertirAObjeto<HistoriaClinica>(
                    JsonConversor.ConvertirAStringHC(datos["Entidad"]));

                this.iAplicacion!.Configurar(Configuracion.ObtenerValor("ConnectionStrings:DefaultConnection"));
                entidad = this.iAplicacion!.Modificar(entidad);

                respuesta["Entidad"] = entidad;
                respuesta["Respuesta"] = "OK";
                respuesta["Fecha"] = DateTime.Now.ToString();
                return JsonConversor.ConvertirAStringHC(respuesta);
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.Message.ToString();
                return JsonConversor.ConvertirAStringHC(respuesta);
            }
        }

        [HttpPost]
        public string Borrar()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                var datos = ObtenerDatos();
                if (!tokenController!.Validate(datos))
                {
                    respuesta["Error"] = "lbNoAutenticacion";
                    return JsonConversor.ConvertirAStringHC(respuesta);
                }

                var entidad = JsonConversor.ConvertirAObjeto<HistoriaClinica>(
                    JsonConversor.ConvertirAStringHC(datos["Entidad"]));

                this.iAplicacion!.Configurar(Configuracion.ObtenerValor("ConnectionStrings:DefaultConnection"));
                entidad = this.iAplicacion!.Borrar(entidad);

                respuesta["Entidad"] = entidad;
                respuesta["Respuesta"] = "OK";
                respuesta["Fecha"] = DateTime.Now.ToString();
                return JsonConversor.ConvertirAStringHC(respuesta);
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.Message.ToString();
                return JsonConversor.ConvertirAStringHC(respuesta);
            }
        }

    }
}