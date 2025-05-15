using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SenaiNotes.Dto;
using SenaiNotes.Interfaces;
using SenaiNotes.Services;

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
    }
}
