using System;
using System.Collections.Generic;

namespace AccessControl.Models
{
    public partial class PessoaTipoAcesso
    {
        public int Id { get; set; }
        public int IdPessoa { get; set; }
        public int IdTipoAcesso { get; set; }
        public Guid IdCodigoAcesso { get; set; }
        public DateTime Entrada { get; set; }
        public DateTime? Saida { get; set; }
        public bool InCondominio { get; set; }

        public virtual CodigoAcesso IdCodigoAcessoNavigation { get; set; }
        public virtual Pessoa IdPessoaNavigation { get; set; }
        public virtual TipoAcesso IdTipoAcessoNavigation { get; set; }
    }
}
