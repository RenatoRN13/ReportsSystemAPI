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
            List<Usuario> usuarios = new List<Usuario>();

            foreach(Usuario user in await _context.Usuarios.ToListAsync()){
                Usuario usuarioTemp = new Usuario();
                usuarioTemp.id = user.id;
                usuarioTemp.login = user.login;
                usuarioTemp.nome = user.nome;
                usuarioTemp.idPerfil = user.idPerfil;

                usuarios.Add(usuarioTemp);
            }

            return usuarios;
        }

        // GET api/Usuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetItem(int id)
        {
            var item = await _context.Usuarios.FindAsync(id);

            if(item == null){
                return NotFound("Usuário não encontrado!");
            }

            return item;
        }

        // POST api/Usuario
        [HttpPost]
        public async Task<ActionResult<Usuario>> Post(Usuario usuario)
        {
            try{
                Perfil perfil = await _context.Perfis.FindAsync(usuario.idPerfil);

                if(perfil != null){
                    _context.Usuarios.Add(usuario);
                    await _context.SaveChangesAsync();

                    return Ok("Usuário cadastrado com sucesso!");
                }
                
                return Ok("Não foi possível cadastrar o usuário. É necesário informar um perfil válido!");                
            } catch (Exception e){
                new Exception(e.Message);
                return Ok("Erro ao tentar cadastrar usuário.");
            }
        }

        // PUT api/Usuario/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Usuario usuario)
        {
            if(id != usuario.id){
                return BadRequest();
            }

            Perfil perfil = await _context.Perfis.FindAsync(usuario.idPerfil);

            if(perfil == null)
                return BadRequest("Não foi possível atualizar o usuário. É necesário informar um perfil válido!");

            if(usuario.senha == "" || usuario.senha == null)
                return BadRequest("Não foi possível atualizar o usuário. É necesário informar uma senha válida!");

            if(usuario.login == "" || usuario.login == null)
                return BadRequest("Não foi possível atualizar o usuário. É necesário informar um login válido!");

            if(usuario.nome == "" || usuario.nome == null)
                return BadRequest("Não foi possível atualizar o usuário. É necesário informar o seu nome!");
            
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok("Usuário atualizado com sucesso!");
        }

        // DELETE api/Usuario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if(usuario == null){
                return BadRequest("Usuário não existe!");
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return Ok("Usuário removido com sucesso!");
        }
    }
}
