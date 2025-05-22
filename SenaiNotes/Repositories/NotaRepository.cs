using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SenaiNotes.Context;
using SenaiNotes.Dto;
using SenaiNotes.Interfaces;
using SenaiNotes.Models;
using SenaiNotes.ViewModel;

namespace SenaiNotes.Repositories
{
    public class NotaRepository : INotaRepository
    {
        private readonly ITagRepository _tagRepository;

        public NotaRepository(SenaiNotesContext context, ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        private readonly SenaiNotesContext _context;
        public NotaRepository(SenaiNotesContext context)
        {
            _context = context;
        }
        public void Atualizar(int id, CadastrarNotaDto nota)
        {
            Nota notaEncontrado = _context.Notas.Find(id);
            if (notaEncontrado == null)

            {
                throw new NotImplementedException();
            }
            notaEncontrado.Titulo = nota.Titulo;
            notaEncontrado.ConteudoNotas = nota.ConteudoNotas;
            notaEncontrado.Arquivado = nota.Arquivado;  

            _context.SaveChanges();
        }
        public Nota BuscarPorId(int id)
        {
            //Qualquer metodo que vai me trazer apenas 1 cliente 
            //First or Default

            //// Sem Id na Tag
            //var tags = _context.Tags.Include(t => t.TagNota).ThenInclude(ta => ta.Notas).ThenInclude(n => n.Usuario).FirstOrDefault(t => t.TagsId == id);

            //var idUsuario = tags.TagNota.First().Notas.Usuario.UsuarioId;


            //// Com Id na Tag
            //var id = _context.Tags.FirstOrDefault(t => t.TagsId == id).UsuarioId;

            return _context.Notas.FirstOrDefault(n => n.NotasId == id);
         
        }
        public CadastrarNotaDto? Cadastrar(CadastrarNotaDto notaDto)
        {
           // 1 - Percorrer a Lista de Tags
           // 1.1 - Essa Tag ja existe?
           // 1.2 - Pegar o Id dela 
           // 1.2 - Cadastrar a Tag, e pegar o Id

            List<int> idTags = new List<int>();

            foreach (var item in notaDto.Tags)
            {

                var tag = _tagRepository.BuscarPorID(notaDto.UsuarioId, item);

                if (tag == null)
                {
                    //TODO: Cadastrar a Tag
                }
                idTags.Add(tag.TagsId);
            }
        }
        public void Deletar(int id)
        {
            var notaEncontrado = _context.Notas.FirstOrDefault(n => n.NotasId == id); // Encontrar quem eu quero deletar
            if (notaEncontrado == null)
            {
                throw new Exception("Nota nao encontrada");
            }
            _context.Notas.Remove(notaEncontrado);
            _context.SaveChanges();
        }

        public List<ListarNotaViewModel> ListarTodos()
        {
            var notas = _context.Notas
                .Include(n => n.TagNota)
                .ThenInclude(ta => ta.TagNotasId)
                .Select(n => new ListarNotaViewModel
                {
                    NotasId = n.NotasId,
                    Titulo = n.Titulo,
                    ConteudoNotas = n.ConteudoNotas,
                    Lixeira = n.Lixeira,
                    Arquivado = n.Arquivado,
                    Imagem = n.Imagem,
                    UsuarioId = n.UsuarioId,
                    TagsId = n.TagNota.Select(ta => new TagViewModel
                    {
                        TagsId = ta.Tags.TagsId,
                        NomeTag = ta.Tags.NomeTag
                    }).ToList()
                })
                .ToList();  

                return notas;
        }
        public void Arquivar(int id)
        {
            var notaArquivada = _context.Notas.Find(id);
            if (notaArquivada != null)
            {
               notaArquivada.Arquivado = !notaArquivada.Arquivado;
                _context.SaveChanges();

            }
        }
    }
}