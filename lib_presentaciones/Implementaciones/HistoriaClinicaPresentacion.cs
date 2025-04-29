using lib_comunicaciones.Interfaces;
using lib_entidades.Modelos;
using lib_presentaciones.Interfaces;
using lib_utilidades;

namespace lib_presentaciones.Implementaciones
{
    public class HistoriaClinicaPresentacion : IHistoriaClinicaPresentacion
    {
        private IHistoriaClinicaComunicacion? iComunicacion = null;

        public HistoriaClinicaPresentacion(IHistoriaClinicaComunicacion iComunicacion)
        {
            this.iComunicacion = iComunicacion;
        }

        public async Task<List<HistoriaClinica>> Listar()
        {
            var lista = new List<HistoriaClinica>();
            var datos = new Dictionary<string, object>();

            var respuesta = await iComunicacion!.Listar(datos);
            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            lista = JsonConversor.ConvertirAObjeto<List<HistoriaClinica>>(
                JsonConversor.ConvertirAStringHC(respuesta["Entidades"]));
            return lista;
        }

        public async Task<List<HistoriaClinica>> Buscar(HistoriaClinica entidad, string tipo)
        {
            var lista = new List<HistoriaClinica>();
            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;
            datos["Tipo"] = tipo;

            var respuesta = await iComunicacion!.Buscar(datos);
            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            lista = JsonConversor.ConvertirAObjeto<List<HistoriaClinica>>(
                JsonConversor.ConvertirAStringHC(respuesta["Entidades"]));
            return lista;
        }

        public async Task<HistoriaClinica> Guardar(HistoriaClinica entidad)
        {
            if (entidad.IdHistoria != 0 || !entidad.Validar())
            {
                throw new Exception("lbFaltaInformacion");
            }

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            var respuesta = await iComunicacion!.Guardar(datos);
            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<HistoriaClinica>(
                JsonConversor.ConvertirAStringHC(respuesta["Entidad"]));
            return entidad;
        }

        public async Task<HistoriaClinica> Modificar(HistoriaClinica entidad)
        {
            if (entidad.IdHistoria == 0 || !entidad.Validar())
            {
                throw new Exception("lbFaltaInformacion");
            }

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            var respuesta = await iComunicacion!.Modificar(datos);
            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<HistoriaClinica>(
                JsonConversor.ConvertirAStringHC(respuesta["Entidad"]));
            return entidad;
        }

        public async Task<HistoriaClinica> Borrar(HistoriaClinica entidad)
        {
            if (entidad.IdHistoria == 0 || !entidad.Validar())
            {
                throw new Exception("lbFaltaInformacion");
            }

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            var respuesta = await iComunicacion!.Borrar(datos);
            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<HistoriaClinica>(
                JsonConversor.ConvertirAStringHC(respuesta["Entidad"]));
            return entidad;
        }
    }
}