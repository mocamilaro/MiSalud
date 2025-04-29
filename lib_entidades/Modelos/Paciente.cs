using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace lib_entidades.Modelos
{
    [Table("paciente")]
    public class Paciente : Persona
    {
        
        [Column("eps")]
        public string eps { get; set; }

        [Column("acudiente")]
        public string Acudiente { get; set; }

        [Column("tipo_sang")]
        public string TipoSang { get; set; }

        // Relación 1-a-1 con HistoriaClinica
        [JsonIgnore] public HistoriaClinica HistoriaClinica { get; set; }

        // Relaciones 1-a-muchos
        public ICollection<Cita> Citas { get; set; } = new List<Cita>();
        public ICollection<Recomendacion> Recomendaciones { get; set; } = new List<Recomendacion>();
        public ICollection<Notificacion> Notificaciones { get; set; } = new List<Notificacion>();
    }
}
