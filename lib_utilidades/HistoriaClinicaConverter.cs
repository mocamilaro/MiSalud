using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using lib_entidades.Modelos;

public class HistoriaClinicaConverter : JsonConverter<HistoriaClinica>
{
    public override void WriteJson(JsonWriter writer, HistoriaClinica? value, JsonSerializer serializer)
    {
        var obj = new JObject
        {
            ["IdHistoria"] = value!.IdHistoria,
            ["PacienteId"] = value.PacienteId,
            ["Email"] = value.Email,
            ["FechaCreacion"] = value.FechaCreacion,
            ["Diagnosticos"] = JArray.FromObject(value.Diagnosticos ?? new List<Diagnostico>(), serializer),
            ["Tratamientos"] = JArray.FromObject(value.Tratamientos ?? new List<Tratamiento>(), serializer),
            ["Formulas"] = JArray.FromObject(value.Formulas ?? new List<Formula>(), serializer),
            // Nota: Si quieres incluir info del paciente sin bucle, puedes controlar manualmente qué incluir aquí
        };

        obj.WriteTo(writer);
    }

    public override HistoriaClinica? ReadJson(JsonReader reader, Type objectType, HistoriaClinica? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        // Si no necesitas deserializar desde JSON usando este convertidor, puedes devolver null o usar JObject:
        return JObject.Load(reader).ToObject<HistoriaClinica>(serializer);
    }
}
