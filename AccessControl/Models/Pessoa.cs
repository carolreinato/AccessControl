using System;
using System.Collections.Generic;

namespace AccessControl.Models
{
    public partial class Pessoa
    {
        public Pessoa()
        {
            PessoaTipoAcesso = new HashSet<PessoaTipoAcesso>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }

        public virtual ICollection<PessoaTipoAcesso> PessoaTipoAcesso { get; set; }

    }
}
