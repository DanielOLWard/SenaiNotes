using Microsoft.AspNetCore.Mvc;
using SenaiNotes.Context;
using SenaiNotes.Interfaces;
using SenaiNotes.Models;

namespace SenaiNotes.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class TagController : ControllerBase
        {
            private ITagRepository _tagRepository;

            public TagController( ITagRepository tagRepository)
            {
                _tagRepository = tagRepository;
            }

            [HttpPost]
            public IActionResult CadastrarTag(Tag CadastroTag)
            {
                _tagRepository.Cadastrar(CadastroTag);
                return Created();
            }

            [HttpGet]
            public IActionResult BuscarPorId(int id)
            {
                Tag tag = _tagRepository.BuscarPorID(id);
                if (tag == null)
                {
                    return NotFound();
                }
                return Ok(tag);
            }

            [HttpPut]
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


        }
   }
