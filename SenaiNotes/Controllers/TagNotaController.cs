
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SenaiNotes.Context;
using SenaiNotes.Dto;
using SenaiNotes.Interfaces;
using SenaiNotes.Models;
using SenaiNotes.Repositories;

namespace SenaiNotes.Controllers
{
    [Route("api/[controller]")]
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
        public IActionResult Cadastrar(TagNotaDto CadastrarTagNotas)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
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
        public async Task<ActionResult<IEnumerable<Tag>>> GetTagsPorUsuario(int Id)
        {
            var tagnotas = await _context.TagNotas
                .Where(t => t.NotasId == Id)
                .ToListAsync();

            return Ok(tagnotas);
        }

    }
}
