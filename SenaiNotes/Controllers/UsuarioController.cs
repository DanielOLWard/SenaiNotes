using Microsoft.AspNetCore.Authorization;
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

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cadastra um Usuario",
            Description = "Este EndPoint Cadastra um Usuario"
            )]
        public IActionResult CadastrarUsuario(CadastrarUsuarioDto cadastrarUsuario)
        {
            _usuarioRepository.Cadastrar(cadastrarUsuario);

            return Created();
        }

        [HttpGet]
        //[Authorize]
        [SwaggerOperation(
           Summary = "Listar todos os Usuarios",
           Description = "Este EndPoint lista todos os Usuarios"
           )]
        public async Task<IActionResult> ListarUsuariosAsync()
        {
            var usuario = await _usuarioRepository.ListarusuarioAsync();
            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        //[Authorize]
        [SwaggerOperation(
           Summary = "Deletar um Usuario",
           Description = "Este EndPoint deleta um Usuario pelo Id Fornecido"
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
        //[Authorize]
        [SwaggerOperation(
           Summary = "Atualizar Usuario",
           Description = "Este EndPoint Atualiza um usuario pelo Id Fornecido"
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
        //[Authorize]
        [SwaggerOperation(
            Summary = "Lista um Usuario pelo Id Informado",
            Description = "Este EndPoint lista um Usuario de acordo com o Id informado"
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
