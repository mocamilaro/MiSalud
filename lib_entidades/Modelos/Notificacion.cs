using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_entidades.Modelos
{
    [Table("notificacion")]
    public class Notificacion
    {
        [Key]
        [Column("idnotificacion")]
        public int IdNotificacion { get; set; }

        [Required]
        [Column("tipo")]
        [StringLength(50)]
        public string Tipo { get; set; } = string.Empty;

        [Required]
        [Column("mensaje")]
        public string Mensaje { get; set; } = string.Empty;

        [Column("fechaenvio")]
        public DateTime FechaEnvio { get; set; }

        [Column("estado")]
        [StringLength(20)]
        public string Estado { get; set; } = string.Empty;  
        // Para ver si esta: Enviada, Leída, Pendiente

        // Relaciones
        [ForeignKey("Paciente")]
        [Column("paciente_id")]
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }

        [ForeignKey("Cita")]
        [Column("cita_id")]
        public int? CitaId { get; set; }
        public Cita Cita { get; set; }

        public bool Validar()
        {
            return true; //Aqui se implementan las validaciones (esto falta)
        }

    }
}
