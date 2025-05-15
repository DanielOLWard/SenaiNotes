using SenaiNotes.Context;
using SenaiNotes.Interfaces;
using SenaiNotes.Models;
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

        public void Atualizar(int id, TagNota tagNota)
        {
            var TagNotas = _context.Tags.FirstOrDefault(t => t.TagsId == id);
            if (TagNotas == null)
            {
                throw new ArgumentNullException("Tag nao encontrado");
            }
            _context.SaveChanges();

        }

        public Tag BuscarPorId(int Id)
        {
            return _context.Tags.FirstOrDefault(t => t.TagsId == Id);
        }

        public void Cadastrar(Tag tagN)
        {
            _context.Tags.Add(tagN);
            _context.SaveChanges();
        }

        public void Deletar(int id)
        {
            var TagEncontrado = _context.Tags.FirstOrDefault(t => t.TagsId == id);

            if (TagEncontrado != null)
            {
                throw new ArgumentNullException("Tag nao encontrado");
            }
            _context.Tags.Remove(TagEncontrado);
            _context.SaveChanges();
        }

    }
}
