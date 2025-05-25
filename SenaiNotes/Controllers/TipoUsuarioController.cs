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
    //[Authorize]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {
        private ITipoUsuarioRepository _tipoUsuario;

        public TipoUsuarioController(ITipoUsuarioRepository tipoUsuarioRepository)
        {
            _tipoUsuario = tipoUsuarioRepository;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Listar Tipo-Usuario",
            Description = "Este EndPoint Lista os Tipo-Usuario"
            )]
        public async Task<IActionResult> ListarTipoUsuarioAsync()
        {
            var tipoUsuario = await _tipoUsuario.ListarTipoUsuariosuarioAsync();
            return Ok(tipoUsuario);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cadastrar Tipo-Usuario",
            Description = "Este EndPoint Cadastra um Tipo-Usuario"
            )]
        public IActionResult CadastrarTipoUsuario(CadastrarTipoUsuarioDto tipoUsuario)
        {
            _tipoUsuario.Cadastrar(tipoUsuario);
            return Created();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Deletar Tipo-Usuario",
            Description = "Este EndPoint delete um Tipo-Usuario"
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
            Summary = "Atualizar Tipo-Usuario",
            Description = "Este EndPoint Atualiza um Tipo-Usuario"
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
            Summary = "Listar por Id Tipo-Usuario",
            Description = "Este EndPoint Lista por Id os Tipo-Usuario"
            )]
        public IActionResult ListarPorId(int id)
        {
            ListarTipoUsuarioViewModel tipoUsuario = _tipoUsuario.BuscarPorId(id);
            if (tipoUsuario == null) return NotFound("Tipo Usuario nao encontrado!!");

            return Ok(tipoUsuario);
        }
    }
}
