using AccessControl.Models;
using AccessControl.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccessControl.Repository
{
    public interface IPessoaRepository
    {
        Task<List<PessoaTipoAcesso>> GetPessoaTipoAcesso();

        Task<List<PessoaViewModel>> GetPessoas();

        Task<PessoaViewModel> GetPessoa(string cpf);

        Task<int> AddPessoa(string nome, string cpf, string telefone);

        Task UpdatePessoa(Pessoa pessoa);
    }
}
