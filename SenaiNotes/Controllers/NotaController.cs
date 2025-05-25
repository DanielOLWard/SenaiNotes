using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SenaiNotes.Dto;
using SenaiNotes.Interfaces;
using SenaiNotes.Models;
using SenaiNotes.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace SenaiNotes.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class NotaController : ControllerBase
    {
        private INotaRepository _Notarepository;
        public NotaController(INotaRepository Notarepository)
        {
            _Notarepository = Notarepository;
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cadastra Nota",
            Description = "Este endpoint cadastra uma nota e uma tag de acordo com os dados fornecidos pelo usario"
            )]
        public IActionResult CadastrarNotas(CadastrarNotaDto nota)
        {
            _Notarepository.Cadastrar(nota);
            return Created();
        }

        [HttpGet]
        [SwaggerOperation(
          Summary = "Lista todas as Notas",
          Description = "Este endpoint Lista todas as notas cadastradas"
          )]
        public IActionResult ListarNotas()
        {
            return Ok(_Notarepository.ListarTodos());
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Deleta uma Nota",
            Description = "Este endpoint Deleta uma Nota com base no ID fornecido"
            )]
        public IActionResult Deletar(int id)
        {
            try
            {
                _Notarepository.Deletar(id);
                return NoContent();
            }

            catch (ArgumentNullException)
            {
                return NotFound("Nota nao encontrada!");
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Editar Nota",
            Description = "Este endpoint edita uma Nota com base no ID fornecido"
            )]
        public IActionResult Editar(int id, CadastrarNotaDto nota)
        {
            try
            {
                _Notarepository.Atualizar(id, nota);
                return Ok();
            }
            catch (ArgumentNullException)
            {
                return NotFound("Nota nao encontrada!");
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Lista notas por ID",
            Description = "Este endpoint Lista uma Nota com base no ID fornecido"
            )]
        public IActionResult ListarPorId(int id)
        {
            Nota nota = _Notarepository.BuscarPorId(id);
            if (nota == null)
            {
                return NotFound();
            }
            return Ok(nota);
        }

        [HttpPatch("/arquivar{id}/nota")]
        [SwaggerOperation(
            Summary = "Arquiva uma Nota",
            Description = "Este endpoint arquiva uma Nota com base no ID fornecido"
            )]
        public IActionResult Arquivar(int id)
        {
            _Notarepository.Arquivar(id);
            return NoContent();
        }

    } 
    
}


