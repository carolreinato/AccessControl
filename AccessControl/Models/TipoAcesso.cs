using System;
using System.Collections.Generic;

namespace AccessControl.Models
{
    public partial class TipoAcesso
    {
        public TipoAcesso()
        {
            PessoaTipoAcesso = new HashSet<PessoaTipoAcesso>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<PessoaTipoAcesso> PessoaTipoAcesso { get; set; }
    }
}
