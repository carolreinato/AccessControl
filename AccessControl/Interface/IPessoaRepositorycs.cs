﻿using AccessControl.Models;
using AccessControl.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccessControl.Interface
{
    public interface IPessoaRepository
    {
        Task<List<PessoaViewModel>> GetPessoas();

        Task<PessoaViewModel> GetPessoa(string cpf);

        Task<int> AddPessoa(string nome, string cpf, string telefone);

        //Task UpdatePessoa(Pessoa pessoa);
    }
}
