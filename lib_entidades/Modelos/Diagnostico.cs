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
    [Table("diagnostico")]
    public class Diagnostico
    {
        [Key]
        [Column("iddiagnostico")]
        public int IdDiagnostico { get; set; }

        [Required]
        [Column("descripcion")]
        public string Descripcion { get; set; } = string.Empty;

        [Column("fecha")]
        public DateTime Fecha { get; set; }

        [ForeignKey("Medico")]
        [Column("medico_id")]
        public int MedicoId { get; set; }
        public Medico Medico { get; set; }

        [ForeignKey("HistoriaClinica")]
        [Column("historia_id")]
        public int HistoriaId { get; set; }

        
        public HistoriaClinica HistoriaClinica { get; set; }

        public bool Validar()
        {
            return true; //Aqui se implementan las validaciones (esto falta)
        }

    }
}
