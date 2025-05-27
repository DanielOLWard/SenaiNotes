using Azure;
using SenaiNotes.Context;
using SenaiNotes.Dto;
using SenaiNotes.Interfaces;
using SenaiNotes.Models;
using SenaiNotes.ViewModel;
using System.Linq;

namespace SenaiNotes.Repositories
{
    public class TagNotasRepository : ITagNotasRepository
    {
        private readonly SenaiNotesContext _context;

        public TagNotasRepository(SenaiNotesContext context)
        {
            _context = context;
        }

        public void Atualizar(int id, TagNotaDto tagNota)
        {
            var tagNotasEncontrada = _context.TagNotas.Find(id);

            if (tagNotasEncontrada == null) throw new ArgumentException();

            tagNotasEncontrada.NotasId = tagNota.NotasId;
            tagNotasEncontrada.TagsId = tagNota.TagsId;

            _context.SaveChanges();
        }

        public ListarTagNotasViewModel BuscarPorId(int id)
        {
            return _context.TagNotas.Select(tn => new ListarTagNotasViewModel
            {
                TagNotasId = tn.TagNotasId,
                TagsId = tn.TagsId,
                NotasId = tn.NotasId
            })
            .FirstOrDefault(tn => tn.NotasId == id);
        }

        public void Cadastrar(TagNotaDto tagNota)
        {
            var tagNotaCadastrada = new TagNota()
            {
                TagsId = tagNota.NotasId,
                NotasId = tagNota.NotasId
            };

            _context.Add(tagNotaCadastrada);

            _context.SaveChanges();
        }

        public void Deletar(int id)
        {
            var tagNotaEncontrada = _context.TagNotas.Find(id);

            if (tagNotaEncontrada == null) throw new ArgumentNullException("TagNota não Encontrada!!");

            _context.Remove(tagNotaEncontrada);

            _context.SaveChanges();
        }

        public List<ListarTagNotasViewModel> ListarTodos()
        {
            return _context.TagNotas
                .Select(tn => new ListarTagNotasViewModel
                {
                    TagNotasId = tn.TagNotasId,
                    NotasId = tn.NotasId,
                    TagsId = tn.TagsId
                })
                .ToList();
        }
    }
}
