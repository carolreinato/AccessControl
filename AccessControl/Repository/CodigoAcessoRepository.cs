using AccessControl.Interface;
using AccessControl.Models;
using AccessControl.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccessControl.Repository
{
    public class CodigoAcessoRepository : ICodigoAcessoRepository
    {
        AccessControlContext db;
        public CodigoAcessoRepository(AccessControlContext _db)
        {
            db = _db;
        }

        public async Task<int> AddCodigoAcesso()
        {
            if (db != null)
            {
                CodigoAcesso codigoAcesso = new CodigoAcesso
                {
                    Id = Guid.NewGuid(),
                    EmUso = false
                };

                await db.CodigoAcesso.AddAsync(codigoAcesso);
                await db.SaveChangesAsync();

                return 1;
            }
            return 0;
        }

        public async Task<List<CodigoAcessoViewModel>> GetCodigosAcesso()
        {
            if (db != null)
            {
                return await db.CodigoAcesso.Select(x => new CodigoAcessoViewModel
                {
                    Id = x.Id,
                    EmUso = x.EmUso
                }).ToListAsync();
            }

            return null;
        }

        public async Task<List<CodigoAcessoViewModel>> GetCodigosAcessoLivres()
        {
            if (db != null)
            {
                return await db.CodigoAcesso.Select(x => new CodigoAcessoViewModel
                {
                    Id = x.Id,
                    EmUso = x.EmUso
                }).Where(x => x.EmUso == false).ToListAsync();
            }

            return null;
        }

        public async Task<CodigoAcessoViewModel> GetCodigoAcesso(Guid id)
        {
            if (db != null)
            {
                return await db.CodigoAcesso.Where(x => x.Id == id)
                    .Select(x => new CodigoAcessoViewModel
                    {
                        Id = x.Id,
                        EmUso = x.EmUso
                    }).FirstOrDefaultAsync();
            }
            return null;
        }

        public async Task UpdateCodigoAcesso(Guid id, bool status)
        {
            if (db != null)
            {
                //Acha o código
                var codigoAcesso = db.CodigoAcesso.Where(x => x.Id == id).FirstOrDefault();
                CodigoAcesso codigoAcessoUpdate = new CodigoAcesso
                {
                    Id = codigoAcesso.Id,
                    EmUso = status
                };

                var local = db.Set<CodigoAcesso>()
                    .Local
                    .FirstOrDefault(entry => entry.Id.Equals(id));

                // check if local is not null 
                if (local != null)
                {
                    // detach
                    db.Entry(local).State = EntityState.Detached;
                }
                // set Modified flag in your entry
                db.Entry(codigoAcessoUpdate).State = EntityState.Modified;

                //Comita a transação
                await db.SaveChangesAsync();
            }
        }
    }
}
