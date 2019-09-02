using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportsSystemApi.Domain.Entities;
using ReportsSystemApi.Infra;

namespace ReportsSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioPerfilController : ControllerBase
    {
        private readonly RSApiContext _context;

        public UsuarioPerfilController(RSApiContext context){
            _context = context;
        }

        // GET api/UsuarioPerfil
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioPerfil>>> GetItems()
        {
            return await _context.UsuarioPerfis.ToListAsync();
        }

        // GET api/UsuarioPerfil/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioPerfil>> GetItem(int id)
        {
            var item = await _context.UsuarioPerfis.FindAsync(id);

            if(item == null){
                return NotFound();
            }

            return item;
        }

        // POST api/UsuarioPerfil
        [HttpPost]
        public async Task<ActionResult<UsuarioPerfil>> Post(UsuarioPerfil usuarioPerfil)
        {
            _context.UsuarioPerfis.Add(usuarioPerfil);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetItem), new UsuarioPerfil {id = usuarioPerfil.id}, usuarioPerfil);
        }

        // PUT api/UsuarioPerfil/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UsuarioPerfil usuarioPerfil)
        {
            if(id != usuarioPerfil.id){
                return BadRequest();
            }

            _context.Entry(usuarioPerfil).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/UsuarioPerfil/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var usuarioPerfil = await _context.UsuarioPerfis.FindAsync(id);

            if(usuarioPerfil == null){
                return NotFound();
            }

            _context.UsuarioPerfis.Remove(usuarioPerfil);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
