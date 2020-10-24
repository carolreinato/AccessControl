using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AccessControl;
using AccessControl.Models;

namespace AccessControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaTipoAcessoController : ControllerBase
    {
        private readonly AccessControlContext _context;

        public PessoaTipoAcessoController(AccessControlContext context)
        {
            _context = context;
        }

        // GET: api/PessoaTipoAcesso
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PessoaTipoAcesso>>> GetPessoaTipoAcesso()
        {
            return await _context.PessoaTipoAcesso.ToListAsync();
        }

        // GET: api/PessoaTipoAcesso/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PessoaTipoAcesso>> GetPessoaTipoAcesso(int id)
        {
            var pessoaTipoAcesso = await _context.PessoaTipoAcesso.FindAsync(id);

            if (pessoaTipoAcesso == null)
            {
                return NotFound();
            }

            return pessoaTipoAcesso;
        }

        // PUT: api/PessoaTipoAcesso/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPessoaTipoAcesso(int id, PessoaTipoAcesso pessoaTipoAcesso)
        {
            if (id != pessoaTipoAcesso.Id)
            {
                return BadRequest();
            }

            _context.Entry(pessoaTipoAcesso).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PessoaTipoAcessoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PessoaTipoAcesso
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PessoaTipoAcesso>> PostPessoaTipoAcesso(PessoaTipoAcesso pessoaTipoAcesso)
        {
            _context.PessoaTipoAcesso.Add(pessoaTipoAcesso);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPessoaTipoAcesso", new { id = pessoaTipoAcesso.Id }, pessoaTipoAcesso);
        }

        private bool PessoaTipoAcessoExists(int id)
        {
            return _context.PessoaTipoAcesso.Any(e => e.Id == id);
        }
    }
}
