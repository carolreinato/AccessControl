using AccessControl.Interface;
using AccessControl.Models;
using AccessControl.Repository;
using AccessControl.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccessControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private IPessoaRepository pessoaRepository;
        public PessoaController(IPessoaRepository _pessoaRepository)
        {
            pessoaRepository = _pessoaRepository;
        }

        [HttpGet]
        [Route("GetPessoas")]
        public async Task<IActionResult> GetPessoas()
        {
            try
            {
                var pessoas = await pessoaRepository.GetPessoas();
                if (pessoas == null)
                {
                    return NotFound();
                }

                return Ok(pessoas);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetPessoa")]
        public async Task<IActionResult> GetPessoa(string cpf)
        {
            if (cpf == null)
            {
                return BadRequest();
            }

            try
            {
                var pessoa = await pessoaRepository.GetPessoa(cpf);

                if (pessoa == null)
                {
                    return NotFound();
                }

                return Ok(pessoa);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("AddPessoa")]
        public async Task<IActionResult> AddPessoa(string nome, string cpf, string telefone)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var pessoaId = await pessoaRepository.AddPessoa(nome, cpf, telefone);
                    if (pessoaId > 0)
                    {
                        return Ok(pessoaId);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {

                    return BadRequest();
                }

            }

            return BadRequest();
        }
    }
}
