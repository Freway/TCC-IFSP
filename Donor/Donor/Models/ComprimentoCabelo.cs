using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Donor.Models
{
    [Table("ComprimentoCabelo")]
    public class ComprimentoCabelo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdComprimentoCabelo { get; set; }
        [Display(Name = "Comprimento (cm)")]
        public double Comprimento { get; set; }

        [Display(Name = "Data da Medição")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Column(TypeName = "date")]
        public DateTime DataRegistro { get; set; }

        public int IdUsuario { get; set; }

        public virtual Usuario Usuario { get; set; }

        [Display(Name = "Estimativa para doação")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Column(TypeName = "date")]
        public DateTime DataDoacaoCabelo { get; set; }
    }
}
