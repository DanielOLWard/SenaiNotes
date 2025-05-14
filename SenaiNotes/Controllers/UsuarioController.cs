using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SenaiNotes.Dto;
using SenaiNotes.Interfaces;

namespace SenaiNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IUsuariorepository _usuarioRepository;

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
    }
}
