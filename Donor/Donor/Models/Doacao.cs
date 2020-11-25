using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Donor.Models
{
    [Table("Doacao")]
    public class Doacao
    {
        [Key]
        public int IdDoacao { get; set; }
        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Column(TypeName = "date")]
        public DateTime Data { get; set; }

        public int IdPontoDoacao { get; set; }

        public int IdTipoDoacao { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual TipoDoacao TipoDoacao { get; set; }

        public bool Excluido{ get; set; }
    }
}
