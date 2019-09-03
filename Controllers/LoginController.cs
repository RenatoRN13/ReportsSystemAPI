using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using ReportsSystemApi.Infra;
using ReportsSystemApi.Domain;
using ReportsSystemApi.Domain.Entities;

namespace ReportsSystemApi.Controllers
{
    [Route("api/login")]
    public class LoginController : Controller
    {
        private RSApiContext _context;

        public LoginController(RSApiContext _context)
        {
            this._context = _context;
        }

        [AllowAnonymous]
        [HttpPost]
        public object Post(
        [FromBody]Usuario usuario,
        [FromServices]SigningConfig signingConfigurations,
        [FromServices]TokenConfig tokenConfigurations)
        {
            bool credenciaisValidas = false;
            if (usuario != null && !String.IsNullOrWhiteSpace(usuario.login))
            {
                try {
                    var usuarioBase = _context.Usuarios.Where(u => u.login == usuario.login).First();
                    credenciaisValidas = (usuarioBase != null && usuario.login == usuarioBase.login && usuario.senha == usuarioBase.senha);
                } catch {
                    return new { authenticated = false, message = "Falha na autenticação! Usuário não encontrado " };
                }
            }

            if (credenciaisValidas)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(usuario.login, "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, usuario.login)
                    }
                );

                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao + TimeSpan.FromSeconds(tokenConfigurations.seconds);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {

                    Issuer = tokenConfigurations.issuer,
                    Audience = tokenConfigurations.audience,
                    SigningCredentials = signingConfigurations.signingCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                });

                var usuarioBase = _context.Usuarios.Where(u => u.login == usuario.login).First();

                var token = handler.WriteToken(securityToken);

                return new
                {
                    authenticated = true,
                    user = new NewClass(usuarioBase.nome, usuarioBase.login, usuarioBase.id),
                    created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken = token,
                    message = "Login Realizado com Sucesso!"
                };
            }
            else
            {
                return new {
                    authenticated = false, message = "Falha na auteticação! Senha incorreta ou usuário não informado."
                };
            }
        }

    }

    internal class NewClass
    {
        public string Nome { get; }
        public string Login { get; }
        public int Id { get; }

        public NewClass(string nome, string login, int id)
        {
            Nome = nome;
            Login = login;
            Id = id;
        }

        public override bool Equals(object obj)
        {
            return obj is NewClass other &&
                   Nome == other.Nome &&
                   Login == other.Login &&
                   Id == other.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Nome, Login, Id);
        }
    }
}
