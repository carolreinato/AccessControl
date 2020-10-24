using AccessControl.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccessControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaTipoAcessoController : ControllerBase
    {
        private IPessoaTipoAcessoRepository pessoaTipoAcessoRepository;
        public PessoaTipoAcessoController(IPessoaTipoAcessoRepository _pessoaTipoAcessoRepository)
        {
            pessoaTipoAcessoRepository = _pessoaTipoAcessoRepository;
        }

        [HttpGet]
        [Route("GetPessoasTipoAcesso")]
        public async Task<IActionResult> GetPessoasTipoAcesso()
        {
            try
            {
                var pessoasTipoAcesso = await pessoaTipoAcessoRepository.GetPessoasTipoAcesso();
                if (pessoasTipoAcesso == null)
                {
                    return NotFound();
                }

                return Ok(pessoasTipoAcesso);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetPessoaTipoAcesso")]
        public async Task<IActionResult> GetPessoaTipoAcesso(string cpf)
        {
            if (cpf == null)
            {
                return BadRequest();
            }

            try
            {
                var pessoaTipoAcesso = await pessoaTipoAcessoRepository.GetPessoaTipoAcesso(cpf);

                if (pessoaTipoAcesso == null)
                {
                    return NotFound();
                }

                return Ok(pessoaTipoAcesso);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("AddPessoaTipoAcesso")]
        public async Task<IActionResult> AddPessoaTipoAcesso(int idPessoa, int idTipoAcesso, Guid idCodigoAcesso)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var pessoaId = await pessoaTipoAcessoRepository.AddPessoaTipoAcesso(idPessoa,idTipoAcesso,idCodigoAcesso);
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

        [HttpPut("UpdatePessoaTipoAcesso")]
        //[Route("UpdatePessoaTipoAcesso")]
        public async Task<IActionResult> UpdatePessoaTipoAcesso(int idPessoaTipoAcesso)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await pessoaTipoAcessoRepository.UpdatePessoaTipoAcesso(idPessoaTipoAcesso);

                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }

            return BadRequest();
        }
    }
}
