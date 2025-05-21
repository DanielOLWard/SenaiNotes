using SenaiNotes.Dto;
using SenaiNotes.Models;
using SenaiNotes.ViewModel;

namespace SenaiNotes.Interfaces
{
    public interface IUsuariorepository
    {
        Task<List<ListarusuarioViewModel>> ListarusuarioAsync();

        ListarusuarioViewModel BuscarPorId(int id);

        Usuario Login(string email, string senha);

        void Cadastrar(CadastrarUsuarioDto usuarioDto);

        void Atualizar (int id, AtualizarusuarioDto UsuarioAtualizado);

        void Deletar(int id);
    }
}
