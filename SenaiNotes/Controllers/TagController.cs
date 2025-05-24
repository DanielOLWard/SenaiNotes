using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SenaiNotes.Context;
using SenaiNotes.Dto;
using SenaiNotes.Interfaces;
using SenaiNotes.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;

namespace SenaiNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private ITagRepository _tagRepository;
        private SenaiNotesContext _context;

        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cadastrar Tag",
            Description = "Este EndPoint Cadastra Tag"
            )]
        public IActionResult CadastrarTag(TagDto tagDto)
        {
            _tagRepository.Cadastrar(tagDto);
            return Created();
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Buscar Nome Por Id Tag",
            Description = "Este EndPoint Buscar Nome por Id Tag"
            )]
        public IActionResult BuscarPorNomeId(int id, string nome)
        {
            Tag tag = _tagRepository.BuscarPorNomeId(id, nome);
            if (tag == null)
            {
                return NotFound();
            }
            return Ok(tag);
        }

        [HttpPut]
        [SwaggerOperation(
            Summary = "Atualizar Tag",
            Description = "Este EndPoint Atualiza Tag"
            )]
        public IActionResult Atualizar(int id, Tag tag)
        {
            try
            {
                _tagRepository.Atualizar(id, tag);

                return Ok(tag);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }

        }

        [HttpDelete]
        [SwaggerOperation(
            Summary = "Deletar Tag",
            Description = "Este EndPoint Deleta Tag"
            )]
        public IActionResult Deletar(int id)
        {
            try
            {
                _tagRepository.Deletar(id);

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
            Summary = "Buscar Tag por Id",
            Description = "Este EndPoint busca Tag por Id"
            )]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTagsPorUsuario(int Id)
        {
            var tags = await _context.Tags
                .Where(t => t.TagsId == Id)
                .ToListAsync();

            return Ok(tags);
        }
    }
}
