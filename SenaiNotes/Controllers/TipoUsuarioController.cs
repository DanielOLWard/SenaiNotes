using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SenaiNotes.Dto;
using SenaiNotes.Interfaces;
using SenaiNotes.Models;
using SenaiNotes.ViewModel;
using Swashbuckle.AspNetCore.Annotations;

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

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                _tipoUsuario.Deletar(id);
                return NoContent();
            }
            catch (ArgumentNullException ex)
            {
                return NotFound();
            }
        }
        [HttpPatch("/arquivar{id}/tipo-usuario")]
        [SwaggerOperation(
           Summary = "Arquiva um TipoUsuario",
           Description = "Este endpoint arquiva um TipoUsuario com base no ID fornecido"
           )]


        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, CadastrarTipoUsuarioDto tipoUsuarioAtualizado)
        {
            try
            {
                _tipoUsuario.Atualizar(id, tipoUsuarioAtualizado);
                return Ok(tipoUsuarioAtualizado);
            }
            catch
            {
                return NotFound("Tipo Usuario nao Encontrado");
            }
        }

        [HttpGet("{id}")]
        public IActionResult ListarPorId(int id)
        {
            ListarTipoUsuarioViewModel tipoUsuario = _tipoUsuario.BuscarPorId(id);
            if (tipoUsuario == null) return NotFound("Tipo Usuario nao encontrado!!");

            return Ok(tipoUsuario);
        }
    }
}
