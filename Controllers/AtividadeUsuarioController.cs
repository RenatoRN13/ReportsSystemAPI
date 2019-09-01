using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportsSystemApi.Domain.Entities;
using ReportsSystemAPI.Infra;

namespace ReportsSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtividadeUsuarioController : ControllerBase
    {
        private readonly RSApiContext _context;

        public AtividadeUsuarioController(RSApiContext context){
            _context = context;
        }

        // GET api/AtividadeUsuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AtividadeUsuario>>> GetItems()
        {
            return await _context.AtividadeUsuarios.ToListAsync();
        }

        // GET api/AtividadeUsuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AtividadeUsuario>> GetItem(int id)
        {
            var item = await _context.AtividadeUsuarios.FindAsync(id);

            if(item == null){
                return NotFound();
            }

            return item;
        }

        // POST api/AtividadeUsuario
        [HttpPost]
        public async Task<ActionResult<AtividadeUsuario>> Post(AtividadeUsuario atividadeUsuario)
        {
            _context.AtividadeUsuarios.Add(atividadeUsuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetItem), new AtividadeUsuario {id = atividadeUsuario.id}, atividadeUsuario);
        }

        // PUT api/AtividadeUsuario/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, AtividadeUsuario atividadeUsuario)
        {
            if(id != atividadeUsuario.id){
                return BadRequest();
            }

            _context.Entry(atividadeUsuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/AtividadeUsuario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var atividadeUsuario = await _context.AtividadeUsuarios.FindAsync(id);

            if(atividadeUsuario == null){
                return NotFound();
            }

            _context.AtividadeUsuarios.Remove(atividadeUsuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
