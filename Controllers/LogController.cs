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

        // POST api/Log
        [HttpPost]
        public async Task<ActionResult<Log>> Post(Log log)
        {
            try{
                Usuario usuario = await _context.Usuarios.FindAsync(log.idUsuario);
                if(usuario != null){
                    _context.Logs.Add(log);
                    await _context.SaveChangesAsync();

                    return Ok("Log registrado com sucesso!");
                }
                return Ok("Log não registrado. Usuário não informado!");             

            } catch(Exception e){
                Exception exception = new Exception(e.Message);
                return Ok("Erro ao tentar registrar o log.");
            }
            
        }
    }
}
