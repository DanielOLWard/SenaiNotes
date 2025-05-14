using Microsoft.EntityFrameworkCore;
using SenaiNotes.Context;
using SenaiNotes.Dto;
using SenaiNotes.Interfaces;
using SenaiNotes.Models;
using SenaiNotes.ViewModel;

namespace SenaiNotes.Repositories
{
    public class UsuarioRepository : IUsuariorepository
    {
        private readonly SenaiNotesContext _context;

        public UsuarioRepository(SenaiNotesContext context)
        {
            _context = context;
        }

        public Usuario Atualizar(int id, AtualizarusuarioDto UsuarioAtualizado)
        {
            throw new NotImplementedException();
        }

        public ListarusuarioViewModel BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(CadastrarUsuarioDto usuarioDto)
        {
            var usuarioCadastrado = new Usuario
            {
                Nome = usuarioDto.Nome,
                Email = usuarioDto.Email,
                Senha = usuarioDto.Senha,
                Telefone = usuarioDto.Telefone,
                DataCadastro = usuarioDto.DataCadastro,
                TipoUsuarioId = usuarioDto.TipoUsuarioId
            };

            _context.Usuarios.Add(usuarioCadastrado);

            _context.SaveChanges();
        }

        public Usuario Deletar(int id)
        {
            Usuario usuarioEncontrado = _context.Usuarios.Find(id);

            if (usuarioEncontrado == null) return null;

            _context.Usuarios.Remove(usuarioEncontrado);

            _context.SaveChanges();

            return usuarioEncontrado;
        }

        public async Task<List<ListarusuarioViewModel>> ListarusuarioAsync()
        {
            return await _context.Usuarios
                .Select(u => new ListarusuarioViewModel
                {
                    UsuarioId = u.UsuarioId,
                    Nome = u.Nome,
                    Email = u.Email,
                    Telefone = u.Telefone,
                    DataCadastro = u.DataCadastro,
                    TipoUsuarioId = u.TipoUsuarioId,
                })
                .ToListAsync();
        }
    }
}
