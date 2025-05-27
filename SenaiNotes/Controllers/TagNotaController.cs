
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SenaiNotes.Context;
using SenaiNotes.Dto;
using SenaiNotes.Interfaces;
using SenaiNotes.Models;
using SenaiNotes.Repositories;
using SenaiNotes.ViewModel;
using Swashbuckle.AspNetCore.Annotations;

namespace SenaiNotes.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class TagNotaController : ControllerBase
    {
        private ITagNotasRepository _tagNotasRepository;
        private SenaiNotesContext _context;

        public TagNotaController(ITagNotasRepository tagNotaRepository)
        {

            _tagNotasRepository = tagNotaRepository;
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cadastra uma TagNota",
            Description = "Este EndPoint Cadastra uma TagNota"
            )]
        public IActionResult Casatrar(TagNotaDto tagNota)
        {
            _tagNotasRepository.Cadastrar(tagNota);

            return Created();
        }

        [HttpGet]
        [SwaggerOperation(
           Summary = "Listar todas as TagNotas",
           Description = "Este EndPoint lista todas as TagNotas"
           )]
        public IActionResult ListarTagNota()
        {
            return Ok(_tagNotasRepository.ListarTodos());
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
           Summary = "Deletar uma TagNota",
           Description = "Este EndPoint deleta uma TagNota pelo Id Fornecido"
           )]
        public IActionResult Deletar(int id)
        {
            try
            {
                _tagNotasRepository.Deletar(id);
                return NoContent();
            }
            catch (ArgumentNullException)
            {
                return NotFound("TagNota não encontrada!!");
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
           Summary = "Atualizar TagNota",
           Description = "Este EndPoint Atualiza uma tagNota pelo Id Fornecido"
           )]
        public IActionResult AtualizarTagNota (int id, TagNotaDto tagNota)
        {
            try
            {
                _tagNotasRepository.Atualizar(id, tagNota);
                return Ok(tagNota);
            }
            catch (ArgumentNullException)
            {
                return NotFound("TagNota não encontrada!!");
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Lista uma TagNota pelo Id Informado",
            Description = "Este EndPoint lista uma TagNota de acordo com o Id informado"
            )]
        public IActionResult ListarPorId (int id)
        {
            ListarTagNotasViewModel tagNotas = _tagNotasRepository.BuscarPorId(id);

            if (tagNotas == null) return NotFound("TagNota não encontrada!!");

            return Ok(tagNotas);
        }
    }
}
