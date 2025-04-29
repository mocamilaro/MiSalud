using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_entidades.Modelos
{
    [Table("medico")]
    public class Medico : Persona
    {
        [Required]
        [Column("especialidad")]
        [StringLength(100)]
        public string Especialidad { get; set; } = string.Empty;

        // Relación 1-a-muchos
        public ICollection<Cita> Citas { get; set; }
        public ICollection<Diagnostico> Diagnosticos { get; set; }

       

    }
}
