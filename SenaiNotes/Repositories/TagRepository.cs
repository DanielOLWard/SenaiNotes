using SenaiNotes.Context;
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

            public void Cadastrar(Tag Tag)
            {
                _context.Tags.Add(Tag);
                _context.SaveChanges();
            }

            public void Atualizar(int id, TagNota tagNota)
            {
                var TagEncontrado = _context.Tags.FirstOrDefault(t => t.TagsId == id);
                if (TagEncontrado == null)
                {
                    throw new ArgumentNullException("Tag nao encontrado");
                }
                _context.SaveChanges();

            }

            public Tag BuscarPorID(int id)
            {
                return _context.Tags.FirstOrDefault(t => t.TagsId == id);
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

            public List<Tag> ListarPorNome(string nome)
            {
                var ListarTags = _context.Tags
                    .ToList();
                return ListarTags;
            }

            public void Atualizar(int id, Tag tag)
            {
                throw new NotImplementedException();
            }

        public Tag BuscarPorUsuario(Tag tagUser)
        {
            var TagsUser = _context.Tags
                .ToList();
               
            return 
        }
    }
    }
 }


