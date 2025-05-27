using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {
        private ITipoUsuarioRepository _tipoUsuario;

        public TipoUsuarioController(ITipoUsuarioRepository tipoUsuarioRepository)
        {
            _tipoUsuario = tipoUsuarioRepository;
        }

        [HttpPost]

        [SwaggerOperation(
            Summary = "Cadastra um TipoUsuario",
            Description = "Este EndPoint Cadastra um TipoUsuario"
            )]
        public IActionResult CadastrarTipoUsuario(CadastrarTipoUsuarioDto tipoUsuario)
        {
            _tipoUsuario.Cadastrar(tipoUsuario);
            return Created();
        }

        [HttpGet]
        [SwaggerOperation(
           Summary = "Listar todos os TipoUsuarios",
           Description = "Este EndPoint lista todos os TipoUsuarios"
           )]
        public async Task<IActionResult> ListarTipoUsuarioAsync()
        {
            var tipoUsuario = await _tipoUsuario.ListarTipoUsuariosuarioAsync();
            return Ok(tipoUsuario);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
           Summary = "Deletar um TipoUsuario",
           Description = "Este EndPoint deleta um TipoUsuario pelo Id Fornecido"
           )]
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
        
        [HttpPut("{id}")]
        [SwaggerOperation(
           Summary = "Atualizar Tag",
           Description = "Este EndPoint Atualiza um Usuario pelo Id Fornecido"
           )]
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
        [SwaggerOperation(
            Summary = "Lista um TipoUsuario pelo Id Informado",
            Description = "Este EndPoint lista um TipoUsuario de acordo com o Id informado"
            )]
        public IActionResult ListarPorId(int id)
        {
            ListarTipoUsuarioViewModel tipoUsuario = _tipoUsuario.BuscarPorId(id);
            if (tipoUsuario == null) return NotFound("Tipo Usuario nao encontrado!!");

            return Ok(tipoUsuario);
        }
    }
}
