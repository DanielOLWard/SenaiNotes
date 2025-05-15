
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SenaiNotes.Context;
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

        public TagNotaController( ITagNotasRepository tagNotaRepository)
        {
            
            _tagNotasRepository = tagNotaRepository;
        }

        [HttpPost]
        public IActionResult Cadastrar(Tag CadastrarTagNotas)
        {
            _tagNotasRepository.Cadastrar(CadastrarTagNotas);
            return Created();
            
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
        public IActionResult Atualizar(int id, TagNota tag)
        {
            try 
            { 
                _tagNotasRepository.Atualizar(id, tag);
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



    }
}
