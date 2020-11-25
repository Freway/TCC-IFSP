using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Donor.Models
{
    [Table("DivulgacaoDoacao")]
    public sealed class DivulgacaoDoacao
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DivulgacaoDoacao()
        {
            Notificacao = new HashSet<Notificacao>();
        }

        [Key]
        public int IdDivulgacaoDoacao { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Column(TypeName = "date")]
        public DateTime DataInicio { get; set; }

        [Column(TypeName = "date")]
        public DateTime DataFim { get; set; }

        public int IdTipoDoacao { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Notificacao> Notificacao { get; set; }

        public TipoDoacao TipoDoacao { get; set; }
    }
}
