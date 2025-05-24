
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SenaiNotes.Context;
using SenaiNotes.Dto;
using SenaiNotes.Interfaces;
using SenaiNotes.Models;
using SenaiNotes.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace SenaiNotes.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TagNotaController : ControllerBase
    {
        private ITagNotasRepository _tagNotasRepository;
        private SenaiNotesContext _context;

        public TagNotaController( ITagNotasRepository tagNotaRepository)
        {
            
            _tagNotasRepository = tagNotaRepository;
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cadastrar TagNota",
            Description = "Este EndPoint Cadastra TagNota"
            )]
        public IActionResult CadastrarTag( TagNotaDto CadastrarTagNotas)
        {
            _tagNotasRepository.CadastrarTag(CadastrarTagNotas);
            return Created();
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Listar por Id TagNota",
            Description = "Este EndPoint Lista por Id TagNota"
            )]
        public IActionResult BuscarPorId(int id)
        {
            Tag tagNota = _tagNotasRepository.BuscarPorId(id);
            if (tagNota == null)
            {
                return NotFound();
            }
            return Ok(tagNota);
        }

        [HttpPut]
        [SwaggerOperation(
            Summary = "Atualizar TagNota",
            Description = "Este EndPoint Atualiza TagNota"
            )]
        public IActionResult Atualizar(int id, TagNotaDto tag)
        {
            try 
            { 
                _tagNotasRepository.Atualizar(id, tag );
                return Ok(id);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }

        }

        [HttpDelete]
        [SwaggerOperation(
            Summary = "Deleta TagNota",
            Description = "Este EndPoint Deleta TagNota"
            )]
        public IActionResult Deletar(int id)
        {
            try
            {
                _tagNotasRepository.Deletar(id);

                // 204 - Deu certo!
                return NoContent();
            }
            // Caso de erro
            catch (Exception ex)
            {
                return NotFound("Tag não encontrado!");
            }
        }

        [HttpGet("usuario/{Id}")]
        [SwaggerOperation(
            Summary = "Busca TagNota por Id",
            Description = "Este EndPoint busca TagNota por Id"
            )]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTagsPorUsuario(int Id)
        {
            var tagnotas = await _context.TagNotas
                .Where(t => t.NotasId == Id)
                .ToListAsync();

            return Ok(tagnotas);
        }

    }
}
