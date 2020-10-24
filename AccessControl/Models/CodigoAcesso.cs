using System;
using System.Collections.Generic;

namespace AccessControl.Models
{
    public partial class CodigoAcesso
    {
        public CodigoAcesso()
        {
            PessoaTipoAcesso = new HashSet<PessoaTipoAcesso>();
        }

        public Guid Id { get; set; }
        public bool EmUso { get; set; }

        public virtual ICollection<PessoaTipoAcesso> PessoaTipoAcesso { get; set; }
    }
}
