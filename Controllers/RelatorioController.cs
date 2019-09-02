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
    public class RelatorioController : ControllerBase
    {
        private readonly RSApiContext _context;

        public RelatorioController(RSApiContext context){
            _context = context;
        }

        // GET api/Relatorio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Relatorio>>> GetItems()
        {
            return await _context.Relatorios.ToListAsync();
        }

        // GET api/Relatorio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Relatorio>> GetItem(int id)
        {
            var item = await _context.Relatorios.FindAsync(id);

            if(item == null){
                return NotFound();
            }

            return item;
        }

        // POST api/Relatorio
        [HttpPost]
        public async Task<ActionResult<Relatorio>> Post(Relatorio relatorio)
        {
            _context.Relatorios.Add(relatorio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetItem), new Relatorio {id = relatorio.id}, relatorio);
        }

        // PUT api/Relatorio/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Relatorio relatorio)
        {
            if(id != relatorio.id){
                return BadRequest();
            }

            _context.Entry(relatorio).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/Relatorio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var relatorio = await _context.Relatorios.FindAsync(id);

            if(relatorio == null){
                return NotFound();
            }

            _context.Relatorios.Remove(relatorio);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
