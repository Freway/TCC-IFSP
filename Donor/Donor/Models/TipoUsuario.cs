using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Donor.Models
{
    [Table("TipoUsuario")]
    public sealed class TipoUsuario
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoUsuario()
        {
            Usuario = new HashSet<Usuario>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdTipoUsuario { get; set; }

        [Column("TipoUsuario")]
        [Required]
        [StringLength(30)]
        public string TipoUsuario1 { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Descricao { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Usuario> Usuario { get; set; }
    }
}
