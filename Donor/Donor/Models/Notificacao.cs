using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Donor.Models
{
    [Table("Notificacao")]
    public class Notificacao
    {
        [Key]
        public int IdNotificacao { get; set; }

        public DateTime Envio { get; set; }

        public DateTime? Confirmacao { get; set; }

        public bool Enviado { get; set; }

        public int IdUsuario { get; set; }

        public int IdDivulgacaoDoacao { get; set; }

        public virtual DivulgacaoDoacao DivulgacaoDoacao { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
