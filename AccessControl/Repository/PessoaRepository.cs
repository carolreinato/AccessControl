using AccessControl.Models;
using AccessControl.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace AccessControl.Repository
{
    public class PessoaRepository : IPessoaRepository
    {
        AccessControlContext db;
        public PessoaRepository(AccessControlContext _db)
        {
            db = _db;
        }

        public async Task<int> AddPessoa(string nome, string cpf, string telefone)
        {
            if (db != null)
            {
                Pessoa pessoa = new Pessoa
                {
                    Nome = nome,
                    Cpf = cpf,
                    Telefone = telefone
                };

                await db.Pessoa.AddAsync(pessoa);
                await db.SaveChangesAsync();

                return 1;
            }

            return 0;
        }

        public async Task<PessoaViewModel> GetPessoa(string cpf)
        {
            if (db != null)
            {
                return await db.Pessoa.Where(x => x.Cpf == cpf).Select(x => new PessoaViewModel
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Cpf = x.Cpf,
                    Telefone = x.Telefone
                }).FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<List<PessoaViewModel>> GetPessoas()
        {
            if (db != null)
            {
                return await db.Pessoa.Select(x => new PessoaViewModel
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Cpf = x.Cpf,
                    Telefone = x.Telefone
                }).ToListAsync();
            }

            return null;
        }

        //public async Task UpdatePessoa(Pessoa pessoa)
        //{
        //    if (db != null)
        //    {
        //        //Deleta a pessoa
        //        db.Pessoa.Update(pessoa);

        //        //Comita a transação
        //        await db.SaveChangesAsync();
        //    }
        //}
    }
}
