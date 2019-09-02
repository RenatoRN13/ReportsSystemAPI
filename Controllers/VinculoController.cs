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
    public class VinculoController : ControllerBase
    {
        private readonly RSApiContext _context;

        public VinculoController(RSApiContext context){
            _context = context;
        }

        // GET api/Vinculo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vinculo>>> GetItems()
        {
            return await _context.Vinculos.ToListAsync();
        }

        // GET api/Vinculo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vinculo>> GetItem(int id)
        {
            var item = await _context.Vinculos.FindAsync(id);

            if(item == null){
                return NotFound();
            }

            return item;
        }

        // POST api/Vinculo
        [HttpPost]
        public async Task<ActionResult<Vinculo>> Post(Vinculo vinculo)
        {
            _context.Vinculos.Add(vinculo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetItem), new Vinculo {id = vinculo.id}, vinculo);
        }

        // PUT api/Vinculo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Vinculo vinculo)
        {
            if(id != vinculo.id){
                return BadRequest();
            }

            _context.Entry(vinculo).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/Vinculo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var vinculo = await _context.Vinculos.FindAsync(id);

            if(vinculo == null){
                return NotFound();
            }

            _context.Vinculos.Remove(vinculo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
