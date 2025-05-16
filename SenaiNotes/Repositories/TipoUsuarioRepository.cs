using Microsoft.EntityFrameworkCore.ChangeTracking;
using SenaiNotes.Context;
using SenaiNotes.Interfaces;
using SenaiNotes.Models;

namespace SenaiNotes.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        private readonly SenaiNotesContext _context;

        public TipoUsuarioRepository(SenaiNotesContext context)
        {
            context = _context;
        }
        public void Atualizar(TipoUsuario tipoUsuario, int id)
        {
            throw new NotImplementedException();
        }

        public TipoUsuario BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(TipoUsuario tipoUsuario)
        {
            _context.TipoUsuarios.Add(tipoUsuario);
            _context.SaveChanges();
        }

        public void Deletar(int id)
        {
            var tipoUsuarioEncontrado = _context.TipoUsuarios.Find(id);
            if (tipoUsuarioEncontrado == null) throw new ArgumentNullException("Tipo Usuario nao encontrado");
            _context.Remove(tipoUsuarioEncontrado);
            _context.SaveChanges();
        }

        public Task<List<TipoUsuario>> ListarTipoUsuariosuarioAsync()
        {
            throw new NotImplementedException();
        }
    }
}
