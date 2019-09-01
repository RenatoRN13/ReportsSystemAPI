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
    public class PerfilController : ControllerBase
    {
        private readonly RSApiContext _context;

        public PerfilController(RSApiContext context){
            _context = context;
        }

        // GET api/Perfil
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Perfil>>> GetItems()
        {
            return await _context.Perfis.ToListAsync();
        }

        // GET api/Perfil/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Perfil>> GetItem(int id)
        {
            var item = await _context.Perfis.FindAsync(id);

            if(item == null){
                return NotFound();
            }

            return item;
        }

        // POST api/Perfil
        [HttpPost]
        public async Task<ActionResult<Perfil>> Post(Perfil perfil)
        {
            _context.Perfis.Add(perfil);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetItem), new Perfil {id = perfil.id}, perfil);
        }

        // PUT api/Perfil/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Perfil perfil)
        {
            if(id != perfil.id){
                return BadRequest();
            }

            _context.Entry(perfil).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/Perfil/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var perfil = await _context.Perfis.FindAsync(id);

            if(perfil == null){
                return NotFound();
            }

            _context.Perfis.Remove(perfil);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
