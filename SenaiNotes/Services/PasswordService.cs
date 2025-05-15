using System.Diagnostics;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using SenaiNotes.Models;

namespace SenaiNotes.Services
{
    public class PasswordService
    {
        private readonly PasswordHasher<Usuario> _passwordHasher = new();

        public string HashPassword(Usuario usuario)
        {
            return _passwordHasher.HashPassword(usuario, usuario.Senha);
        }

        public bool VerificarSenha(Usuario usuario, string senhainformada)
        {
            var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.Senha, senhainformada);
            return resultado == PasswordVerificationResult.Success;
        }
    }
}
