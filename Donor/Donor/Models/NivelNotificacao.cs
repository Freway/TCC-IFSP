using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Donor.Models
{
    [Table("NivelNotificacao")]
    public sealed class NivelNotificacao
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NivelNotificacao()
        {
            NivelNotificacaoUsuario = new HashSet<NivelNotificacaoUsuario>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdNivelNotificacao { get; set; }

        [Column("NivelNotificacao")]
        public int NivelNotificacao1 { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Descricao { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<NivelNotificacaoUsuario> NivelNotificacaoUsuario { get; set; }
    }
}
