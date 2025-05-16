using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SenaiNotes.Dto;
using SenaiNotes.Interfaces;
using SenaiNotes.Services;
using SenaiNotes.ViewModel;

namespace SenaiNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IUsuariorepository _usuarioRepository;

        private PasswordService _passwordService;

        public UsuarioController(IUsuariorepository usuariorepository)
        {
            _usuarioRepository = usuariorepository;
        }

        [HttpGet]
        public async Task<IActionResult> ListarUsuariosAsync()
        {
            var usuario = await _usuarioRepository.ListarusuarioAsync();
            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult CadastrarUsuario(CadastrarUsuarioDto cadastrarUsuario)
        {
            _usuarioRepository.Cadastrar(cadastrarUsuario);

            return Created();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                _usuarioRepository.Deletar(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound("Usuario nao encontrado!");
            }
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarUsuario(int id, AtualizarusuarioDto usuarioAtualizado)
        {
            try
            {
                _usuarioRepository.Atualizar(id, usuarioAtualizado);
                return Ok(usuarioAtualizado);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound("Usuario nao Encontrado!!");
            }
        }

        [HttpGet("{id}")]
        public IActionResult ListarPorId (int id)
        {
            ListarusuarioViewModel usuario = _usuarioRepository.BuscarPorId(id);

            if (usuario == null) return NotFound("Usuario nao Encontrado!!");

            return Ok(usuario);
        }

        [HttpPost("login")]
        public IActionResult Login (LoginDto login)
        {
            var usuario = _usuarioRepository.Login(login.Email, login.Senha);

            if (usuario == null) return Unauthorized("Email ou Senha invalidos");

            var tokenService = new TokenService();

            return Ok("Login efetuado com Sucesso!!");
        }
    }
}
