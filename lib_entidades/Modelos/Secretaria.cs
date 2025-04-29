using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_entidades.Modelos
{
    [Table("secretaria")]
    public class Secretaria : Persona
    {
        // Relación 1-a-muchos
        public ICollection<Cita> CitasPendientes { get; set; }

        

    }
}
