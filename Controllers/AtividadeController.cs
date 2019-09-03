using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportsSystemApi.Domain.Entities;
using ReportsSystemApi.Infra;

namespace ReportsSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtividadeController : ControllerBase
    {
        private readonly RSApiContext _context;

        public AtividadeController(RSApiContext context){
            _context = context;
        }

        // GET api/Atividade
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Atividade>>> GetItems()
        {
            return await _context.Atividades.ToListAsync();
        }
        [AllowAnonymous]
        // GET api/Atividade/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Atividade>> GetItem(int id)
        {
            var item = await _context.Atividades.FindAsync(id);

            if(item == null){
                return NotFound("Atividade não encontrada");
            }

            return item;
        }

        // POST api/Atividade
        [HttpPost]
        public async Task<ActionResult<Atividade>> Post(Atividade atividade)
        {
            if(atividade.descricao == null)
                return BadRequest("É necessário inserir uma descrição para a atividade.");

            if(atividade.dataInicio == null || atividade.dataFim == null)
                return BadRequest("É necessario inserir data de início e fim para a atividade.");

            _context.Atividades.Add(atividade);
            await _context.SaveChangesAsync();
            
            return Ok("Atividade cadastrada com sucesso!");
        }

        // PUT api/Atividade/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Atividade atividade)
        {
            if(id != atividade.id){
                return BadRequest();
            }
            if(atividade.descricao == null)
                return BadRequest("É necessário inserir uma descrição para a atividade.");

            if(atividade.dataInicio == null || atividade.dataFim == null)
                return BadRequest("É necessario inserir data de início e fim para a atividade.");

            _context.Entry(atividade).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok("Atividade atualizada com sucesso!");
        }

        // DELETE api/Atividade/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var atividade = await _context.Atividades.FindAsync(id);

            if(atividade == null){
                return NotFound();
            }

            _context.Atividades.Remove(atividade);
            await _context.SaveChangesAsync();

            return Ok("Atividade removida com sucesso!");
        }
    }
}
