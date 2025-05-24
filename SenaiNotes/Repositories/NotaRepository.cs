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
        private readonly SenaiNotesContext _context;

        public NotaRepository(SenaiNotesContext context, ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
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

            _context.SaveChanges();
        }
        public Nota BuscarPorId(int id)
        {
            //Qualquer metodo que vai me trazer apenas 1 cliente 
            //First or Default
            return _context.Notas.FirstOrDefault(n => n.NotasId == id);
        }
        public CadastrarNotaDto Cadastrar(CadastrarNotaDto notaDto)
        {
            // 1 - Percorrer a Lista de Tags
            // 1.1 - Essa Tag ja existe?
            // 1.2 - Pegar o Id dela 
            // 1.2 - Cadastrar a Tag, e pegar o Id

            List<int> idTags = new List<int>();

            foreach (var item in notaDto.Tags) // Percorro a lista de Tags
            {
                // Procuro se a Tag existe
                var tag = _tagRepository.BuscarPorNomeId(notaDto.UsuarioId, item);
                // Caso nao exista eu crio uma
                if (tag == null)
                {
                    tag = new Tag
                    {
                        NomeTag = item,
                        UsuarioId = notaDto.UsuarioId,
                    };
                    _context.Add(tag);
                    _context.SaveChanges();
                }
                idTags.Add(tag.TagsId);
            }

            // Cadastrar Nota
            var novaNota = new Nota
            {
                Titulo = notaDto.Titulo,
                ConteudoNotas = notaDto.ConteudoNotas,
                Lixeira = false,
                Arquivado = false,
                Imagem = notaDto.Imagem,
                UsuarioId = notaDto.UsuarioId
            };
            _context.Add(novaNota);
            _context.SaveChanges();

            // Cadastrar a TagNota
            foreach (var id in idTags)
            {
                var tagNota = new TagNota
                {
                    NotasId = novaNota.NotasId,
                    TagsId = id
                };
                _context.Add(tagNota);
                _context.SaveChanges();
            }
            return notaDto;
        }
        public void Deletar(int id)
        {
            var notaEncontrada = _context.Notas
                .Include(ta => ta.TagNota)
                .FirstOrDefault(n => n.NotasId == id); // Encontrar quem eu quero deletar
            if (notaEncontrada == null)
            {
                throw new ArgumentNullException("Nota nao encontrada");
            }

            if (notaEncontrada.TagNota.Any() == true)
            {
                _context.TagNotas.RemoveRange(notaEncontrada.TagNota);
            }
            _context.Notas.Remove(notaEncontrada);
            _context.SaveChanges();
        }

        public List<ListarNotaViewModel> ListarTodos()
        {
            var notas = _context.Notas
                .Include(n => n.TagNota)
                .ThenInclude(tn => tn.Tags)
                .Select(n => new ListarNotaViewModel
                {
                    NotasId = n.NotasId,
                    Titulo = n.Titulo,
                    ConteudoNotas = n.ConteudoNotas,
                    Lixeira = n.Lixeira,
                    Arquivado = n.Arquivado,
                    Imagem = n.Imagem,
                    UsuarioId = n.UsuarioId,
                    TagsId = n.TagNota.Select(tn => new TagViewModel
                    {
                        TagsId = tn.Tags.TagsId,
                        NomeTag = tn.Tags.NomeTag
                    }).ToList()
                })
                .ToList();  

                return notas;
        }
        public Nota Arquivar(int id)
        {
            var nota = _context.Notas.Find(id);

            if (nota == null) return null;

            nota.Arquivado = !nota.Arquivado;

            _context.SaveChanges();

            return nota;
        }
    }
}