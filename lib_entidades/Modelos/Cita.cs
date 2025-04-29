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
    public enum EstadoCita
    {
        Asignada,
        Cancelada,
        Reasignada,
        Completada,
        Pendiente
    }

    [Table("cita")]
    public class Cita
    {
        [Key]
        [Column("idcita")]
        public int IdCita { get; set; }

        [Column("fecha")]
        public DateTime Fecha { get; set; }

        [Column("hora")]
        public TimeSpan Hora { get; set; }

        [Column("estado")]
        public EstadoCita Estado { get; set; }

        [Column("motivo")]
        public string Motivo { get; set; } = string.Empty;

        // Relaciones
        [ForeignKey("Paciente")]
        [Column("paciente_id")]
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }

        [ForeignKey("Medico")]
        [Column("medico_id")]
        public int MedicoId { get; set; }
        public Medico Medico { get; set; }

        [ForeignKey("Secretaria")]
        [Column("secretaria_id")]
        public int? SecretariaId { get; set; }
        public Secretaria Secretaria { get; set; }

        [ForeignKey("HistoriaClinica")]
        [Column("historia_id")]
        public int? HistoriaId { get; set; }
       
        [JsonIgnore]
        public HistoriaClinica HistoriaClinica { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        public bool Validar()
        {
            return true; //Aqui se implementan las validaciones (esto falta)
        }

    }
}
