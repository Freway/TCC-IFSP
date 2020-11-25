using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Donor.Models
{
    [Table("TipoDoacao")]
    public sealed class TipoDoacao
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoDoacao()
        {
            DivulgacaoDoacao = new HashSet<DivulgacaoDoacao>();
            Doacao = new HashSet<Doacao>();
            NivelNotificacaoUsuario = new HashSet<NivelNotificacaoUsuario>();
            PontoDeDoacao = new HashSet<PontoDeDoacao>();
        }

        [Key]
        public int IdTipoDoacao { get; set; }

        [Column("TipoDoacao")]
        [Required]
        [StringLength(30)]
        public string TipoDoacao1 { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Descricao { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<DivulgacaoDoacao> DivulgacaoDoacao { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Doacao> Doacao { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<NivelNotificacaoUsuario> NivelNotificacaoUsuario { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<PontoDeDoacao> PontoDeDoacao { get; set; }
    }
}
