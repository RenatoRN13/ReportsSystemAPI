using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using app.Domain.Entities;
using app.Domain.DTOs;
using AutoMapper;

namespace app.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class VinculoController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public IActionResult Get(VinculoService service)
        {
            var retorno = service.GetAll();
            //return new string[] { "value1", "value2" };
            return Ok(retorno);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(VinculoService service, int id)
        {
            var retorno = service.GetById(id);
            //return new string[] { "value1", "value2" };
            return Ok(retorno);
        }

        // POST api/values
        [HttpPost]
        public void Create(VinculoService service, [FromBody] VinculoDTO vinculo)
        {
            //var mapped = IMapper.Map<VinculoDTO, Vinculo>(vinculo);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}