using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Donor.Models
{
    [Table("Municipio")]
    public sealed class Municipio
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Municipio()
        {
            Usuario = new HashSet<Usuario>();
            PontoDeDoacao = new HashSet<PontoDeDoacao>();
            Usuario1 = new HashSet<Usuario>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdMunicipio { get; set; }

        [Column("Municipio")]
        [Required]
        [StringLength(50)]
        public string Municipio1 { get; set; }

        [Required]
        [StringLength(2)]
        public string Estado { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Usuario> Usuario { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<PontoDeDoacao> PontoDeDoacao { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Usuario> Usuario1 { get; set; }
    }
}
