using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_entidades.Modelos
{
    [Table("recomendacion")]
    public class Recomendacion
    {
        [Key]
        [Column("idrecomendacion")]
        public int IdRecomendacion { get; set; }

        [Required]
        [Column("tipo")]
        [StringLength(50)]
        public string Tipo { get; set; } = string.Empty;

        [Column("descripcion")]
        public string Descripcion { get; set; } = string.Empty;

        [Column("fechaemision")]
        public DateTime FechaEmision { get; set; }

        // Relación
        [ForeignKey("Paciente")]
        [Column("paciente_id")]
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }

        public bool Validar()
        {
            return true; //Aqui se implementan las validaciones (esto falta)
        }


    }
}
