using Microsoft.EntityFrameworkCore;
using SenaiNotes.Context;
using SenaiNotes.Dto;
using SenaiNotes.Interfaces;
using SenaiNotes.Models;
using SenaiNotes.ViewModel;
using System.Collections.Generic;

namespace SenaiNotes.Repositories
{

    public class TagRepository : ITagRepository
    {
        private readonly SenaiNotesContext _context;


        public TagRepository(SenaiNotesContext context)
        {
            _context = context;
        }

        public void Atualizar(int id, TagDto tag)
        {
            var tagEncontrada = _context.Tags.Find(id);

            if (tagEncontrada == null) throw new ArgumentException();

            tagEncontrada.NomeTag = tag.NomeTag;

            _context.SaveChanges();
        }

        public TagViewModel BuscarPorId(int id)
        {
            return _context.Tags.Select(t => new TagViewModel
            {
                TagsId = t.TagsId,
                NomeTag = t.NomeTag,
                UsuarioId = t.UsuarioId,
            })
            .FirstOrDefault(t => t.TagsId == id);
        }

        public Tag BuscarPorNomeId(int id, string nome)
        {
            var tags = _context.Tags.FirstOrDefault(t => t.UsuarioId == id && t.NomeTag == nome);

            return tags;
        }

        // Listar Tags por nomeParcial
        public IList<Tag> BuscarTagsPorNomeParcial(string nomeParcial)
        {
            if (string.IsNullOrWhiteSpace(nomeParcial))
            {
                return new List<Tag>(); // Retorna uma lista vazia se o nome parcial for nulo ou vazio
            }

            return BuscarTagsPorNomeParcial(nomeParcial)
                .Where(t => t.NomeTag.Contains(nomeParcial, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }



        public void Cadastrar(TagDto tag)
        {
            var tagCadastrada = new Tag()
            {
                NomeTag = tag.NomeTag,
                UsuarioId = tag.UsuarioId
            };

            _context.Tags.Add(tagCadastrada);

            _context.SaveChanges();
        }

        public void Deletar(int id)
        {
            var tagEncontrada = _context.Tags.Find(id);

            if (tagEncontrada == null) throw new ArgumentNullException("Tag não encontrada!!");

            _context.Remove(tagEncontrada);

            _context.SaveChanges();
        }

        public List<TagViewModel> ListarTodos()
        {
            return _context.Tags
                .Select(t => new TagViewModel
                {
                    TagsId = t.TagsId,
                    NomeTag = t.NomeTag,
                    UsuarioId = t.UsuarioId,
                })
                .ToList();
        }

    }
}



