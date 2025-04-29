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
    [Table("tratamiento")]
    public class Tratamiento
    {
        [Key]
        [Column("idtratamiento")]
        public int IdTratamiento { get; set; }

        [Required]
        [Column("descripcion")]
        public string Descripcion { get; set; } = string.Empty;

        [Column("fecha_inicio")]
        public DateTime FechaInicio { get; set; }

        [Column("fecha_fin")]
        public DateTime? FechaFin { get; set; }

        // Relación
        [ForeignKey("HistoriaClinica")]
        [Column("historia_id")]
        public int HistoriaId { get; set; }
        
        [JsonIgnore]
        public HistoriaClinica HistoriaClinica { get; set; }

        public bool Validar()
        {
            return true; //Aqui se implementan las validaciones (esto falta)
        }

    }
}
