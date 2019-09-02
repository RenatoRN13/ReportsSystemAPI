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
    public class UsuarioController : ControllerBase
    {
        private readonly RSApiContext _context;

        public UsuarioController(RSApiContext context){
            _context = context;
        }

        // GET api/Usuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetItems()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET api/Usuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetItem(int id)
        {
            var item = await _context.Usuarios.FindAsync(id);

            if(item == null){
                return NotFound();
            }

            return item;
        }

        // POST api/Usuario
        [HttpPost]
        public async Task<ActionResult<Usuario>> Post(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetItem), new Usuario {id = usuario.id}, usuario);
        }

        // PUT api/Usuario/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Usuario usuario)
        {
            if(id != usuario.id){
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/Usuario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if(usuario == null){
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
