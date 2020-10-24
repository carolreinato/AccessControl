using AccessControl.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccessControl.Repository
{
    public interface ICodigoAcessoRepository
    {
        Task<List<CodigoAcessoViewModel>> GetCodigosAcessoLivres();

        Task<List<CodigoAcessoViewModel>> GetCodigosAcesso();

        Task<CodigoAcessoViewModel> GetCodigoAcesso(Guid id);

        Task<int> AddCodigoAcesso();

        Task UpdateCodigoAcesso(Guid id, bool status);
    }
}
