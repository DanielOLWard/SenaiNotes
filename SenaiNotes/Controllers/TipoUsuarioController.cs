using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SenaiNotes.Dto;
using SenaiNotes.Interfaces;
using SenaiNotes.Models;

namespace SenaiNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {
        private ITipoUsuarioRepository _tipoUsuario;

        public TipoUsuarioController(ITipoUsuarioRepository tipoUsuarioRepository)
        {
            _tipoUsuario = tipoUsuarioRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ListarTipoUsuarioAsync()
        {
            var tipoUsuario = await _tipoUsuario.ListarTipoUsuariosuarioAsync();
            return Ok(tipoUsuario);
        }

        [HttpPost]
        public IActionResult CadastrarTipoUsuario(CadastrarTipoUsuarioDto tipoUsuario)
        {
            _tipoUsuario.Cadastrar(tipoUsuario);
            return Created();
        }
    }
}
