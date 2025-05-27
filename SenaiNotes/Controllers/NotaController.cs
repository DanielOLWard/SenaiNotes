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
    [Authorize]
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
            Summary = "Cadastra uma Nota",
            Description = "Este EndPoint Cadastra uma Nota"
            )]
        public IActionResult CadastrarNotas(CadastrarNotaDto nota)
        {
            //EXTRA - Verificar se o arquivo e uma imagem 
            if (nota.ArquivoNotas != null)
            {

                //1 - Criar uma variavel - Pasta de destino
                var pastaDestino = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

                //2 - Salvar o arquivo 
                //EXTRA - Criar um nome personalizado para o arquivo
                var nomeArquivo = nota.ArquivoNotas.FileName;

                var caminhoCompleto = Path.Combine(pastaDestino, nomeArquivo);

                using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                {
                    nota.ArquivoNotas.CopyTo(stream);   
                }
                //3 - Guardar o local do arquivo no bd
                nota.Imagem = nomeArquivo;

            }
            _Notarepository.Cadastrar(nota);
            return Created();
        }
        [HttpPost("semImagem")]
        [SwaggerOperation(
            Summary = "Cadastra uma Nota sem imagem",
            Description = "Este EndPoint Cadastra uma Nota"
            )]
        public IActionResult CadastrarNotasSemImagem(CadastrarNotaDto nota)
        {
            _Notarepository.Cadastrar(nota);
            return Created();
        }

        [HttpGet]
        [SwaggerOperation(
           Summary = "Listar todas as Notas",
           Description = "Este EndPoint lista todas as Notas"
           )]
        public IActionResult ListarNotas()
        {
            return Ok(_Notarepository.ListarTodos());
        }


        [HttpDelete("{id}")]
        [SwaggerOperation(
           Summary = "Deletar uma Nota",
           Description = "Este EndPoint deleta uma Nota pelo Id Fornecido"
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
           Summary = "Atualizar Nota",
           Description = "Este EndPoint Atualiza uma Nota pelo Id Fornecido"
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
            Summary = "Lista uma Nota pelo Id Informado",
            Description = "Este EndPoint lista uma Nota de acordo com o Id informado"
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

        [HttpPatch("Arquivar/{id}")]
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


