using AccessControl.Interface;
using AccessControl.Models;
using AccessControl.ViewModel;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccessControl.Repository
{
    public class PessoaTipoAcessoRepository : IPessoaTipoAcessoRepository
    {
        AccessControlContext db;
        public PessoaTipoAcessoRepository(AccessControlContext _db)
        {
            db = _db;
        }

        public async Task<int> AddPessoaTipoAcesso(int idPessoa, int idTipoAcesso, Guid idCodigoAcesso)
        {
            if (db != null)
            {
                PessoaTipoAcesso pessoaTipoAcesso = new PessoaTipoAcesso
                {
                    IdPessoa = idPessoa,
                    IdTipoAcesso = idTipoAcesso,
                    IdCodigoAcesso = idCodigoAcesso,
                    Entrada = DateTime.Now,
                    InCondominio = true
                };

                await db.PessoaTipoAcesso.AddAsync(pessoaTipoAcesso);
                await db.SaveChangesAsync();

                return 1;
            }
            return 0;
        }

        public async Task<List<PessoaTipoAcessoViewModel>> GetPessoasTipoAcesso()
        {
            if (db != null)
            {
                return await db.PessoaTipoAcesso.Select(x => new PessoaTipoAcessoViewModel
                {
                    Id = x.Id,
                    IdPessoa = x.IdPessoa,
                    IdTipoAcesso = x.IdTipoAcesso,
                    IdCodigoAcesso = x.IdCodigoAcesso,
                    Entrada = x.Entrada,
                    Saida = x.Saida.HasValue ? x.Saida : DateTime.Parse("1900-01-01"),
                    InCondominio = x.InCondominio
                }).ToListAsync();
            }

            return null;
        }

        public async Task<PessoaTipoAcessoViewModel> GetPessoaTipoAcesso(string cpf)
        {
            if(db != null)
            {
                return await db.PessoaTipoAcesso.Where(x => x.IdPessoaNavigation.Cpf == cpf)
                    .Select(x => new PessoaTipoAcessoViewModel
                    {
                        Id = x.Id,
                        IdPessoa = x.IdPessoa,
                        IdTipoAcesso = x.IdTipoAcesso,
                        IdCodigoAcesso = x.IdCodigoAcesso,
                        Entrada = x.Entrada,
                        Saida = x.Saida.HasValue ? x.Saida : DateTime.Parse("1900-01-01"),
                        InCondominio = x.InCondominio
                    }).FirstOrDefaultAsync();
            }
            return null;
        }

        public async Task UpdatePessoaTipoAcesso(int idPessoaTipoAcesso)
        {
            if (db != null)
            {
                //Acha a pessoa
                var pessoaTipoAcesso = db.PessoaTipoAcesso.Where(x => x.Id == idPessoaTipoAcesso).FirstOrDefault();
                var horaSaida = DateTime.Now;
                PessoaTipoAcesso pessoa = new PessoaTipoAcesso
                {
                    Id = pessoaTipoAcesso.Id,
                    IdPessoa = pessoaTipoAcesso.IdPessoa,
                    IdTipoAcesso = pessoaTipoAcesso.IdTipoAcesso,
                    IdCodigoAcesso = pessoaTipoAcesso.IdCodigoAcesso,
                    Entrada = pessoaTipoAcesso.Entrada,
                    Saida = horaSaida,
                    InCondominio = false
                };

                var local = db.Set<PessoaTipoAcesso>()
                    .Local
                    .FirstOrDefault(entry => entry.Id.Equals(idPessoaTipoAcesso));

                // check if local is not null 
                if (local != null)
                {
                    // detach
                    db.Entry(local).State = EntityState.Detached;
                }
                // set Modified flag in your entry
                db.Entry(pessoa).State = EntityState.Modified;

                //Comita a transação
                await db.SaveChangesAsync();
            }
        }
       
    }
}
