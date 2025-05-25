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
using System;

namespace SenaiNotes.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class TagController : ControllerBase
    {
        private ITagRepository _tagRepository;
        private SenaiNotesContext _context;

        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [HttpGet]
        [SwaggerOperation(
           Summary = "Listar todas as Tags",
           Description = "Este EndPoint lista todas as Tags"
           )]
        public IActionResult ListarTag()
        {
            return Ok(_tagRepository.ListarTodos());
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cadastra uma Tag",
            Description = "Este EndPoint Cadastra uma Tag"
            )]
        public IActionResult Cadastrar(TagDto tag)
        {
            _tagRepository.Cadastrar(tag);

            return Created();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
           Summary = "Deletar Tag",
           Description = "Este EndPoint delete uma Tag"
           )]
        public IActionResult Deletar(int id)
        {
            try
            {
                _tagRepository.Deletar(id);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
           Summary = "Atualizar Tag",
           Description = "Este EndPoint Atualiza uma tag"
           )]
        public IActionResult Atualizar (int id, TagDto tag)
        {
            try
            {
                _tagRepository.Atualizar(id, tag);
                return Ok(tag);
            } 
            catch (ArgumentNullException) 
            {
                return NotFound("Tag não Encontrada");
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Lista uma pelo Id Informado",
            Description = "Este EndPoint lista uma Tag de acordo com o Id informado"
            )]
        public IActionResult ListarPorId(int id)
        {
            TagViewModel tag = _tagRepository.BuscarPorId(id);

            if (tag == null) return NotFound("Tag não encontrada!!");

            return Ok(tag);
        }
    }
}
