using AccessControl.Interface;
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
    public class CodigoAcessoController : ControllerBase
    {
        private ICodigoAcessoRepository codigoAcessoRepository;
        public CodigoAcessoController(ICodigoAcessoRepository _codigoAcessoRepository)
        {
            codigoAcessoRepository = _codigoAcessoRepository;
        }

        [HttpGet]
        [Route("GetCodigosAcesso")]
        public async Task<IActionResult> GetCodigosAcesso()
        {
            try
            {
                var codigosAcesso = await codigoAcessoRepository.GetCodigosAcesso();
                if (codigosAcesso == null)
                {
                    return NotFound();
                }

                return Ok(codigosAcesso);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetCodigosAcessoLivres")]
        public async Task<IActionResult> GetCodigosAcessoLivres()
        {
            try
            {
                var codigosAcessoLivre = await codigoAcessoRepository.GetCodigosAcessoLivres();
                if (codigosAcessoLivre == null)
                {
                    return NotFound();
                }

                return Ok(codigosAcessoLivre);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetCodigoAcesso")]
        public async Task<IActionResult> GetCodigoAcesso(Guid id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            try
            {
                var codigoAcesso = await codigoAcessoRepository.GetCodigoAcesso(id);

                if (codigoAcesso == null)
                {
                    return NotFound();
                }

                return Ok(codigoAcesso);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("AddCodigoAcesso")]
        public async Task<IActionResult> AddCodigoAcesso()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var codigo = await codigoAcessoRepository.AddCodigoAcesso();
                    if (codigo > 0)
                    {
                        return Ok(codigo);
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

        [HttpPut("UpdateCodigoAcesso")]
        public async Task<IActionResult> UpdateCodigoAcesso(Guid id, bool status)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await codigoAcessoRepository.UpdateCodigoAcesso(id, status);

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
