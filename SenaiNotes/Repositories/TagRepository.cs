using Microsoft.EntityFrameworkCore;
using SenaiNotes.Context;
using SenaiNotes.Dto;
using SenaiNotes.Interfaces;
using SenaiNotes.Models;
using SenaiNotes.ViewModel;

namespace SenaiNotes.Repositories
{

    public class TagRepository : ITagRepository
    {
        private readonly SenaiNotesContext _context;

        public TagRepository(SenaiNotesContext context)
        {
            _context = context;
        }

        public void Cadastrar(TagDto tag)
        {
            _context.AddAsync(tag);
            _context.SaveChanges();
        }

        public void Atualizar(int id, TagNota tagNota)
        {
            var TagEncontrado = _context.Tags.FirstOrDefault(t => t.Id == id);
            if (TagEncontrado == null)
            { 

                throw new ArgumentNullException("Tag nao encontrado");
            }
            _context.SaveChanges();

        }

        public Tag BuscarPorID(string nometag)
        {
            return _context.Tags.FirstOrDefault(t => t.NomeTag == nometag);
        }

        public void Deletar(int id)
        {
            var TagEncontrado = _context.Tags.FirstOrDefault(t => t.Id == id);

            if (TagEncontrado != null)
            {
                throw new ArgumentNullException("Tag nao encontrado");
            }
            _context.Tags.Remove(TagEncontrado);
            _context.SaveChanges();
        }

        public List<Tag> ListarPorNome(string nome)
        {
            var ListarTags = _context.Tags
                .ToList();
            return ListarTags;
        }

        public void Atualizar(int id, Tag tag)
        {
            var Tag = _context.Tags.FirstOrDefault(t => t.Id == id);
            if (Tag == null)
            {
                throw new ArgumentNullException("Tag nao encontrado");
            }
            _context.SaveChanges();
        }

        public Tag BuscarPorUsuario(int Tag)
        {
            return _context.Tags.FirstOrDefault(t => t.Id == Tag);

        }
    }
}



