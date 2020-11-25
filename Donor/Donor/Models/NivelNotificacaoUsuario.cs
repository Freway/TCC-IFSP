using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Donor.Models
{
    [Table("NivelNotificacaoUsuario")]
    public sealed class NivelNotificacaoUsuario
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NivelNotificacaoUsuario()
        {
            TipoDoacao = new HashSet<TipoDoacao>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdNivelNotificacaoUsuario { get; set; }

        public int IdNivelNotificacao { get; set; }

        public int IdUsuario { get; set; }

        public NivelNotificacao NivelNotificacao { get; set; }

        public Usuario Usuario { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<TipoDoacao> TipoDoacao { get; set; }
    }
}
