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
using System.IO;

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

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cadastra uma Tag",
            Description = "Este EndPoint Cadastra uma Tag"
            )]
        public IActionResult Cadastrar(TagDto tag)
        {
            if (tag.ArquivoTag != null)
            {
                // Extra - Verificar se o arquivo é uma imagem

                // 1- Criar uma variavel - pasta de destino

                var pastaDestino = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

                // 2- Salvar o arquivo

                // Extra - Criar um nome personalizado para o Arquivo
                var nomeArquivo = tag.ArquivoTag.FileName;

                var caminhoCompleto = Path.Combine(pastaDestino, nomeArquivo); 

                using (var stream = new FileStream(caminhoCompleto, FileMode.Create)) // FileMode - Manipulacao de arquivos
                {
                    tag.ArquivoTag.CopyTo(stream);
                }

                // 3- Guardar o local do arquivo no BD


               
            }

           
            _tagRepository.Cadastrar(tag);

            return Created();
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

        [HttpDelete("{id}")]
        [SwaggerOperation(
           Summary = "Deletar uma Tag",
           Description = "Este EndPoint deleta uma Tag pelo Id Fornecido"
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
           Description = "Este EndPoint Atualiza uma Tag pelo Id Fornecido"
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
            Summary = "Lista uma Tag pelo Id Informado",
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
