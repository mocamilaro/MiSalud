using lib_utilidades;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace asp_servicios.Nucleo
{
    public class Configuracion
    {
        private static JObject? _config;

        public static string ObtenerValor(string clave)
        {
            if (_config == null)
                Cargar();

            // Maneja claves anidadas como "ConnectionStrings:DefaultConnection"
            var keys = clave.Split(':');
            JToken current = _config!;

            foreach (var key in keys)
            {
                current = current[key] ?? throw new KeyNotFoundException($"Clave '{key}' no encontrada");
            }

            return current.ToString();
        }

        public static void Cargar()
        {
            if (!File.Exists(DatosGenerales.ruta_json))
                throw new FileNotFoundException($"Archivo no encontrado: {DatosGenerales.ruta_json}");

            string json = File.ReadAllText(DatosGenerales.ruta_json);
            _config = JObject.Parse(json);
        }
    }
}