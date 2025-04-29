using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_entidades.Modelos
{
    [Table("medicamento")]
    public class Medicamento
    {
        [Key]
        [Column("idmedicamento")]
        public int IdMedicamento { get; set; }

        [Required]
        [Column("nombre")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [Column("dosis")]
        [StringLength(50)]
        public string Dosis { get; set; } = string.Empty;

        [Required]
        [Column("frecuencia")]
        [StringLength(100)]
        public string Frecuencia { get; set; } = string.Empty;

        // Relación
        [ForeignKey("Formula")]
        [Column("formula_id")]
        public int FormulaId { get; set; }
        public Formula Formula { get; set; }

        public bool Validar()
        {
            return true; //Aqui se implementan las validaciones (esto falta)
        }


    }
}
