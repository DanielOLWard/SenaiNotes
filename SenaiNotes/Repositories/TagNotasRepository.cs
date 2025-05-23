using Azure;
using SenaiNotes.Context;
using SenaiNotes.Dto;
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

        public void Atualizar(int id, TagNotaDto tagNota)
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

     
        public void Cadastrar(TagNotaDto tagN)
        {
            var notaTag=0;
        }

        public void Cadastrar(TagDto tagN)
        {
            _context.AddAsync(tagN);
            _context.SaveChanges();
        }

        public void CadastrarTag(TagNotaDto cadastrarTagNotas)
        {
            _context.AddAsync(cadastrarTagNotas);
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
