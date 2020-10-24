using AccessControl.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccessControl.Interface
{
    public interface IPessoaTipoAcessoRepository
    {
        Task<List<PessoaTipoAcessoViewModel>> GetPessoasTipoAcesso();

        Task<PessoaTipoAcessoViewModel> GetPessoaTipoAcesso(string cpf);

        Task<int> AddPessoaTipoAcesso(int idPessoa, int idTipoAcesso, Guid idCodigoAcesso);

        Task UpdatePessoaTipoAcesso(int idPessoaTipoAcesso);
    }
}
