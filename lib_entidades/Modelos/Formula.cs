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
    [Table("formula")]
    public class Formula
    {
        [Key]
        [Column("idformula")]
        public int IdFormula { get; set; }

        [Column("fechacreacion")]
        public DateTime FechaCreacion { get; set; }

        // Relaciones
        [ForeignKey("Paciente")]
        [Column("paciente_id")]
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }

        [ForeignKey("HistoriaClinica")]
        [Column("historia_id")]
        public int HistoriaId { get; set; }

        [JsonIgnore]
        public HistoriaClinica HistoriaClinica { get; set; }

        // Relación 1-a-muchos
        public ICollection<Medicamento> Medicamentos { get; set; }

        public bool Validar()
        {
            return true; //Aqui se implementan las validaciones (esto falta)
        }

    }
}
