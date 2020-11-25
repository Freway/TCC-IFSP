using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Donor.Models
{
    [Table("RequisitoDoacao")]
    public class RequisitoDoacao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdRequisitoDoacao { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Descricao { get; set; }

        public int IdPontoDoacao { get; set; }

        public virtual PontoDeDoacao PontoDeDoacao { get; set; }
    }
}
