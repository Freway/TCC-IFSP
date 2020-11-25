using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Donor.Models
{
    [Table("Usuario")]
    public sealed class Usuario
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            ComprimentoCabelo = new HashSet<ComprimentoCabelo>();
            Doacao = new HashSet<Doacao>();
            NivelNotificacaoUsuario = new HashSet<NivelNotificacaoUsuario>();
            Notificacao = new HashSet<Notificacao>();
            Municipios = new HashSet<Municipio>();
        }

        [Key]
        public int IdUsuario { get; set; }

        [Required]
        [StringLength(150)]
        public string Nome { get; set; }

        [Required]
        [StringLength(1)]
        public string Sexo { get; set; }

        [Required]
        [StringLength(40)]
        public string Email { get; set; }

        [Required]
        [StringLength(3)]
        [Display(Name = "Tipo Sanguíneo")]
        public string TipoSanguineo { get; set; }


        [StringLength(15)]        
        public string Telefone { get; set; }

        [Display(Name = "Município")]
       public int IdMunicipio { get; set; }

        public int IdTipoUsuario { get; set; }

        [DefaultValue(false)]
        public bool ContaInativa { get; set; }

        
        [Display(Name = "Doador de medula?")]
        public bool DoadorMedula { get; set; }

        [StringLength(255)]
        public string Token { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<ComprimentoCabelo> ComprimentoCabelo { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Doacao> Doacao { get; set; }

        public Municipio Municipio { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<NivelNotificacaoUsuario> NivelNotificacaoUsuario { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Notificacao> Notificacao { get; set; }

        public TipoUsuario TipoUsuario { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Municipio> Municipios { get; set; }
    }
}
