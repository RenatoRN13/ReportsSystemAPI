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
    public class LogController : ControllerBase
    {
        private readonly RSApiContext _context;

        public LogController(RSApiContext context){
            _context = context;
        }

        // GET api/Log
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Log>>> GetItems()
        {
            return await _context.Logs.ToListAsync();
        }

        // // GET api/Log/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<Log>> GetItem(int id)
        // {
        //     var item = await _context.Logs.FindAsync(id);

        //     if(item == null){
        //         return NotFound();
        //     }

        //     return item;
        // }

        // POST api/Log
        [HttpPost]
        public async Task<ActionResult<Log>> Post(Log log)
        {
            _context.Logs.Add(log);
            await _context.SaveChangesAsync();

            return Ok("Sucesso!"); //CreatedAtAction(nameof(GetItem), new Log {id = log.id}, log);
        }

        // // PUT api/Log/5
        // [HttpPut("{id}")]
        // public async Task<IActionResult> Put(int id, Log log)
        // {
        //     if(id != log.id){
        //         return BadRequest();
        //     }

        //     _context.Entry(log).State = EntityState.Modified;
        //     await _context.SaveChangesAsync();

        //     return NoContent();
        // }

        // // DELETE api/Log/5
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> Delete(int id)
        // {
        //     var log = await _context.Logs.FindAsync(id);

        //     if(log == null){
        //         return NotFound();
        //     }

        //     _context.Logs.Remove(log);
        //     await _context.SaveChangesAsync();

        //     return NoContent();
        // }
    }
}
