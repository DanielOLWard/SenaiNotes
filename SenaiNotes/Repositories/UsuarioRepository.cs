using Microsoft.EntityFrameworkCore;
using SenaiNotes.Context;
using SenaiNotes.Dto;
using SenaiNotes.Interfaces;
using SenaiNotes.Models;
using SenaiNotes.Services;
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

        public void Atualizar(int id, AtualizarusuarioDto usuarioAtualizado)
        {
            var usuarioEncontrado = _context.Usuarios.Find(id);

            var passwordSercvice = new PasswordService();

            if (usuarioEncontrado == null) throw new ArgumentNullException("Usuario nao Encontrado");

            usuarioEncontrado.Nome = usuarioAtualizado.Nome;
            usuarioEncontrado.Email = usuarioAtualizado.Email;
            usuarioEncontrado.Senha = usuarioAtualizado.Senha;
            usuarioEncontrado.DataAtualizacao = usuarioAtualizado.DataAtualizacao;
            usuarioEncontrado.Telefone = usuarioAtualizado.Telefone;
            usuarioEncontrado.TipoUsuarioId = usuarioAtualizado.TipoUsuarioId;

            usuarioEncontrado.Senha = passwordSercvice.HashPassword(usuarioEncontrado);

            _context.SaveChanges();
        }

        public ListarusuarioViewModel BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(CadastrarUsuarioDto usuarioDto)
        {
            var passwordService = new PasswordService();

            var usuarioCadastrado = new Usuario
            {
                Nome = usuarioDto.Nome,
                Email = usuarioDto.Email,
                Senha = usuarioDto.Senha,
                Telefone = usuarioDto.Telefone,
                DataCadastro = usuarioDto.DataCadastro,
                TipoUsuarioId = usuarioDto.TipoUsuarioId
            };

            usuarioCadastrado.Senha = passwordService.HashPassword(usuarioCadastrado);

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

        public Usuario Login(string email, string senha)
        {
            throw new NotImplementedException();
        }
    }
}
