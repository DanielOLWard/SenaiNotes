using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SenaiNotes.Context;
using SenaiNotes.Dto;
using SenaiNotes.Interfaces;
using SenaiNotes.Models;
using SenaiNotes.ViewModel;

namespace SenaiNotes.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        private readonly SenaiNotesContext _context;

        public TipoUsuarioRepository(SenaiNotesContext context)
        {
            _context = context;
        }
        public void Atualizar(CadastrarTipoUsuarioDto tipoUsuarioAtualizado, int id)
        {
            var tipoUsuarioEncontrado = _context.TipoUsuarios.Find(id);

            if (tipoUsuarioEncontrado == null) throw new ArgumentNullException("Tipo Usuario nao encontrado");

            tipoUsuarioEncontrado.Descricao = tipoUsuarioAtualizado.Descricao;

            _context.SaveChanges();
        }

        public ListarTipoUsuarioViewModel BuscarPorId(int id)
        {
            return _context.TipoUsuarios
                .Select(t=> new ListarTipoUsuarioViewModel
                {
                    TipoUsuarioId = t.TipoUsuarioId,
                    Descricao = t.Descricao
                })
                .FirstOrDefault(t=> t.TipoUsuarioId == id);
        }

        public void Cadastrar(CadastrarTipoUsuarioDto tipoUsuarioDto)
        {
            var tipoUsuarioCadastrado = new TipoUsuario
            {
                Descricao = tipoUsuarioDto.Descricao
            };

            _context.TipoUsuarios.Add(tipoUsuarioCadastrado);
            
            _context.SaveChanges();
        }

        public void Deletar(int id)
        {
            var tipoUsuarioEncontrado = _context.TipoUsuarios.Find(id);

            if (tipoUsuarioEncontrado == null) throw new ArgumentNullException("Tipo Usuario nao encontrado");

            _context.Remove(tipoUsuarioEncontrado);

            _context.SaveChanges();
        }

        public async Task<List<ListarTipoUsuarioViewModel>> ListarTipoUsuariosuarioAsync()
        {
            return await _context.TipoUsuarios
                .Select(t => new ListarTipoUsuarioViewModel
                {
                    TipoUsuarioId = t.TipoUsuarioId,
                    Descricao = t.Descricao
                })
                .ToListAsync();
        }
    }
}
