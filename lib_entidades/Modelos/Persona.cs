using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_entidades.Modelos
{
    [Table("persona")]
    public class Persona
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("cedula")]
        [StringLength(20)]
        public string Cedula { get; set; } = string.Empty;

        [Required]
        [Column("nombre")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Column("direccion")]
        public string Direccion { get; set; } = string.Empty;

        [Column("telefono")]
        [StringLength(20)]
        public string Telefono { get; set; } = string.Empty;

        [Required]
        [Column("email")]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        public bool Validar()
        {
            return true; //Aqui se implementan las validaciones (esto falta)
        }

    }
}

// = string.Empty; lo uso para inicializar las variables pero se puede
// quitar y ponerle el ? despues del nombre del campo
