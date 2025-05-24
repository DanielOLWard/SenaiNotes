using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SenaiNotes.Dto;
using SenaiNotes.Interfaces;
using SenaiNotes.Services;
using SenaiNotes.ViewModel;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(
            Summary = "Listar UsuarioAsync",
            Description = "Este EndPoint Lista os UsuarioAsync"
            )]
        public async Task<IActionResult> ListarUsuariosAsync()
        {
            var usuario = await _usuarioRepository.ListarusuarioAsync();
            return Ok(usuario);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Fornece um Usuario",
            Description = "Este endpoint fornece um usuario"
            )]
        public IActionResult CadastrarUsuario(CadastrarUsuarioDto cadastrarUsuario)
        {
            _usuarioRepository.Cadastrar(cadastrarUsuario);

            return Created();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Deletar Usuario",
            Description = "Este EndPoint delete um Usuario"
            )]
        public IActionResult Deletar(int id)
        {
            try
            {
                _usuarioRepository.Deletar(id);
                return NoContent();
            }
            catch (ArgumentNullException ex)
            {
                return NotFound("Usuario nao encontrado!");
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Atualizar Usuario",
            Description = "Este EndPoint Atualiza um Usuario"
            )]
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
        [SwaggerOperation(
            Summary = "Listar por Id Usuario",
            Description = "Este EndPoint Lista por Id os Usuario"
            )]
        public IActionResult ListarPorId (int id)
        {
            ListarusuarioViewModel usuario = _usuarioRepository.BuscarPorId(id);

            if (usuario == null) return NotFound("Usuario nao Encontrado!!");

            return Ok(usuario);
        }

        [HttpPost("login")]
        [SwaggerOperation(
            Summary = "Login Usuario",
            Description = "Este EndPoint e responsavel pelo Login do Usuario"
            )]
        public IActionResult Login (LoginDto login)
        {
            var usuario = _usuarioRepository.Login(login.Email, login.Senha);

            if (usuario == null) return Unauthorized("Email ou Senha invalidos");

            var tokenService = new TokenService();
            var token = tokenService.GerarToken(usuario.Email);
            var viewModel = new ListarusuarioViewModel
            {
                UsuarioId = usuario.UsuarioId,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Telefone = usuario.Telefone,
                DataCadastro = usuario.DataCadastro,
                TipoUsuarioId = usuario.TipoUsuarioId,
            };
            return Ok(new
            {
                token,
                usuario = viewModel
            });
        }
    }
}
