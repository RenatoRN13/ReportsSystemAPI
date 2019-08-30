using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infra.Context;
using app.Domain.Entities;
using System.Linq;

namespace app.Domain.Entities {
    public class VinculoService : ControllerBase {
        private readonly RSApiContext _context;

        public VinculoService(RSApiContext context){
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Vinculo>>> GetAll(){
            return await _context.Vinculo.ToListAsync();
        }

        public async Task<ActionResult<Vinculo>> GetById(int id){
            var item = await _context.Vinculo.FindAsync(id);

            if(item == null){
                return NotFound();
            }

            return item;
        }
    }
}