using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Diagnostics.CodeAnalysis;

namespace Donor.Models
{
    [Table("PontoDeDoacao")]
    public sealed class PontoDeDoacao
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PontoDeDoacao()
        {
            RequisitoDoacao = new HashSet<RequisitoDoacao>();
            TipoDoacao = new HashSet<TipoDoacao>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdPontoDeDoacao { get; set; }

        [Required]
        [StringLength(150)]
        public string Nome { get; set; }

        public bool? ContaInativa { get; set; }

        [StringLength(255)]
        public string Token { get; set; }

        public DbGeography Localizacao { get; set; }

        [DisplayName("Municipio")]
        public int IdMunicipio { get; set; }

        [StringLength(120)]
        [DisplayName("Nome do Responsável")]
        public string NomeResponsavel { get; set; }

        [StringLength(16)]
        public string Telefone { get; set; }

        public Municipio Municipio { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<RequisitoDoacao> RequisitoDoacao { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<TipoDoacao> TipoDoacao { get; set; }
    }
}
