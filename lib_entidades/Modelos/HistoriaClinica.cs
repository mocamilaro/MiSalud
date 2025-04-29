using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace lib_entidades.Modelos
{
    [Table("historia_clinica")]
    public class HistoriaClinica
    {
        [Key]
        [Column("idhistoria")]
        public int IdHistoria { get; set; }

        [ForeignKey("Paciente")]
        [Column("paciente_id")]
        [JsonIgnore]
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; } 

        [Column("email")]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Column("fecha_creacion")]
        public DateTimeOffset FechaCreacion { get; set; }

        // Relaciones 1-a-muchos
        [JsonIgnore]
        public ICollection<Diagnostico> Diagnosticos { get; set; } = new List<Diagnostico>();
        [JsonIgnore]
        public ICollection<Tratamiento> Tratamientos { get; set; } = new List<Tratamiento>();
        [JsonIgnore]
        public ICollection<Formula> Formulas { get; set; } = new List<Formula>();

        public bool Validar()
        {
            return true; //Aqui se implementan las validaciones (esto falta)
        }

    }
}
