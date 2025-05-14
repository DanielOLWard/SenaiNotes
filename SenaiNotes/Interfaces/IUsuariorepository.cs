using SenaiNotes.Dto;
using SenaiNotes.Models;
using SenaiNotes.ViewModel;

namespace SenaiNotes.Interfaces
{
    public interface IUsuariorepository
    {
        Task<List<ListarusuarioViewModel>> ListarusuarioAsync();

        ListarusuarioViewModel BuscarPorId(int id);

        void Cadastrar(CadastrarUsuarioDto usuarioDto);

        Usuario Atualizar (int id, AtualizarusuarioDto UsuarioAtualizado);

        Usuario Deletar(int id);
    }
}
