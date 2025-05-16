using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SenaiNotes.Dto;
using SenaiNotes.Interfaces;
using SenaiNotes.Models;

namespace SenaiNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotaController : ControllerBase
    {
        private INotaRepository _Notarepository;
        public NotaController(INotaRepository Notarepository)
        {
            _Notarepository = Notarepository;
        }

        [HttpGet("{id}")]
        public IActionResult ListarPorId(int id)
        {
            Nota nota = _Notarepository.BuscarPorId(id);
            if (nota == null)
            {
                return NotFound();
            }
            return Ok(nota);
        }
        [HttpGet]
        public IActionResult ListarNotas()
        {
            return Ok(_Notarepository.ListarTodos());
        }

        [HttpPost]
        public IActionResult CadastrarNotas(CadastrarNotaDto nota)
        {
            _Notarepository.Cadastrar(nota);



            return Created();
        }
        [HttpPut("{id}")]
        public IActionResult Editar(int id, CadastrarNotaDto nota)
        {
            try
            {
                _Notarepository.Atualizar(id, nota);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Nota nao encontrada!");
            }

        }
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                _Notarepository.Deletar(id);
                return NoContent();
            }

            catch (Exception ex)
            {
                return NotFound("Nota nao encontrada!");
            }
        }
    } 
    
}