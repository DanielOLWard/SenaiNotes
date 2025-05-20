using Microsoft.AspNetCore.Http.HttpResults;
using SenaiNotes.Context;
using SenaiNotes.Dto;
using SenaiNotes.Interfaces;
using SenaiNotes.Models;
using SenaiNotes.ViewModel;

namespace SenaiNotes.Repositories
{
    public class NotaRepository : INotaRepository
    {
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
            _context.SaveChanges();
        }
        public Nota BuscarPorId(int id)
        {
            //Qualquer metodo que vai me trazer apenas 1 cliente 
            //First or Default
            return _context.Notas.FirstOrDefault(n => n.NotasId == id);
        }

        public void Cadastrar(CadastrarNotaDto notaDto)
        {
            var nota = new Nota
            {
                Titulo = notaDto.Titulo,
                ConteudoNotas = notaDto.ConteudoNotas,
                UsuarioId = notaDto.UsuarioId,
            };
            _context.Notas.Add(nota);
            _context.SaveChanges();

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
            return _context.Notas.Select(n => new ListarNotaViewModel
            {
                Titulo = n.Titulo,
                ConteudoNotas = n.ConteudoNotas,
            })
            .ToList();
        }
        //TEM Q FAZER O LISTAR TAGS NO LISTAR TODOS
        public void Arquivar(bool id)
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
